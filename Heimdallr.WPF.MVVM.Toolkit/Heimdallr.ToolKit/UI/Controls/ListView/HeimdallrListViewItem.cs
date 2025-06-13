using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// ListViewItem 을 상속받아
/// </summary>
public class HeimdallrListViewItem : ListViewItem
{
  /// <summary>
  /// 생성자
  /// </summary>
  static HeimdallrListViewItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrListViewItem),
      new FrameworkPropertyMetadata(typeof(HeimdallrListViewItem)));
  }
}
