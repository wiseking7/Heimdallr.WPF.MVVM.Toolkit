using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.UI.Controls;


public class SliderContentMenu : ContentControl
{
  #region IsOpen 속성 (슬라이드 메뉴가 열려있는지 여부를 결정) 
  public bool IsOpen
  {
    get { return (bool)GetValue(IsOpenProperty); }
    set { SetValue(IsOpenProperty, value); }
  }

  // IsOpen DependencyProperty (Menu가 열려 있는지 상태를 나타냄)
  // OnIsOpenPropertyChanged 값이 변경될 때 호출되는 콜백 메서드
  public static readonly DependencyProperty IsOpenProperty =
      DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(SliderContentMenu),
          new PropertyMetadata(false, OnIsOpenPropertyChanged));
  #endregion

  #region OpenCloseDuration 속성 (메뉴 열기/닫기 애니메이션 시간 설정)
  public Duration OpenCloseDuration
  {
    get { return (Duration)GetValue(OpenCloseDurationProperty); }
    set { SetValue(OpenCloseDurationProperty, value); }
  }

  // OpenCloseDuration DependencyProperty (메뉴 열기/닫기 애니메이션 지속시간)
  // 기본값은 Duration.Automatic 이지만 사용자가 이 값을 설정할 수 있습니다. 
  public static readonly DependencyProperty OpenCloseDurationProperty =
      DependencyProperty.Register(nameof(OpenCloseDuration), typeof(Duration), typeof(SliderContentMenu),
          new PropertyMetadata(Duration.Automatic));
  #endregion

  #region FallbackOpenWidth 속성 (콘텐츠가 측정되지 않았을 때 사용할 너비) 콘텐츠의 너비를 자동으로 측정할 수 없을 경우 
  public double FallbackOpenWidth
  {
    get { return (double)GetValue(FallbackOpenWidthProperty); }
    set { SetValue(FallbackOpenWidthProperty, value); }
  }

  // FallbackOpenWidth DependencyProperty (콘텐츠의 너비가 측정되지 않았을 때의 기본 너비)
  // 기본값은 100.0
  public static readonly DependencyProperty FallbackOpenWidthProperty =
      DependencyProperty.Register(nameof(FallbackOpenWidth), typeof(double), typeof(SliderContentMenu),
          new PropertyMetadata(100.0));
  #endregion

  #region Content 속성 (슬라이더 메뉴의 콘텐츠) 속성은 메뉴에 표시할 UI 요소(ContentControl)를 설정
  // FrameworkElement로 타입이 지정되어 있기 때문에, 모든 UI 요소를 설정할 수 있습니다
  public new FrameworkElement Content
  {
    get { return (FrameworkElement)GetValue(ContentProperty); }
    set { SetValue(ContentProperty, value); }
  }

  // Content DependencyProperty(컨텐츠의 내용)
  public new static readonly DependencyProperty ContentProperty =
      DependencyProperty.Register(nameof(Content), typeof(FrameworkElement), typeof(SliderContentMenu),
          new PropertyMetadata(null));
  #endregion

  #region 생성자 SliderContentMenu의 기본 스타일을 지정하는 static 생성자
  static SliderContentMenu()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(SliderContentMenu),
      new FrameworkPropertyMetadata(typeof(SliderContentMenu)));
  }

  // 메뉴의 초기 너비를 0으로 설정
  // 초기 너비를 0으로 설정하여 메뉴가 기본적으로 숨겨진 상태로 시작
  public SliderContentMenu()
  {
    Width = 0;
  }
  #endregion

  #region Method
  // IsOpen 속성이 변경될 때 호출되는 콜백
  // OnIsOpenPropertyChanged 는 IsOpen 속성이 변경될 때 호출됩니다.
  // 메뉴가 열리거나 닫힐 때 애니메이션을 처리하는 메서드를 호출합니다.
  private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is SliderContentMenu sliderContentMenu)
    {
      sliderContentMenu.OnIsOpenPropertyChanged();
    }
  }

  // IsOpen 속성에 따라 메뉴 열기/닫기 애니메이션 실행
  // OnIsOpenPropertyChanged 에서는 IsOpen 값에 따라 메뉴를 열거나 닫는 애니메이션을
  private void OnIsOpenPropertyChanged()
  {
    if (IsOpen)
    {
      // 메뉴 열기 애니메이션
      OpenMenuAnimated();
    }
    else
    {
      // 메뉴 닫기 애니메이션
      CloseMenuAnimated();
    }
  }

  // 메뉴 열기 애니메이션
  // OpenMenuAnimated 에서는 메뉴를 열기 위한 애니메이션을 설정합니다.
  // 메뉴의 최종 너비는 GetDesiredContentWidth 메서드를 통해 계산됩니다.
  private void OpenMenuAnimated()
  {
    double contentWidth = GetDesiredContentWidth();

    DoubleAnimation openingAnimation = new DoubleAnimation(contentWidth, OpenCloseDuration);
    BeginAnimation(WidthProperty, openingAnimation);
  }

  // GetDesiredContentWidth 메서드는 Content 의 DesiredSize.Width 를 측정하여 실제 콘텐츠의 너비를 가져옵니다.
  // 만약 콘텐츠가 없으면 FallbackOpenWidth를 사용합니다.
  private double GetDesiredContentWidth()
  {
    if (Content == null)
    {
      return FallbackOpenWidth;
    }

    Content.Measure(new Size(MaxWidth, MaxHeight));

    return Content.DesiredSize.Width;
  }

  // 메뉴 닫기 애니메이션
  // CloseMenuAnimated에서는 메뉴를 닫는 애니메이션을 설정합니다.너비를 0으로 설정하여 메뉴를 숨깁니다.
  private void CloseMenuAnimated()
  {
    DoubleAnimation closingAnimation = new DoubleAnimation(0, OpenCloseDuration);
    BeginAnimation(WidthProperty, closingAnimation);
  }
  #endregion
}

