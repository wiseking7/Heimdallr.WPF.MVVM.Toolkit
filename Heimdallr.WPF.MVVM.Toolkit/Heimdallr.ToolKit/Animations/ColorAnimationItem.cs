using Heimdallr.ToolKit.Animations.EasingFunctions;
using System.Windows;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.Animations;

/// <summary>
/// 이 클래스는 WPF에서 색상 애니메이션을 제어하는 ColorAnimation을 상속받아
/// 애니메이션을 적용할 대상과 속성, 이징 모드 등을 설정하는 기능을 제공합니다.
/// </summary>

public class ColorAnimationItem : ColorAnimation
{
  #region TargetName
  /// <summary>
  /// TargetName 속성은 애니메이션을 적용할 대상의 이름을 지정합니다.
  /// 이 속성이 변경될 때, 애니메이션의 적용 대상이 업데이트됩니다.
  /// </summary>
  public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register(
      "TargetName",                          // 속성 이름
      typeof(string),                        // 속성 타입
      typeof(ColorAnimationItem),                     // 속성이 속한 클래스
      new PropertyMetadata(null, OnTargetNameChanged)  // 기본값과 속성 변경 시 호출될 콜백 함수
  );

  /// <summary>
  /// TargetName 속성의 값을 가져오거나 설정합니다.
  /// </summary>
  public string TargetName
  {
    get { return (string)GetValue(TargetNameProperty); }
    set { SetValue(TargetNameProperty, value); }
  }
  #endregion

  #region Property
  /// <summary>
  /// Property 속성은 애니메이션이 적용될 속성을 지정합니다.
  /// Storyboard.SetTargetProperty를 사용하여 해당 속성을 업데이트합니다.
  /// </summary>
  public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register(
      "Property",                          // 속성 이름
      typeof(PropertyPath),                 // 속성 타입
      typeof(ColorAnimationItem),                    // 속성이 속한 클래스
      new PropertyMetadata(null, OnPropertyChanged)  // 기본값과 속성 변경 시 호출될 콜백 함수
  );

  /// <summary>
  /// Property 속성의 값을 가져오거나 설정합니다.
  /// </summary>
  public PropertyPath Property
  {
    get { return (PropertyPath)GetValue(PropertyProperty); }
    set { SetValue(PropertyProperty, value); }
  }
  #endregion

  // TargetName 속성이 변경될 때 호출되는 콜백 함수
  private static void OnTargetNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ColorAnimationItem)d;
    var targetName = (string)e.NewValue;

    // Storyboard에 애니메이션의 대상 이름을 설정
    Storyboard.SetTargetName(item, targetName);
  }

  // Property 속성이 변경될 때 호출되는 콜백 함수
  private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ColorAnimationItem)d;
    var propertyPath = (PropertyPath)e.NewValue;

    // Storyboard에 애니메이션이 적용될 속성을 설정
    Storyboard.SetTargetProperty(item, propertyPath);
  }

  #region Mode
  /// <summary>
  /// Mode 속성은 애니메이션에 적용할 이징 함수의 모드를 정의합니다.
  /// 이 속성 변경 시, 해당 모드에 맞는 이징 함수가 자동으로 설정됩니다.
  /// </summary>
  public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
      "Mode",                               // 속성 이름
      typeof(EasingFunctionBaseMode),       // 속성 타입
      typeof(ColorAnimationItem),                    // 속성이 속한 클래스
      new PropertyMetadata(EasingFunctionBaseMode.CubicEaseIn, OnEasingModeChanged) // 기본값과 콜백 함수
  );

  /// <summary>
  /// Mode 속성의 값을 가져오거나 설정합니다.
  /// </summary>
  public EasingFunctionBaseMode Mode
  {
    get { return (EasingFunctionBaseMode)GetValue(ModeProperty); }
    set { SetValue(ModeProperty, value); }
  }
  #endregion

  // Mode 속성이 변경될 때 호출되는 콜백 함수
  private static void OnEasingModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ColorAnimationItem)d;
    var easingMode = (EasingFunctionBaseMode)e.NewValue;

    // 이미 CubicEase일 경우, 해당 이징 모드를 설정하고 리턴
    if (item.EasingFunction is CubicEase cubicEase)
    {
      cubicEase.EasingMode = GetMode(easingMode);
      return;
    }

    // CubicEase가 아니면, 새로운 이징 함수를 설정
    item.EasingFunction = GetEasingFunc(easingMode);
  }

  /// <summary>
  /// 이 메서드는 Mode 속성에 맞는 이징 함수를 생성하고 반환합니다.
  /// CubicEase, BackEase 등 다양한 이징 함수들이 지원됩니다.
  /// </summary>
  private static IEasingFunction GetEasingFunc(EasingFunctionBaseMode mode)
  {
    EasingMode easingMode = GetMode(mode);
    EasingFunctionBase easingFunctionBase = GetFunctonBase(mode);

    // 선택된 이징 함수의 EasingMode를 설정
    easingFunctionBase.EasingMode = easingMode;

    return (IEasingFunction)easingFunctionBase;
  }

  /// <summary>
  /// EasingFunctionBaseMode에 해당하는 이징 함수 객체를 반환합니다.
  /// </summary>
  private static EasingFunctionBase GetFunctonBase(EasingFunctionBaseMode mode)
  {
    // Mode 값에 따른 적합한 이징 함수 반환
    var enumString = mode.ToString()
        .Replace("EaseInOut", "")
        .Replace("EaseIn", "")
        .Replace("EaseOut", "");

    switch (enumString)
    {
      case "Back":
        return new BackEase();
      case "Bounce":
        return new BounceEase();
      case "Circle":
        return new CircleEase();
      case "Cubic":
        return new CubicEase();
      case "Elastic":
        return new ElasticEase();
      case "Exponential":
        return new ExponentialEase();
      case "Power":
        return new PowerEase();
      case "Quadratic":
        return new QuadraticEase();
      case "Quartic":
        return new QuarticEase();
      case "Quintic":
        return new QuinticEase();
      case "Sine":
        return new SineEase();
      default:
        // 기본값은 CubicEase
        return new CubicEase();
    }
  }

  /// <summary>
  /// EasingFunctionBaseMode에 따라 EasingMode를 설정합니다.
  /// </summary>
  private static EasingMode GetMode(EasingFunctionBaseMode mode)
  {
    var enumString = mode.ToString();

    if (enumString.Contains("EaseInOut"))
      return EasingMode.EaseInOut;
    else if (enumString.Contains("EaseIn"))
      return EasingMode.EaseIn;

    return EasingMode.EaseOut;
  }
}

