using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Heimdallr 스타일의 Placeholder 텍스트 입력 컨트롤 (TextBox 기반 커스텀 컨트롤)
/// 좌측 아이콘 + Placeholder 지원 기능 포함
/// </summary>
public class HeimdallrIconPlaceholderTextBox : Control
{
  /// <summary>
  /// 내부에서 실제 입력을 처리하는 TextBox
  /// </summary>
  private TextBox? _textBox;

  /// <summary>
  /// 기본 스타일 키 등록 (Generic.xaml에서 템플릿 정의 필요)
  /// </summary>
  static HeimdallrIconPlaceholderTextBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrIconPlaceholderTextBox),
        new FrameworkPropertyMetadata(typeof(HeimdallrIconPlaceholderTextBox)));
  }

  /// <summary>
  /// 기본 생성자
  /// </summary>
  public HeimdallrIconPlaceholderTextBox()
  {
  }

  /// <summary>
  /// 컨트롤 템플릿 적용 시 내부 요소 바인딩 및 이벤트 연결
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    _textBox = GetTemplateChild("PART_TextBox") as TextBox;

    if (_textBox != null)
    {
      // 초기 텍스트 설정
      _textBox.Text = Text;

      // TextBox 내용이 바뀔 때 Text 속성 및 HasText 갱신
      _textBox.TextChanged += (s, e) =>
      {
        Text = _textBox.Text;
        HasText = !string.IsNullOrEmpty(_textBox.Text);
      };
    }
  }

  //====================== Text 바인딩 ======================//
  /// <summary>
  /// 사용자 입력 텍스트
  /// </summary>
  public string Text
  {
    get => (string)GetValue(TextProperty);
    set => SetValue(TextProperty, value);
  }

  /// <summary>
  /// Text 속성의 DependencyProperty 정의
  /// </summary>
  public static readonly DependencyProperty TextProperty =
      DependencyProperty.Register(nameof(Text), typeof(string), typeof(HeimdallrIconPlaceholderTextBox),
          new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged));

  /// <summary>
  /// Text 속성 변경 시 내부 TextBox와 동기화
  /// </summary>
  /// <param name="d">HeimdallrIconPlaceholderTextBox</param>
  /// <param name="e">NewValue</param>
  private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var control = (HeimdallrIconPlaceholderTextBox)d;
    if (control._textBox != null && control._textBox.Text != (string)e.NewValue)
    {
      control._textBox.Text = (string)e.NewValue;
    }

    // Text 변경에 따라 HasText 갱신
    control.HasText = !string.IsNullOrEmpty((string)e.NewValue);
  }

  //====================== Placeholder 속성 ======================//
  /// <summary>
  /// 입력 전 표시할 안내 텍스트 (Placeholder)
  /// </summary>
  public string Placeholder
  {
    get => (string)GetValue(PlaceholderProperty);
    set => SetValue(PlaceholderProperty, value);
  }

  /// <summary>
  /// Placeholder 속성의 DependencyProperty 정의
  /// </summary>
  public static readonly DependencyProperty PlaceholderProperty =
      DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(string.Empty));

  /// <summary>
  /// Placeholder 텍스트의 색상
  /// </summary>
  public Brush PlaceholderForeground
  {
    get => (Brush)GetValue(PlaceholderForegroundProperty);
    set => SetValue(PlaceholderForegroundProperty, value);
  }

  /// <summary>
  /// PlaceholderForeground 속성의 DependencyProperty 정의
  /// </summary>
  public static readonly DependencyProperty PlaceholderForegroundProperty =
      DependencyProperty.Register(nameof(PlaceholderForeground), typeof(Brush), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AAAAAA"))));

  //====================== 내부 상태 ======================//
  /// <summary>
  /// 현재 입력 텍스트가 있는지 여부
  /// </summary>
  public bool HasText
  {
    get => (bool)GetValue(HasTextProperty);
    private set => SetValue(HasTextPropertyKey, value);
  }

  /// <summary>
  /// HasText 속성의 읽기 전용 DependencyPropertyKey 정의
  /// </summary>
  private static readonly DependencyPropertyKey HasTextPropertyKey =
      DependencyProperty.RegisterReadOnly(nameof(HasText), typeof(bool), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(false));

  /// <summary>
  /// HasText 속성의 DependencyProperty 정의 읽기전용
  /// </summary>
  public static readonly DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;

  //====================== 아이콘 관련 ======================//
  /// <summary>
  /// 좌측에 표시할 PathIcon 종류
  /// </summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }

  /// <summary>
  /// PathIcon 속성의 DependencyProperty 정의
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(PathIconType.None));

  /// <summary>
  /// 아이콘의 색상 (PathIcon의 Fill 브러시)
  /// </summary>
  public Brush Fill
  {
    get => (Brush)GetValue(FillProperty);
    set => SetValue(FillProperty, value);
  }

  /// <summary>
  /// Fill 속성의 DependencyProperty 정의
  /// </summary>
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AAAAAA"))));

  /// <summary>
  /// 커서 색상
  /// </summary>
  public Brush CaretBrush
  {
    get => (Brush)GetValue(CaretBrushProperty);
    set => SetValue(CaretBrushProperty, value);
  }

  /// <summary>
  /// 기본값: 검은색
  /// </summary>
  public static readonly DependencyProperty CaretBrushProperty =
      DependencyProperty.Register(nameof(CaretBrush), typeof(Brush), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"))));
}
