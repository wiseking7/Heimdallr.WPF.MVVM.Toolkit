using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// EllipseHelper 클래스: Ellipse.xaml 컨트롤의 StrokeDashArray 속성을 제어하는 클래스
/// </summary>
public class EllipseHelper
{
  /// <summary>
  /// StrokeDashArrayValue라는 Attached Property를 정의
  /// (Ellipse 개체에 적용됨)
  /// StrokeDashArrayValue는 Ellipse의 StrokeDashArray
  /// 값을 제어하기 위한 속성입니다.
  /// </summary>
  public static readonly DependencyProperty StrokeDashArrayValueProperty =
      DependencyProperty.RegisterAttached("StrokeDashArrayValue",
          typeof(double), typeof(EllipseHelper),
          new PropertyMetadata(0.0, OnStrokeDashArrayValueChanged));

  /// <summary>
  /// Ellipse 객체에서 StrokeDashArrayValue 속성 값을 가져오는 메서드
  /// </summary>
  /// <param name="ellipse"></param>
  /// <returns></returns>
  public static double GetStrokeDashArrayValue(Ellipse ellipse)
  {
    // GetValue 메서드를 통해 Ellipse 개체에 설정된
    // StrokeDashArrayValue 값을 반환
    return (double)ellipse.GetValue(StrokeDashArrayValueProperty);
  }

  /// <summary>
  /// Ellipse 개체에 StrokeDashArrayValue 속성 값을 설정하는 메서드
  /// </summary>
  /// <param name="ellipse"></param>
  /// <param name="value"></param>
  public static void SetStrokeDashArrayValue(Ellipse ellipse, double value)
  {
    // SetValue 메서드를 사용하여 Ellipse 객체에 StrokeDashArrayValue 값을 설정
    ellipse.SetValue(StrokeDashArrayValueProperty, value);
  }

  /// <summary>
  /// StrokeDashArrayValue 속성 값이 변경될 때 호출되는 콜백 메서드
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
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
      ellipse.StrokeDashArray = new DoubleCollection() { value, 100 };
    }
  }
}

