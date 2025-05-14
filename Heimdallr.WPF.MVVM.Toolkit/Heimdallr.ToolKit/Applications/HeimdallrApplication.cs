using Heimdallr.ToolKit.Infrastructure.AutoWiring;
using Heimdallr.ToolKit.Infrastructure.Messaging;
using Heimdallr.ToolKit.Infrastructure.Navigation;
using Heimdallr.ToolKit.Resources.Initialization;
using Heimdallr.ToolKit.Resources.Initializationl;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace Heimdallr.ToolKit.Applications;

public abstract class HeimdallrApplication : PrismApplication
{
  // 사용자 정의 Module 을 수집하는 List<Imodule)제네릭 (Imodule 은 Prism 의 모듈 인텨페이스) 
  private List<IModule> _modules = new();

  /// <summary>
  /// theme 리소스를 초기화하는 개체(InitializeResource 메서드)
  /// </summary>
  private object? theme;

  /// <summary>
  /// 생성자
  /// </summary>
  public HeimdallrApplication()
  {
    try
    {
      // App 시작시 AddDefaultThemeResource 메서드를 추가
      AddDefaultThemeResource();
    }

    // 실행하면 예외 메시지를 메세지박스로 보여줌 (개발 편의용)
    catch (Exception ex)
    {
      MessageBox.Show("기본 테마 리소스를 추가하는 동안 오류가 발생했습니다: " + ex.Message);
      throw;
    }
  }

  /// <summary>
  /// OnStartup 오버라이드 App 의 시작 지점
  /// </summary>
  /// <param name="e"></param>
  protected override void OnStartup(StartupEventArgs e)
  {
    // 클래스
    ViewModelLocatorCollection items = new ViewModelLocatorCollection();

    // 호출하여 ViewModel 등록 (MVVM 연결용)
    RegisterWireDataContexts(items);

    //  base.OnStartup 으로 Prism 의 기본 동작도 유지 및 
    base.OnStartup(e);
  }

  /// <summary>
  /// 테마 리소스 추가 메서드
  /// </summary>
  private void AddDefaultThemeResource()
  {
    // entryAssembly 의 참조된 어셈블리들에서 Themes/Default.xaml 을 찾기
    Assembly? entryAssembly = Assembly.GetEntryAssembly();
    if (entryAssembly == null)
    {
      Debug.WriteLine("Error: entryAssembly 를 가져올 수 없습니다.");
      return;
    }

    AssemblyName[] referencedAssemblies = entryAssembly.GetReferencedAssemblies();
    AssemblyName[] array = referencedAssemblies;
    foreach (AssemblyName assemblyName in array)
    {
      try
      {
        string text = assemblyName.Name + ";component/Themes/Default.xaml";
        Uri source = new Uri("/" + text, UriKind.RelativeOrAbsolute);
        ResourceDictionary item = new ResourceDictionary
        {
          Source = source
        };

        // 리소스를 Application.Resources.MergedDictionaries에 추가
        base.Resources.MergedDictionaries.Add(item);
        return;
      }
      catch (Exception ex)
      {
        Console.WriteLine("Themes/Default.xaml 로드할 수 없습니다" +
                                       assemblyName.Name + ": " + ex.Message);
      }
    }

    // 유저 환경에서 콘솔은 보이지 않기 때문에, 디버깅용이 아니라면 Degug.WriteLine() 아니 Logger 사용 추천
    Debug.WriteLine("Error: 참조된 어셈블리에서 Themes/Default.xaml 리소스를 찾을 수 없습니다");
  }

  /// <summary>
  /// Inversion of Control (Ioc) 모듈을 추가합니다.
  /// </summary>
  /// <typeparam name="T">모듈타입</typeparam>
  /// <returns>현재 인스턴스</returns>
  public HeimdallrApplication AddInversionModule<T>() where T : IModule, new()
  {
    // Imodule<T>제네릭을 인스턴스화하여 _modules. 리스트 저장 (Ioc 관련 모듈 개발자 정의)
    IModule item = new T();
    _modules.Add(item);
    return this;
  }

  /// <summary>
  /// ViewModelLocationScenario 는 View-ViewModel 매핑 설정하는 클래스 (체이닝 지원)
  /// ViewModel 을 prism 의 ViewModelLocator 에 등록
  /// 이 메서드는 ViewModelLocationScenario 타입의 ViewModel을 생성하고, ViewModelLocator 에 등록하는 작업을 수행
  /// </summary>
  /// <typeparam name="T">
  /// 등록할 iewModelLocationScenario 타입. 이 타입은 ViewModelLocator에 ViewModel을 등독할 때 필요한 로직을 구현해야 합니다. 
  /// </typeparam>
  /// <returns>
  /// 현재 HeimdallrApplication 인스턴스를 반환하여 메서드 체이닝을 지원합니다.</returns>
  public HeimdallrApplication AddWireDataContext<T>() where T : ViewModelLocationScenario, new()
  {
    ViewModelLocationScenario viewModelLocationScenario = new T();

    // 내부에서 Publish() 호출하여 Prism 의 ViewModelLocator 에 등록
    viewModelLocationScenario.Publish();
    return this;

    // 자식클래스에서 ViewModel 을 등록할 때 필요한 작업을 오버라이드 하세요
  }

  /// <summary>
  /// Hook 메서드 자식클래스에서 오버라이딩해서 ViewModelLocator 등록 가능 (기본은 비어있음)
  /// </summary>
  /// <param name="items"></param>
  protected virtual void RegisterWireDataContexts(ViewModelLocatorCollection items)
  {
  }

  /// <summary>
  /// Prism 의 RegisterTypes 오버라이드 DI 컨테이너에서 필요한 서비스 등록
  ///  this 자체등 등록(RegisterInstance), theme 개체가 설정되어 있으면 관련 서비스 등록
  /// _modules 리스스트에 있는 Imodule 들의 RegisterTypes() 실행
  /// 이곳이 서비스, 테마, 외부 모듈 등 종합적으로 DI 등록을 총괄하는 곳입니다.
  /// </summary>
  /// <param name="containerRegistry"></param>
  protected override void RegisterTypes(IContainerRegistry containerRegistry)
  {
    containerRegistry.RegisterInstance(this);
    containerRegistry.RegisterSingleton<ContentManager>();
    containerRegistry.RegisterSingleton<IEventHub, EventAggregatorHub>();

    if (theme != null && theme is BaseResourceInitializer initializer)
    {
      // theem 가 BaseResourceInitializer 이 아닐 경우 null 로 등록할 수 있습니다.
      containerRegistry.RegisterInstance(initializer);
      containerRegistry.RegisterSingleton<ResourceManager>();

      // RegisterSingleton 이후 바로 갸져올 수 있습니다.
      var service = GetService<ResourceManager>();
    }

    else
    {
      Debug.WriteLine("Error: theme 초기화 되지 않았습니다");
    }

    foreach (IModule module in _modules)
    {
      module.RegisterTypes(containerRegistry);
    }
  }

  /// <summary>
  /// Themes 를 초기화할 개체를 생성해서 저장, 추후 RegisterTypes 에서 DI 등록해서 활용됨
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public HeimdallrApplication InitializeResource<T>() where T : BaseResourceInitializer, new()
  {
    if (theme == null)
    {
      // theme가 초기화된 경우에는 다시 초기화되지 않음
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
