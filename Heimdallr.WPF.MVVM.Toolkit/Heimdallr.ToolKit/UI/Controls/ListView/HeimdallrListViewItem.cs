using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

public class HeimdallrListViewItem : ListViewItem
{
  static HeimdallrListViewItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrListViewItem),
      new FrameworkPropertyMetadata(typeof(HeimdallrListViewItem)));
  }
}
