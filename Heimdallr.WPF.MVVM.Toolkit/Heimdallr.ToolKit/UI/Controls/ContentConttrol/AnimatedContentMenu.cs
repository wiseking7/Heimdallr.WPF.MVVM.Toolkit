using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 좌우 슬라이드로 열리고 닫히는 메뉴 컨트롤입니다.
/// 내부 콘텐츠의 너비에 따라 열림/닫힘 애니메이션을 적용합니다.
/// </summary>
public class AnimatedContentMenu : ContentControl
{
  #region IsOpen 속성 (슬라이드 메뉴가 열려있는지 여부를 결정)

  /// <summary>
  /// 슬라이더 메뉴가 열려 있는지 여부를 나타냅니다.
  /// true면 메뉴가 열리고, false면 닫힙니다.
  /// </summary>
  public bool IsOpen
  {
    get => (bool)GetValue(IsOpenProperty);
    set => SetValue(IsOpenProperty, value);
  }

  /// <summary>
  /// 종속성 주입
  /// </summary>
  public static readonly DependencyProperty IsOpenProperty =
      DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(AnimatedContentMenu),
          new PropertyMetadata(false, OnIsOpenPropertyChanged)); // 상태가 바뀌면 애니메이션 실행

  #endregion

  #region OpenCloseDuration 속성 (메뉴 열기/닫기 애니메이션 시간 설정)

  /// <summary>
  /// 슬라이드 열기/닫기 애니메이션의 지속 시간입니다.
  /// 기본값은 Duration.Automatic입니다.
  /// </summary>
  public Duration OpenCloseDuration
  {
    get => (Duration)GetValue(OpenCloseDurationProperty);
    set => SetValue(OpenCloseDurationProperty, value);
  }

  /// <summary>
  /// 종속성 주입
  /// </summary>
  public static readonly DependencyProperty OpenCloseDurationProperty =
      DependencyProperty.Register(nameof(OpenCloseDuration), typeof(Duration), typeof(AnimatedContentMenu),
          new PropertyMetadata(Duration.Automatic));

  #endregion

  #region FallbackOpenWidth 속성 (측정 실패 시 사용할 너비)

  /// <summary>
  /// 콘텐츠의 실제 너비를 측정할 수 없을 때 사용할 기본 너비입니다.
  /// </summary>
  public double FallbackOpenWidth
  {
    get => (double)GetValue(FallbackOpenWidthProperty);
    set => SetValue(FallbackOpenWidthProperty, value);
  }

  /// <summary>
  /// 종속성 주입 기본값 100
  /// </summary>
  public static readonly DependencyProperty FallbackOpenWidthProperty =
      DependencyProperty.Register(nameof(FallbackOpenWidth), typeof(double), typeof(AnimatedContentMenu),
          new PropertyMetadata(100.0));

  #endregion

  #region Content 속성 (표시할 UI 콘텐츠)

  /// <summary>
  /// 메뉴 안에 표시될 실제 콘텐츠입니다.
  /// FrameworkElement 타입으로, UIElement 전반을 수용합니다.
  /// </summary>
  public new FrameworkElement Content
  {
    get => (FrameworkElement)GetValue(ContentProperty);
    set => SetValue(ContentProperty, value);
  }

  /// <summary>
  /// 종속성 주입
  /// </summary>
  public new static readonly DependencyProperty ContentProperty =
      DependencyProperty.Register(nameof(Content), typeof(FrameworkElement), typeof(AnimatedContentMenu),
          new PropertyMetadata(null));

  #endregion

  #region 생성자

  static AnimatedContentMenu()
  {
    // 스타일을 Generic.xaml에서 찾을 수 있도록 설정
    DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedContentMenu),
      new FrameworkPropertyMetadata(typeof(AnimatedContentMenu)));
  }

  /// <summary>
  /// 생성자에서 초기 너비를 0으로 설정하여 메뉴가 기본적으로 닫힌 상태로 시작합니다.
  /// </summary>
  public AnimatedContentMenu()
  {
    Width = 0;
  }

  #endregion

  #region 메서드

  /// <summary>
  /// IsOpen 속성 변경 시 호출되는 콜백 메서드입니다.
  /// </summary>
  private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is AnimatedContentMenu menu)
    {
      menu.OnIsOpenPropertyChanged();
    }
  }

  /// <summary>
  /// 메뉴 열림 여부에 따라 애니메이션을 실행합니다.
  /// </summary>
  private void OnIsOpenPropertyChanged()
  {
    if (IsOpen)
      OpenMenuAnimated();
    else
      CloseMenuAnimated();
  }

  /// <summary>
  /// 메뉴 열기 애니메이션을 실행합니다.
  /// 콘텐츠의 측정된 너비 또는 FallbackOpenWidth만큼 너비를 확장합니다.
  /// </summary>
  private void OpenMenuAnimated()
  {
    double contentWidth = GetDesiredContentWidth();

    var animation = new DoubleAnimation(contentWidth, OpenCloseDuration);
    BeginAnimation(WidthProperty, animation);
  }

  /// <summary>
  /// 콘텐츠의 실제 너비를 측정하여 반환합니다.
  /// 측정 실패 시 FallbackOpenWidth를 사용합니다.
  /// </summary>
  private double GetDesiredContentWidth()
  {
    if (Content == null)
      return FallbackOpenWidth;

    Content.Measure(new Size(MaxWidth, MaxHeight));
    return Content.DesiredSize.Width;
  }

  /// <summary>
  /// 메뉴 닫기 애니메이션을 실행합니다 (너비를 0으로 애니메이션).
  /// </summary>
  private void CloseMenuAnimated()
  {
    var animation = new DoubleAnimation(0, OpenCloseDuration);
    BeginAnimation(WidthProperty, animation);
  }

  #endregion
}


