using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

public class HeimdallrMenuTreeViewItem : TreeViewItem
{
  static HeimdallrMenuTreeViewItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrMenuTreeViewItem),
      new FrameworkPropertyMetadata(typeof(HeimdallrMenuTreeViewItem)));
  }
}
