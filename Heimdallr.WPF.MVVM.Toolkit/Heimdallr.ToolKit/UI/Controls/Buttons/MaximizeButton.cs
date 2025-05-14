using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

public class MaximizeButton : Button
{
  public bool IsMaximize
  {
    get { return (bool)GetValue(IsMaximizeProperty); }
    set { SetValue(IsMaximizeProperty, value); }
  }
  public static readonly DependencyProperty IsMaximizeProperty =
      DependencyProperty.Register(nameof(IsMaximize), typeof(bool), typeof(MaximizeButton),
        new PropertyMetadata(false, MaximizePropertyChanged));

  private static void MaximizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var btn = (MaximizeButton)d;
    btn.img!.Icon = btn.IsMaximize ? IconType.Restore : IconType.Maximize;
  }

  static MaximizeButton()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(MaximizeButton),
      new FrameworkPropertyMetadata(typeof(MaximizeButton)));
  }

  private HeimdallrIcon? img;
  public MaximizeButton()
  {
  }
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    if (GetTemplateChild("PART_IMG") is HeimdallrIcon maxbtn)
    {
      img = maxbtn;
    }
  }
}
