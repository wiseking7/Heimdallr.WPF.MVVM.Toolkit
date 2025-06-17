using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrToolTip은 사용자 정의 ToolTip 클래스로, Heimdallr UI에서 사용되는 툴팁을 나타냅니다.
/// </summary>
public class HeimdallrToolTip : ToolTip
{
  static HeimdallrToolTip()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrToolTip),
      new FrameworkPropertyMetadata(typeof(HeimdallrToolTip)));
  }
}
