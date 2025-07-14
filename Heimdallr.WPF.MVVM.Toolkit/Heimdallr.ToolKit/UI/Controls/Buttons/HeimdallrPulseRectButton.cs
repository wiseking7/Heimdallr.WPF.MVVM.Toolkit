using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 클릭 시 사각형 애니메이션이 적용되는 버튼
/// </summary>
public class HeimdallrPulseRectButton : Button
{
  static HeimdallrPulseRectButton()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrPulseRectButton),
        new FrameworkPropertyMetadata(typeof(HeimdallrPulseRectButton)));
  }

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
        typeof(HeimdallrPulseRectButton), new PropertyMetadata(PathIconType.None));
  #endregion

  #region Text (버튼 우측에 표시할 문자열)
  /// <summary>
  /// 버튼 우측에 표시할 문자열을 설정하는 속성
  /// </summary>
  public string ButtonText
  {
    get => (string)GetValue(ButtonTextProperty);
    set => SetValue(ButtonTextProperty, value);
  }
  /// <summary>
  /// 버튼 우측에 표시할 문자열을 설정하는 속성
  /// </summary>
  public static readonly DependencyProperty ButtonTextProperty =
      DependencyProperty.Register(nameof(ButtonText), typeof(string),
        typeof(HeimdallrPulseRectButton), new PropertyMetadata(string.Empty));
  #endregion

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
        typeof(HeimdallrPulseRectButton), new PropertyMetadata(new CornerRadius(0)));
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
      DependencyProperty.Register(nameof(IconFill), typeof(Brush), typeof(HeimdallrPulseRectButton),
        new PropertyMetadata(Brushes.Black));
  #endregion

  #region MouseOverBackground
  /// <summary>
  /// 
  /// </summary>
  public Brush MouseOverBackground
  {
    get => (Brush)GetValue(MouseOverBackgroundProperty);
    set => SetValue(MouseOverBackgroundProperty, value);
  }
  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty MouseOverBackgroundProperty =
      DependencyProperty.Register(nameof(MouseOverBackground), typeof(Brush),
          typeof(HeimdallrPulseRectButton), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0x39, 0x3E, 0x46))));
  #endregion

  #region MouseOverBorderBrush
  /// <summary>
  /// 
  /// </summary>
  public Brush MouseOverBorderBrush
  {
    get => (Brush)GetValue(MouseOverBorderBrushProperty);
    set => SetValue(MouseOverBorderBrushProperty, value);
  }
  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty MouseOverBorderBrushProperty =
      DependencyProperty.Register(nameof(MouseOverBorderBrush), typeof(Brush),
          typeof(HeimdallrPulseRectButton), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0x88, 0x88, 0x88))));
  #endregion

  #region PressedBackground
  /// <summary>
  /// 
  /// </summary>
  public Brush PressedBackground
  {
    get => (Brush)GetValue(PressedBackgroundProperty);
    set => SetValue(PressedBackgroundProperty, value);
  }
  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty PressedBackgroundProperty =
      DependencyProperty.Register(nameof(PressedBackground), typeof(Brush),
          typeof(HeimdallrPulseRectButton), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0xAD, 0x49, 0xE1))));
  #endregion

  #region PressedBorderBrush
  /// <summary>
  /// 
  /// </summary>
  public Brush PressedBorderBrush
  {
    get => (Brush)GetValue(PressedBorderBrushProperty);
    set => SetValue(PressedBorderBrushProperty, value);
  }
  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty PressedBorderBrushProperty =
      DependencyProperty.Register(nameof(PressedBorderBrush), typeof(Brush),
          typeof(HeimdallrPulseRectButton), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0x66, 0x66, 0x66))));
  #endregion

  #region FocusedBorderBrush
  /// <summary>
  /// 
  /// </summary>
  public Brush FocusedBorderBrush
  {
    get => (Brush)GetValue(FocusedBorderBrushProperty);
    set => SetValue(FocusedBorderBrushProperty, value);
  }
  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty FocusedBorderBrushProperty =
      DependencyProperty.Register(nameof(FocusedBorderBrush), typeof(Brush),
          typeof(HeimdallrPulseRectButton), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0x6E, 0xAC, 0xDA))));
  #endregion

  /// <summary>
  /// 
  /// </summary>
  #region FocusedBorderThickness
  public Thickness FocusedBorderThickness
  {
    get => (Thickness)GetValue(FocusedBorderThicknessProperty);
    set => SetValue(FocusedBorderThicknessProperty, value);
  }
  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty FocusedBorderThicknessProperty =
      DependencyProperty.Register(nameof(FocusedBorderThickness), typeof(Thickness),
          typeof(HeimdallrPulseRectButton), new PropertyMetadata(new Thickness(2)));
  #endregion
}

