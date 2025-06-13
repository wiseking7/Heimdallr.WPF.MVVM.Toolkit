using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Heimdallr.ToolKit.Extensions;

/// <summary>
/// Background, Foreground, BorderBrush, Fill 프로퍼티에 대해
/// 색상 변경 시 부드러운 애니메이션을 적용하는 확장 클래스입니다.
/// </summary>
public static class AnimationExtensions
{
  /// <summary>
  /// 1. AnimatedBackground 부착 속성 등록, UIElement 에 부착할 수 있는 Brush 타입의 속성
  /// 값이 변경될 때 애니메이션을 실행
  /// </summary>
  public static readonly DependencyProperty AnimatedBackgroundProperty =
      DependencyProperty.RegisterAttached(
          "AnimatedBackground",           // 속성 이름
          typeof(Brush),                  // 속성 타입 (Brush)
          typeof(AnimationExtensions),   // 속성 소유자 타입
          new PropertyMetadata(null, OnAnimatedBackgroundChanged));  // 기본값 및 변경 콜백

  /// <summary>
  /// AnimatedBackground 부착 속성 Setter 메서드
  /// </summary>
  /// <param name="element"></param>
  /// <param name="value"></param>
  public static void SetAnimatedBackground(UIElement element, Brush value)
  {
    element.SetValue(AnimatedBackgroundProperty, value);
  }

  /// <summary>
  /// AnimatedBackground 부착 속성 Getter 메서드
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public static Brush GetAnimatedBackground(UIElement element)
  {
    return (Brush)element.GetValue(AnimatedBackgroundProperty);
  }


  /// <summary>
  /// 2. AnimatedForeground 부착 속성 등록 (Foreground 속성용)
  /// </summary>
  public static readonly DependencyProperty AnimatedForegroundProperty =
      DependencyProperty.RegisterAttached(
          "AnimatedForeground",
          typeof(Brush),
          typeof(AnimationExtensions),
          new PropertyMetadata(null, OnAnimatedForegroundChanged));

  /// <summary>
  /// AnimatedForeground Setter
  /// </summary>
  /// <param name="element"></param>
  /// <param name="value"></param>
  public static void SetAnimatedForeground(UIElement element, Brush value)
  {
    element.SetValue(AnimatedForegroundProperty, value);
  }

  /// <summary>
  /// AnimatedForeground Getter
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public static Brush GetAnimatedForeground(UIElement element)
  {
    return (Brush)element.GetValue(AnimatedForegroundProperty);
  }

  /// <summary>
  /// 3. AnimatedBackground 값이 바뀔 때 호출되는 콜백 메서드
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnAnimatedBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // 변경된 값이 SolidColorBrush 타입인지 확인
    if (e.NewValue is SolidColorBrush newBrush)
    {
      // 대상 객체(d)의 Background 프로퍼티에 대해 색상 애니메이션 수행
      AnimateColor(d, newBrush, Control.BackgroundProperty);
    }
  }

  /// <summary>
  /// 4. AnimatedForeground 값이 바뀔 때 호출되는 콜백 메서드
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnAnimatedForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (e.NewValue is SolidColorBrush newBrush)
    {
      // 대상 객체의 Foreground 프로퍼티에 대해 색상 애니메이션 수행
      AnimateColor(d, newBrush, Control.ForegroundProperty);
    }
  }


  /// <summary>
  /// 5. AnimatedBorderBrush 부착 속성 등록 (BorderBrush 용)
  /// </summary>
  public static readonly DependencyProperty AnimatedBorderBrushProperty =
      DependencyProperty.RegisterAttached(
          "AnimatedBorderBrush",
          typeof(Brush),
          typeof(AnimationExtensions),
          new PropertyMetadata(null, OnAnimatedBorderBrushChanged));

  /// <summary>
  /// AnimatedBorderBrush Setter
  /// </summary>
  /// <param name="element"></param>
  /// <param name="value"></param>
  public static void SetAnimatedBorderBrush(UIElement element, Brush value)
  {
    element.SetValue(AnimatedBorderBrushProperty, value);
  }

  /// <summary>
  /// AnimatedBorderBrush Getter
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public static Brush GetAnimatedBorderBrush(UIElement element)
  {
    return (Brush)element.GetValue(AnimatedBorderBrushProperty);
  }

  /// <summary>
  /// 6. AnimatedBorderBrush 값 변경 콜백
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnAnimatedBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // 대상이 Border 타입인지 확인, 그리고 새 값이 SolidColorBrush인지 확인
    if (d is Border && e.NewValue is SolidColorBrush newBrush)
    {
      // BorderBrush 프로퍼티에 대해 색상 애니메이션 수행
      AnimateColor(d, newBrush, Border.BorderBrushProperty);
    }
  }


  /// <summary>
  /// 7. AnimatedFill 부착 속성 등록 (Shape.Fill용)
  /// </summary>
  public static readonly DependencyProperty AnimatedFillProperty =
      DependencyProperty.RegisterAttached(
          "AnimatedFill",
          typeof(Brush),
          typeof(AnimationExtensions),
          new PropertyMetadata(null, OnAnimatedFillChanged));

  /// <summary>
  /// AnimatedFill Setter
  /// </summary>
  /// <param name="element"></param>
  /// <param name="value"></param>
  public static void SetAnimatedFill(UIElement element, Brush value)
  {
    element.SetValue(AnimatedFillProperty, value);
  }

  /// <summary>
  /// AnimatedFill Getter
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public static Brush GetAnimatedFill(UIElement element)
  {
    return (Brush)element.GetValue(AnimatedFillProperty);
  }

  /// <summary>
  /// 8. AnimatedFill 값 변경 콜백
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnAnimatedFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // 대상이 Shape 타입이며, 새 값이 SolidColorBrush인지 확인
    if (d is Shape && e.NewValue is SolidColorBrush newBrush)
    {
      // Fill 프로퍼티에 대해 색상 애니메이션 수행
      AnimateColor(d, newBrush, Shape.FillProperty);
    }
  }


  /// <summary>
  /// 실제 색상 애니메이션을 수행하는 메서드
  /// </summary>
  /// <param name="target">애니메이션을 적용할 대상 객체</param>
  /// <param name="newBrush">새로 변경할 SolidColorBrush</param>
  /// <param name="property">애니메이션 대상 DependencyProperty (Background, Foreground 등)</param>
  private static void AnimateColor(DependencyObject target, SolidColorBrush newBrush, DependencyProperty property)
  {
    // target, newBrush가 null이 아닌 경우에만 실행
    if (target != null && newBrush != null)
    {
      // 현재 대상의 해당 프로퍼티 값을 가져옴 (SolidColorBrush)
      var currentBrush = target.GetValue(property) as SolidColorBrush ?? new SolidColorBrush();

      // 색상 애니메이션 객체 생성
      var animation = new ColorAnimation
      {
        From = currentBrush.Color,               // 시작 색상: 현재 색상
        To = newBrush.Color,                      // 끝 색상: 새로 지정된 색상
        Duration = TimeSpan.FromSeconds(1),      // 애니메이션 시간 1초
        EasingFunction = new CubicEase           // 부드러운 이징 함수 적용 (EaseInOut)
        {
          EasingMode = EasingMode.EaseInOut
        }
      };

      // 새 SolidColorBrush 인스턴스를 만들어서 애니메이션을 적용할 준비를 함
      var animatedBrush = new SolidColorBrush(currentBrush.Color);

      // 대상 객체의 해당 프로퍼티를 새 브러시로 설정 (애니메이션을 적용하려면 새 브러시 필요)
      target.SetValue(property, animatedBrush);

      // 새 브러시의 Color 속성에 대해 애니메이션 시작
      animatedBrush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
    }
  }
}

