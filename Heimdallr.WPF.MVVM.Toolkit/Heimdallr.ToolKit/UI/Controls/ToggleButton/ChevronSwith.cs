using System.Windows;
using System.Windows.Controls.Primitives;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// ChevronSwith 
/// </summary>
public class ChevronSwith : ToggleButton
{
  static ChevronSwith()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(ChevronSwith),
      new FrameworkPropertyMetadata(typeof(ChevronSwith)));
  }
}
