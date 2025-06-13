using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 좌우 슬라이드로 열리고 닫히는 메뉴 컨트롤입니다.
/// 내부 콘텐츠의 너비에 따라 열림/닫힘 애니메이션을 적용합니다.
/// </summary>
public class SliderContentMenu : ContentControl
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
      DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(SliderContentMenu),
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
      DependencyProperty.Register(nameof(OpenCloseDuration), typeof(Duration), typeof(SliderContentMenu),
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
      DependencyProperty.Register(nameof(FallbackOpenWidth), typeof(double), typeof(SliderContentMenu),
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
      DependencyProperty.Register(nameof(Content), typeof(FrameworkElement), typeof(SliderContentMenu),
          new PropertyMetadata(null));

  #endregion

  #region 생성자

  static SliderContentMenu()
  {
    // 스타일을 Generic.xaml에서 찾을 수 있도록 설정
    DefaultStyleKeyProperty.OverrideMetadata(typeof(SliderContentMenu),
      new FrameworkPropertyMetadata(typeof(SliderContentMenu)));
  }

  /// <summary>
  /// 생성자에서 초기 너비를 0으로 설정하여 메뉴가 기본적으로 닫힌 상태로 시작합니다.
  /// </summary>
  public SliderContentMenu()
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
    if (d is SliderContentMenu menu)
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


/// <summary>
/// 슬라이더 메뉴 내부에서 사용하는 전용 라디오 버튼입니다.
/// 별도 스타일을 적용할 수 있도록 기본 스타일 키를 재정의합니다.
/// </summary>
public class SlideContentMenuItemRadio : RadioButton
{
  static SlideContentMenuItemRadio()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(SlideContentMenuItemRadio),
      new FrameworkPropertyMetadata(typeof(SlideContentMenuItemRadio)));
  }
}


/// <summary>
/// 슬라이드 메뉴 항목으로, Header와 자식 항목(Items)을 가질 수 있는 컨트롤입니다.
/// 펼침 여부, 아이콘 종류를 설정할 수 있습니다.
/// </summary>
public class SlideContentMenuItem : HeaderedItemsControl
{
  static SlideContentMenuItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(SlideContentMenuItem),
        new FrameworkPropertyMetadata(typeof(SlideContentMenuItem)));
  }

  #region IsExpanded (자식 항목 펼침 상태)

  /// <summary>
  /// 자식 항목이 펼쳐져 있는지 여부를 나타냅니다.
  /// </summary>
  public bool IsExpanded
  {
    get => (bool)GetValue(IsExpandedProperty);
    set => SetValue(IsExpandedProperty, value);
  }

  /// <summary>
  /// 종속성 주입
  /// </summary>
  public static readonly DependencyProperty IsExpandedProperty =
      DependencyProperty.Register(nameof(IsExpanded), typeof(bool), typeof(SlideContentMenuItem),
          new PropertyMetadata(false));

  #endregion

  #region PathIcon (벡터 아이콘)

  /// <summary>
  /// HeimdallrIcon에 사용할 Path 기반 벡터 아이콘입니다.
  /// </summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }

  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(SlideContentMenuItem),
          new PropertyMetadata(PathIconType.None));

  #endregion

  #region Image (이미지 아이콘)

  /// <summary>
  /// 이미지 타입 아이콘을 설정합니다.
  /// </summary>
  public ImageType Image
  {
    get => (ImageType)GetValue(ImageProperty);
    set => SetValue(ImageProperty, value);
  }

  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty ImageProperty =
      DependencyProperty.Register(nameof(Image), typeof(ImageType), typeof(SlideContentMenuItem),
          new PropertyMetadata(ImageType.None));

  #endregion

  #region Icon (IconType - 공통 아이콘 열거형)

  /// <summary>
  /// 아이콘 열거형을 사용하여 아이콘을 지정합니다.
  /// (Path 또는 Image로 변환 가능한 값)
  /// </summary>
  public IconType Icon
  {
    get => (IconType)GetValue(IconProperty);
    set => SetValue(IconProperty, value);
  }

  /// <summary>
  /// 종속성 주입
  /// </summary>
  public static readonly DependencyProperty IconProperty =
      DependencyProperty.Register(nameof(Icon), typeof(IconType), typeof(SlideContentMenuItem),
          new PropertyMetadata(IconType.None));

  #endregion
}

