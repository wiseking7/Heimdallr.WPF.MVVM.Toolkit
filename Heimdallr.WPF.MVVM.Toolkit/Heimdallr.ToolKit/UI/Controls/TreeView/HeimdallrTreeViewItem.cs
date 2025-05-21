using Heimdallr.ToolKit.Models.TreeViewTest;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Heimdallr.ToolKit.UI.Controls;

public class HeimdallrTreeViewItem : TreeViewItem
{
  #region CornerRadius

  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(
          "CornerRadius",
          typeof(CornerRadius),
          typeof(HeimdallrTreeViewItem),
          new FrameworkPropertyMetadata());

  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }
  #endregion



  public ICommand SelectionCommand
  {
    get { return (ICommand)GetValue(SelectionCommandProperty); }
    set { SetValue(SelectionCommandProperty, value); }
  }

  public static readonly DependencyProperty SelectionCommandProperty =
      DependencyProperty.Register("SelectionCommand", typeof(ICommand), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(null));

  static HeimdallrTreeViewItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrTreeViewItem),
      new FrameworkPropertyMetadata(typeof(HeimdallrTreeViewItem)));
  }

  protected override DependencyObject GetContainerForItemOverride()
  {
    return new HeimdallrTreeViewItem();
  }

  public HeimdallrTreeViewItem()
  {
    MouseLeftButtonUp += HeimdallrTreeViewItem_MouseLeftButtonUp;
  }

  private void HeimdallrTreeViewItem_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
  {
    e.Handled = true;

    if (DataContext is FileItem items)
    {
      SelectionCommand?.Execute(items);
    }
  }
}
