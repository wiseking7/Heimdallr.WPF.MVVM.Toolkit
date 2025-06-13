using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Heimdallr 스타일의 아이콘 + 워터마크 패스워드 입력 컨트롤.
/// PasswordBox와 동일하게 ● 입력 형태이며, MVVM 패턴을 위해 Password 속성을 바인딩 가능하게 확장함.
/// </summary>
public class HeimdallrIconWaterMarkPasswordBox : Control
{
  /// 비밀번호 표시/숨기기를 위한 커맨드 정의 (예: 눈 모양 아이콘 클릭 시)
  public static readonly RoutedCommand ToggleShowPasswordCommand = new RoutedCommand();

  // 템플릿에서 찾을 컨트롤들
  private PasswordBox? _passwordBox; // ● 문자로 표시되는 실제 입력용
  private TextBox? _textBox;         // 비밀번호 보기 상태일 때 사용하는 일반 텍스트 박스

  /// <summary>
  /// 생성자: Toggle 커맨드 바인딩 등록
  /// </summary>
  public HeimdallrIconWaterMarkPasswordBox()
  {
    CommandManager.RegisterClassCommandBinding(typeof(HeimdallrIconWaterMarkPasswordBox),
        new CommandBinding(ToggleShowPasswordCommand, OnToggleShowPassword));
  }

  // 커맨드 실행 시 ShowPassword 토글 처리
  private void OnToggleShowPassword(object sender, ExecutedRoutedEventArgs e)
  {
    if (sender is HeimdallrIconWaterMarkPasswordBox control)
    {
      control.ShowPassword = !control.ShowPassword;
    }
  }

  // 기본 스타일 템플릿을 연결하기 위해 오버라이드 (Generic.xaml 등)
  static HeimdallrIconWaterMarkPasswordBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrIconWaterMarkPasswordBox),
      new FrameworkPropertyMetadata(typeof(HeimdallrIconWaterMarkPasswordBox)));
  }

  //======================= WaterMark 관련 =======================//

  /// <summary>
  /// 비어 있을 때 표시되는 안내 텍스트
  /// </summary>
  public string WaterMark
  {
    get => (string)GetValue(WaterMarkProperty);
    set => SetValue(WaterMarkProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty WaterMarkProperty =
      DependencyProperty.Register(nameof(WaterMark), typeof(string), typeof(HeimdallrIconWaterMarkPasswordBox),
          new PropertyMetadata(string.Empty));

  //======================= WaterMarkForeground =======================//

  /// <summary>
  /// WaterMark 문자열 색상
  /// </summary>
  public Brush WaterMarkForeground
  {
    get => (Brush)GetValue(WaterMarkForegroundProperty);
    set => SetValue(WaterMarkForegroundProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty WaterMarkForegroundProperty =
      DependencyProperty.Register(nameof(WaterMarkForeground), typeof(Brush), typeof(HeimdallrIconWaterMarkPasswordBox),
          new PropertyMetadata(Brushes.Gray));

  //======================= HasPassword (읽기 전용) =======================//

  /// <summary>
  /// 현재 입력된 비밀번호가 있는지 여부 (워터마크 표시 조건으로 사용됨)
  /// </summary>
  public bool HasPassword
  {
    get => (bool)GetValue(HasPasswordProperty);
    private set => SetValue(HasPasswordPropertyKey, value);
  }

  // 읽기 전용 의존 속성 정의
  private static readonly DependencyPropertyKey HasPasswordPropertyKey =
      DependencyProperty.RegisterReadOnly(nameof(HasPassword), typeof(bool), typeof(HeimdallrIconWaterMarkPasswordBox),
          new PropertyMetadata(false));
  /// <summary>
  /// 현재 입력된 비밀번호가 있는지 여부 (워터마크 표시 Trigger 등에 사용)
  /// 읽기 전용 DependencyProperty 이므로 외부에서 직접 Set할 수 없음.
  /// 내부에서는 HasPasswordPropertyKey를 통해만 변경 가능.
  /// </summary>
  public static readonly DependencyProperty HasPasswordProperty = HasPasswordPropertyKey.DependencyProperty;

  //======================= 아이콘 관련 =======================//

  /// <summary>
  /// HeimdallrIcon에 표시될 PathGeometry 아이콘 타입
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
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrIconWaterMarkPasswordBox),
          new PropertyMetadata(PathIconType.None));

  /// <summary>
  /// 아이콘 색상 (HeimdallrIcon의 Fill과 바인딩됨)
  /// </summary>
  public Brush Fill
  {
    get => (Brush)GetValue(FillProperty);
    set => SetValue(FillProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrIconWaterMarkPasswordBox),
        new PropertyMetadata(Brushes.Gray));

  //======================= Password MVVM 바인딩 =======================//

  /// <summary>
  /// MVVM 바인딩을 지원하는 Password 속성 (실제 PasswordBox.Password와 동기화)
  /// </summary>
  public string Password
  {
    get => (string)GetValue(PasswordProperty);
    set => SetValue(PasswordProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty PasswordProperty =
      DependencyProperty.Register(nameof(Password), typeof(string), typeof(HeimdallrIconWaterMarkPasswordBox),
          new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordChanged));

  // ViewModel → Password 속성 변경 시 내부 PasswordBox/TextBox 값도 업데이트
  private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var control = (HeimdallrIconWaterMarkPasswordBox)d;
    var newPassword = e.NewValue as string ?? string.Empty;

    if (control._passwordBox != null && control._passwordBox.Password != newPassword)
    {
      control._passwordBox.Password = newPassword;
    }

    if (control._textBox != null && control._textBox.Text != newPassword)
    {
      control._textBox.Text = newPassword;
    }
  }

  //======================= 비밀번호 보기/숨기기 토글 =======================//

  /// <summary>
  /// 비밀번호 보기 상태 여부 (true일 경우 일반 텍스트로 표시)
  /// </summary>
  public bool ShowPassword
  {
    get => (bool)GetValue(ShowPasswordProperty);
    set => SetValue(ShowPasswordProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty ShowPasswordProperty =
      DependencyProperty.Register(nameof(ShowPassword), typeof(bool), typeof(HeimdallrIconWaterMarkPasswordBox),
          new PropertyMetadata(false));

  //======================= 템플릿 적용 시 연결 설정 =======================//

  /// <summary>
  /// 템플릿 적용 시 내부 PasswordBox, TextBox와 바인딩 설정
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    // ControlTemplate에 정의된 이름을 기준으로 내부 요소를 가져옴
    _passwordBox = GetTemplateChild("PART_PasswordBox") as PasswordBox;
    _textBox = GetTemplateChild("PART_TextBox") as TextBox;

    if (_passwordBox != null)
    {
      // 사용자가 PasswordBox에 입력할 때마다 속성 동기화
      _passwordBox.PasswordChanged += (s, e) =>
      {
        Password = _passwordBox.Password;
        HasPassword = !string.IsNullOrEmpty(_passwordBox.Password);
      };
    }

    if (_textBox != null)
    {
      // TextBox는 ShowPassword=true일 때만 보여짐
      _textBox.Text = Password;
      _textBox.TextChanged += (s, e) =>
      {
        Password = _textBox.Text;
        HasPassword = !string.IsNullOrEmpty(_textBox.Text);
      };
    }
  }
}

