using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// ListBox 커스터마이징
/// </summary>
public class HeimdallrListBox : ListBox
{
  #region CornerRadius
  /// <summary>
  /// CornerRadius 지정
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }
  /// <summary>
  /// 종송성주입
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(
          "CornerRadius",
          typeof(CornerRadius),
          typeof(HeimdallrListBox),
          new FrameworkPropertyMetadata());

  #endregion
}
