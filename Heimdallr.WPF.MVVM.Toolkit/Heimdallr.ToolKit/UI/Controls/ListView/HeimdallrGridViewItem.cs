using System.Windows;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrGridViewItem 클래스는 HeimdallrListViewBaseItem을 확장하여 GridView 형식의 항목을 표시하는 사용자 지정 ListView 컨트롤입니다.
/// </summary>
public class HeimdallrGridViewItem : HeimdallrListViewBaseItem
{
  static HeimdallrGridViewItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrGridViewItem),
      new FrameworkPropertyMetadata(typeof(HeimdallrGridViewItem)));
  }
}
