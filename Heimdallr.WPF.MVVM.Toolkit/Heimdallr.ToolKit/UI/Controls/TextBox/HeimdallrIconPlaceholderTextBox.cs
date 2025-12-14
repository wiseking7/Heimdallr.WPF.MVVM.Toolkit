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
  #region Fields
  /// <summary>
  /// 내부에서 실제 입력을 처리하는 TextBox
  /// </summary>
  private TextBox? _textBox;
  #endregion

  #region Constructor & DefaultStyle
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
  public HeimdallrIconPlaceholderTextBox() { }

  /// <summary>
  /// 컨트롤 템플릿 적용 시 내부 요소 바인딩 및 이벤트 연결
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    _textBox = GetTemplateChild("PART_TextBox") as TextBox;
    if (_textBox != null)
    {
      // 초기 TextAlignment 적용
      _textBox.TextAlignment = TextAlignment;

      // TextBox 내용 변경 시 Text 속성 및 HasText 갱신
      _textBox.TextChanged += (s, e) =>
      {
        if (Text != _textBox.Text)
        {
          Text = _textBox.Text;
          HasText = !string.IsNullOrEmpty(_textBox.Text);
        }
      };
    }
  }
  #endregion

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
         typeof(HeimdallrIconPlaceholderTextBox),
         new FrameworkPropertyMetadata(new CornerRadius(0)));
  #endregion

  #region Text Property
  /// <summary>
  /// 사용자 입력 텍스트
  /// </summary>
  public string Text
  {
    get => (string)GetValue(TextProperty);
    set => SetValue(TextProperty, value);
  }

  /// <summary>
  /// 기본값 없음, 콜백 메서드 OnTextChanged 
  /// </summary>
  public static readonly DependencyProperty TextProperty =
      DependencyProperty.Register(nameof(Text), typeof(string), typeof(HeimdallrIconPlaceholderTextBox),
          new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged));

  /// <summary>
  /// 텍스트 변경시 
  /// </summary>
  /// <param name="d">HeimdallrIconPlaceholderTextBox 인스턴스</param>
  /// <param name="e">변경된 값 정보</param>
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
  #endregion

  #region Placeholder Properties
  /// <summary>
  /// 입력 전 표시할 안내 텍스트 (Placeholder)
  /// </summary>
  public string Placeholder
  {
    get => (string)GetValue(PlaceholderProperty);
    set => SetValue(PlaceholderProperty, value);
  }

  /// <summary>
  /// 기본값 없음
  /// </summary>
  public static readonly DependencyProperty PlaceholderProperty =
      DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(string.Empty));

  /// <summary>
  /// Placeholder 색상지정
  /// </summary>
  public Brush PlaceholderForeground
  {
    get => (Brush)GetValue(PlaceholderForegroundProperty);
    set => SetValue(PlaceholderForegroundProperty, value);
  }

  /// <summary>
  /// 기본값 #AAAAAA
  /// </summary>
  public static readonly DependencyProperty PlaceholderForegroundProperty =
      DependencyProperty.Register(nameof(PlaceholderForeground), typeof(Brush), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AAAAAA"))));
  #endregion

  #region Internal State
  /// <summary>
  /// 현재 입력 텍스트가 있는지 여부
  /// </summary>
  public bool HasText
  {
    get => (bool)GetValue(HasTextProperty);
    private set => SetValue(HasTextPropertyKey, value);
  }

  /// <summary>
  /// 현재 입력된 텍스트가 있는지 여부를 나타내는 읽기 전용 속성 으로 수정하면 좋습니다.
  /// </summary>
  private static readonly DependencyPropertyKey HasTextPropertyKey =
      DependencyProperty.RegisterReadOnly(nameof(HasText), typeof(bool), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(false));

  /// <summary>
  /// HasTextPropertyKey.DependencyProperty
  /// </summary>
  public static readonly DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;
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
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrIconPlaceholderTextBox),
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
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AAAAAA"))));
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
          typeof(HeimdallrIconPlaceholderTextBox), new PropertyMetadata(25.0));
  #endregion

  #region Caret & Border Properties
  /// <summary>
  /// 커서 색상지정
  /// </summary>
  public Brush CaretBrush
  {
    get => (Brush)GetValue(CaretBrushProperty);
    set => SetValue(CaretBrushProperty, value);
  }

  /// <summary>
  /// 커서 색상지정 속성
  /// </summary>
  public static readonly DependencyProperty CaretBrushProperty =
      DependencyProperty.Register(nameof(CaretBrush), typeof(Brush), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"))));

  /// <summary>
  /// 마우스오버시 색상지정
  /// </summary>
  public Brush MouseOverBorderBrush
  {
    get => (Brush)GetValue(MouseOverBorderBrushProperty);
    set => SetValue(MouseOverBorderBrushProperty, value);
  }

  /// <summary>
  /// 마우스오버시 색상지정 속성
  /// </summary>
  public static readonly DependencyProperty MouseOverBorderBrushProperty =
      DependencyProperty.Register(nameof(MouseOverBorderBrush), typeof(Brush),
          typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF58F84"))));

  /// <summary>
  /// 포커스시 보더브러시 지정
  /// </summary>
  public Brush FocusedBorderBrush
  {
    get => (Brush)GetValue(FocusedBorderBrushProperty);
    set => SetValue(FocusedBorderBrushProperty, value);
  }

  /// <summary>
  /// 포커스시 보더브러시 지정 속성
  /// </summary>
  public static readonly DependencyProperty FocusedBorderBrushProperty =
      DependencyProperty.Register(nameof(FocusedBorderBrush), typeof(Brush),
          typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF58F84"))));
  #endregion

  #region TextAlignment Property
  /// <summary>
  /// 텍스트 입력 정렬 (사용자가 XAML에서 지정 가능)
  /// 커서 및 입력 텍스트 위치를 좌/우로 조정
  /// </summary>
  public TextAlignment TextAlignment
  {
    get => (TextAlignment)GetValue(TextAlignmentProperty);
    set => SetValue(TextAlignmentProperty, value);
  }

  /// <summary>
  /// 텍스트 입력 정렬
  /// </summary>
  public static readonly DependencyProperty TextAlignmentProperty =
      DependencyProperty.Register(nameof(TextAlignment), typeof(TextAlignment), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(TextAlignment.Left, OnTextAlignmentChanged));

  // 파일: HeimdallrIconPlaceholderTextBox.cs (클래스 내부에 추가) 오류발생
  #region Input-related DependencyProperties
  /// <summary>
  /// 입력 내용에 대해 줄바꿈 동작을 제어합니다.
  /// TextWrapping.NoWrap  : 줄바꿈 없음(한 줄).
  /// TextWrapping.Wrap    : 너비에 맞춰 줄바꿈.
  /// TextWrapping.WrapWithOverflow : 너비 초과 시에도 동작(대부분 Wrap과 동일 사용).
  /// 
  /// 템플릿에서 TextBox의 TextWrapping 속성과 TemplateBinding으로 연결되어야 동작합니다:
  /// &lt;TextBox TextWrapping="{TemplateBinding TextWrapping}" ... /&gt;
  /// </summary>
  public TextWrapping TextWrapping
  {
    get => (TextWrapping)GetValue(TextWrappingProperty);
    set => SetValue(TextWrappingProperty, value);
  }

  /// <summary>
  /// 속성 기본값 NoWrap
  /// </summary>
  public static readonly DependencyProperty TextWrappingProperty =
      DependencyProperty.Register(nameof(TextWrapping), typeof(TextWrapping),
          typeof(HeimdallrIconPlaceholderTextBox), new PropertyMetadata(TextWrapping.NoWrap));

  /// <summary>
  /// Enter(줄바꿈)을 허용할지 여부입니다.
  /// - true : 엔터로 줄 내림(멀티라인)이 가능. TextWrapping과 함께 사용되는 경우가 많습니다.
  /// - false: Enter는 기본적으로 포커스 이동(또는 다른 동작)으로 처리됩니다.
  /// 
  /// 템플릿에서 TextBox의 AcceptsReturn에 TemplateBinding 으로 바인딩하세요.
  /// </summary>
  public bool AcceptsReturn
  {
    get => (bool)GetValue(AcceptsReturnProperty);
    set => SetValue(AcceptsReturnProperty, value);
  }

  /// <summary>
  /// 기본값 한줄모드 false
  /// </summary>
  public static readonly DependencyProperty AcceptsReturnProperty =
      DependencyProperty.Register(nameof(AcceptsReturn), typeof(bool),
          typeof(HeimdallrIconPlaceholderTextBox), new PropertyMetadata(false));

  /// <summary>
  /// 탭(Tab) 입력을 허용할지 여부입니다.
  /// - true : 탭 문자를 입력(탭키가 포커스 이동 대신 텍스트에 삽입됨).
  /// - false: 탭키는 기본적으로 포커스 이동에 사용됩니다.
  /// 
  /// 주로 멀티라인 텍스트 편집기에서 사용됩니다.
  /// 템플릿에서 TextBox의 AcceptsTab에 TemplateBinding 하세요.
  /// </summary>
  public bool AcceptsTab
  {
    get => (bool)GetValue(AcceptsTabProperty);
    set => SetValue(AcceptsTabProperty, value);
  }

  /// <summary>
  /// 기본값 속성
  /// </summary>
  public static readonly DependencyProperty AcceptsTabProperty =
      DependencyProperty.Register(nameof(AcceptsTab), typeof(bool),
          typeof(HeimdallrIconPlaceholderTextBox), new PropertyMetadata(false));

  /// <summary>
  /// 입력 필드의 읽기 전용 여부입니다.
  /// - true : 텍스트 선택은 가능하지만 수정 불가.
  /// - false: 일반 편집 가능.
  /// 
  /// 템플릿에서 TextBox의 IsReadOnly에 TemplateBinding 하여 사용합니다.
  /// </summary>
  public bool IsReadOnly
  {
    get => (bool)GetValue(IsReadOnlyProperty);
    set => SetValue(IsReadOnlyProperty, value);
  }

  /// <summary>
  /// 기본값 false
  /// </summary>
  public static readonly DependencyProperty IsReadOnlyProperty =
      DependencyProperty.Register(nameof(IsReadOnly), typeof(bool),
          typeof(HeimdallrIconPlaceholderTextBox), new PropertyMetadata(false));

  /// <summary>
  /// 텍스트 입력 길이의 최대 문자 수를 지정합니다.
  /// - 기본값 0 : 제한 없음 (TextBox의 기본 동작과 동일).
  /// - 설정된 값 > 0 : 그 수만큼 문자를 허용하고 초과 입력은 차단합니다.
  /// 
  /// 템플릿에서는 TextBox.MaxLength에 TemplateBinding 하세요.
  /// </summary>
  public int MaxLength
  {
    get => (int)GetValue(MaxLengthProperty);
    set => SetValue(MaxLengthProperty, value);
  }

  /// <summary>
  /// 기본값  0 = 제한 없음 (TextBox 규칙과 동일)
  /// </summary>
  public static readonly DependencyProperty MaxLengthProperty =
      DependencyProperty.Register(nameof(MaxLength), typeof(int),
          typeof(HeimdallrIconPlaceholderTextBox), new PropertyMetadata(0));

  /// <summary>
  /// 입력 문자 대소문자 자동 변환 동작을 지정합니다.
  /// - CharacterCasing.Normal  : 입력 그대로.
  /// - CharacterCasing.Upper   : 모두 대문자.
  /// - CharacterCasing.Lower   : 모두 소문자.
  /// 
  /// 템플릿에서 TextBox의 CharacterCasing에 TemplateBinding 하세요.
  /// (CharacterCasing 열거형은 System.Windows.Controls 네임스페이스에 있습니다)
  /// </summary>
  public CharacterCasing CharacterCasing
  {
    get => (CharacterCasing)GetValue(CharacterCasingProperty);
    set => SetValue(CharacterCasingProperty, value);
  }

  /// <summary>
  /// 기본값 Normal
  /// </summary>
  public static readonly DependencyProperty CharacterCasingProperty =
      DependencyProperty.Register(nameof(CharacterCasing), typeof(CharacterCasing),
          typeof(HeimdallrIconPlaceholderTextBox), new PropertyMetadata(CharacterCasing.Normal));

  #endregion

  /// <summary>
  /// 텍스트 입력시 위치 조정
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnTextAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var control = (HeimdallrIconPlaceholderTextBox)d;
    if (control._textBox != null)
    {
      control._textBox.TextAlignment = (TextAlignment)e.NewValue;
    }
  }
  #endregion
}

