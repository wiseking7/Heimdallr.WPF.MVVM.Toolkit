namespace Heimdallr.App;

public class Starter
{
  [STAThread]
  private static void Main(string[] args)
  {
    // WireDataContent 을 연결시켜주면 됨

    // _ = 반환타입이 있는데 사용하지 않을 거야
    // AddWireDataContext 메서드를 사용하여 View 와 ViewModel 을 연결시줌
    _ = new App()
     .AddWireDataContext<WireDataContent>() // View 와 ViewModel 을 연결
                                            //.AddInversionModule<HelperModules>() // 종속성(Help)을 관리하는 모듈클래스
                                            //.AddInversionModule<ViewModules>() // View를 관리
     .Run();
  }
}
