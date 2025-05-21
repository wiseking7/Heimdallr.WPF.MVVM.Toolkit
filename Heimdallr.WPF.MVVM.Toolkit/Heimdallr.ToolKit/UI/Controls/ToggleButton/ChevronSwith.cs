using System.Windows;
using System.Windows.Controls.Primitives;

namespace Heimdallr.ToolKit.UI.Controls;

public class ChevronSwith : ToggleButton
{
  static ChevronSwith()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(ChevronSwith),
      new FrameworkPropertyMetadata(typeof(ChevronSwith)));
  }
}
