using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 최소화 버튼
/// </summary>
public class MinimizeButton : Button
{
  static MinimizeButton()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(MinimizeButton),
      new FrameworkPropertyMetadata(typeof(MinimizeButton)));
  }
}

