using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// ListBoxItem 커스터마이징
/// </summary>
public class HeimdallrListBoxItem : ListBoxItem
{
  #region CornerRadius
  /// <summary>
  /// CornerRadius 값 지정
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }
  /// <summary>
  /// 종속성 주입
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(
          "CornerRadius",
          typeof(CornerRadius),
          typeof(HeimdallrListBoxItem),
          new FrameworkPropertyMetadata());

  #endregion
}
