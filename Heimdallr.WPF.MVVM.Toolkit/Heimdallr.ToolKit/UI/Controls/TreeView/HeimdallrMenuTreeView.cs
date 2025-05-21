using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

public class HeimdallrMenuTreeView : TreeView
{
  static HeimdallrMenuTreeView()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrMenuTreeView),
      new FrameworkPropertyMetadata(typeof(HeimdallrMenuTreeView)));
  }

  protected override DependencyObject GetContainerForItemOverride()
  {
    return new HeimdallrMenuTreeViewItem();
  }
}
