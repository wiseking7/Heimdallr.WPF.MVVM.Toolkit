using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;


/// <summary>
/// 아이콘과 워터마크(Placeholder)를 지원하는 커스텀 TextBox 컨트롤입니다.
/// 기본 TextBox를 상속하며, PathIcon 타입 아이콘과 워터마크 텍스트, 색상 등의 속성을 제공합니다.
/// </summary>
public class HeimdallrIconWaterMarkTextBox : TextBox
{
  /// <summary>
  /// 정적 생성자: 이 컨트롤의 기본 스타일 키를 이 타입으로 지정합니다.
  /// 이를 통해 Themes/Generic.xaml 등에 정의된 기본 스타일이 적용됩니다.
  /// </summary>
  static HeimdallrIconWaterMarkTextBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrIconWaterMarkTextBox),
      new FrameworkPropertyMetadata(typeof(HeimdallrIconWaterMarkTextBox)));
  }

  #region PathIcon
  /// <summary>
  /// PathIconType 아이콘을 지정하는 의존성 속성.
  /// 이 속성에 따라 XAML 스타일에서 PathIcon을 그려서 표시 가능.
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
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType),
          typeof(HeimdallrIconWaterMarkTextBox), new PropertyMetadata(PathIconType.None));
  #endregion

  #region WaterMark 문자열
  /// <summary>
  /// 워터마크(Placeholder) 텍스트를 지정하는 의존성 속성.
  /// 텍스트박스가 비어있을 때 화면에 흐릿하게 표시하는 안내 텍스트용.
  /// </summary>
  public string WaterMark
  {
    get { return (string)GetValue(WaterMarkProperty); }
    set { SetValue(WaterMarkProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty WaterMarkProperty =
      DependencyProperty.Register(nameof(WaterMark), typeof(string), typeof(HeimdallrIconWaterMarkTextBox),
        new PropertyMetadata(string.Empty));
  #endregion

  #region WaterMarkForeground 색상 
  /// <summary>
  /// 워터마크 텍스트의 전경색(색상)을 지정하는 의존성 속성.
  /// 기본값은 회색(Gray)이며, 워터마크 텍스트 색상을 조절할 수 있음.
  /// </summary>
  public Brush WaterMarkForeground
  {
    get { return (Brush)GetValue(WaterMarkForegroundProperty); }
    set { SetValue(WaterMarkForegroundProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty WaterMarkForegroundProperty =
      DependencyProperty.Register(nameof(WaterMarkForeground), typeof(Brush), typeof(HeimdallrIconWaterMarkTextBox),
        new PropertyMetadata(Brushes.Gray));
  #endregion

  #region Fill
  /// <summary>
  /// 아이콘 등의 채우기 색상을 지정하는 의존성 속성.
  /// 기본적으로 회색(Gray)이 설정되어 있으며, 아이콘의 색상을 바꾸는 데 사용.
  /// </summary>
  public Brush Fill
  {
    get { return (Brush)GetValue(FillProperty); }
    set { SetValue(FillProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrIconWaterMarkTextBox),
        new PropertyMetadata(Brushes.Gray));
  #endregion

  #region
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
    DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(HeimdallrIconWaterMarkTextBox),
      new PropertyMetadata(new CornerRadius(0)));
  #endregion

  #region
  /// <summary>
  /// AutoGrow 속성은 텍스트 박스가 입력된 내용에 따라 자동으로 크기를 조절할지 여부를 결정합니다.
  /// </summary>
  public static readonly DependencyProperty AutoGrowProperty =
    DependencyProperty.Register(nameof(AutoGrow), typeof(bool), typeof(HeimdallrIconWaterMarkTextBox),
        new PropertyMetadata(false));
  /// <summary>
  /// AutoGrow 속성은 텍스트 박스가 입력된 내용에 따라 자동으로 크기를 조절할지 여부를 결정합니다.
  /// </summary>
  public bool AutoGrow
  {
    get => (bool)GetValue(AutoGrowProperty);
    set => SetValue(AutoGrowProperty, value);
  }
  #endregion

  #region
  /// <summary>
  /// FocusFill 속성은 텍스트 박스가 포커스를 받을 때 적용되는 배경색을 지정합니다.
  /// </summary>
  public Brush FocusFill
  {
    get => (Brush)GetValue(FocusFillProperty);
    set => SetValue(FocusFillProperty, value);
  }
  /// <summary>
  /// FocusFill 속성은 텍스트 박스가 포커스를 받을 때 적용되는 배경색을 지정합니다.
  /// </summary>
  public static readonly DependencyProperty FocusFillProperty =
      DependencyProperty.Register(nameof(FocusFill), typeof(Brush), typeof(HeimdallrIconWaterMarkTextBox),
          new PropertyMetadata(Brushes.White)); // 기본 포커스 색상 지정 가능
  #endregion


  /// <summary>
  /// 텍스트가 변경될 때 자동으로 높이를 조정합니다.
  /// </summary>
  /// <param name="e"></param>
  protected override void OnTextChanged(TextChangedEventArgs e)
  {
    base.OnTextChanged(e);

    if (AutoGrow)
    {
      UpdateHeightByText();
    }
  }
  /// <summary>
  /// 텍스트가 변경될 때 자동으로 높이를 조정합니다.
  /// </summary>
  private void UpdateHeightByText()
  {
    var formattedText = new FormattedText(
        this.Text + " ", // 공백 추가해서 마지막 줄 포함
        System.Globalization.CultureInfo.CurrentCulture,
        FlowDirection.LeftToRight,
        new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch),
        this.FontSize,
        Brushes.Black,
        VisualTreeHelper.GetDpi(this).PixelsPerDip);

    formattedText.MaxTextWidth = this.ActualWidth - this.Padding.Left - this.Padding.Right - 10;

    double newHeight = formattedText.Height + this.Padding.Top + this.Padding.Bottom + 10;

    // 높이 제한 고려 (MinHeight ~ MaxHeight)
    newHeight = Math.Max(this.MinHeight, newHeight);
    if (this.MaxHeight > 0) newHeight = Math.Min(this.MaxHeight, newHeight);

    this.Height = newHeight;
  }
}

