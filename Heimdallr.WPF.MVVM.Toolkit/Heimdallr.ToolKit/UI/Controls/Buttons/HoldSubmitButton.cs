using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 일정 시간 동안 버튼을 눌러야 클릭 처리가 되는 원형 Hold Submit 버튼입니다.
/// 사용자 커스터마이징을 위한 다양한 색상 속성과 완료/취소 이벤트를 제공합니다.
/// </summary>
public class HoldSubmitButton : Button
{
  private CancellationTokenSource? _cancellationTokenSource;

  #region HoldDuration (DependencyProperty)

  /// <summary>
  /// 사용자가 버튼을 누르고 있어야 하는 최소 시간입니다.
  /// 기본값은 자동(Duration.Automatic)이며, 최소 0.5초 이상이어야 합니다.
  /// </summary>
  public static readonly DependencyProperty HoldDurationProperty =
      DependencyProperty.Register("HoldDuration", typeof(Duration), typeof(HoldSubmitButton),
          new PropertyMetadata(Duration.Automatic, null, CoerceHoldDuration));

  /// <summary>
  /// HoldDuration 값 보정 콜백 - 최소 0.5초 미만이면 0.5초로 강제 설정합니다.
  /// </summary>
  private static object CoerceHoldDuration(DependencyObject d, object baseValue)
  {
    if (baseValue is Duration duration && duration.TimeSpan.TotalSeconds >= 0.5)
    {
      return baseValue;
    }
    return new Duration(TimeSpan.FromSeconds(0.5));
  }

  /// <summary>
  /// HoldDuration CLR 속성 Wrapper
  /// </summary>
  public Duration HoldDuration
  {
    get => (Duration)GetValue(HoldDurationProperty);
    set => SetValue(HoldDurationProperty, value);
  }

  #endregion

  #region RoutedEvents (HoldCompleted / HoldCancelled)

  /// <summary>
  /// 사용자가 버튼을 지정된 시간 동안 누른 뒤 손을 떼지 않고 완료되었을 때 발생하는 이벤트입니다.
  /// </summary>
  public static readonly RoutedEvent HoldCompletedEvent =
      EventManager.RegisterRoutedEvent(nameof(HoldCompleted), RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(HoldSubmitButton));

  /// <summary>
  /// HoldCompleted 이벤트 핸들러 등록
  /// </summary>
  public event RoutedEventHandler HoldCompleted
  {
    add => AddHandler(HoldCompletedEvent, value);
    remove => RemoveHandler(HoldCompletedEvent, value);
  }

  /// <summary>
  /// 사용자가 버튼을 누르던 중 손을 떼거나, 마우스가 벗어나는 등의 이유로 작업이 취소되었을 때 발생하는 이벤트입니다.
  /// </summary>
  public static readonly RoutedEvent HoldCancelledEvent =
      EventManager.RegisterRoutedEvent(nameof(HoldCancelled), RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(HoldSubmitButton));

  /// <summary>
  /// HoldCancelled 이벤트 핸들러 등록
  /// </summary>
  public event RoutedEventHandler HoldCancelled
  {
    add => AddHandler(HoldCancelledEvent, value);
    remove => RemoveHandler(HoldCancelledEvent, value);
  }

  #endregion

  #region 생성자 및 스타일 메타데이터

  static HoldSubmitButton()
  {
    // XAML에서 DefaultStyleKey로 연결된 템플릿을 적용할 수 있도록 설정
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HoldSubmitButton),
      new FrameworkPropertyMetadata(typeof(HoldSubmitButton)));
  }

  #endregion

  #region 마우스 동작 처리

  /// <summary>
  /// 마우스 왼쪽 버튼이 눌렸을 때, 지정된 시간만큼 기다린 후 클릭 처리 및 이벤트 발생
  /// </summary>
  protected override async void OnMouseLeftButtonDown(MouseButtonEventArgs e)
  {
    _cancellationTokenSource = new CancellationTokenSource();

    try
    {
      // 지정된 시간 대기 (사용자가 누르고 있는 동안)
      await Task.Delay((int)HoldDuration.TimeSpan.TotalMilliseconds, _cancellationTokenSource.Token);

      // 버튼 기본 Click 처리 수동 호출
      base.OnClick();

      // 완료 이벤트 발생
      RaiseEvent(new RoutedEventArgs(HoldCompletedEvent));
    }
    catch (TaskCanceledException)
    {
      // 중간에 취소되었을 경우 취소 이벤트 발생
      RaiseEvent(new RoutedEventArgs(HoldCancelledEvent));
    }

    base.OnMouseLeftButtonDown(e);
  }

  /// <summary>
  /// 기본 Click 동작은 무효화합니다.
  /// OnMouseLeftButtonDown에서 수동으로 호출되므로 필요 없음
  /// </summary>
  protected override void OnClick() { }

  /// <summary>
  /// 마우스에서 손을 뗄 경우 취소 처리
  /// </summary>
  protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
  {
    CancelSubmit();
    base.OnMouseLeftButtonUp(e);
  }

  /// <summary>
  /// 마우스가 버튼 영역 밖으로 벗어날 경우 취소 처리
  /// </summary>
  protected override void OnMouseLeave(MouseEventArgs e)
  {
    CancelSubmit();
    base.OnMouseLeave(e);
  }

  /// <summary>
  /// Task 취소 요청 처리
  /// </summary>
  private void CancelSubmit()
  {
    if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
    {
      _cancellationTokenSource.Cancel();
    }
  }

  #endregion

  #region 색상 관련 DependencyProperty

  /// <summary>
  /// 버튼 내부의 기본 원형 배경색입니다 (비활성 상태 시 적용)
  /// </summary>
  public static readonly DependencyProperty EllipseFillProperty =
       DependencyProperty.Register(nameof(EllipseFill), typeof(Brush), typeof(HoldSubmitButton),
         new PropertyMetadata(Brushes.DarkRed));

  /// <summary>
  /// 버튼 내부의 기본 원형 색상에 대한 브러시입니다
  /// </summary>
  public Brush EllipseFill
  {
    get => (Brush)GetValue(EllipseFillProperty);
    set => SetValue(EllipseFillProperty, value);
  }

  /// <summary>
  /// 버튼을 누르고 있을 때 활성화되는 원형의 색상입니다.
  /// </summary>
  public static readonly DependencyProperty HoldEllipseFillProperty =
      DependencyProperty.Register(nameof(HoldEllipseFill), typeof(Brush), typeof(HoldSubmitButton),
        new PropertyMetadata(Brushes.DarkRed));

  /// <summary>
  /// 버튼 누름 시 나타나는 강조된 원형의 브러시입니다
  /// </summary>
  public Brush HoldEllipseFill
  {
    get => (Brush)GetValue(HoldEllipseFillProperty);
    set => SetValue(HoldEllipseFillProperty, value);
  }

  /// <summary>
  /// 버튼 배경을 구성하는 가장 바깥쪽 원형 색상입니다.
  /// </summary>
  public static readonly DependencyProperty BgEllipseFillProperty =
      DependencyProperty.Register(nameof(BgEllipseFill), typeof(Brush), typeof(HoldSubmitButton),
        new PropertyMetadata(Brushes.Goldenrod));

  /// <summary>
  /// 외부 원형(백그라운드) 색상 브러시입니다
  /// </summary>
  public Brush BgEllipseFill
  {
    get => (Brush)GetValue(BgEllipseFillProperty);
    set => SetValue(BgEllipseFillProperty, value);
  }

  #endregion
}

