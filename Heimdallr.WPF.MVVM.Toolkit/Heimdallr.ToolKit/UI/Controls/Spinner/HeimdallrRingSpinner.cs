using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Heimdallr.ToolKit.UI.Controls;
public class HeimdallrRingSpinner : Control
{
  static HeimdallrRingSpinner()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrRingSpinner),
        new FrameworkPropertyMetadata(typeof(HeimdallrRingSpinner)));
  }

  public static readonly DependencyProperty SpinnerColorProperty =
      DependencyProperty.Register("SpinnerColor", typeof(Brush), typeof(HeimdallrRingSpinner),
        new PropertyMetadata(Brushes.Red));

  public static readonly DependencyProperty MessageProperty =
      DependencyProperty.Register("Message", typeof(string), typeof(HeimdallrRingSpinner),
        new PropertyMetadata("Please wait..."));

  public static readonly DependencyProperty DurationProperty =
      DependencyProperty.Register("Duration", typeof(double), typeof(HeimdallrRingSpinner),
        new PropertyMetadata(1.5));

  public static readonly DependencyProperty IsLoadingProperty =
      DependencyProperty.Register("IsLoading", typeof(bool), typeof(HeimdallrRingSpinner),
        new PropertyMetadata(false));


  public bool IsLoading
  {
    get => (bool)GetValue(IsLoadingProperty);
    set => SetValue(IsLoadingProperty, value);
  }

  public Brush SpinnerColor
  {
    get => (Brush)GetValue(SpinnerColorProperty);
    set => SetValue(SpinnerColorProperty, value);
  }

  public string Message
  {
    get => (string)GetValue(MessageProperty);
    set => SetValue(MessageProperty, value);
  }

  public double Duration
  {
    get => (double)GetValue(DurationProperty);
    set => SetValue(DurationProperty, value);
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    if (GetTemplateChild("PART_SpinnerCanvas") is Canvas dots)
    {
      dots.Children.Clear();

      int dotCount = 8;
      double radius = 40;

      for (int i = 0; i < dotCount; i++)
      {
        double angle = 360.0 / dotCount * i;
        double rad = angle * Math.PI / 180;
        double x = radius * Math.Cos(rad);
        double y = radius * Math.Sin(rad);

        Ellipse ellipse = new Ellipse
        {
          Width = 10,
          Height = 10,
          Fill = SpinnerColor
        };

        // 중심에 배치 후 TranslateTransform으로 퍼짐
        Canvas.SetLeft(ellipse, 50 - 5);
        Canvas.SetTop(ellipse, 50 - 5);

        TranslateTransform translate = new TranslateTransform(x, y);
        ellipse.RenderTransform = translate;

        dots.Children.Add(ellipse);
      }
    }
  }

}





