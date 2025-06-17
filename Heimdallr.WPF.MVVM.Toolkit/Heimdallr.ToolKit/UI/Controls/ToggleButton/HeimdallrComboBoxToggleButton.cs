using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrComboBoxToggleButton은 Heimdallr 스타일의 커스텀 콤보박스 토글 버튼입니다.
/// </summary>
public class HeimdallrComboBoxToggleButton : ToggleButton
{
  static HeimdallrComboBoxToggleButton()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrComboBoxToggleButton),
      new FrameworkPropertyMetadata(typeof(HeimdallrComboBoxToggleButton)));
  }
  /// <summary>
  /// 마우스 오버 시 배경색을 설정하는 종속성 속성입니다.
  /// </summary>
  public static readonly DependencyProperty MouseOverBackgroundProperty =
        DependencyProperty.Register(
            nameof(MouseOverBackground),
            typeof(Brush),
            typeof(HeimdallrComboBoxToggleButton),
            new PropertyMetadata(Brushes.Transparent));
  /// <summary>
  /// 마우스 오버 시 배경색을 설정하는 종속성 속성입니다.
  /// </summary>
  public Brush MouseOverBackground
  {
    get => (Brush)GetValue(MouseOverBackgroundProperty);
    set => SetValue(MouseOverBackgroundProperty, value);
  }

  /// <summary>
  /// 그리드 항목의 모서리 반경을 설정하는 종속성 속성입니다.
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }
  /// <summary>
  /// 그리드 항목의 모서리 반경을 설정하는 종속성 속성입니다.
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
    DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(HeimdallrComboBoxToggleButton),
      new PropertyMetadata(new CornerRadius(0)));

  /// <summary>
  /// 주 아이콘(PathIcon)의 너비 (픽셀 단위) 지정
  /// 기본값은 16픽셀
  /// </summary>
  public double IconWidth
  {
    get => (double)GetValue(IconWidthProperty);
    set => SetValue(IconWidthProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty IconWidthProperty =
    DependencyProperty.Register(nameof(IconWidth), typeof(double), typeof(HeimdallrComboBoxToggleButton),
      new PropertyMetadata(16.0)); // 기본값 16

  /// <summary>
  /// 주 아이콘(PathIcon)의 높이 (픽셀 단위) 지정
  /// 기본값은 16픽셀
  /// </summary>
  public double IconHeight
  {
    get => (double)GetValue(IconHeightProperty);
    set => SetValue(IconHeightProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty IconHeightProperty =
    DependencyProperty.Register(nameof(IconHeight), typeof(double), typeof(HeimdallrComboBoxToggleButton),
      new PropertyMetadata(16.0)); // 기본값 16

  /// <summary>
  /// PathIcon (주 아이콘) 의 색상 지정용 Brush
  /// 기본값은 회색(Gray)
  /// </summary>
  public Brush PathIconFill
  {
    get { return (Brush)GetValue(PathIconFillProperty); }
    set { SetValue(PathIconFillProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty PathIconFillProperty =
      DependencyProperty.Register(nameof(PathIconFill), typeof(Brush), typeof(HeimdallrComboBoxToggleButton), new PropertyMetadata(Brushes.Gray));

  /// <summary>
  /// IsChecked=True일 때 배경에 적용할 색상
  /// </summary>
  public Brush CheckedBackground
  {
    get => (Brush)GetValue(CheckedBackgroundProperty);
    set => SetValue(CheckedBackgroundProperty, value);
  }
  /// <summary>
  /// 종속성 주입
  /// </summary>
  public static readonly DependencyProperty CheckedBackgroundProperty =
      DependencyProperty.Register(nameof(CheckedBackground), typeof(Brush), typeof(HeimdallrComboBoxToggleButton),
          new PropertyMetadata(Brushes.DarkCyan)); // 기본값

  /// <summary>
  /// 타이틀 아이콘으로 표시될 PathIcon 타입 지정 (예: Barcode, Setting 등)
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
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrComboBoxToggleButton),
        new PropertyMetadata(PathIconType.None));

}
