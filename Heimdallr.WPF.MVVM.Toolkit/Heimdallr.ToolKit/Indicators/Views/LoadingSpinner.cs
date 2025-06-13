using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.Indicators;

/// <summary>
/// VisualStateManager를 사용한 UI 요소의 시각적 상태(Visible, Hidden)를 관리하는 LoadingSpinner 컨트롤 정의
/// </summary>
[TemplateVisualState(Name = VisualStates.StateHidden, GroupName = VisualStates.GroupVisibility)]
[TemplateVisualState(Name = VisualStates.StateVisible, GroupName = VisualStates.GroupVisibility)]
public class LoadingSpinner : ContentControl
{
  #region Spinner 활성 상태(IsLoading)
  /// <summary>
  /// IsLoading
  /// </summary>
  [Category(nameof(Heimdallr.ToolKit.Indicators))]
  [Description("Spinner 가 사용중인지 여부를 가져오거나 설정합니다")]
  public bool IsLoading
  {
    get => (bool)GetValue(IsLoadingProperty);
    set => SetValue(IsLoadingProperty, value);
  }

  /// <summary>
  /// Spinner 활성 상태를 나타내는 DependencyProperty
  /// 값이 변경되면 VisualState 및 포커스 상태를 업데이트함
  /// </summary>
  public static readonly DependencyProperty IsLoadingProperty =
      DependencyProperty.Register(
        nameof(IsLoading),
        typeof(bool),
        typeof(LoadingSpinner),
        new PropertyMetadata(false, OnIsLoadingChanged));

  #endregion

  #region Spinner 메시지 (LoadingContent)
  /// <summary>
  /// LoadingContent
  /// </summary>
  [Category(nameof(Heimdallr.ToolKit.Indicators))]
  [Description("대기 메시지와 같은 Spinner 내용을 가져오거나 설정합니다")]
  public string? LoadingContent
  {
    get => (string)GetValue(LoadingContentProperty);
    set => SetValue(LoadingContentProperty, value);
  }

  /// <summary>
  /// Spinner 상태 메시지를 나타내는 DependencyProperty
  /// 기본 메시지는 "기다려 주세요..."
  /// </summary>
  public static readonly DependencyProperty LoadingContentProperty =
    DependencyProperty.Register(
      nameof(LoadingContent),
      typeof(string),
      typeof(LoadingSpinner),
      new PropertyMetadata("기다려 주세요..."));

  #endregion

  #region Spinner Content의 마진 (LoadingContentMargin)
  /// <summary>
  /// LoadingContentMargin
  /// </summary>
  [Category(nameof(Heimdallr.ToolKit.Indicators))]
  [Description("Spinner Content Margin 을 가져오거나 설정합니다.")]
  public Thickness LoadingContentMargin
  {
    get => (Thickness)GetValue(LoadingContentMarginProperty);
    set => SetValue(LoadingContentMarginProperty, value);
  }

  /// <summary>
  /// Spinner 메시지 콘텐츠의 마진을 나타내는 DependencyProperty
  /// 기본값은 Thickness(10)
  /// </summary>
  public static readonly DependencyProperty LoadingContentMarginProperty =
    DependencyProperty.Register(
      nameof(LoadingContentMargin),
      typeof(Thickness),
      typeof(LoadingSpinner),
      new PropertyMetadata(new Thickness(10)));

  #endregion

  #region Spinner 스타일 지정 (SpinnerType)
  /// <summary>
  /// SpinnerType
  /// </summary>
  [Category(nameof(Heimdallr.ToolKit.Indicators))]
  [Description("Spinner 형식을 가져오거나 설정합니다")]
  public LoadingSpinnerType SpinnerType
  {
    get => (LoadingSpinnerType)GetValue(SpinnerTypeProperty);
    set => SetValue(SpinnerTypeProperty, value);
  }

  /// <summary>
  /// Spinner의 외형 타입을 지정하는 DependencyProperty
  /// 예: Ring, Bar, Dots 등 (LoadingSpinnerType Enum 사용)
  /// </summary>
  public static readonly DependencyProperty SpinnerTypeProperty =
    DependencyProperty.Register(
      nameof(SpinnerType),
      typeof(LoadingSpinnerType),
      typeof(LoadingSpinner),
      new PropertyMetadata(LoadingSpinnerType.None));

  #endregion

  #region 로딩 후 포커스 이동 대상 (FocusAfterLoading)
  /// <summary>
  /// FocusAfterLoading
  /// </summary>
  [Category(nameof(Heimdallr.ToolKit.Indicators))]
  [Description("대기 시간이 끝난 후 집중되는 컨트롤을 가져오거나 설정합니다")]
  public Control FocusAfterLoading
  {
    get => (Control)GetValue(FocusAfterLoadingProperty);
    set => SetValue(FocusAfterLoadingProperty, value);
  }

  /// <summary>
  /// 로딩이 끝난 후 포커스를 줄 컨트롤을 지정하는 DependencyProperty
  /// </summary>
  public static readonly DependencyProperty FocusAfterLoadingProperty =
    DependencyProperty.Register(
      nameof(FocusAfterLoading),
      typeof(Control),
      typeof(LoadingSpinner),
      new PropertyMetadata(null));

  #endregion

  #region 생성자
  /// <summary>
  /// 생성자
  /// </summary>
  public LoadingSpinner()
  {
    // 이 컨트롤에 대한 기본 스타일 키 설정
    DefaultStyleKeyProperty.OverrideMetadata(
      typeof(LoadingSpinner),
      new FrameworkPropertyMetadata(typeof(LoadingSpinner)));
  }

  #endregion

  #region IsLoading 변경 처리

  private static void OnIsLoadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    ((LoadingSpinner)d).OnIsLoadingChanged(e);
  }

  /// <summary>
  /// IsLoading 변경 시 시각 상태 및 포커스 처리 수행
  /// </summary>
  protected virtual void OnIsLoadingChanged(DependencyPropertyChangedEventArgs e)
  {
    if (!(bool)e.NewValue && FocusAfterLoading != null)
    {
      FocusAfterLoading.Dispatcher.InvokeAsync(async () =>
      {
        await Task.Delay(100); // 약간의 지연 후 포커스
        FocusAfterLoading.Focus();
      });
    }

    ChangeVisualState((bool)e.NewValue);
  }

  /// <summary>
  /// VisualStateManager를 사용하여 Visible/Hidden 상태 전환
  /// </summary>
  protected virtual void ChangeVisualState(bool isLoadingContentVisible = false)
  {
    VisualStateManager.GoToState(
      this,
      isLoadingContentVisible ? "Visible" : "Hidden",
      true);
  }

  #endregion

  #region Template 적용 시 처리

  /// <summary>
  /// 컨트롤 템플릿이 적용되면 초기 IsLoading 상태에 따라 시각 상태 설정
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    ChangeVisualState(IsLoading);
  }

  #endregion
}
