using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

public class HoldSubmitButton : Button
{
  private CancellationTokenSource? _cancellationTokenSource;

  // HoldDuration: 버튼을 누르고 있어야 할 최소 시간
  public static readonly DependencyProperty HoldDurationProperty =
      DependencyProperty.Register("HoldDuration", typeof(Duration), typeof(HoldSubmitButton),
          new PropertyMetadata(Duration.Automatic, null, CoerceHoldDuration));

  // HoldDuration 강제 제한 로직: 최소 0.5초 이상이어야 함
  private static object CoerceHoldDuration(DependencyObject d, object baseValue)
  {
    if (baseValue is Duration duration && duration.TimeSpan.TotalSeconds >= 0.5)
    {
      return baseValue;
    }

    // 0.5초보다 작을 경우 0.5초로 보정
    return new Duration(TimeSpan.FromSeconds(0.5));
  }

  // HoldDuration CLR wrapper
  public Duration HoldDuration
  {
    get { return (Duration)GetValue(HoldDurationProperty); }
    set { SetValue(HoldDurationProperty, value); }
  }

  // 사용자 정의 라우티드 이벤트 - 완료
  public static readonly RoutedEvent HoldCompletedEvent =
      EventManager.RegisterRoutedEvent(nameof(HoldCompleted), RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(HoldSubmitButton));

  // 사용자 정의 라우티드 이벤트 - 취소
  public event RoutedEventHandler HoldCompleted
  {
    add => AddHandler(HoldCompletedEvent, value);
    remove => RemoveHandler(HoldCompletedEvent, value);
  }

  public static readonly RoutedEvent HoldCancelledEvent =
      EventManager.RegisterRoutedEvent(nameof(HoldCancelled), RoutingStrategy.Bubble,
        typeof(RoutedEventHandler), typeof(HoldSubmitButton));

  public event RoutedEventHandler HoldCancelled
  {
    add => AddHandler(HoldCancelledEvent, value);
    remove => RemoveHandler(HoldCancelledEvent, value);
  }

  static HoldSubmitButton()
  {
    // 커스텀 스타일을 적용할 수 있도록 메타데이터 수정
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HoldSubmitButton),
      new FrameworkPropertyMetadata(typeof(HoldSubmitButton)));
  }

  // 마우스를 누른 순간부터 지정된 시간까지 기다림
  protected override async void OnMouseLeftButtonDown(MouseButtonEventArgs e)
  {
    _cancellationTokenSource = new CancellationTokenSource();

    try
    {
      // 지정된 시간만큼 대기
      await Task.Delay((int)HoldDuration.TimeSpan.TotalMilliseconds, _cancellationTokenSource.Token);

      // Button 클릭 처리 실행
      base.OnClick();

      // 완료 이벤트 발생
      RaiseEvent(new RoutedEventArgs(HoldCompletedEvent));
    }
    catch (TaskCanceledException)
    {
      // 취소 이벤트 발생
      RaiseEvent(new RoutedEventArgs(HoldCancelledEvent));
    }

    base.OnMouseLeftButtonDown(e);
  }

  // 기본 Click 이벤트 무효화 (OnMouseLeftButtonDown에서 처리하므로)
  // 상속된 기본 Click 동작은 비활성화. 대신 OnMouseLeftButtonDown에서 수동으로 실행
  protected override void OnClick() { }

  // 마우스 뗐을 때 취소 처리
  protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
  {
    CancelSubmit();
    base.OnMouseLeftButtonUp(e);
  }

  // 마우스가 영역 밖으로 나갔을 때도 취소 처리
  protected override void OnMouseLeave(MouseEventArgs e)
  {
    CancelSubmit();
    base.OnMouseLeave(e);
  }

  // 취소 요청
  private void CancelSubmit()
  {
    // 내부에서 CancellationTokenSource를 통해 Task 취소 요청.
    if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
    {
      _cancellationTokenSource.Cancel();
    }
  }

  public static readonly DependencyProperty EllipseFillProperty =
       DependencyProperty.Register(nameof(EllipseFill), typeof(Brush), typeof(HoldSubmitButton),
         new PropertyMetadata(Brushes.DarkRed));

  public Brush EllipseFill
  {
    get => (Brush)GetValue(EllipseFillProperty);
    set => SetValue(EllipseFillProperty, value);
  }

  public static readonly DependencyProperty HoldEllipseFillProperty =
      DependencyProperty.Register(nameof(HoldEllipseFill), typeof(Brush), typeof(HoldSubmitButton),
        new PropertyMetadata(Brushes.DarkRed));

  public Brush HoldEllipseFill
  {
    get => (Brush)GetValue(HoldEllipseFillProperty);
    set => SetValue(HoldEllipseFillProperty, value);
  }

  public static readonly DependencyProperty BgEllipseFillProperty =
      DependencyProperty.Register(nameof(BgEllipseFill), typeof(Brush), typeof(HoldSubmitButton),
        new PropertyMetadata(Brushes.Goldenrod));

  public Brush BgEllipseFill
  {
    get => (Brush)GetValue(BgEllipseFillProperty);
    set => SetValue(BgEllipseFillProperty, value);
  }
}
