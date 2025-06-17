using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Login View UserControl for Heimdallr.
/// </summary>
public class HeimdallrLogin : UserControl
{
  static HeimdallrLogin()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrLogin),
      new FrameworkPropertyMetadata(typeof(HeimdallrLogin)));
  }
}
