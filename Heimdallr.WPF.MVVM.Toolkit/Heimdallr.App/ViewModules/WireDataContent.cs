using Heimdallr.App.ViewMoels;
using Heimdallr.ToolKit.Infrastructure.AutoWiring;

namespace Heimdallr.App;

public class WireDataContent : ViewModelLocationScenario
{
  protected override void Match(ViewModelLocatorCollection items)
  {
    // View 와 ViewModel(등록) 이 100개 이상 있을시 명확하게 관리하게 위해서
    items.Register<MainWindow, MainViewModel>(); //매칭
  }
}
