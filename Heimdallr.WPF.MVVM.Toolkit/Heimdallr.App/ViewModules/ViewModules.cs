namespace Heimdallr.App;

public class ViewModules : IModule
{
  /// <summary>
  /// 모듈이 모두 로드된 후에 호출됩니다. 즉 DI 등록이 모두 끝난 뒤 실행됩니다.
  /// 뷰를 직접 띄우거나 어떤 서비스의 동작을 강제로 시작시키는 용도로 쓸 수 있지만, Prism의 구조에서는 그런 식의 강제 동작이 권장되지 않습니다.
  /// </summary>
  /// <param name="containerProvider"></param>
  public void OnInitialized(IContainerProvider containerProvider)
  {
    // 무시해도 됨, 없다고 생각해도 됨
    // 왜 사용하지 않는가
    // 언제 사용하는가..
    // 모듈 간 통신 초기화
  }

  /// <summary>
  /// 종속성 등록 전용 (서비스, View, ViewModel, 등 DI 컨테이너에 등록)
  /// 실제 앱이 시작되기 전에 먼저 호출됩니다. 가장 중요한 시점, 대부분의 작업은 이곳에서 이루어집니다.
  /// </summary>
  /// <param name="containerRegistry"></param>
  public void RegisterTypes(IContainerRegistry containerRegistry)
  {
    // View 의 등록시 어느것을 사용할지 singleton, Instance 냐 여기는 View 등록
    //containerRegistry.RegisterSingleton<IViewable, MainContent>("MainContent");
    //containerRegistry.RegisterSingleton<IViewable, LocationContent>("LocationContent");
  }
}
