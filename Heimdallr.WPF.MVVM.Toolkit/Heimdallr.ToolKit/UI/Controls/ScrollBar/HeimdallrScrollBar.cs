using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrScrollBar는 커스터마이징된 WPF ScrollBar 컨트롤입니다.
/// 아이콘 사이즈, 트랙 너비, 썸 너비, 썸 높이 자동계산 등을 지원합니다.
/// </summary>
public class HeimdallrScrollBar : ScrollBar
{
  static HeimdallrScrollBar()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrScrollBar),
      new FrameworkPropertyMetadata(typeof(HeimdallrScrollBar)));
  }

  #region CornerRadius Property
  /// <summary>
  /// 코너라디우스
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }

  /// <summary>
  /// 기본값 0
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
     DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius),
         typeof(HeimdallrScrollBar),
         new FrameworkPropertyMetadata(new CornerRadius(0)));
  #endregion

  #region Icon Properties
  /// <summary>
  /// 아이콘 지정
  /// </summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }

  /// <summary>
  /// 아이콘 속성
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrScrollBar),
          new PropertyMetadata(PathIconType.None));

  /// <summary>
  /// 아이콘 색상지정
  /// </summary>
  public Brush Fill
  {
    get => (Brush)GetValue(FillProperty);
    set => SetValue(FillProperty, value);
  }

  /// <summary>
  /// 아이콘 색상지정 속성
  /// </summary>
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrScrollBar),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF456882"))));
  #endregion

  #region IconSize
  /// <summary>
  /// 이이콘 사이즈 너비,높이
  /// </summary>
  public double IconSize
  {
    get => (double)GetValue(IconSizeProperty);
    set => SetValue(IconSizeProperty, value);
  }

  /// <summary>
  /// 아이콘사이즈 기본값
  /// </summary>
  public static readonly DependencyProperty IconSizeProperty =
      DependencyProperty.Register(nameof(IconSize), typeof(double),
          typeof(HeimdallrScrollBar), new PropertyMetadata(8.0));
  #endregion

  /// <summary>
  /// HeimdallrScrollbar 템플릿이 적용될 때 호출됩니다.
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    // Thumb 가 너무 작아지는 문제 방지용
    if (ViewportSize <= 0)
      ViewportSize = 1; // 기본 가시 영영 크기 설정
  }
}







