using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

public class HeimdallrRegion : ContentControl
{
  public static readonly DependencyProperty RegionNameProperty =
    DependencyProperty.Register(nameof(RegionName), typeof(string), typeof(HeimdallrRegion),
      new PropertyMetadata(ContentNamePropertyChanged));

  public string RegionName
  {
    get => (string)GetValue(RegionNameProperty);
    set => SetValue(RegionNameProperty, value);
  }

  private static void ContentNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (e.NewValue is string str && str != "")
    {
      IRegionManager rm = RegionManager.GetRegionManager(Application.Current.MainWindow);
      RegionManager.SetRegionName((HeimdallrRegion)d, str);
      RegionManager.SetRegionManager(d, rm);
    }
  }

  static HeimdallrRegion()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrRegion),
      new FrameworkPropertyMetadata(typeof(HeimdallrRegion)));
  }

  public HeimdallrRegion()
  {
  }
}

