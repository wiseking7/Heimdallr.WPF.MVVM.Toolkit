using System.Windows;
using System.Windows.Controls.Primitives;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 테마스위치버튼
/// </summary>
public class ThemeSwitchButton : ToggleButton
{
  static ThemeSwitchButton()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemeSwitchButton),
      new FrameworkPropertyMetadata(typeof(ThemeSwitchButton)));
  }
}
