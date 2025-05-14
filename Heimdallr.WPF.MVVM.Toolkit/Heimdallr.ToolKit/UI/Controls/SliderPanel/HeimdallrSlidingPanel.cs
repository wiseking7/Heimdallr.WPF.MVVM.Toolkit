using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 슬라이딩 사이드 패널 UI 컨트롤.
/// 열림/닫힘 애니메이션과 콘텐츠 영역 포함.
/// </summary>
public class HeimdallrSlidingPanel : Control
{
  /// <summary>
  /// 정적 생성자: DefaultStyleKey 설정 (Generic.xaml 스타일 연결용)
  /// </summary>
  static HeimdallrSlidingPanel()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrSlidingPanel),
        new FrameworkPropertyMetadata(typeof(HeimdallrSlidingPanel)));
  }

  #region IsOpen - 패널 열기/닫기

  /// <summary>
  /// 패널 열기/닫기 상태.
  /// true면 열리고, false면 닫힘.
  /// </summary>
  public bool IsOpen
  {
    get => (bool)GetValue(IsOpenProperty);
    set => SetValue(IsOpenProperty, value);
  }

  public static readonly DependencyProperty IsOpenProperty =
      DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(HeimdallrSlidingPanel),
          new PropertyMetadata(false, OnIsOpenPropertyChanged));

  private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is HeimdallrSlidingPanel panel)
    {
      panel.UpdateOpenState();
    }
  }

  #endregion

  #region OpenCloseDuration - 열기/닫기 애니메이션 지속시간

  /// <summary>
  /// 열고 닫는 애니메이션의 지속 시간.
  /// </summary>
  public Duration OpenCloseDuration
  {
    get => (Duration)GetValue(OpenCloseDurationProperty);
    set => SetValue(OpenCloseDurationProperty, value);
  }

  public static readonly DependencyProperty OpenCloseDurationProperty =
      DependencyProperty.Register(nameof(OpenCloseDuration), typeof(Duration), typeof(HeimdallrSlidingPanel),
          new PropertyMetadata(Duration.Automatic));

  #endregion

  #region PanelContent - 패널 내부 콘텐츠

  /// <summary>
  /// 패널 내부에 표시할 콘텐츠. 기본 FrameworkElement(임) 더 상위 UIElement로 변경
  /// </summary>
  public UIElement PanelContent
  {
    get => (UIElement)GetValue(PanelContentProperty);
    set => SetValue(PanelContentProperty, value);
  }

  public static readonly DependencyProperty PanelContentProperty =
      DependencyProperty.Register(nameof(PanelContent), typeof(UIElement), typeof(HeimdallrSlidingPanel),
          new PropertyMetadata(null));

  #endregion

  #region DefaultOpenWidth - 콘텐츠 없을 때 기본 너비

  /// <summary>
  /// 콘텐츠가 없을 때 기본 열림 너비.
  /// </summary>
  public double DefaultOpenWidth
  {
    get => (double)GetValue(DefaultOpenWidthProperty);
    set => SetValue(DefaultOpenWidthProperty, value);
  }

  public static readonly DependencyProperty DefaultOpenWidthProperty =
      DependencyProperty.Register(nameof(DefaultOpenWidth), typeof(double), typeof(HeimdallrSlidingPanel),
          new PropertyMetadata(100.0));

  #endregion

  #region 동작 메서드

  /// <summary>
  /// IsOpen 변경 시 동작 처리.
  /// </summary>
  private void UpdateOpenState()
  {
    if (IsOpen)
    {
      AnimateOpen();
    }
    else
    {
      AnimateClose();
    }
  }

  /// <summary>
  /// 열기 애니메이션 실행.
  /// </summary>
  private void AnimateOpen()
  {
    double targetWidth = GetPanelContentWidth();
    DoubleAnimation openAnim = new DoubleAnimation(targetWidth, OpenCloseDuration);
    BeginAnimation(WidthProperty, openAnim);
  }

  /// <summary>
  /// 닫기 애니메이션 실행.
  /// </summary>
  private void AnimateClose()
  {
    DoubleAnimation closeAnim = new DoubleAnimation(0, OpenCloseDuration);
    BeginAnimation(WidthProperty, closeAnim);
  }

  /// <summary>
  /// 콘텐츠의 실제 원하는 너비 계산.
  /// 콘텐츠가 없으면 DefaultOpenWidth 반환.
  /// </summary>
  private double GetPanelContentWidth()
  {
    if (PanelContent == null)
      return DefaultOpenWidth;

    PanelContent.Measure(new Size(MaxWidth, MaxHeight));
    return PanelContent.DesiredSize.Width;
  }

  #endregion

  /// <summary>
  /// 기본 생성자: 초기 Width 설정
  /// </summary>
  public HeimdallrSlidingPanel()
  {
    Width = 200; // 초깃값. 열림 시 기준 너비로 사용됨.
  }
}
