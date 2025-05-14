namespace Heimdallr.ToolKit.Resources.Initialization;

/// <summary>
/// WPF 또는 .NET 애플리케이션에서 테마 및 로케일(언어/문화 설정) 관련 리소스 경로를 초기화하기 위한 추상 클래스입니다.
/// 
/// 이 클래스는 직접 인스턴스화되지 않으며, 상속을 통해 구체 클래스에서 다음 값을 동적으로 결정하도록 설계되어 있습니다:
/// - 테마 리소스 경로
/// - 로케일 리소스 경로
/// - 기본 테마 이름
/// - 기본 로케일
/// 
/// </summary>
public abstract class BaseResourceInitializer
{
  /// <summary>
  /// 테마 리소스 파일의 기본 경로
  /// 예: "Resources/Themes"
  /// </summary>
  public string ThemePath { get; }

  /// <summary>
  /// 애플리케이션의 기본 로케일 (언어-국가 코드)
  /// 예: "en-US", "ko-KR"
  /// </summary>
  public string DefaultLocale { get; }

  /// <summary>
  /// 기본 테마 이름
  /// 예: "Light", "Dark"
  /// </summary>
  public string DefaultThemeName { get; }

  /// <summary>
  /// 로케일 리소스 파일의 기본 경로
  /// 예: "Resources/Locales"
  /// </summary>
  public string LocalePath { get; }

  /// <summary>
  /// 생성자에서 각 속성을 초기화합니다.
  /// 구체 클래스에서 구현된 추상 메서드를 호출하여 값을 동적으로 가져옵니다.
  /// </summary>
  protected BaseResourceInitializer()
  {
    ThemePath = FetchThemePath();
    DefaultThemeName = DetermineDefaultThemeName();
    LocalePath = FetchLocalePath();
    DefaultLocale = DetermineDefaultLocale();
  }

  #region 추상 메서드 (자식 클래스에서 반드시 구현 필요)

  /// <summary>
  /// 테마 리소스의 경로를 반환합니다.
  /// </summary>
  protected abstract string FetchThemePath();

  /// <summary>
  /// 기본 테마 이름을 결정하여 반환합니다.
  /// </summary>
  protected abstract string DetermineDefaultThemeName();

  /// <summary>
  /// 로케일 리소스의 경로를 반환합니다.
  /// </summary>
  protected abstract string FetchLocalePath();

  /// <summary>
  /// 기본 로케일을 결정하여 반환합니다.
  /// </summary>
  protected abstract string DetermineDefaultLocale();

  #endregion
}

