using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Heimdallr.App;

public class App : Application
{
  protected override void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    MainWindow window = new();

    window.Title = "HEIMDALLR";

    //Title 색상변경
    window.TitleHeaderBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF7A1CAC"));

    // 매그러운 모서리 
    //win.SnapsToDevicePixels = false;
    //win.UseLayoutRounding = true;

    // 헤더만 흐림효과 적용
    //win.Dimming = true;
    //win.DimmingOpacity = 4.0;
    //win.DimmingColor = new SolidColorBrush(Color.FromArgb(128, 20, 20, 20)); // 반투명한 검정

    // Background 변경(Content)
    var brush = new BrushConverter().ConvertFrom("#FF393E46") as Brush;
    if (brush != null)
    {
      window.Background = brush;
    }

    // Loaded 이벤트 핸들러
    window.Loaded += (s, e) =>
    {
      Debug.WriteLine($"Width: {window.ActualWidth}, Height: {window.ActualHeight}");
    };

    window.Show();
  }
}
