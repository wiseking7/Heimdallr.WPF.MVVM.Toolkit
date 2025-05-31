using Heimdallr.ToolKit.Animations;
using Heimdallr.ToolKit.Animations.EasingFunctions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.UI.Controls;

public class MagicBar : ListView
{
  static MagicBar()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(MagicBar),
      new FrameworkPropertyMetadata(typeof(MagicBar)));
  }

  private DoubleAnimationItem? _vi;
  private Storyboard? _sb;

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    Grid grid = (Grid)GetTemplateChild("PART_Circle");

    InitStoryboard(grid);
  }

  private void InitStoryboard(Grid circle)
  {
    _vi = new();
    _sb = new();

    _vi.Mode = EasingFunctionBaseMode.QuinticEaseInOut;
    _vi.Property = new PropertyPath(Canvas.LeftProperty);
    _vi.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 500));

    Storyboard.SetTarget(_vi, circle);
    Storyboard.SetTargetProperty(_vi, _vi.Property);

    _sb.Children.Add(_vi);
  }

  protected override void OnSelectionChanged(SelectionChangedEventArgs e)
  {
    base.OnSelectionChanged(e);

    _vi!.To = SelectedIndex * 80;
    _sb!.Begin();
  }
}
