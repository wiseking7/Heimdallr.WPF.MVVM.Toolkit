using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

public class HeimdallrTreeView : TreeView
{
  #region CornerRadius

  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(
          "CornerRadius",
          typeof(CornerRadius),
          typeof(HeimdallrTreeView),
          new FrameworkPropertyMetadata());

  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }
  #endregion

  static HeimdallrTreeView()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrTreeView),
      new FrameworkPropertyMetadata(typeof(HeimdallrTreeView)));
  }

  protected override DependencyObject GetContainerForItemOverride()
  {
    return new HeimdallrTreeViewItem();
  }
}
