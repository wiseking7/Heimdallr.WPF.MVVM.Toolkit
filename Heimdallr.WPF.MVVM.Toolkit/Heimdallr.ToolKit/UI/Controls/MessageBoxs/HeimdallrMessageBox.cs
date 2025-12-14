using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 커스터마이징 가능한 메시지 박스 컨트롤.
/// </summary>
public class HeimdallrMessageBox : Control
{
  // 정적 생성자: 이 클래스의 스타일 메타데이터를 설정합니다.
  static HeimdallrMessageBox()
  {
    // DefaultStyleKeyProperty를 오버라이드하여, 이 컨트롤이 사용할 기본 스타일을 설정합니다.
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrMessageBox),
        new FrameworkPropertyMetadata(typeof(HeimdallrMessageBox)));
  }

  // 의존성 프로퍼티들 선언
  // 각 의존성 프로퍼티는 XAML에서 바인딩을 지원하고 스타일, 트리거 등을 활용할 수 있습니다.

  /// <summary>
  /// 메시지 텍스트
  /// </summary>
  public static readonly DependencyProperty MessageProperty =
      DependencyProperty.Register("Message", typeof(string), typeof(HeimdallrMessageBox), new PropertyMetadata(string.Empty));

  /// <summary>
  /// 메시지 박스 제목
  /// </summary>
  public static readonly DependencyProperty TitleProperty =
      DependencyProperty.Register("Title", typeof(string), typeof(HeimdallrMessageBox), new PropertyMetadata(string.Empty));

  /// <summary>
  /// 아이콘 경로
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register("PathIcon", typeof(PathIconType), typeof(HeimdallrMessageBox), new PropertyMetadata(PathIconType.None));

  /// <summary>
  /// 아이콘의 가시성 (아이콘이 표시될지 말지를 결정)
  /// </summary>
  public static readonly DependencyProperty IconVisibilityProperty =
      DependencyProperty.Register("IconVisibility", typeof(Visibility), typeof(HeimdallrMessageBox), new PropertyMetadata(Visibility.Collapsed));

  /// <summary>
  /// 버튼에 대한 커맨드들 (각 버튼 클릭 시 실행할 동작)
  /// </summary>
  public static readonly DependencyProperty YesCommandProperty =
      DependencyProperty.Register("YesCommand", typeof(ICommand), typeof(HeimdallrMessageBox), new PropertyMetadata(null));

  /// <summary>
  /// 버튼에 대한 커맨드들 (각 버튼 클릭 시 실행할 동작)
  /// </summary>
  public static readonly DependencyProperty NoCommandProperty =
      DependencyProperty.Register("NoCommand", typeof(ICommand), typeof(HeimdallrMessageBox), new PropertyMetadata(null));

  /// <summary>
  /// 버튼에 대한 커맨드들 (각 버튼 클릭 시 실행할 동작)
  /// </summary>
  public static readonly DependencyProperty OkCommandProperty =
      DependencyProperty.Register("OkCommand", typeof(ICommand), typeof(HeimdallrMessageBox), new PropertyMetadata(null));

  /// <summary>
  /// 버튼에 대한 커맨드들 (각 버튼 클릭 시 실행할 동작)
  /// </summary>
  public static readonly DependencyProperty CancelCommandProperty =
      DependencyProperty.Register("CancelCommand", typeof(ICommand), typeof(HeimdallrMessageBox), new PropertyMetadata(null));

  #region 각 프로퍼티들은 의존성 프로퍼티를 래핑하여 실제 값을 가져오고 설정합니다.
  /// <summary>
  /// 메세지 박스에 표시될 메시지 텍스트
  /// </summary>
  public string Message
  {
    get => (string)GetValue(MessageProperty);
    set => SetValue(MessageProperty, value);
  }

  /// <summary>
  /// 타이틀 텍스트
  /// </summary>
  public string Title
  {
    get => (string)GetValue(TitleProperty);
    set => SetValue(TitleProperty, value);
  }

  /// <summary>
  /// 아이콘 경로  
  /// </summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }

  /// <summary>
  /// 아이콘의 가시성 (아이콘이 표시될지 말지를 결정)
  /// </summary>
  public Visibility IconVisibility
  {
    get => (Visibility)GetValue(IconVisibilityProperty);
    set => SetValue(IconVisibilityProperty, value);
  }

  /// <summary>
  /// Yes 버튼 클릭 시 실행될 커맨드
  /// </summary>
  public ICommand YesCommand
  {
    get => (ICommand)GetValue(YesCommandProperty);
    set => SetValue(YesCommandProperty, value);
  }

  /// <summary>
  /// No 버튼 클릭 시 실행될 커맨드
  /// </summary>
  public ICommand NoCommand
  {
    get => (ICommand)GetValue(NoCommandProperty);
    set => SetValue(NoCommandProperty, value);
  }

  /// <summary>
  /// Ok 버튼 클릭 시 실행될 커맨드
  /// </summary>
  public ICommand OkCommand
  {
    get => (ICommand)GetValue(OkCommandProperty);
    set => SetValue(OkCommandProperty, value);
  }

  /// <summary>
  /// Cancel 버튼 클릭 시 실행될 커맨드  
  /// </summary>
  public ICommand CancelCommand
  {
    get => (ICommand)GetValue(CancelCommandProperty);
    set => SetValue(CancelCommandProperty, value);
  }
  #endregion

  /// <summary>
  /// 템플릿이 적용된 후 호출되는 메서드로, 템플릿 내에서 필요한 UI 요소들을 상호작용할 수 있습니다.
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    // 템플릿에 정의된 버튼들과 상호작용할 수 있도록 필요시 여기서 추가 로직을 작성할 수 있습니다.
  }

  /// <summary>
  /// 정적 메서드: 메시지 박스를 보여주는 역할을 합니다.
  /// </summary>
  /// <param name="message"></param>
  /// <param name="title"></param>
  /// <param name="buttons"></param>
  /// <param name="icon"></param>
  /// <returns></returns>
  // 이 메서드는 메시지와 제목, 아이콘, 버튼 설정 등을 받아 메시지 박스를 표시합니다.
  public static MessageBoxResult Show(string message, string title, MessageBoxButton buttons, MessageBoxImage icon)
  {
    // 메시지 박스를 구성할 HeimdallrMessageBox 인스턴스 생성
    var control = new HeimdallrMessageBox
    {
      Message = message,
      Title = title,
    };

    // 버튼 클릭 시 처리할 커맨드 설정
    control.YesCommand = new DelegateCommand(() => control.OnYesClicked());
    control.NoCommand = new DelegateCommand(() => control.OnNoClicked());
    control.OkCommand = new DelegateCommand(() => control.OnOkClicked());
    control.CancelCommand = new DelegateCommand(() => control.OnCancelClicked());

    // 아이콘 설정 (MessageBoxImage에 따라 알맞은 아이콘 경로 설정)
    control.PathIcon = GetIconPath(icon);
    control.IconVisibility = icon == MessageBoxImage.None ? Visibility.Collapsed : Visibility.Visible;

    // 메시지 박스를 표시할 윈도우 생성
    var window = new Window
    {
      Content = control,
      SizeToContent = SizeToContent.WidthAndHeight,  // 콘텐츠에 맞게 윈도우 크기 자동 조정
      WindowStartupLocation = WindowStartupLocation.CenterScreen,  // 화면 중앙에 윈도우 표시
      ShowInTaskbar = false,  // 작업 표시줄에 윈도우 표시 안함
      ResizeMode = ResizeMode.CanResize  // 윈도우 크기 조정 불가
    };

    // 다이얼로그 형태로 윈도우를 표시하고, 사용자가 버튼을 클릭할 때까지 대기
    window.ShowDialog();

    // 사용자가 클릭한 버튼의 결과 반환
    return control.Result;
  }

  // 각 버튼 클릭 시 처리할 동작들 (버튼별 클릭 핸들러)
  private void OnYesClicked() { Result = MessageBoxResult.Yes; CloseWindow(); }
  private void OnNoClicked() { Result = MessageBoxResult.No; CloseWindow(); }
  private void OnOkClicked() { Result = MessageBoxResult.OK; CloseWindow(); }
  private void OnCancelClicked() { Result = MessageBoxResult.Cancel; CloseWindow(); }

  // 아이콘 경로를 반환하는 헬퍼 메서드 (MessageBoxImage와 PathIconType을 매핑)
  private static PathIconType GetIconPath(MessageBoxImage icon)
  {
    // MessageBoxImage 타입에 맞는 PathIconType 반환
    return icon switch
    {
      MessageBoxImage.Information => PathIconType.Information,
      MessageBoxImage.Warning => PathIconType.Warning,
      MessageBoxImage.Question => PathIconType.Question,
      MessageBoxImage.Error => PathIconType.Error,
      _ => PathIconType.None,  // 기본값: 아이콘 없음
    };
  }

  /// <summary>
  /// 메시지 박스의 결과를 저장하는 프로퍼티
  /// </summary>
  public MessageBoxResult Result { get; private set; }

  // 버튼 클릭 후 윈도우를 닫는 메서드
  private void CloseWindow()
  {
    // 현재 활성화된 윈도우를 닫습니다.
    if (Application.Current.MainWindow != null && Application.Current.MainWindow is Window mainWindow)
    {
      mainWindow.Close();  // 윈도우를 닫습니다.
    }
  }
}

