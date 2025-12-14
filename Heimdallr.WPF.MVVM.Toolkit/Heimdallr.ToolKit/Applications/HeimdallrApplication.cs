using Heimdallr.ToolKit.Infrastructure;
using Heimdallr.ToolKit.Resources;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace Heimdallr.ToolKit.Applications;

/// <summary>
/// PrismApplication 을 상속받는 추상 클래스
/// Prism 의 MVVM 및 DI 기능을 기반으로, 테마 리소스와 ViewModel,
/// IoC 모듈 설정을 포함하여 앱 전반을 초기화합니다.
/// </summary>

public abstract class HeimdallrApplication : PrismApplication
{
  // Prism 모듈 인터페이스를 구현한 사용자 정의 모듈을 담는 List
  private List<IModule> _modules = new();

  /// <summary>
  /// 테마 리소스를 초기화할 개체를 담는 변수
  /// BaseResourceInitializer 를 상속한 클래스가 저장됩니다.
  /// </summary>
  private object? theme;

  /// <summary>
  /// 생성자 (애플리케이션이 시작될 때 테마 리소스를 로드합니다.)
  /// </summary>
  public HeimdallrApplication()
  {
    try
    {
      // 기본 테마 리소스를 애플리케이션 리소스에 추가
      AddDefaultThemeResource();

      // 테마 초기화 개체 생성
      InitializeResource<HeimdallrDefaultResource>();

      Debug.WriteLine($"[{nameof(HeimdallrApplication)}.{MethodBase.GetCurrentMethod()?.Name}] Generic.xaml Themes Load 완료");
    }

    catch (Exception ex)
    {
      // 오류 발생 시 메시지 박스로 알림
      Debug.WriteLine($"[{nameof(HeimdallrApplication)}.{MethodBase.GetCurrentMethod()?.Name}] 기본 Themes(Generic) Resource 추가하는 동안 오류가 발생했습니다 -> {ex.Message}");
      throw;
    }
  }

  /// <summary>
  /// 애플리케이션 시작 지점 - OnStartup 오버라이드
  /// </summary>
  /// <param name="e">StartupEventArgs</param>
  protected override void OnStartup(StartupEventArgs e)
  {
    // ViewModelLocator 등록용 컨테이너 생성
    ViewModelLocatorCollection items = new ViewModelLocatorCollection();

    // ViewModelLocator 등록을 위한 사용자 정의 메서드 호출 (자식 클래스에서 override 가능)
    RegisterWireDataContexts(items);

    // PrismApplication 기본 동작 호출 (Shell 생성 등)
    base.OnStartup(e);
  }

  /// <summary>
  /// 애플리케이션의 리소스에 기본 테마(Heimdallr.ToolKit의 Generic.xaml)를 동적으로 추가합니다.
  /// </summary>
  private void AddDefaultThemeResource()
  {
    try
    {
      // 1. Generic.xaml 리소스를 가져올 URI를 정의합니다. 여기서는 Theme.xaml 로 고정 (주소입력 정확히)
      Uri resourceUri = new Uri("pack://application:,,,/Heimdallr.ToolKit;component/Themes/Generic.xaml", UriKind.Absolute);

      // 2. ResourceDictionary 객체를 생성하고, 정의된 URI를 Source에 할당하여 리소스를 로드합니다.
      ResourceDictionary resourceDictionary = new ResourceDictionary { Source = resourceUri };

      // 3. 리소스를 애플리케이션 리소스에 병합합니다.
      base.Resources.MergedDictionaries.Add(resourceDictionary);

      // 4. 테마 로드 성공 로그를 출력합니다.
      Debug.WriteLine($"[{nameof(HeimdallrApplication)}.{MethodBase.GetCurrentMethod()?.Name}] Themes(Generic) Load Succrss");
    }
    catch (Exception ex)
    {
      // 5. 예외 발생 시 오류 메시지를 출력합니다.
      Debug.WriteLine($"[{nameof(HeimdallrApplication)}.{MethodBase.GetCurrentMethod()?.Name}] Error Loading Themes(Generic) -> {ex.Message}");
    }
  }


  /// <summary>
  /// IoC 모듈을 애플리케이션에 추가
  /// </summary>
  /// <typeparam name="T">모듈타입</typeparam>
  /// <returns>현재 인스턴스</returns>
  public HeimdallrApplication AddInversionModule<T>() where T : IModule, new()
  {
    // 새 모듈 인스턴스를 생성하여 리스트에 추가
    IModule item = new T();
    _modules.Add(item);
    return this;
  }

  /// <summary>
  /// ViewModelLocationScenario 는 View-ViewModel 매핑 설정하는 클래스 (체이닝 지원)
  /// ViewModel 을 prism 의 ViewModelLocator 에 등록
  /// 이 메서드는 ViewModelLocationScenario 타입의 ViewModel을 생성하고, 
  /// ViewModelLocator 에 등록하는 작업을 수행
  /// </summary>
  /// <typeparam name="T">
  /// 등록할 iewModelLocationScenario 타입. 이 타입은 ViewModelLocator에 
  /// ViewModel을 등독할 때 필요한 로직을 구현해야 합니다. 
  /// View 와 ViewModel 간의 매핑 규칙을 정의하는 역할을 합니다.
  /// </typeparam>
  /// <returns>
  /// 현재 HeimdallrApplication 인스턴스를 반환하여 메서드 체이닝을 지원합니다.</returns>
  public HeimdallrApplication AddWireDataContext<T>() where T : ViewModelLocationScenario, new()
  {
    // ViewModel 등록 시나리오 객체 생성
    ViewModelLocationScenario viewModelLocationScenario = new T();

    // ViewModelLocator 에 ViewModel 을 매핑
    viewModelLocationScenario.Publish();

    return this;

  }

  /// <summary>
  /// ViewModel 등록을 위해 오버라이딩 가능한 메서드
  /// 자식 클래스에서 ViewModelLocatorCollection 을 이용해 ViewModel 등록 가능
  /// </summary>
  /// <param name="items"></param>
  protected virtual void RegisterWireDataContexts(ViewModelLocatorCollection items)
  {
    // 기본적으로 아무 작업 없음
  }

  /// <summary>
  /// Prism 의 RegisterTypes 오버라이드 DI 컨테이너에서 필요한 서비스 등록
  /// this 자체등 등록(RegisterInstance), theme 개체가 설정되어 있으면 관련 서비스 등록
  /// _modules 리스스트에 있는 Imodule 들의 RegisterTypes() 실행
  /// 이곳이 서비스, 테마, 외부 모듈 등 종합적으로 DI 등록을 총괄하는 곳입니다.
  /// </summary>
  /// <param name="containerRegistry"></param>
  protected override void RegisterTypes(IContainerRegistry containerRegistry)
  {
    // 현재 애플리케이션 인스턴스 등록
    containerRegistry.RegisterInstance(this);

    // 공통 서비스 등록
    containerRegistry.RegisterSingleton<ContentManager>();
    containerRegistry.RegisterSingleton<IEventHub, EventAggregatorHub>();

    // 테마 초기화 객체가 설정되어 있다면 DI 등록
    if (theme != null && theme is BaseResourceInitializer initializer)
    {
      // theem 가 BaseResourceInitializer 이 아닐 경우 null 로 등록할 수 있습니다.
      containerRegistry.RegisterInstance(initializer);
      containerRegistry.RegisterSingleton<ResourceManager>();

      // 등록 직후 리소스 매니저를 바로 사용할 수 있음
      // var service = GetService<ResourceManager>();
    }

    else
    {
      Debug.WriteLine($"[{nameof(HeimdallrApplication)}.{MethodBase.GetCurrentMethod()?.Name}] Error: Themes(Genrric) 초기화 되지 않았습니다");
    }

    // 사용자 정의 모듈들의 RegisterTypes 메서드 호출하여 모듈 별 서비스 등록
    foreach (IModule module in _modules)
    {
      module.RegisterTypes(containerRegistry);
    }
  }

  /// <summary>
  /// 테마를 초기화할 객체를 생성하고 보관, RegisterTypes 에서 이를 DI 에 등록하여 사용
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public HeimdallrApplication InitializeResource<T>() where T : BaseResourceInitializer, new()
  {
    // 이미 초기화되어 있으면 재초기화하지 않음
    if (theme == null)
    {
      theme = new T();
    }

    return this;
  }

  /// <summary>
  /// 어디서든 static 으로 DI 컨테이너에서 서비스 조회 가능
  /// 간편하게 ViewModel 또는 Helper 클래스에서 서비스를 불러올 수 있음 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public static T? GetService<T>()
  {
    if (Application.Current is HeimdallrApplication heimdallrApplication)
    {
      return heimdallrApplication.Container.Resolve<T>();
    }

    return default; // return default(T) 
  }

  /// <summary>
  /// 현재 App 리소스를 가져오는 static Helper 
  /// View 또는 코드에서 Themes/Defalut.xaml 같은 리소스 접근에 활용 가능
  /// </summary>
  /// <returns></returns>
  public static ResourceDictionary? Resource()
  {
    if (Application.Current is HeimdallrApplication heimdallrApplication)
    {
      return heimdallrApplication.Resources;
    }

    return null;
  }
}
