using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 커스터마이징 리피트버튼
/// </summary>
public class HeimdallrRepeatButton : RepeatButton
{
  static HeimdallrRepeatButton()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrRepeatButton),
        new FrameworkPropertyMetadata(typeof(HeimdallrRepeatButton)));
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
          typeof(HeimdallrRepeatButton),
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
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrRepeatButton),
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
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrRepeatButton),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AAAAAA"))));
  #endregion

  #region IconSize
  /// <summary>
  /// 아이콘 사이즈 너비, 높이
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
          typeof(HeimdallrRepeatButton), new PropertyMetadata(20.0));
  #endregion

  #region ButtonBackgroundColor Property
  /// <summary>
  /// 버튼 배경 색상
  /// </summary>
  public Brush ButtonBackgroundColor
  {
    get => (Brush)GetValue(ButtonBackgroundColorProperty);
    set => SetValue(ButtonBackgroundColorProperty, value);
  }

  /// <summary>
  /// 버튼 배경 색상 속성
  /// </summary>
  public static readonly DependencyProperty ButtonBackgroundColorProperty =
      DependencyProperty.Register(nameof(ButtonBackgroundColor), typeof(Brush), typeof(HeimdallrRepeatButton),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF234C6A"))));
  #endregion

  #region MouseOverBackground Property
  /// <summary>
  /// 마우스 오버 시 배경색
  /// </summary>
  public Brush MouseOverBackground
  {
    get => (Brush)GetValue(MouseOverBackgroundProperty);
    set => SetValue(MouseOverBackgroundProperty, value);
  }

  /// <summary>
  /// 종속성 속성
  /// </summary>
  public static readonly DependencyProperty MouseOverBackgroundProperty =
      DependencyProperty.Register(nameof(MouseOverBackground), typeof(Brush), typeof(HeimdallrRepeatButton),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEEEEEE"))));
  #endregion

  #region PressedBackground Property
  /// <summary>
  /// 눌렀을 때 배경색
  /// </summary>
  public Brush PressedBackground
  {
    get => (Brush)GetValue(PressedBackgroundProperty);
    set => SetValue(PressedBackgroundProperty, value);
  }

  /// <summary>
  /// 종속성 속성
  /// </summary>
  public static readonly DependencyProperty PressedBackgroundProperty =
      DependencyProperty.Register(nameof(PressedBackground), typeof(Brush), typeof(HeimdallrRepeatButton),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCCCCCC"))));
  #endregion

  #region PressedForeground Property
  /// <summary>
  /// 눌렀을 때 텍스트/아이콘 색상
  /// </summary>
  public Brush PressedForeground
  {
    get => (Brush)GetValue(PressedForegroundProperty);
    set => SetValue(PressedForegroundProperty, value);
  }

  /// <summary>
  /// 종속성 속성
  /// </summary>
  public static readonly DependencyProperty PressedForegroundProperty =
      DependencyProperty.Register(nameof(PressedForeground), typeof(Brush), typeof(HeimdallrRepeatButton),
        new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF444444"))));
  #endregion


  /// <summary>
  /// 
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    Loaded += (s, e) =>
    {
      if (GetTemplateChild("Icon") is HeimdallrIcon icon)
      {
        icon.Width = ActualWidth * 0.6;
        icon.Height = ActualHeight * 0.6;
      }
    };
  }
}

