using System.Windows;
using System.Windows.Controls.Primitives;

namespace Heimdallr.ToolKit.UI.Controls;

public class HeimdallrToggleSwitch : ToggleButton
{
  #region CornerRadius
  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(
          "CornerRadius",
          typeof(CornerRadius),
          typeof(HeimdallrToggleSwitch),
          new FrameworkPropertyMetadata());

  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }

  static HeimdallrToggleSwitch()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrToggleSwitch),
     new FrameworkPropertyMetadata(typeof(HeimdallrToggleSwitch)));
  }
  #endregion
}

