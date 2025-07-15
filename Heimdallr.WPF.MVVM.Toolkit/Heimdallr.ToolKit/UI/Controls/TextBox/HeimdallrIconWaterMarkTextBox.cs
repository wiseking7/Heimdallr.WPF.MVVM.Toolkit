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
  /// PathIconType 아이콘을 지정하는 종속성 속성.
  /// XAML 스타일에서 PathIcon을 그려서 표시 가능.
  /// </summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }

  /// <summary>
  /// 기본값은 PathIconType.None이며, 아이콘이 없는 상태입니다.
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
  /// 기본값은 빈 문자열이며, 워터마크가 표시되지 않습니다.
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
  /// 기본값은 회색(Gray)이며, 워터마크 텍스트의 색상을 지정합니다.
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
  /// 기본값은 회색(Gray)이며, 아이콘의 채우기 색상을 지정합니다.
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
  /// 기본값은 CornerRadius(0)이며, 모서리 반경을 설정합니다.
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
    DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(HeimdallrIconWaterMarkTextBox),
      new PropertyMetadata(new CornerRadius(0)));
  #endregion

  #region AutoGrow (자동 크기 조절 여부)
  /// <summary>
  /// 텍스트 내용에 따라 자동으로 높이를 조절할지 여부를 결정하는 종속성 속성.
  /// </summary>
  public bool AutoGrow
  {
    get => (bool)GetValue(AutoGrowProperty);
    set => SetValue(AutoGrowProperty, value);
  }
  /// <summary>
  /// 기본값은 false이며, 텍스트 내용에 따라 높이를 자동으로 조절하지 않습니다.
  /// </summary>
  public static readonly DependencyProperty AutoGrowProperty =
    DependencyProperty.Register(nameof(AutoGrow), typeof(bool), typeof(HeimdallrIconWaterMarkTextBox),
        new PropertyMetadata(false));
  #endregion

  #region FocusFill (포커스 시 배경색)
  /// <summary>
  /// FocusFill 속성은 텍스트 박스가 포커스를 받을 때 적용되는 배경색을 지정합니다.
  /// </summary>
  public Brush FocusFill
  {
    get => (Brush)GetValue(FocusFillProperty);
    set => SetValue(FocusFillProperty, value);
  }
  /// <summary>
  /// 기본값은 흰색(White)이며, 포커스가 있을 때 텍스트 박스의 배경색을 지정합니다.
  /// </summary>
  public static readonly DependencyProperty FocusFillProperty =
      DependencyProperty.Register(nameof(FocusFill), typeof(Brush), typeof(HeimdallrIconWaterMarkTextBox),
          new PropertyMetadata(Brushes.White));
  #endregion

  private TextBox? _innerTextBox;

  /// <summary>
  /// 템플릿이 적용될 때 내부 TextBox (PART_TextBox)를 찾아서 AutoGrow 처리를 위한 TextChanged 이벤트 등록
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    _innerTextBox = GetTemplateChild("PART_TextBox") as TextBox;

    if (_innerTextBox != null)
    {
      // AutoGrow가 true일 때만 텍스트 변경 시 높이 계산 수행
      _innerTextBox.TextChanged += (s, e) =>
      {
        if (AutoGrow)
          UpdateHeightByInnerText();
      };
    }
  }

  /// <summary>
  /// 내부 TextBox 기준으로 텍스트 높이를 계산해 외부 컨트롤 높이를 늘림
  /// </summary>
  private void UpdateHeightByInnerText()
  {
    if (_innerTextBox == null)
      return;

    // 가로 폭이 아직 0일 경우 측정 보류
    if (_innerTextBox.ActualWidth <= 0)
      return;

    // 내부 TextBox의 원하는 크기 측정
    _innerTextBox.Measure(new Size(_innerTextBox.ActualWidth, double.PositiveInfinity));

    // 원하는 높이를 계산 (Padding 포함)
    double desiredHeight = _innerTextBox.DesiredSize.Height + this.Padding.Top + this.Padding.Bottom;

    // 기존 MinHeight와 비교하여 더 큰 값으로 설정
    double newMinHeight = Math.Max(this.MinHeight, desiredHeight);

    // MaxHeight가 설정되어 있다면 그보다 크지 않도록 제한
    if (this.MaxHeight > 0)
      newMinHeight = Math.Min(this.MaxHeight, newMinHeight);

    this.MinHeight = newMinHeight;
  }
}

