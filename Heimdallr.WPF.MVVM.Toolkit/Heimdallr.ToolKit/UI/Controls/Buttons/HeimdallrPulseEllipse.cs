using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 클릭 시 원 애니메이션이 적용되는 버튼
/// </summary>
public class HeimdallrPulseEllipse : Button
{
  static HeimdallrPulseEllipse()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrPulseEllipse),
        new FrameworkPropertyMetadata(typeof(HeimdallrPulseEllipse)));
  }

  #region 첫 번째 원 색상지정
  /// <summary>
  /// 첫 번째 원의 색상을 설정하는 속성
  /// </summary>
  public Brush Ellipse1Brush
  {
    get => (Brush)GetValue(Ellipse1BrushProperty);
    set => SetValue(Ellipse1BrushProperty, value);
  }
  /// <summary>
  /// 첫 번째 원의 색상을 설정하는 속성
  /// </summary>
  public static readonly DependencyProperty Ellipse1BrushProperty =
        DependencyProperty.Register(nameof(Ellipse1Brush), typeof(Brush), typeof(HeimdallrPulseEllipse), new PropertyMetadata(Brushes.LightBlue));
  #endregion

  #region 두 번째 원 색상지정
  /// <summary>
  /// 두 번째 원의 색상을 설정하는 속성
  /// </summary>
  public Brush Ellipse2Brush
  {
    get => (Brush)GetValue(Ellipse2BrushProperty);
    set => SetValue(Ellipse2BrushProperty, value);
  }
  /// <summary>
  /// 두 번째 원의 색상을 설정하는 속성
  /// </summary>
  public static readonly DependencyProperty Ellipse2BrushProperty =
      DependencyProperty.Register(nameof(Ellipse2Brush), typeof(Brush), typeof(HeimdallrPulseEllipse), new PropertyMetadata(Brushes.LightGreen));
  #endregion

  #region 세 번째 원 색상지정
  /// <summary>
  /// 세 번째 원의 색상을 설정하는 속성
  /// </summary>
  public Brush Ellipse3Brush
  {
    get => (Brush)GetValue(Ellipse3BrushProperty);
    set => SetValue(Ellipse3BrushProperty, value);
  }
  /// <summary>
  /// 세 번째 원의 색상을 설정하는 속성
  /// </summary>
  public static readonly DependencyProperty Ellipse3BrushProperty =
      DependencyProperty.Register(nameof(Ellipse3Brush), typeof(Brush), typeof(HeimdallrPulseEllipse), new PropertyMetadata(Brushes.LightCoral));
  #endregion

  #region HeimdallrIcon 색상지정
  /// <summary>
  /// HeimdallrIcon 의 색상을 설정하는 속성
  /// </summary>
  public Brush IconFill
  {
    get => (Brush)GetValue(IconFillProperty);
    set => SetValue(IconFillProperty, value);
  }
  /// <summary>
  /// 아이콘의 색상을 설정하는 속성
  /// </summary>
  public static readonly DependencyProperty IconFillProperty =
      DependencyProperty.Register(nameof(IconFill), typeof(Brush), typeof(HeimdallrPulseEllipse), new PropertyMetadata(Brushes.Orange));
  #endregion

  #region Heimdallr PathIcon 지정
  /// <summary>
  /// Heimdallr 아이콘의 경로를 지정하는 속성
  /// </summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }
  /// <summary>
  /// Heimdallr 아이콘의 경로를 지정하는 속성
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
        DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrPulseEllipse), new PropertyMetadata(PathIconType.None));

  #endregion
}

