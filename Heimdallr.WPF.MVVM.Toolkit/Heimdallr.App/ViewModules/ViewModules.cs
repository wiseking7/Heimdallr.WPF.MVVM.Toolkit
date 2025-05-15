namespace Heimdallr.App;

public class ViewModules : IModule
{
  public void OnInitialized(IContainerProvider containerProvider)
  {
    // 무시해도 됨, 없다고 생각해도 됨
  }

  public void RegisterTypes(IContainerRegistry containerRegistry)
  {
    // View 의 등록시 어느것을 사용할지 singleton, Instance 냐 여기는 View 등록
    //containerRegistry.RegisterSingleton<IViewable, MainContent>("MainContent");
    //containerRegistry.RegisterSingleton<IViewable, LocationContent>("LocationContent");
  }
}
