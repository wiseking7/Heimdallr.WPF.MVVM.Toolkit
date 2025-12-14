using Heimdallr.ToolKit.Enums;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Heimdallr 스타일의 우측 아이콘 텍스트박스 커스텀 컨트롤입니다.
/// 
/// 특징:
/// - Placeholder 텍스트 지원
/// - 우측 아이콘 버튼 지원 (예: 정보 아이콘)
/// - 엔티티 속성의 Description 표시 기능
/// - 텍스트 입력 시 Placeholder 자동 숨김
/// </summary>
public class HeimdallrRightIconTextBox : Control
{
  #region Fields
  /// <summary>내부 TextBox 참조</summary>
  private TextBox? _textBox;

  /// <summary>엔티티 Description을 표시할 TextBlock 참조</summary>
  private TextBlock? _descriptionTextBlock;

  /// <summary>우측 버튼(아이콘) 참조</summary>
  private Button? _rightButton;

  /// <summary>Placeholder TextBlock 참조</summary>
  private TextBlock? _placeholderTextBlock;
  #endregion

  #region Constructor
  static HeimdallrRightIconTextBox()
  {
    // 기본 스타일 키를 이 컨트롤로 지정
    DefaultStyleKeyProperty.OverrideMetadata(
        typeof(HeimdallrRightIconTextBox),
        new FrameworkPropertyMetadata(typeof(HeimdallrRightIconTextBox)));
  }
  #endregion

  #region Properties
  /// <summary>Placeholder 텍스트</summary>
  public string Placeholder
  {
    get => (string)GetValue(PlaceholderProperty);
    set => SetValue(PlaceholderProperty, value);
  }

  /// <summary>Placeholder 텍스트 색상</summary>
  public Brush PlaceholderForeground
  {
    get => (Brush)GetValue(PlaceholderForegroundProperty);
    set => SetValue(PlaceholderForegroundProperty, value);
  }

  /// <summary>왼쪽 아이콘 종류</summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }

  /// <summary>우측 아이콘 종류</summary>
  public PathIconType RightPathIcon
  {
    get => (PathIconType)GetValue(RightPathIconProperty);
    set => SetValue(RightPathIconProperty, value);
  }

  /// <summary>왼쪽 아이콘 색상</summary>
  public Brush Fill
  {
    get => (Brush)GetValue(FillProperty);
    set => SetValue(FillProperty, value);
  }

  /// <summary>우측 아이콘 색상</summary>
  public Brush RightFill
  {
    get => (Brush)GetValue(RightFillProperty);
    set => SetValue(RightFillProperty, value);
  }

  /// <summary>텍스트 입력 커서 색상</summary>
  public Brush CaretBrush
  {
    get => (Brush)GetValue(CaretBrushProperty);
    set => SetValue(CaretBrushProperty, value);
  }

  /// <summary>컨트롤의 모서리 둥글기</summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }

  /// <summary>텍스트박스 입력 텍스트</summary>
  public string Text
  {
    get => (string)GetValue(TextProperty);
    set => SetValue(TextProperty, value);
  }

  /// <summary>텍스트가 입력되어 있는지 여부</summary>
  public bool HasText
  {
    get => (bool)GetValue(HasTextProperty);
    private set => SetValue(HasTextPropertyKey, value);
  }

  /// <summary>커맨드 실행 시 전달할 파라미터</summary>
  public object? CommandParameter
  {
    get => GetValue(CommandParameterProperty);
    set => SetValue(CommandParameterProperty, value);
  }

  /// <summary>엔티티 타입, Description을 가져올 대상</summary>
  public Type? EntityType
  {
    get => (Type?)GetValue(EntityTypeProperty);
    set => SetValue(EntityTypeProperty, value);
  }
  /// <summary>
  /// 오른쪽 아이콘 색상변경 클릭시
  /// </summary>
  public Brush RightPressedFill
  {
    get => (Brush)GetValue(RightPressedFillProperty);
    set => SetValue(RightPressedFillProperty, value);
  }
  #endregion

  #region DescriptionParamer
  /// <summary>
  /// Entity 의 Description 의 값을 가져옵니다.
  /// </summary>
  public string? DescriptionParameter
  {
    get => (string?)GetValue(DescriptionParameterProperty);
    set => SetValue(DescriptionParameterProperty, value);
  }

  /// <summary>
  /// 기본값은 null 입니다.
  /// </summary>
  public static readonly DependencyProperty DescriptionParameterProperty =
      DependencyProperty.Register(nameof(DescriptionParameter), typeof(string),
          typeof(HeimdallrRightIconTextBox), new PropertyMetadata(null));
  #endregion

  #region Dependency Properties
  /// <summary>
  /// Placeholder 관련
  /// </summary>
  public static readonly DependencyProperty PlaceholderProperty =
      DependencyProperty.Register(nameof(Placeholder), typeof(string),
          typeof(HeimdallrRightIconTextBox),
          new PropertyMetadata(string.Empty));

  /// <summary>
  /// PlaceHolderForeground
  /// </summary>
  public static readonly DependencyProperty PlaceholderForegroundProperty =
      DependencyProperty.Register(nameof(PlaceholderForeground), typeof(Brush),
          typeof(HeimdallrRightIconTextBox),
          new PropertyMetadata(Brushes.Gray));

  /// <summary>
  /// PahtIcon
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType),
          typeof(HeimdallrRightIconTextBox),
          new PropertyMetadata(PathIconType.None));

  /// <summary>
  /// RightPathIon
  /// </summary>
  public static readonly DependencyProperty RightPathIconProperty =
      DependencyProperty.Register(nameof(RightPathIcon), typeof(PathIconType),
          typeof(HeimdallrRightIconTextBox),
          new PropertyMetadata(PathIconType.None));

  /// <summary>
  /// Fill
  /// </summary>
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush),
          typeof(HeimdallrRightIconTextBox),
          new PropertyMetadata(Brushes.Gray));

  /// <summary>
  /// RgithPathIcon Fill
  /// </summary>
  public static readonly DependencyProperty RightFillProperty =
      DependencyProperty.Register(nameof(RightFill), typeof(Brush),
          typeof(HeimdallrRightIconTextBox),
          new PropertyMetadata(Brushes.Gray));

  /// <summary>
  /// CareBrush
  /// </summary>
  public static readonly DependencyProperty CaretBrushProperty =
      DependencyProperty.Register(nameof(CaretBrush), typeof(Brush),
          typeof(HeimdallrRightIconTextBox),
          new PropertyMetadata(Brushes.Black));

  /// <summary>
  /// CornerRadius
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius),
          typeof(HeimdallrRightIconTextBox),
          new FrameworkPropertyMetadata());

  /// <summary>
  /// Text와 HasText
  /// </summary>
  public static readonly DependencyProperty TextProperty =
      DependencyProperty.Register(nameof(Text), typeof(string),
          typeof(HeimdallrRightIconTextBox),
          new FrameworkPropertyMetadata(string.Empty,
              FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
              OnTextChanged));

  /// <summary>
  /// HasTextPropertyKey
  /// </summary>
  private static readonly DependencyPropertyKey HasTextPropertyKey =
      DependencyProperty.RegisterReadOnly(nameof(HasText), typeof(bool),
          typeof(HeimdallrRightIconTextBox),
          new PropertyMetadata(false));

  /// <summary>
  /// HasTextProperty 
  /// </summary>
  public static readonly DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;


  /// <summary>
  /// CommandParameter 
  /// </summary>
  public static readonly DependencyProperty CommandParameterProperty =
      DependencyProperty.Register(nameof(CommandParameter), typeof(object),
          typeof(HeimdallrRightIconTextBox));

  /// <summary>
  /// 엔티티 타입
  /// </summary>
  public static readonly DependencyProperty EntityTypeProperty =
      DependencyProperty.Register(nameof(EntityType), typeof(Type),
          typeof(HeimdallrRightIconTextBox), new PropertyMetadata(null));

  /// <summary>
  /// 오른쪽 아이콘 색상 변경 기본값 DarkGray
  /// </summary>
  public static readonly DependencyProperty RightPressedFillProperty =
     DependencyProperty.Register(nameof(RightPressedFill), typeof(Brush),
         typeof(HeimdallrRightIconTextBox),
         new PropertyMetadata(Brushes.DarkGray));
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
          typeof(HeimdallrRightIconTextBox), new PropertyMetadata(20.0));

  /// <summary>
  /// 이이콘 사이즈 너비,높이
  /// </summary>
  public double IconSizeRigth
  {
    get => (double)GetValue(IconSizeRigthProperty);
    set => SetValue(IconSizeRigthProperty, value);
  }

  /// <summary>
  /// 아이콘사이즈 기본값
  /// </summary>
  public static readonly DependencyProperty IconSizeRigthProperty =
      DependencyProperty.Register(nameof(IconSizeRigth), typeof(double),
          typeof(HeimdallrRightIconTextBox), new PropertyMetadata(16.0));
  #endregion

  #region Command 추가
  /// <summary>
  /// TextBox 에서 명령어 추가
  /// </summary>
  public ICommand? RightButtonCommand
  {
    get => (ICommand?)GetValue(RightButtonCommandProperty);
    set => SetValue(RightButtonCommandProperty, value);
  }

  /// <summary>
  /// 종속성
  /// </summary>
  public static readonly DependencyProperty RightButtonCommandProperty =
      DependencyProperty.Register(
          nameof(RightButtonCommand),
          typeof(ICommand),
          typeof(HeimdallrRightIconTextBox));
  #endregion

  #region TextAlignment Property
  /// <summary>
  /// 텍스트 입력 정렬 (좌/우/가운데)
  /// </summary>
  public TextAlignment TextAlignment
  {
    get => (TextAlignment)GetValue(TextAlignmentProperty);
    set => SetValue(TextAlignmentProperty, value);
  }

  /// <summary>
  /// TextAlignment 속성
  /// </summary>
  public static readonly DependencyProperty TextAlignmentProperty =
      DependencyProperty.Register(nameof(TextAlignment), typeof(TextAlignment),
          typeof(HeimdallrRightIconTextBox), new PropertyMetadata(TextAlignment.Left));
  #endregion

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
          typeof(HeimdallrRightIconTextBox), new PropertyMetadata(TextWrapping.NoWrap));

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
          typeof(HeimdallrRightIconTextBox), new PropertyMetadata(false));

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
          typeof(HeimdallrRightIconTextBox), new PropertyMetadata(false));

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
          typeof(HeimdallrRightIconTextBox), new PropertyMetadata(false));

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
          typeof(HeimdallrRightIconTextBox), new PropertyMetadata(0));

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
          typeof(HeimdallrRightIconTextBox), new PropertyMetadata(CharacterCasing.Normal));

  #endregion

  #region Callbacks
  /// <summary>
  /// Text 속성 변경 콜백
  /// - 내부 TextBox와 동기화
  /// - HasText 업데이트
  /// - Placeholder 표시/숨김 처리
  /// </summary>
  private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var control = (HeimdallrRightIconTextBox)d;
    if (control._textBox != null && control._textBox.Text != (string)e.NewValue)
      control._textBox.Text = (string)e.NewValue;

    control.HasText = !string.IsNullOrEmpty((string)e.NewValue);

    // Placeholder 표시/숨기기
    if (control._placeholderTextBlock != null)
      control._placeholderTextBlock.Visibility = control.HasText ? Visibility.Collapsed : Visibility.Visible;
  }

  /// <summary>
  /// 지정한 엔티티 속성의 DisplayAttribute.Description을 표시
  /// </summary>
  /// <param name="propertyName">엔티티 속성 이름</param>
  public void ShowDescription(string propertyName)
  {
    if (_descriptionTextBlock == null || string.IsNullOrEmpty(propertyName) || EntityType == null)
      return;

    var prop = EntityType.GetProperty(propertyName);
    if (prop != null)
    {
      var displayAttr = prop.GetCustomAttributes(typeof(DisplayAttribute), true)
                            .FirstOrDefault() as DisplayAttribute;
      if (displayAttr != null)
      {
        // Placeholder 숨기기
        if (_placeholderTextBlock != null)
          _placeholderTextBlock.Visibility = Visibility.Collapsed;

        // Description 표시
        _descriptionTextBlock.Text = displayAttr.Description;
        _descriptionTextBlock.Visibility = Visibility.Visible;

        _ = HideDescriptionAfterDelay();
      }
    }
  }

  /// <summary>
  /// Description 표시 후 일정 시간 후 자동 숨김
  /// </summary>
  private async Task HideDescriptionAfterDelay()
  {
    await Task.Delay(3000);
    if (_descriptionTextBlock != null)
      _descriptionTextBlock.Visibility = Visibility.Collapsed;

    // Description 사라지면 Placeholder 다시 표시
    if (_placeholderTextBlock != null && string.IsNullOrEmpty(Text))
      _placeholderTextBlock.Visibility = Visibility.Visible;
  }
  #endregion

  #region Overrides
  /// <summary>
  /// 템플릿 적용 시 각 파트 컨트롤 참조 가져오기
  /// </summary>
  public override void OnApplyTemplate()
  {
    _textBox = GetTemplateChild("PART_TextBox") as TextBox;
    _descriptionTextBlock = GetTemplateChild("PART_TextBlock") as TextBlock;
    _rightButton = GetTemplateChild("PART_RightButton") as Button;
    _placeholderTextBlock = GetTemplateChild("PlaceholderTextBlock") as TextBlock;

    if (_textBox != null)
    {
      // 내부 TextBox 이벤트 연결
      _textBox.TextChanged += (s, e) =>
      {
        if (Text != _textBox.Text)
          Text = _textBox.Text;

        HasText = !string.IsNullOrEmpty(_textBox.Text);


        // Placeholder 표시/숨김
        if (_placeholderTextBlock != null)
          _placeholderTextBlock.Visibility = HasText ? Visibility.Collapsed : Visibility.Visible;
      };
    }

    if (_rightButton != null)
    {
      // 우측 버튼 클릭 시 Description 표시
      _rightButton.Click += (s, e) =>
      {
        // 1 Description 표시 (Entity 의 Description 값)
        ShowDescription(DescriptionParameter ?? "");

        // 2️ Command 실행 (CommandParameter 전달)
        if (RightButtonCommand != null && RightButtonCommand.CanExecute(CommandParameter))
        {
          RightButtonCommand.Execute(CommandParameter);
        }
      };
    }

    base.OnApplyTemplate();
  }
  #endregion
}


