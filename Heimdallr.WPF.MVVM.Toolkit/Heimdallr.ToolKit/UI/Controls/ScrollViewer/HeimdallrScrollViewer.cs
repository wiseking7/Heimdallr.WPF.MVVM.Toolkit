using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// ScrollViewer 커스터마이징
/// </summary>
public class HeimdallrScrollViewer : ScrollViewer
{
  static HeimdallrScrollViewer()
  {
    // 기본 스타일 키를 HeimdallrScrollViewer로 설정하여 테마에서 해당 스타일을 찾도록 함
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrScrollViewer),
      new FrameworkPropertyMetadata(typeof(HeimdallrScrollViewer)));
  }
}
