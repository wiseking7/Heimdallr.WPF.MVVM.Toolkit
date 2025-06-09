using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Heimdallr.ToolKit.Indicators;

// EllipseHelper 클래스: Ellipse 컨트롤의 StrokeDashArray 속성을 제어하는
// 도우미 클래스
public class EllipseHelper
{
  // StrokeDashArrayValue라는 Attached Property를 정의
  // (Ellipse 개체에 적용됨)
  // StrokeDashArrayValue는 Ellipse의 StrokeDashArray
  // 값을 제어하기 위한 속성입니다.
  // RegisterAttached -> UIElement 계층 구조에서 부모 요소에서 자식 요소로 속성을 "전달"하거나 "부착"할 때 사용
  public static readonly DependencyProperty StrokeDashArrayValueProperty =
      DependencyProperty.RegisterAttached("StrokeDashArrayValue",
          typeof(double), typeof(EllipseHelper),
          new PropertyMetadata(0.0, OnStrokeDashArrayValueChanged));

  // Ellipse 객체에서 StrokeDashArrayValue 속성 값을 가져오는 메서드
  public static double GetStrokeDashArrayValue(Ellipse ellipse)
  {
    // GetValue 메서드를 통해 Ellipse 개체에 설정된
    // StrokeDashArrayValue 값을 반환
    return (double)ellipse.GetValue(StrokeDashArrayValueProperty);
  }

  // Ellipse 개체에 StrokeDashArrayValue 속성 값을 설정하는 메서드
  public static void SetStrokeDashArrayValue(Ellipse ellipse, double value)
  {
    // SetValue 메서드를 사용하여 Ellipse 객체에 StrokeDashArrayValue 값을 설정
    ellipse.SetValue(StrokeDashArrayValueProperty, value);
  }

  // StrokeDashArrayValue 속성 값이 변경될 때 호출되는 콜백 메서드
  private static void OnStrokeDashArrayValueChanged
    (DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // d 가 Ellipse 개체인지 확인
    if (d is Ellipse ellipse)
    {
      // 새로운 값 (double)으로 변환
      var value = (double)e.NewValue;

      // Ellipse의 StrokeDashArray 속성 값 설정
      // StrokeDashArray는 `DoubleCollection` 타입이므로 새로운 값을 두 개의 값으로 설정
      // 첫 번째 값은 새로 설정된 값, 두 번째 값은 고정된 100으로 설정됨
      ellipse.StrokeDashArray = new DoubleCollection()
      {
        value, 100
      };
    }
  }
}
/* 사용방법
  EllipseHelper 클래스를 사용하려면 Ellipse 객체에서 StrokeDashArrayValue 속성에 값을 설정하거나 가져올 수 있습니다. 
  예를 들어, XAML 또는 코드에서 Ellipse 객체에 StrokeDashArrayValue 값을 설정하는 방식은 다음과 같습니다.

  XAML에서 사용
  먼저 EllipseHelper 클래스를 사용하려면 해당 클래스를 XAML에서 xmlns로 포함시켜야 합니다. 예를 들어:
<Window x:Class="YourNamespace.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:YourNamespace"
        Title="MainWindow" Height="350" Width="525">
    <Grid>
        <!-- Ellipse 객체에 StrokeDashArrayValue를 설정 -->
        <Ellipse Width="100" Height="100" Stroke="Black" StrokeThickness="5"
                 local:EllipseHelper.StrokeDashArrayValue="20"/>
    </Grid>
</Window>
 
 */

