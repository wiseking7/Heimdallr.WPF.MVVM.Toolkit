using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

public class RiotSlider : Slider
{
  static RiotSlider()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(RiotSlider),
      new FrameworkPropertyMetadata(typeof(RiotSlider)));
  }
}

