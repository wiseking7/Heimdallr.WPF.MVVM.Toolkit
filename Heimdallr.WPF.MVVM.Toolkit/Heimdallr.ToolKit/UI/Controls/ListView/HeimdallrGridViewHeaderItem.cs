using System.Windows;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrGridViewHeaderItem 클래스는 Heimdallr UI 스타일에 맞춘 GridView 헤더 항목 컨트롤입니다.
/// </summary>
public class HeimdallrGridViewHeaderItem : HeimdallrListViewBaseHeaderItem
{
  /// <summary>
  /// HeimdallrGridViewHeaderItem 클래스의 정적 생성자입니다.
  /// </summary>
  static HeimdallrGridViewHeaderItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrGridViewHeaderItem),
      new FrameworkPropertyMetadata(typeof(HeimdallrGridViewHeaderItem)));
  }
  /// <summary>
  /// HeimdallrGridViewHeaderItem 클래스의 새 인스턴스를 초기화합니다.
  /// </summary>
  public HeimdallrGridViewHeaderItem()
  {
  }
}
