using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

public class CloseButton : Button
{
  static CloseButton()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(CloseButton),
      new FrameworkPropertyMetadata(typeof(CloseButton)));
  }
}
