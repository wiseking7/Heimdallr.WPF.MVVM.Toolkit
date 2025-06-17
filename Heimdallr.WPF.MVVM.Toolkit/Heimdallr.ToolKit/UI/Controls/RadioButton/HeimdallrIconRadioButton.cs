using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrUniFieldRadioButton은 ModernWpf의 RadioButtons를 상속받아
/// </summary>
public class HeimdallrIconRadioButton : RadioButton
{
  static HeimdallrIconRadioButton()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrIconRadioButton),
        new FrameworkPropertyMetadata(typeof(HeimdallrIconRadioButton)));
  }

  #region CornerRadius
  /// <summary>
  /// 버튼의 모서리 둥글기를 설정하는 속성
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }
  /// <summary>
  /// 버튼의 모서리 둥글기를 설정하는 속성
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius),
        typeof(HeimdallrIconRadioButton), new PropertyMetadata(null));
  #endregion

  #region IconFill
  /// <summary>
  /// 아이콘의 색상을 설정하는 속성
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
      DependencyProperty.Register(nameof(IconFill), typeof(Brush),
        typeof(HeimdallrIconRadioButton), new PropertyMetadata(Brushes.Black));
  #endregion

  #region PathIcon
  /// <summary>
  /// PathGeometry 리소스를 사용하여 아이콘을 표시하는 속성
  /// </summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }
  /// <summary>
  /// XAML에서 직접 등록된 PathGeometry 리소스를 사용하는 방식
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType),
        typeof(HeimdallrIconRadioButton), new PropertyMetadata(PathIconType.None));
  #endregion

  #region RadioButton 선택시 Icon 색상 변경
  /// <summary>
  /// 
  /// </summary>
  public Brush CheckedForeground
  {
    get => (Brush)GetValue(CheckedForegroundProperty);
    set => SetValue(CheckedForegroundProperty, value);
  }
  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty CheckedForegroundProperty =
      DependencyProperty.Register(nameof(CheckedForeground), typeof(Brush), typeof(HeimdallrIconRadioButton),
        new PropertyMetadata(Brushes.Red));
  #endregion

  #region RadioButton Mouse Overred
  /// <summary>
  /// 
  /// </summary>
  public Brush MouseOverForeground
  {
    get => (Brush)GetValue(MouseOverForegroundProperty);
    set => SetValue(MouseOverForegroundProperty, value);
  }
  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty MouseOverForegroundProperty =
      DependencyProperty.Register(nameof(MouseOverForeground), typeof(Brush), typeof(HeimdallrIconRadioButton),
        new PropertyMetadata(Brushes.Orange));
  #endregion
}
