using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.Indicators;

/// VisualStateManager를 사용한 UI 요소의 시각적 상태 관리
[TemplateVisualState(Name = VisualStates.StateHidden, GroupName = VisualStates.GroupVisibility)]
[TemplateVisualState(Name = VisualStates.StateVisible, GroupName = VisualStates.GroupVisibility)]

// LoadingSpinner 컨트롤 정의 (로딩 중 표시하는 스피너)
public class LoadingSpinner : ContentControl
{
  #region Spinner 형식이 사용 여부
  // Category 지정(디지이너에서 그룹화)
  // 디자이너나 도구 상자에서 해당 컨트롤을 구분하기 위한 그룹명으로 사용
  [Category(nameof(Heimdallr.ToolKit.Indicators))]
  // 설명추가 (디자이너에서 표기)
  [Description("Spinner 가 사용중인지 여부를 가져오거나 설정합니다")]

  public bool IsLoading
  {
    get => (bool)GetValue(IsLoadingProperty);
    set => SetValue(IsLoadingProperty, value);
  }

  public static readonly DependencyProperty IsLoadingProperty =
      DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(LoadingSpinner),
        new PropertyMetadata(false, OnIsLoadingChanged));
  #endregion

  #region 더이상 사용하지 않는 속성
  //[Description("시작시 기본적으로 Spinner 가 사용 중인지 여부를 가져오거나 설정합니다")]
  //private bool _isLoadingATStartup;

  //// 더이상 사용하지 않는 속성
  //[Obsolete("IsLoading 의 초기 값을 사용하여 초기 상태 제어")]
  //public bool IsLoadingATStartup
  //{
  //  get => _isLoadingATStartup;
  //  set { _isLoadingATStartup = value; }
  //}
  #endregion

  #region Spinner 진행시 문자열 메시지
  // LoadingContent : Spinner 의 상태 메세지 (로딩중에 사용자에게 보여줄 텍스트)
  [Category(nameof(Heimdallr.ToolKit.Indicators))]
  [Description("대기 메시지와 같은 Spinner 내용을 가져오거나 설정합니다")]
  public string? LoadingContent
  {
    get => (string)GetValue(LoadingContentProperty);
    set => SetValue(LoadingContentProperty, value);
  }

  // DependencyProperty: BusyContent 속성의 DependencyProperty 등록
  // 기본 텍스트: "Please wait..., 기다려 주세요..."
  public static readonly DependencyProperty LoadingContentProperty =
    DependencyProperty.Register(nameof(LoadingContent), typeof(string), typeof(LoadingSpinner),
      new PropertyMetadata("기다려 주세요..."));
  #endregion

  #region Spinner 형식 지정시 Margin 
  // BusyContentMargin: Indicator의 콘텐츠 마진
  // (텍스트 및 표시되는 아이콘 주변의 공간)
  [Category(nameof(Heimdallr.ToolKit.Indicators))]
  [Description("Spinner Content Margin 을 가져오거나 설정합니다.")]
  public Thickness LoadingContentMargin
  {
    get => (Thickness)GetValue(LoadingContentMarginProperty);
    set => SetValue(LoadingContentMarginProperty, value);
  }

  // DependencyProperty: BusyContentMargin 속성의 DependencyProperty 등록
  // 기본값: 모든 방향 10px 마진
  public static readonly DependencyProperty LoadingContentMarginProperty =
        DependencyProperty.Register(nameof(LoadingContentMargin), typeof(Thickness),
          typeof(LoadingSpinner), new PropertyMetadata(new Thickness(10)));

  #endregion

  #region Spinner Type 지정
  [Category(nameof(Heimdallr.ToolKit.Indicators))]
  [Description("Spinner 형식을 가져오거나 설정합니다")]
  public LoadingSpinnerType SpinnerType
  {
    get => (LoadingSpinnerType)GetValue(SpinnerTypeProperty);
    set => SetValue(SpinnerTypeProperty, value);
  }

  public static readonly DependencyProperty SpinnerTypeProperty =
    DependencyProperty.Register(nameof(SpinnerType), typeof(LoadingSpinnerType),
      typeof(LoadingSpinner), new PropertyMetadata(LoadingSpinnerType.None));
  #endregion

  #region FocusAfterLoading 로딩후 집중되는 컨트로
  // FocusAfterBusy: "busy" 상태가 끝난 후 포커스를 받을 컨트롤
  [Category(nameof(Heimdallr.ToolKit.Indicators))]
  [Description("대기 시간이 끝난 후 집중되는 컨트롤을 가져오거나 설정합니다")]
  public Control FocusAfterLoading
  {
    get => (Control)GetValue(FocusAfterLoadingProperty);
    set => SetValue(FocusAfterLoadingProperty, value);
  }

  // DependencyProperty: FocusAfterLoading 속성의 DependencyProperty 등록
  // 기본값: null, 포커스를 받을 컨트롤 없음
  public static readonly DependencyProperty FocusAfterLoadingProperty =
    DependencyProperty.Register(nameof(FocusAfterLoading), typeof(Control),
      typeof(LoadingSpinner), new PropertyMetadata(null));
  #endregion

  #region 생성자
  public LoadingSpinner()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingSpinner),
      new FrameworkPropertyMetadata(typeof(LoadingSpinner)));
  }
  #endregion

  #region Method
  // IsLoading 속성 변경시 호출된는 콜백 메서드
  private static void OnIsLoadingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // 현재 개체의 메서드를 호출
    ((LoadingSpinner)d).OnIsLoadingChanged(e);
  }

  // IsLoading 값이 변경되면 호출되는 메서드
  protected virtual void OnIsLoadingChanged(DependencyPropertyChangedEventArgs e)
  {
    // IsLoading 가 false 일 때 FocusAfterLoading 에 설정된 컨트롤에 포커스를 주기 위한 처리
    if (!(bool)e.NewValue)
    {
      if (FocusAfterLoading != null)
      {
        // Dispatcher 확장메서드 사용
        FocusAfterLoading.Dispatcher.InvokeAsync(async () =>
        {
          // 100ms 지연후 포커스 설정
          await Task.Delay(100);
          FocusAfterLoading.Focus();
        });
      }
    }

    #region 이 방식도 가능

    //if (!(bool)e.NewValue)
    //{
    //  FocusAfterLoading?.Dispatcher.InvokeAsync(() =>
    //  {
    //    FocusAfterLoading.Focus();
    //  });
    //}
    #endregion

    // VisualState 변경 (IsBusy가 true이면 "Visible", false이면 "Hidden" 상태로 변경)
    ChangeVisualState((bool)e.NewValue);
  }

  // VisualState를 변경하는 메서드
  // (IsLoading 상태에 따라 "Visible" 또는 "Hidden"으로 변경)
  protected virtual void ChangeVisualState(bool isLoadingContentVisible = false)
  {
    // VisualStateManager를 사용하여 상태 전환
    // 첫 번째 인자는 전환할 UIElement(이ㅣ 경우 this, 즉 현재 컨트롤)
    // 두 번째 인자는 상태 이름 ("Visible" 또는 "Hidden")
    // 세 번째 인자는 상태 전환이 애니메이션을 사용할지 여부를 지정하는 값입니다.
    // true는 애니메이션을 사용한다는 뜻
    VisualStateManager.GoToState(this, isLoadingContentVisible
      ? "Visible"
      : "Hiddne",
      true);
  }
  #endregion

  #region override Method
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    // 템플릿이 적용될 때 초기 IsLoading 값에 따라 시각적 상태 설정
    ChangeVisualState(IsLoading);
  }
  #endregion
}
