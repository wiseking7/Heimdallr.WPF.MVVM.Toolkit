using System.Windows;
using System.Windows.Controls.Primitives;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Togglebutton Switch
/// </summary>
public class HeimdallrToggleSwitch : ToggleButton
{
  #region CornerRadius
  /// <summary>
  /// CornerRadius 종속성 주입
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(
          "CornerRadius",
          typeof(CornerRadius),
          typeof(HeimdallrToggleSwitch),
          new FrameworkPropertyMetadata());

  /// <summary>
  /// CornerRadius 값 지정
  /// </summary>
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

