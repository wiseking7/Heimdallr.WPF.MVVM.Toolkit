using Heimdallr.ToolKit.Animations.EasingFunctions;
using System.Windows;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.Animations;

/// <summary>
/// DoubleAnimation을 상속받아, 더 정교한 애니메이션 제어가 가능한 사용자 정의 클래스입니다.
/// 주로 WPF에서 애니메이션을 제어하고, Storyboard에서 사용할 수 있도록 대상 속성, 대상 이름, 이징 함수 등을 설정합니다.
/// </summary>
public class DoubleAnimationItem : DoubleAnimation
{
  #region TargetName 속성

  /// <summary>
  /// TargetName 속성은 애니메이션이 적용될 UI 요소의 이름을 설정합니다.
  /// </summary>
  public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register(
      "TargetName",
      typeof(string),
      typeof(DoubleAnimationItem),
      new PropertyMetadata(null, OnTargetNameChanged)
  );

  /// <summary>
  /// TargetName 속성은 애니메이션의 대상 UI 요소의 이름을 설정합니다.
  /// </summary>
  public string TargetName
  {
    get { return (string)GetValue(TargetNameProperty); }
    set { SetValue(TargetNameProperty, value); }
  }
  #endregion

  #region Property 속성

  /// <summary>
  /// Property 속성은 애니메이션이 적용될 대상의 속성 경로를 설정합니다.
  /// </summary>
  public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register(
      "Property",
      typeof(PropertyPath),
      typeof(DoubleAnimationItem),
      new PropertyMetadata(null, OnPropertyChanged)
  );

  /// <summary>
  /// Property 속성은 애니메이션이 적용될 대상의 속성 경로를 설정합니다.
  /// </summary>
  public PropertyPath Property
  {
    get { return (PropertyPath)GetValue(PropertyProperty); }
    set { SetValue(PropertyProperty, value); }
  }
  #endregion

  #region Mode 속성 (이징 함수 설정)

  /// <summary>
  /// Mode 속성은 애니메이션의 진행 방식을 제어하는 이징 함수의 모드를 설정합니다.
  /// </summary>
  public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
      "Mode",
      typeof(EasingFunctionBaseMode),
      typeof(DoubleAnimationItem),
      new PropertyMetadata(EasingFunctionBaseMode.CubicEaseIn, OnEasingModeChanged)
  );

  /// <summary>
  /// Mode 속성은 애니메이션의 진행 방식을 제어하는 이징 함수의 모드를 설정합니다.
  /// </summary>
  public EasingFunctionBaseMode Mode
  {
    get { return (EasingFunctionBaseMode)GetValue(ModeProperty); }
    set { SetValue(ModeProperty, value); }
  }
  #endregion

  #region 메서드들

  /// <summary>
  /// TargetName 속성이 변경되면 호출되는 콜백 메서드입니다.
  /// </summary>
  private static void OnTargetNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (DoubleAnimationItem)d;
    var targetName = (string)e.NewValue;

    // Storyboard의 대상 이름을 설정합니다.
    Storyboard.SetTargetName(item, targetName);
  }

  /// <summary>
  /// Property 속성이 변경되면 호출되는 콜백 메서드입니다.
  /// </summary>
  private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (DoubleAnimationItem)d;
    var propertyPath = (PropertyPath)e.NewValue;

    // Storyboard의 대상 속성을 설정합니다.
    Storyboard.SetTargetProperty(item, propertyPath);
  }

  /// <summary>
  /// Mode 속성이 변경되면 호출되는 콜백 메서드입니다.
  /// 이 메서드는 적절한 이징 함수 객체를 생성하고, 해당 이징 함수의 모드를 설정합니다.
  /// </summary>
  private static void OnEasingModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (DoubleAnimationItem)d;
    var easingMode = (EasingFunctionBaseMode)e.NewValue;

    // 기존 EasingFunction이 CubicEase인 경우, EasingMode만 변경
    if (item.EasingFunction is CubicEase cubicEase)
    {
      cubicEase.EasingMode = GetMode(easingMode);
      return;
    }

    // CubicEase 이외의 EasingFunction을 설정
    item.EasingFunction = GetEasingFunc(easingMode);
  }

  /// <summary>
  /// EasingFunctionBaseMode에 해당하는 EasingFunction 객체를 생성합니다.
  /// </summary>
  private static IEasingFunction GetEasingFunc(EasingFunctionBaseMode mode)
  {
    // EasingMode를 설정
    EasingMode easingMode = GetMode(mode);
    // 이징 함수 객체를 생성
    EasingFunctionBase easingFunctionBase = GetFunctonBase(mode);
    easingFunctionBase.EasingMode = easingMode;

    return easingFunctionBase;
  }

  /// <summary>
  /// EasingFunctionBaseMode에 해당하는 기본 이징 함수 객체를 반환합니다.
  /// </summary>
  private static EasingFunctionBase GetFunctonBase(EasingFunctionBaseMode mode)
  {
    // Enum 값에서 'EaseInOut', 'EaseIn', 'EaseOut'을 제외한 부분을 사용
    var enumString = mode.ToString()
        .Replace("EaseInOut", "")
        .Replace("EaseIn", "")
        .Replace("EaseOut", "");

    // 각 이징 함수에 해당하는 객체를 반환
    switch (enumString)
    {
      case "Back": return new BackEase();
      case "Bounce": return new BounceEase();
      case "Circle": return new CircleEase();
      case "Cubic": return new CubicEase();
      case "Elastic": return new ElasticEase();
      case "Exponential": return new ExponentialEase();
      case "Power": return new PowerEase();
      case "Quadratic": return new QuadraticEase();
      case "Quartic": return new QuarticEase();
      case "Quintic": return new QuinticEase();
      case "Sine": return new SineEase();
      default:
        // 예기치 않은 값이 들어왔을 경우, 기본값으로 CubicEase 반환
        var defaultFunction = new CubicEase();
        throw new ArgumentOutOfRangeException($"예기치 않은 EasingFunctionBaseMode: {mode}. 기본값을 반환합니다: {defaultFunction.GetType().Name}");
    }
  }

  /// <summary>
  /// EasingFunctionBaseMode에 해당하는 EasingMode를 반환합니다.
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

  #endregion
}
