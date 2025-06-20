using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Indicator 컨트롤은 로딩 상태, 진행 상태 등의 단순한 시각적 지표를 제공하는 사용자 정의 컨트롤입니다.
/// VisualStateManager를 사용하여 IsActive 상태에 따라 "Active" / "Inactive" 상태로 전환됩니다.
/// IndicatorType 및 Foreground 등 다양한 속성을 통해 스타일과 동작을 제어할 수 있습니다.
/// </summary>
public class Indicator : Control
{
  static Indicator()
  {
    // Indicator 컨트롤이 로드될 때 이 컨트롤의 기본 스타일을 Generic.xaml에서 가져오도록 메타데이터를 오버라이드.
    DefaultStyleKeyProperty.OverrideMetadata(typeof(Indicator),
        new FrameworkPropertyMetadata(typeof(Indicator)));
  }

  #region IsActive 활성 상태
  /// <summary>
  /// Indicator의 활성 상태 (True: Active, False: Inactive)
  /// </summary>
  public bool IsActive
  {
    get => (bool)GetValue(IsActiveProperty);
    set => SetValue(IsActiveProperty, value);
  }
  /// <summary>
  /// IsActive 속성은 Indicator가 활성 상태인지 비활성 상태인지를 나타냅니다.
  /// 활성화 여부에 따라 VisualStateManager를 통해 상태 전환이 일어납니다.
  /// </summary>
  public static readonly DependencyProperty IsActiveProperty =
      DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(Indicator),
          new PropertyMetadata(false, OnIsActiveChanged));

  /// <summary>
  /// IsActive 속성 값이 변경될 때 호출됩니다.
  /// VisualStateManager.GoToState를 통해 "Active" 또는 "Inactive" 상태로 전환합니다.
  /// </summary>
  private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var control = (Indicator)d;
    VisualStateManager.GoToState(control, control.IsActive ? "Active" : "Inactive", true);
  }
  #endregion

  #region
  /// <summary>
  /// Indicator 종류를 가져오거나 설정합니다.
  /// </summary>
  public IndicatorType IndicatorType
  {
    get => (IndicatorType)GetValue(IndicatorTypeProperty);
    set => SetValue(IndicatorTypeProperty, value);
  }
  /// <summary>
  /// IndicatorType 속성은 Indicator의 종류(예: Spinner, Bar 등)를 정의합니다.
  /// Template에서 IndicatorType에 따라 다른 시각적 요소를 제공하도록 바인딩됩니다.
  /// </summary>
  public static readonly DependencyProperty IndicatorTypeProperty =
      DependencyProperty.Register(nameof(IndicatorType), typeof(IndicatorType), typeof(Indicator),
          new PropertyMetadata(IndicatorType.None));
  #endregion

  #region Foreground 로 색상가져오기
  /// <summary>
  /// Indicator 내부 시각 요소(Ellipse, Rectangle 등)에 색상을 제공하는 Foreground 속성.
  /// Foreground는 XAML에서 Brush를 지정할 수 있으며, 부모 컨트롤의 Foreground 상속도 지원됩니다.
  /// </summary>
  public new Brush Foreground
  {
    get => (Brush)GetValue(ForegroundProperty);
    set => SetValue(ForegroundProperty, value);
  }
  /// 아래 참고																																	
  public new static readonly DependencyProperty ForegroundProperty =
    TextElement.ForegroundProperty.AddOwner(typeof(Indicator),
      new FrameworkPropertyMetadata(Brushes.Gray,
        FrameworkPropertyMetadataOptions.AffectsRender));
  #endregion

  /// <summary>
  /// 템플릿이 적용될 때 VisualStateManager 상태를 초기화합니다.
  /// 컨트롤이 로드될 때 IsActive 값에 따라 "Active" 또는 "Inactive" 상태로 전환됩니다.
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    // OnApplyTemplate 시 초기 상태로 VSM(Visual State Manager) 상태를 설정
    VisualStateManager.GoToState(this, IsActive ? "Active" : "Inactive", true);
  }
}

/*																																		
/// TextElement.ForegroundProperty 는 Foreground 는 주로 텍스트 색상을 나타냅니다.																																		
/// 예를 TextBlock, Label 같은 곳에서 Foreground="Red" 하면 글자가 빨간색이 됩니다.																																		
/// 그런데 Indicator 는 텍스트 요소가 아님에도 Foreground 를 상속해서 																																		
/// Brush(색상/그라데이션/패턴 등) 속성으로 재 사용하려고 한 것입니다. 																																		
/// 즉 Indicator 의 내부 요소(Ellipse, Rectangle 등)의 색상을 바꾸는데 글자색 를 																																		
/// 의미상 재활용																																		
/// AddOwner -> Indicator 도 이제 ForegroundProperty 의 소유자중 하나다																																		
/// 즉 XAML 에서 <Indicator Foreground="Red"/> 같은 문법이 유효해짐																																		
/// 기존 TextElement.,Foreground 와 같은 속성 상속 기능도 받을 수 있음																																		
/// AddOwner → 속성 재사용 + 스타일/바인딩/XAML 지원.																																		
/// Brushes.Gray 는 이 ForegroundProperty 의 기본값 입니다.																																		
/// WPF 에서 Brush 는 단순 색상(Red, Green) 분 아니라, SolidColorBrush(단색), 																																		
/// LinearGradientBrush (그라데이션), VisualBrush (비주얼 요소), 																																		
/// ImageBrush(이미지)도 가능합니다.																																		

/// FrameworkPropertyMetadataOptions.AffectsRender -> Foreground 값이 변경되면 																																		
/// indicator 를 다시 그리게 한다는 의미입니다.																																		
/// AffectsRender 옵션 효과 -> Foreground 속성이 바뀌면 WPF 렌더링 																																		
/// InvalidateVisual()을 내부적으로 호출합니다.																																		
/// 이로인해, OnRender -> 내부 시각적 요소 다시 그려짐, 템플릿 바인딩 된 Brush 도 																																		
/// 색상으로 업데이트																																		

/// new -> Control(또는 상위 클래스)에서 이미 Foreground 가 정의돼있는데 																																		
/// Indicator 에서 다시 정의하므로 숨김(new)을 사용																																		
/// 이로 인해 XAML에서는 Indicator 자체의 Foreground가 컨트롤 수준 																																		
/// Foreground로 동작 																																		
/// FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits ->																																		
/// Inherits를 추가하면 부모 컨트롤에서 Foreground 설정 시 Indicator에 자동 적용됩니다																																		
*/  