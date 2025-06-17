using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls
{
  /// <summary>
  /// 
  /// </summary>
  public class HeimdallrFlatButton : Button
  {
    static HeimdallrFlatButton()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrFlatButton), new FrameworkPropertyMetadata(typeof(HeimdallrFlatButton)));
    }
  }
}
