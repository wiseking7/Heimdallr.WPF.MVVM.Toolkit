namespace Heimdallr.App;

/// <summary>
/// WPF 앱의 진입점을 담당합니다. 
/// 일반적으로 App.xaml 이 있는 프로젝트는 자동으로 App.xaml.cs 에서 Application.Run()을 호출하지만,
/// 여기서는 App.xaml 없이 코드 기반으로 앱을 초기화합니다. 
/// </summary>
public class Starter
{
  // WPF 앱에서 반드시 필요한 특성입니다. 
  // STA(Single Threaded Apartment)는 UI 요소가 단일 스레드에서 실행되어야 함을 명시합니다
  [STAThread]
  // 애플리케이션의 시작 지점입니다.
  // 여기서 App 개체를 생성하고 여러 설정 메서드를 체이닝으로 호출한 후 실행합니다
  private static void Main(string[] args)
  {
    // WireDataContent 을 연결시켜주면 됨

    // _ = 반환값이 있지만 사용하지 않겠다는 의미
    // AddWireDataContext 메서드를 사용하여 View 와 ViewModel 을 연결시줌
    _ = new App() // 인스턴스를 생성
     .AddWireDataContext<WireDataContent>() // View 와 ViewModel 을 자동 연결
                                            //.AddInversionModule<HelperModules>() // 종속성(Help)을 관리하는 모듈클래스
                                            //.AddInversionModule<ViewModules>() // View를 관리

     // PrismApplication.Run을 호출하여 애플리케이션 실행 
     .Run();
  }
}
