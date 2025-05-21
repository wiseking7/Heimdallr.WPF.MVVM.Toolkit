using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;


public class MagicStackPanel : StackPanel
{
  private SolidColorBrush color1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#393E46"));
  private SolidColorBrush color2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#948979"));
  public double ItemHeight
  {
    get { return (double)GetValue(ItemHeightProperty); }
    set { SetValue(ItemHeightProperty, value); }
  }
  public static readonly DependencyProperty ItemHeightProperty =
      DependencyProperty.Register("ItemHeight", typeof(double), typeof(MagicStackPanel),
        new PropertyMetadata(0.0, ItemHeightPropertyChanged));

  private static void ItemHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    MagicStackPanel panel = (MagicStackPanel)d;
    panel.Children.Clear();
    panel.Height = panel.ItemHeight;

    int index = (int)panel.ItemHeight / 36;

    for (int i = 0; i < index; i++)
    {
      Border border = new();
      border.Height = 36;
      border.Background = i % 2 == 0 ? panel.color1 : panel.color2;
      panel.Children.Add(border);
    }
  }
}
