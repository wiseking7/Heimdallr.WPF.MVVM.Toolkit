using System.Windows;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// EscaladeHelper 클래스: Escalade.xaml 개체의 StrokeDashArray 속성을 제어하는 클래스
/// 
/// </summary>
public class EscaladeHelper
{
  /// <summary>
  /// // StrokeDashArrayValue라는 Attached Property를 정의
  /// (Path 개체에 적용됨)
  /// StrokeDashArrayValue는 Path의 StrokeDashArray
  /// 값을 제어하기 위한 속성입니다.
  /// </summary>
  public static readonly DependencyProperty StrokeDashArrayValueProperty =
      DependencyProperty.RegisterAttached("StrokeDashArrayValue",
          typeof(double), typeof(EscaladeHelper),

          new PropertyMetadata(0.0, OnStrokeDashArrayValueChanged));

  /// <summary>
  /// Path 객체에서 StrokeDashArrayValue 속성 값을 가져오는 메서드
  /// </summary>
  /// <param name="path"></param>
  /// <returns></returns>
  public static double GetStrokeDashArrayValue(System.Windows.Shapes.Path path)
  {
    // GetValue 메서드를 통해 Path 객체에 설정된 StrokeDashArrayValue 값을 반환
    return (double)path.GetValue(StrokeDashArrayValueProperty);
  }

  /// <summary>
  /// Path 객체에 StrokeDashArrayValue 속성 값을 설정하는 메서드
  /// </summary>
  /// <param name="path"></param>
  /// <param name="value"></param>
  public static void SetStrokeDashArrayValue
    (System.Windows.Shapes.Path path, double value)
  {
    // SetValue 메서드를 사용하여 Path 객체에 StrokeDashArrayValue 값을 설정
    path.SetValue(StrokeDashArrayValueProperty, value);
  }

  /// <summary>
  /// 값 변경
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnStrokeDashArrayValueChanged
    (DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // d가 Path 객체인지 확인
    if (d is System.Windows.Shapes.Path path)
    {
      // 새로운 값 (double)으로 변환
      var value = (double)e.NewValue;

      // Path의 StrokeDashArray 속성 값 설정
      // StrokeDashArray는 `DoubleCollection`
      // 타입이므로 새로운 값을 두 개의 값으로 설정
      // 첫 번째 값은 새로 설정된 값, 두 번째 값은 고정된 10으로 설정됨
      path.StrokeDashArray = new DoubleCollection() { value, 10 };
    }
  }
}

