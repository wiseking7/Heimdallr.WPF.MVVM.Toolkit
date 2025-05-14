using Heimdallr.ToolKit.Animations.EasingFunctions;
using System.Windows;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.Animations;

/// <summary>
/// WPF에서 Margin, Padding, BorderThickness 등의 Thickness 속성에 애니메이션을 적용할 때 사용되는 사용자 정의 클래스입니다.
/// ThicknessAnimation을 상속하며, 타겟 요소 및 속성, 이징 함수(EasingFunctionBaseMode)를 통해 애니메이션의 동작 방식을 세밀하게 제어할 수 있습니다.
/// </summary>
public class ThicknessAnimationItem : ThicknessAnimation
{
  #region TargetName - 애니메이션이 적용될 대상 요소의 이름 지정

  public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register(
      nameof(TargetName),
      typeof(string),
      typeof(ThicknessAnimationItem),
      new PropertyMetadata(null, OnTargetNameChanged)
  );

  public string TargetName
  {
    get => (string)GetValue(TargetNameProperty);
    set => SetValue(TargetNameProperty, value);
  }

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

  public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register(
      nameof(Property),
      typeof(PropertyPath),
      typeof(ThicknessAnimationItem),
      new PropertyMetadata(null, OnPropertyChanged)
  );

  public PropertyPath Property
  {
    get => (PropertyPath)GetValue(PropertyProperty);
    set => SetValue(PropertyProperty, value);
  }

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

  public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
      nameof(Mode),
      typeof(EasingFunctionBaseMode),
      typeof(ThicknessAnimationItem),
      new PropertyMetadata(EasingFunctionBaseMode.CubicEaseIn, OnEasingModeChanged)
  );

  public EasingFunctionBaseMode Mode
  {
    get => (EasingFunctionBaseMode)GetValue(ModeProperty);
    set => SetValue(ModeProperty, value);
  }

  private static void OnEasingModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var item = (ThicknessAnimationItem)d;

    if (e.NewValue is not EasingFunctionBaseMode mode)
      throw new ArgumentNullException(nameof(Mode), "EasingFunctionBaseMode 값이 유효하지 않습니다.");

    if (item.EasingFunction is CubicEase cubicEase)
    {
      cubicEase.EasingMode = GetMode(mode);
    }
    else
    {
      item.EasingFunction = GetEasingFunction(mode);
    }
  }

  private static IEasingFunction GetEasingFunction(EasingFunctionBaseMode mode)
  {
    var function = GetFunctionBase(mode);
    function.EasingMode = GetMode(mode);
    return function;
  }

  /// <summary>
  /// EasingFunctionBaseMode 값을 기반으로 해당하는 EasingFunctionBase 인스턴스를 반환합니다.
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
  /// EasingFunctionBaseMode 값에서 EasingMode (EaseIn, EaseOut, EaseInOut)를 추출합니다.
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

