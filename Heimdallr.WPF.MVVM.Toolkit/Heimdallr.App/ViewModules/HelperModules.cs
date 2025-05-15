namespace Heimdallr.App;

public class HelperModules : IModule
{
  public void OnInitialized(IContainerProvider containerProvider)
  {
    // 무시해도 됨, 없다고 생각해도 됨
  }

  public void RegisterTypes(IContainerRegistry containerRegistry)
  {
    // 이 메서드가 핵심적인 역할
    //containerRegistry.Register<DirectoryManager>(); // 필요할 때 마다 인스턴스 생성

    // 다섯개의 확장메서드가 있음 4번쨰(인터페이스, 클래스 파마미터가 있음)
    //containerRegistry.RegisterSingleton<DirectoryManager>(); // 한번만 생성됨(선호)
    //containerRegistry.RegisterSingleton<FileService>();
    //containerRegistry.RegisterSingleton<NavigatorService>();

    //DirectoryManager dic = new DirectoryManager();
    //containerRegistry.RegisterInstance<DirectoryManager>(dic); // 지금바로 인스턴스를 실행하겠다 개인적으로 회피
  }
}
