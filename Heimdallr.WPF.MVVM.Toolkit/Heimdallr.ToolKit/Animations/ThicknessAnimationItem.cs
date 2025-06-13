using Heimdallr.ToolKit.Animations.EasingFunctions;
using System.Windows;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.Animations;

/// <summary>
/// WPF에서 Margin, Padding, BorderThickness 등과 같은 Thickness 타입의 속성에 애니메이션을 적용할 때 사용하는
/// 사용자 정의 클래스입니다. 
/// 기본 제공되는 ThicknessAnimation을 상속하여, 
/// 애니메이션을 적용할 대상 요소 이름(TargetName), 속성 경로(Property), 
/// 그리고 다양한 이징 함수(EasingFunctionBaseMode)를 지정할 수 있도록 확장했습니다.
/// </summary>
public class ThicknessAnimationItem : ThicknessAnimation
{
  #region TargetName - 애니메이션이 적용될 대상 요소의 이름 지정

  /// <summary>
  /// TargetName 속성에 대한 종속성 속성 등록입니다.
  /// 애니메이션이 적용될 대상 요소의 이름을 문자열로 지정합니다.
  /// WPF Storyboard에서 대상 요소를 식별할 때 사용됩니다.
  /// </summary>
  public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register(
      nameof(TargetName),
      typeof(string),
      typeof(ThicknessAnimationItem),
      new PropertyMetadata(null, OnTargetNameChanged)
  );

  /// <summary>
  /// 애니메이션 적용 대상 요소의 이름을 가져오거나 설정합니다.
  /// 이름이 변경되면 Storyboard에 해당 이름을 등록해줍니다.
  /// </summary>
  public string TargetName
  {
    get => (string)GetValue(TargetNameProperty);
    set => SetValue(TargetNameProperty, value);
  }

  /// <summary>
  /// TargetName 변경 시 호출되는 콜백 메서드
  /// 새 이름이 null이거나 빈 문자열이면 예외를 발생시키며,
  /// 정상적인 값이면 Storyboard에 대상 이름으로 등록합니다.
  /// </summary>
  private static void OnTargetNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ThicknessAnimationItem)d;
    var newName = e.NewValue as string;

    if (string.IsNullOrWhiteSpace(newName))
      throw new ArgumentNullException(nameof(TargetName), "TargetName은 null 또는 빈 문자열일 수 없습니다.");

    Storyboard.SetTargetName(item, newName);
  }

  #endregion

  #region Property - 애니메이션이 적용될 속성 경로 지정 (예: Margin, Padding 등)

  /// <summary>
  /// Property 속성에 대한 종속성 속성 등록입니다.
  /// 애니메이션이 적용될 대상 속성 경로를 지정합니다.
  /// 예를 들어, "(FrameworkElement.Margin)" 등이 될 수 있습니다.
  /// </summary>
  public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register(
      nameof(Property),
      typeof(PropertyPath),
      typeof(ThicknessAnimationItem),
      new PropertyMetadata(null, OnPropertyChanged)
  );

  /// <summary>
  /// 애니메이션이 적용될 대상 속성 경로를 가져오거나 설정합니다.
  /// 값이 변경되면 Storyboard에 해당 속성 경로를 등록합니다.
  /// </summary>
  public PropertyPath Property
  {
    get => (PropertyPath)GetValue(PropertyProperty);
    set => SetValue(PropertyProperty, value);
  }

  /// <summary>
  /// Property 변경 시 호출되는 콜백 메서드
  /// null 값이면 예외를 발생시키며,
  /// 정상 값이면 Storyboard에 대상 속성 경로로 등록합니다.
  /// </summary>
  private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ThicknessAnimationItem)d;
    var path = e.NewValue as PropertyPath;

    if (path == null)
      throw new ArgumentNullException(nameof(Property), "PropertyPath는 null일 수 없습니다.");

    Storyboard.SetTargetProperty(item, path);
  }

  #endregion

  #region Mode - 사용자 정의 EasingFunctionBaseMode Enum을 통해 다양한 이징 함수 설정

  /// <summary>
  /// Mode 속성에 대한 종속성 속성 등록입니다.
  /// 애니메이션의 이징 함수를 EasingFunctionBaseMode 열거형으로 지정할 수 있게 합니다.
  /// 기본값은 CubicEaseIn으로 설정되어 있습니다.
  /// </summary>
  public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
      nameof(Mode),
      typeof(EasingFunctionBaseMode),
      typeof(ThicknessAnimationItem),
      new PropertyMetadata(EasingFunctionBaseMode.CubicEaseIn, OnEasingModeChanged)
  );

  /// <summary>
  /// 애니메이션에 적용할 이징 함수 모드를 가져오거나 설정합니다.
  /// </summary>
  public EasingFunctionBaseMode Mode
  {
    get => (EasingFunctionBaseMode)GetValue(ModeProperty);
    set => SetValue(ModeProperty, value);
  }

  /// <summary>
  /// Mode 속성이 변경될 때 호출되는 콜백 메서드
  /// 새 이징 모드에 따라 EasingFunction을 새로 생성하거나 설정합니다.
  /// 기존에 CubicEase가 설정된 경우에는 EasingMode만 변경합니다.
  /// </summary>
  private static void OnEasingModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ThicknessAnimationItem)d;

    if (e.NewValue is not EasingFunctionBaseMode mode)
      throw new ArgumentNullException(nameof(Mode), "EasingFunctionBaseMode 값이 유효하지 않습니다.");

    if (item.EasingFunction is CubicEase cubicEase)
    {
      // 기존에 CubicEase가 있으면 EasingMode만 변경
      cubicEase.EasingMode = GetMode(mode);
    }
    else
    {
      // 새로운 이징 함수 생성 후 설정
      item.EasingFunction = GetEasingFunction(mode);
    }
  }

  /// <summary>
  /// EasingFunctionBaseMode에 해당하는 IEasingFunction 인스턴스를 생성합니다.
  /// 생성 후 EasingMode를 적절히 지정하여 반환합니다.
  /// </summary>
  private static IEasingFunction GetEasingFunction(EasingFunctionBaseMode mode)
  {
    var function = GetFunctionBase(mode);
    function.EasingMode = GetMode(mode);
    return function;
  }

  /// <summary>
  /// EasingFunctionBaseMode 문자열에서 "EaseIn", "EaseOut", "EaseInOut" 부분을 제거한 후,
  /// 남은 기본 이름에 따라 실제 WPF 내장 EasingFunctionBase 객체를 생성하여 반환합니다.
  /// </summary>
  private static EasingFunctionBase GetFunctionBase(EasingFunctionBaseMode mode)
  {
    string baseName = mode.ToString()
        .Replace("EaseInOut", "")
        .Replace("EaseIn", "")
        .Replace("EaseOut", "");

    return baseName switch
    {
      "Back" => new BackEase(),
      "Bounce" => new BounceEase(),
      "Circle" => new CircleEase(),
      "Cubic" => new CubicEase(),
      "Elastic" => new ElasticEase(),
      "Exponential" => new ExponentialEase(),
      "Power" => new PowerEase(),
      "Quadratic" => new QuadraticEase(),
      "Quartic" => new QuarticEase(),
      "Quintic" => new QuinticEase(),
      "Sine" => new SineEase(),
      _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, "정의되지 않은 이징 모드입니다.")
    };
  }

  /// <summary>
  /// EasingFunctionBaseMode 이름 문자열에서 이징 모드(EaseIn, EaseOut, EaseInOut)를 추출하여
  /// WPF의 EasingMode 열거형으로 변환하여 반환합니다.
  /// </summary>
  private static EasingMode GetMode(EasingFunctionBaseMode mode)
  {
    var name = mode.ToString();

    if (name.Contains("EaseInOut")) return EasingMode.EaseInOut;
    if (name.Contains("EaseIn")) return EasingMode.EaseIn;
    return EasingMode.EaseOut;
  }

  #endregion
}


