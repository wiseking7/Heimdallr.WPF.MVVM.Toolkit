using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 진행률 표시용 커스텀 프로그레스바 컨트롤
/// 최소값/최대값 지원, 인디터미넌트 모드, 진행률 텍스트 표시, 색상 커스터마이징 포함
/// </summary>
public class HeimdallrProgressBar : Control
{
  static HeimdallrProgressBar()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrProgressBar),
        new FrameworkPropertyMetadata(typeof(HeimdallrProgressBar)));
  }

  #region Minimum 프로퍼티 (최소 진행률 값)
  /// <summary>
  /// 진행률의 최소값 (기본 0)
  /// </summary>
  public double Minimum
  {
    get => (double)GetValue(MinimumProperty);
    set => SetValue(MinimumProperty, value);
  }
  /// <summary>
  /// 기본값 0으로 설정된 최소 진행률 값 속성입니다.
  /// </summary>
  public static readonly DependencyProperty MinimumProperty =
      DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(HeimdallrProgressBar),
          new PropertyMetadata(0d, OnMinimumMaximumChanged));
  #endregion

  #region Maximum 프로퍼티 (최대 진행률 값)
  /// <summary>
  /// 진행률의 최대값 (기본 100)
  /// </summary>
  public double Maximum
  {
    get => (double)GetValue(MaximumProperty);
    set => SetValue(MaximumProperty, value);
  }
  /// <summary>
  /// 기본값 100으로 설정된 최대 진행률 값 속성입니다.
  /// </summary>
  public static readonly DependencyProperty MaximumProperty =
      DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(HeimdallrProgressBar),
          new PropertyMetadata(100d, OnMinimumMaximumChanged));
  #endregion

  #region Value 프로퍼티 (현재 진행률 값)
  /// <summary>
  /// 현재 진행률 값 (Minimum~Maximum 범위 내)
  /// </summary>
  public double Value
  {
    get => (double)GetValue(ValueProperty);
    set
    {
      // Value가 Minimum과 Maximum 사이에 있도록 제한
      var newValue = Math.Max(Minimum, Math.Min(Maximum, value));
      SetValue(ValueProperty, newValue);
    }
  }
  /// <summary>
  /// 기본값 0으로 설정된 현재 진행률 값 속성입니다.
  /// </summary>
  public static readonly DependencyProperty ValueProperty =
      DependencyProperty.Register(nameof(Value), typeof(double), typeof(HeimdallrProgressBar),
          new PropertyMetadata(0d, OnValueChanged));
  #endregion

  #region Fill 프로퍼티 (진행률 채우기 색상)
  /// <summary>
  /// 진행률 영역을 채울 브러시 색상 (기본: DeepSkyBlue)
  /// </summary>
  public Brush Fill
  {
    get => (Brush)GetValue(FillProperty);
    set => SetValue(FillProperty, value);
  }
  /// <summary>
  /// 기본값 DeepSkyBlue로 설정된 진행률 채우기 색상 속성입니다.
  /// </summary>
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrProgressBar),
          new PropertyMetadata(Brushes.DeepSkyBlue));
  #endregion

  #region IsIndeterminate 프로퍼티 (인디터미넌트 모드 여부)
  /// <summary>
  /// 진행률 알 수 없는 경우, 무한 애니메이션 모드 활성화
  /// </summary>
  public bool IsIndeterminate
  {
    get => (bool)GetValue(IsIndeterminateProperty);
    set => SetValue(IsIndeterminateProperty, value);
  }
  /// <summary>
  /// 기본값 false로 설정된 인디터미넌트 모드 여부 속성입니다.
  /// </summary>
  public static readonly DependencyProperty IsIndeterminateProperty =
      DependencyProperty.Register(nameof(IsIndeterminate), typeof(bool), typeof(HeimdallrProgressBar),
          new PropertyMetadata(false, OnIsIndeterminateChanged));
  #endregion

  #region ShowProgressText 프로퍼티 (진행률 텍스트 표시 여부)
  /// <summary>
  /// 진행률 값을 %로 텍스트로 표시할지 여부 (기본 true)
  /// </summary>
  public bool ShowProgressText
  {
    get => (bool)GetValue(ShowProgressTextProperty);
    set => SetValue(ShowProgressTextProperty, value);
  }
  /// <summary>
  /// 기본값 true로 설정된 진행률 텍스트 표시 여부 속성입니다.
  /// </summary>
  public static readonly DependencyProperty ShowProgressTextProperty =
      DependencyProperty.Register(nameof(ShowProgressText), typeof(bool), typeof(HeimdallrProgressBar),
          new PropertyMetadata(true));
  #endregion

  #region 콜백 및 내부 처리
  private static void OnMinimumMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var control = (HeimdallrProgressBar)d;

    // Minimum/Maximum 변경 시 Value 조정 (유효 범위 유지)
    control.Value = Math.Max(control.Minimum, Math.Min(control.Maximum, control.Value));
  }

  private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var control = (HeimdallrProgressBar)d;
    control.UpdateVisuals();
  }

  private static void OnIsIndeterminateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var control = (HeimdallrProgressBar)d;
    control.UpdateVisuals();
  }
  #endregion

  #region 진행 텍스트 색상 속성
  /// <summary>
  /// 진행률 텍스트의 전경색을 지정합니다.
  /// </summary>
  public Brush ProgressTextForeground
  {
    get => (Brush)GetValue(ProgressTextForegroundProperty);
    set => SetValue(ProgressTextForegroundProperty, value);
  }
  /// <summary>
  /// 진행률 텍스트 색상에 사용할 종속성 속성입니다. 기본값은 White입니다.
  /// </summary>
  public static readonly DependencyProperty ProgressTextForegroundProperty =
      DependencyProperty.Register(nameof(ProgressTextForeground), typeof(Brush), typeof(HeimdallrProgressBar),
          new PropertyMetadata(Brushes.White));
  #endregion

  /// <summary>
  /// 템플릿 적용 시 호출됨. 애니메이션 초기화 등을 여기서 처리.
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    UpdateVisuals();
  }

  /// <summary>
  /// 진행률 및 상태에 따라 시각 요소(너비, 애니메이션 등) 갱신
  /// </summary>
  private void UpdateVisuals()
  {
    if (IsIndeterminate)
    {
      VisualStateManager.GoToState(this, "Indeterminate", true);
    }
    else
    {
      VisualStateManager.GoToState(this, "Determinate", true);
    }
  }
}


