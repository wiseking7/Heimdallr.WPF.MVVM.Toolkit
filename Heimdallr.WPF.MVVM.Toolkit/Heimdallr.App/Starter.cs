namespace Heimdallr.App;

public class Starter
{
  [STAThread]
  private static void Main(string[] args)
  {
    App app = new App();
    _ = app.Run();
  }
}
