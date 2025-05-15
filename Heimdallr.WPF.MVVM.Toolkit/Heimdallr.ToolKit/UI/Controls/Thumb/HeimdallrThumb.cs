using System.Windows;
using System.Windows.Controls.Primitives;

namespace Heimdallr.ToolKit.UI.Controls;

public class HeimdallrThumb : Thumb
{
  static HeimdallrThumb()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrThumb),
        new FrameworkPropertyMetadata(typeof(HeimdallrThumb)));
  }
}
