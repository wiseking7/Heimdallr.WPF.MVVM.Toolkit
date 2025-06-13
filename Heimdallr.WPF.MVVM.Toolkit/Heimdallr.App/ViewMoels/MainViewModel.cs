using Heimdallr.ToolKit.Commons;
using System.Windows;

namespace Heimdallr.App.ViewMoels;

public partial class MainViewModel : ObservableBase
{
  public string? TextMessage { get; set; }

  public DelegateCommand HoldSubmitButtonCommamd => new DelegateCommand(ExecuteHoldSubmit);

  private void ExecuteHoldSubmit()
  {
    MessageBox.Show("Hold Button");
  }

  public MainViewModel(IEventAggregator eventAggregator)
  {

  }
}

