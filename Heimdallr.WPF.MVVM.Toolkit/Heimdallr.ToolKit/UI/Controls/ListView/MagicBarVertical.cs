using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;
/// <summary>
/// 미완성 가로는 완성, 세로는 진행중
/// </summary>
public class MagicBarVertical : ListView
{
  static MagicBarVertical()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(MagicBarVertical),
      new FrameworkPropertyMetadata(typeof(MagicBarVertical)));
  }
}

