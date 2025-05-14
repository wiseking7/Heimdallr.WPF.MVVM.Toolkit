using System.Windows;
using System.Windows.Controls.Primitives;

namespace Heimdallr.ToolKit.UI.Controls;

public class ThemeSwitchButton : ToggleButton
{
  static ThemeSwitchButton()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemeSwitchButton),
      new FrameworkPropertyMetadata(typeof(ThemeSwitchButton)));
  }
}
