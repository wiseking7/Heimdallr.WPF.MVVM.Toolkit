using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// LoadingOverlay: 콘텐츠 위에 로딩 마스크 + Indicator + 텍스트를 표시하는 오버레이 컨트롤.
/// ContentControl을 상속하여 ContentPresenter와 함께 로딩용 UI를 중첩시킴.
/// VisualStateManager를 이용해 "Visible" / "Hidden" 상태를 전환.
/// </summary>
public class LoadingOverlay : ContentControl
{
  // 테마 파일에 정의된 기본 스타일을 이 컨트롤에 연결 (Generic.xaml 내 스타일을 찾음)
  static LoadingOverlay()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingOverlay),
      new FrameworkPropertyMetadata(typeof(LoadingOverlay)));
  }

  #region IsLoading
  /// <summary>
  /// IsLoading 시 표기화면 (기본값 False, True)
  /// </summary>
  public bool IsLoading
  {
    get => (bool)GetValue(IsLoadingProperty);
    set => SetValue(IsLoadingProperty, value);
  }

  /// <summary>
  /// 로딩 여부를 나타내는 의존성 속성.
  /// true = 로딩 상태(오버레이 표시), false = 로딩 아님(숨김)
  /// </summary>
  public static readonly DependencyProperty IsLoadingProperty =
  DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(LoadingOverlay),
    new PropertyMetadata(false, OnIsLoadingPropertyChanged));

  /// <summary>
  /// IsLoading 속성 변경 콜백.
  /// VisualStateManager를 통해 Visible / Hidden 상태로 전환.
  /// </summary>
  private static void OnIsLoadingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var control = (LoadingOverlay)d;
    bool isLoading = (bool)e.NewValue;

    // 상태 전환: useTransitions = true (기본)
    control.ChangeVisualState(isLoading);
  }

  /// <summary>
  /// VisualStateManager를 이용해 상태를 전환하는 메서드.
  /// Visible = 오버레이 표시 / Hidden = 오버레이 숨김
  /// </summary>
  /// <param name="isVisible">표시 여부</param>
  /// <param name="useTransitions">전환 애니메이션 사용 여부 (기본값 true)</param>
  private void ChangeVisualState(bool isVisible, bool useTransitions = true)
  {
    // VisualStateManager가 ControlTemplate 내의 상태 그룹을 찾아 상태 전환
    // 상태 그룹명이 정확히 Template에 존재해야 정상 작동
    // ex: <VisualStateGroup x:Name="VisibilityStates">
    VisualStateManager.GoToState(this, isVisible ? "Visible" : "Hidden", useTransitions);
  }
  #endregion

  #region
  /// <summary>
  /// Loading 시 문자열 표기
  /// </summary>
  public string LoadingText
  {
    get => (string)GetValue(LoadingTextProperty);
    set => SetValue(LoadingTextProperty, value);
  }

  /// <summary>
  /// 로딩 메시지 텍스트 (기본값: "Loading...")
  /// </summary>
  public static readonly DependencyProperty LoadingTextProperty =
    DependencyProperty.Register(nameof(LoadingText), typeof(string),
        typeof(LoadingOverlay), new PropertyMetadata("Loading..."));
  #endregion

  #region IndicatorType 열거형 가져오기
  /// <summary>
  /// IndicatorType 열거형 속성
  /// </summary>
  public IndicatorType IndicatorType
  {
    get => (IndicatorType)GetValue(IndicatorTypeProperty);
    set => SetValue(IndicatorTypeProperty, value);
  }

  /// <summary>
  /// IndicatorType: Indicator 컨트롤의 스타일 유형을 나타냄.
  /// ex: Ring, Bar, Cogs 등 IndicatorType 열거형 값
  /// </summary>
  public static readonly DependencyProperty IndicatorTypeProperty =
    DependencyProperty.Register(nameof(IndicatorType), typeof(IndicatorType),
        typeof(LoadingOverlay), new PropertyMetadata(IndicatorType.None));
  #endregion

  /// <summary>
  /// ControlTemplate 이 적용된 이후 초기 VisualState를 설정.
  /// OnApplyTemplate은 WPF가 컨트롤의 비주얼 트리를 준비한 직후 호출됨.
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    // 초기 상태를 IsLoading 값에 맞게 설정(첫 로드 시 깜빡임 방지)
    // VSM 상태 그룹이 존재하지 않을 경우 예외가 발생할 수 있으므로
    // 필요시 try-catch나 상태 존재 여부 체크도 고려 가능
    ChangeVisualState(IsLoading, false); // 초기에는 트랜지션 없이 즉시 적용
  }
}
