using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;


/// <summary>
/// Heimdallr 스타일의 커스텀 ComboBox 컨트롤입니다.
/// </summary>
public class HeimdallrComboBox : ComboBox
{
  static HeimdallrComboBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrComboBox),
        new FrameworkPropertyMetadata(typeof(HeimdallrComboBox)));
  }

  /// <summary>
  /// 마우스 오버 시 배경색을 설정하는 종속성 속성입니다.
  /// </summary>
  public static readonly DependencyProperty MouseOverBackgroundProperty =
        DependencyProperty.Register(
            nameof(MouseOverBackground),
            typeof(Brush),
            typeof(HeimdallrComboBox),
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
    DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(HeimdallrComboBox),
      new PropertyMetadata(new CornerRadius(0)));

  /// <summary>
  /// 아이콘 속성
  /// </summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }
  /// <summary>
  /// 종속성 속성
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrComboBox),
          new PropertyMetadata(PathIconType.None));

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
    DependencyProperty.Register(nameof(IconWidth), typeof(double), typeof(HeimdallrComboBox),
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
    DependencyProperty.Register(nameof(IconHeight), typeof(double), typeof(HeimdallrComboBox),
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
      DependencyProperty.Register(nameof(PathIconFill), typeof(Brush), typeof(HeimdallrComboBox), new PropertyMetadata(Brushes.Gray));

  /// <summary>
  /// DropDown이 열렸을 때 ToggleButton에 적용할 배경색
  /// </summary>
  public Brush CheckedBackground
  {
    get => (Brush)GetValue(CheckedBackgroundProperty);
    set => SetValue(CheckedBackgroundProperty, value);
  }

  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty CheckedBackgroundProperty =
      DependencyProperty.Register(nameof(CheckedBackground), typeof(Brush), typeof(HeimdallrComboBox),
          new PropertyMetadata(Brushes.DarkCyan));

  /// <summary>
  /// 드롭다운 영역의 배경색 (Popup 내부)
  /// </summary>
  public Brush DropDownBackground
  {
    get => (Brush)GetValue(DropDownBackgroundProperty);
    set => SetValue(DropDownBackgroundProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty DropDownBackgroundProperty =
      DependencyProperty.Register(nameof(DropDownBackground), typeof(Brush), typeof(HeimdallrComboBox),
          new PropertyMetadata(Brushes.White));
}


