using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrFlatButton: 평면 스타일의 사용자 정의 버튼입니다.
/// 마우스 오버, 클릭, 포커스, 비활성화 상태에 따른 시각적 효과를 지원합니다.
/// </summary>
public class HeimdallrFlatButton : Button
{
  /// <summary>
  /// 정적 생성자: 기본 스타일 키를 재정의하여
  /// XAML에서 해당 스타일이 자동으로 적용되도록 설정합니다.
  /// </summary>
  static HeimdallrFlatButton()
  {
    // DefaultStyleKey를 재정의하여 XAML에서 스타일을 사용할 수 있게 설정
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrFlatButton),
        new FrameworkPropertyMetadata(typeof(HeimdallrFlatButton)));
  }

  #region Dependency Properties

  /// <summary>
  /// 마우스 오버 시 버튼 배경색을 지정합니다.
  /// 기본값은 Transparent입니다.
  /// </summary>
  public Brush MouseOverBackground
  {
    get => (Brush)GetValue(MouseOverBackgroundProperty);
    set => SetValue(MouseOverBackgroundProperty, value);
  }
  /// <summary>
  /// 기본값은 Transparent입니다.
  /// </summary>
  public static readonly DependencyProperty MouseOverBackgroundProperty =
      DependencyProperty.Register(nameof(MouseOverBackground), typeof(Brush), typeof(HeimdallrFlatButton),
          new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

  /// <summary>
  /// 클릭(Pressed) 상태 시 버튼 배경색을 지정합니다.
  /// 기본값은 Transparent입니다.
  /// </summary>
  public Brush PressedBackground
  {
    get => (Brush)GetValue(PressedBackgroundProperty);
    set => SetValue(PressedBackgroundProperty, value);
  }
  /// <summary>
  /// 기본값은 Transparent입니다.
  /// </summary>
  public static readonly DependencyProperty PressedBackgroundProperty =
      DependencyProperty.Register(nameof(PressedBackground), typeof(Brush), typeof(HeimdallrFlatButton),
          new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

  /// <summary>
  /// 마우스 오버 시 버튼 테두리 색상을 지정합니다.
  /// 기본값은 Transparent입니다.
  /// </summary>
  public Brush MouseOverBorderBrush
  {
    get => (Brush)GetValue(MouseOverBorderBrushProperty);
    set => SetValue(MouseOverBorderBrushProperty, value);
  }
  /// <summary>
  /// 기본값은 Transparent입니다.
  /// </summary>
  public static readonly DependencyProperty MouseOverBorderBrushProperty =
      DependencyProperty.Register(nameof(MouseOverBorderBrush), typeof(Brush), typeof(HeimdallrFlatButton),
          new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

  /// <summary>
  /// 클릭(Pressed) 상태 시 버튼 테두리 색상을 지정합니다.
  /// 기본값은 Transparent입니다.
  /// </summary>
  public Brush PressedBorderBrush
  {
    get => (Brush)GetValue(PressedBorderBrushProperty);
    set => SetValue(PressedBorderBrushProperty, value);
  }
  /// <summary>
  /// 기본값은 Transparent입니다.
  /// </summary>
  public static readonly DependencyProperty PressedBorderBrushProperty =
      DependencyProperty.Register(nameof(PressedBorderBrush), typeof(Brush), typeof(HeimdallrFlatButton),
          new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

  /// <summary>
  /// 포커스 상태 시 버튼 테두리 색상을 지정합니다.
  /// 기본값은 Transparent입니다.
  /// </summary>
  public Brush FocusedBorderBrush
  {
    get => (Brush)GetValue(FocusedBorderBrushProperty);
    set => SetValue(FocusedBorderBrushProperty, value);
  }
  /// <summary>
  /// 기본값은 Transparent입니다.
  /// </summary>
  public static readonly DependencyProperty FocusedBorderBrushProperty =
      DependencyProperty.Register(nameof(FocusedBorderBrush), typeof(Brush), typeof(HeimdallrFlatButton),
          new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

  /// <summary>
  /// 포커스 상태 시 버튼 테두리 두께를 지정합니다.
  /// 기본값은 Thickness(1)입니다.
  /// </summary>
  public Thickness FocusedBorderThickness
  {
    get => (Thickness)GetValue(FocusedBorderThicknessProperty);
    set => SetValue(FocusedBorderThicknessProperty, value);
  }
  /// <summary>
  /// 기본값은 Thickness(1)입니다.
  /// </summary>
  public static readonly DependencyProperty FocusedBorderThicknessProperty =
      DependencyProperty.Register(nameof(FocusedBorderThickness), typeof(Thickness), typeof(HeimdallrFlatButton),
          new PropertyMetadata(new Thickness(1)));

  /// <summary>
  /// 
  /// </summary>
  public Brush DisabledBackground
  {
    get => (Brush)GetValue(DisabledBackgroundProperty);
    set => SetValue(DisabledBackgroundProperty, value);
  }
  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty DisabledBackgroundProperty =
      DependencyProperty.Register(nameof(DisabledBackground), typeof(Brush), typeof(HeimdallrFlatButton),
          new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA))));

  /// <summary>
  /// 
  /// </summary>
  public Brush DisabledBorderBrush
  {
    get => (Brush)GetValue(DisabledBorderBrushProperty);
    set => SetValue(DisabledBorderBrushProperty, value);
  }

  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty DisabledBorderBrushProperty =
      DependencyProperty.Register(nameof(DisabledBorderBrush), typeof(Brush), typeof(HeimdallrFlatButton),
          new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0x88, 0x88, 0x88))));

  /// <summary>
  /// 비활성화 상태에서 글자색을 지정합니다.
  /// </summary>
  public Brush DisabledForeground
  {
    get => (Brush)GetValue(DisabledForegroundProperty);
    set => SetValue(DisabledForegroundProperty, value);
  }

  /// <summary>
  /// 기본값 그레이
  /// </summary>
  public static readonly DependencyProperty DisabledForegroundProperty =
      DependencyProperty.Register(nameof(DisabledForeground), typeof(Brush), typeof(HeimdallrFlatButton),
          new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

  #endregion

  #region CornerRadius Property

  /// <summary>
  /// 버튼의 테두리 둥근 정도를 설정합니다.
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }

  /// <summary>
  /// CornerRadius에 대한 종속성 속성 정의.
  /// 기본값은 (0)입니다.
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(HeimdallrFlatButton),
          new PropertyMetadata(new CornerRadius(0)));

  #endregion
}



