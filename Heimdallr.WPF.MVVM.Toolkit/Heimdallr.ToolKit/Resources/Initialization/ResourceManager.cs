using Heimdallr.ToolKit.Applications;
using Heimdallr.ToolKit.Events;
using Heimdallr.ToolKit.Infrastructure.Messaging;
using Heimdallr.ToolKit.Interfaces;
using Heimdallr.ToolKit.Models;
using Heimdallr.ToolKit.Resources.Initialization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using YamlDotNet.Serialization;

namespace Heimdallr.ToolKit.Resources.Initializationl;

/// <summary>
/// Prism 프레임워크 기반 WPF 애플리케이션에서 테마 및 언어 리소스를 로딩, 전환 및 관리하는 역할을 합니다.
/// </summary>
public class ResourceManager : IResourceManager
{
  private string _currentTheme;
  private string _currentLanguage;
  private readonly HeimdallrApplication _app;
  private readonly BaseResourceInitializer _themeInitializer;
  private readonly IEventHub _eventHub;

  // 테마 및 언어 리소스를 저장하는 딕셔너리
  internal Dictionary<string, ResourceDictionary> ThemeResources { get; private set; }
  internal Dictionary<string, ResourceDictionary> LanguageResources { get; private set; }

  // UI 바인딩용 테마 및 언어 목록
  internal List<ThemeModel> Themes { get; private set; }
  internal List<ThemeModel> Languages { get; private set; }

  /// <summary>
  /// ResourceManager의 생성자입니다. 앱 실행 시 초기 테마와 언어를 설정하고 리소스를 로드합니다.
  /// </summary>
  /// <param name="app">애플리케이션 인스턴스</param>
  /// <param name="themeInitializer">테마 초기화 관련 설정</param>
  /// <param name="eventHub">이벤트 허브 (이벤트 발행 및 구독 처리)</param>
  /// <exception cref="ArgumentNullException">매개변수가 null인 경우 예외 발생</exception>
  public ResourceManager(HeimdallrApplication app, BaseResourceInitializer themeInitializer, IEventHub eventHub)
  {
    // 필드 초기화 및 null 체크
    _app = app ?? throw new ArgumentNullException(nameof(app));
    _themeInitializer = themeInitializer ?? throw new ArgumentNullException(nameof(themeInitializer));
    _eventHub = eventHub ?? throw new ArgumentNullException(nameof(eventHub));

    // 기본 테마 및 언어 설정
    _currentTheme = _themeInitializer.DefaultThemeName;
    _currentLanguage = _themeInitializer.DefaultLocale;

    // 테마 및 언어 리소스 로드
    ThemeResources = LoadResources(_themeInitializer.ThemePath);
    LanguageResources = LoadResources(_themeInitializer.LocalePath);

    // UI 바인딩용 테마 및 언어 목록 생성
    Themes = GetResourceList(ThemeResources);
    Languages = GetResourceList(LanguageResources);

    // 기본 테마 및 언어로 전환
    SwitchTheme(_themeInitializer.DefaultThemeName);
    SwitchLanguage(_themeInitializer.DefaultLocale);
  }

  /// <summary>
  /// 지정된 경로에서 리소스를 로드하여 ResourceDictionary 형태로 반환합니다.
  /// </summary>
  /// <param name="resourcePath">리소스 파일의 경로</param>
  /// <returns>리소스 딕셔너리</returns>
  /// <exception cref="InvalidOperationException">리소스 로드 실패 시 예외 발생</exception>
  /// <exception cref="FileNotFoundException">리소스 파일을 찾을 수 없을 때 예외 발생</exception>
  private Dictionary<string, ResourceDictionary> LoadResources(string resourcePath)
  {
    var result = new Dictionary<string, ResourceDictionary>();
    var assembly = Assembly.GetEntryAssembly()
        ?? throw new InvalidOperationException("EntryAssembly를 가져올 수 없습니다.");

    // 리소스 스트림을 열어 데이터를 읽음
    using var stream = assembly.GetManifestResourceStream(resourcePath)
        ?? throw new FileNotFoundException($"리소스를 찾을 수 없습니다: {resourcePath}");

    using var reader = new StreamReader(stream);
    var yaml = reader.ReadToEnd();

    // YAML 파서 사용하여 ThemeRoot 객체로 디시리얼라이징
    var deserializer = new Deserializer();
    var themeObject = deserializer.Deserialize<ThemeRoot>(new StringReader(yaml));

    if (themeObject == null)
    {
      throw new InvalidOperationException("테마 파일 디시리얼라이징 실패");
    }

    // 테마 데이터를 리소스 딕셔너리로 처리
    ProcessThemeData(themeObject, result);
    return result;
  }

  /// <summary>
  /// YAML에서 파싱한 테마 데이터를 리소스 딕셔너리 형태로 처리합니다.
  /// </summary>
  /// <param name="themeObject">디시리얼라이즈된 테마 객체</param>
  /// <param name="result">리소스 딕셔너리</param>
  private void ProcessThemeData(ThemeRoot themeObject, Dictionary<string, ResourceDictionary> result)
  {
    // 테마 색상 데이터를 처리
    if (themeObject.Themes != null)
    {
      foreach (var theme in themeObject.Themes)
      {
        foreach (var prop in theme.Colors!.GetType().GetProperties())
        {
          var colorValue = theme.Colors.Get(prop.Name);
          var color = (Color)ColorConverter.ConvertFromString(colorValue);

          // 리소스 딕셔너리에 색상 저장
          if (!result.ContainsKey(prop.Name))
            result[prop.Name] = new ResourceDictionary();

          result[prop.Name][theme.Key] = color;
        }
      }
    }

    // 폰트 데이터를 처리
    if (themeObject.Fonts != null)
    {
      foreach (var theme in themeObject.Fonts)
      {
        foreach (var prop in theme.Fonts!.GetType().GetProperties())
        {
          var fontValue = theme.Fonts.Get(prop.Name);
          var font = new FontFamily(fontValue);

          // 리소스 딕셔너리에 폰트 저장
          if (!result.ContainsKey(prop.Name))
            result[prop.Name] = new ResourceDictionary();

          result[prop.Name][theme.Key] = font;
        }
      }
    }

    // 언어 데이터를 처리
    if (themeObject.Languages != null)
    {
      foreach (var lang in themeObject.Languages)
      {
        foreach (var prop in lang.Fonts!.GetType().GetProperties())
        {
          var text = lang.Fonts.Get(prop.Name);

          // 리소스 딕셔너리에 언어 텍스트 저장
          if (!result.ContainsKey(prop.Name))
            result[prop.Name] = new ResourceDictionary();

          result[prop.Name][lang.Key] = text;
        }
      }
    }
  }

  /// <summary>
  /// 테마 및 언어 리소스의 Key 값을 기반으로 UI에 바인딩할 수 있는 리스트를 생성합니다.
  /// </summary>
  /// <param name="source">리소스 딕셔너리</param>
  /// <returns>테마 또는 언어 모델 목록</returns>
  private List<ThemeModel> GetResourceList(Dictionary<string, ResourceDictionary> source)
  {
    return source.Select(kvp => new ThemeModel(kvp.Key, kvp.Key)).ToList();
  }

  /// <summary>
  /// 현재 애플리케이션에서 사용 중인 테마를 다른 테마로 전환합니다.
  /// </summary>
  /// <param name="value">전환할 테마 이름</param>
  public void SwitchTheme(string value)
  {
    // 기존 테마 제거
    if (ThemeResources.TryGetValue(_currentTheme, out var oldTheme))
    {
      _app.Resources.MergedDictionaries.Remove(oldTheme);
    }

    // 새 테마 적용
    if (ThemeResources.TryGetValue(value, out var newTheme))
    {
      _app.Resources.MergedDictionaries.Add(newTheme);
      _currentTheme = value;
      _eventHub.Publish<SwitchThemePubsub, string>(value);
    }
  }

  /// <summary>
  /// 현재 애플리케이션에서 사용 중인 언어 리소스를 다른 언어로 전환합니다.
  /// </summary>
  /// <param name="value">전환할 언어 코드</param>
  public void SwitchLanguage(string value)
  {
    // 기존 언어 리소스 제거
    if (LanguageResources.TryGetValue(_currentLanguage, out var oldLang))
    {
      _app.Resources.MergedDictionaries.Remove(oldLang);
    }

    // 새 언어 리소스 적용
    if (LanguageResources.TryGetValue(value, out var newLang))
    {
      _app.Resources.MergedDictionaries.Add(newLang);
      _currentLanguage = value;
      _eventHub.Publish<SwitchLanguagePubsub, string>(value);
    }
  }
}

