using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// InformationWindow는 정보를 표시하는 기본 클래스 역할을 하는 WPF 창입니다.
/// </summary>
public class InformationWindow : Window
{
  static InformationWindow()
  {
    // 이 클래스에 대한 기본 스타일 키 지정
    DefaultStyleKeyProperty.OverrideMetadata(typeof(InformationWindow),
        new FrameworkPropertyMetadata(typeof(InformationWindow)));

    // 닫기 커맨드 의존성 속성 등록
    CloseCommandProperty = DependencyProperty.Register(
      nameof(CloseCommand),
      typeof(ICommand),
      typeof(InformationWindow), // ← 여기 중요: 'DarkThemeWindow' → 'InformationWindow' 로 수정
      new PropertyMetadata(null));
  }

  /// <summary>
  /// 생성자 - 창 속성 초기화
  /// </summary>
  public InformationWindow()
  {
    WindowStartupLocation = WindowStartupLocation.CenterScreen;
    ResizeMode = ResizeMode.NoResize;
    SizeToContent = SizeToContent.WidthAndHeight;
    MinWidth = 350;
    MinHeight = 350;
    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF435585"));

    WindowStyle = WindowStyle.None;
    AllowsTransparency = true;
  }

  /// <summary>
  /// 닫기 버튼 클릭 시 연결되는 ICommand
  /// ViewModel에서 명령을 바인딩하거나 내부 기본 Close 동작 사용 가능
  /// </summary>
  public static readonly DependencyProperty CloseCommandProperty;

  /// <summary>
  /// 닫기 커맨드 - ViewModel에서 바인딩하거나 내부 Close 동작 사용
  /// </summary>
  public ICommand CloseCommand
  {
    get => (ICommand)GetValue(CloseCommandProperty);
    set => SetValue(CloseCommandProperty, value);
  }

  /// <summary>
  /// 템플릿 적용 후 버튼 등 컨트롤과 이벤트 연결 처리
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    // 닫기 버튼을 템플릿에서 찾아 클릭 이벤트 연결
    if (GetTemplateChild("PART_CloseButton") is System.Windows.Controls.Button btn)
    {
      btn.Click += (s, e) => WindowClose();
    }

    // 현재 시간/날짜 텍스트 블록 찾기
    if (GetTemplateChild("TextBlockCurrentTime") is TextBlock timeBlock)
    {
      _textBlockCurrentTime = timeBlock;
    }

    if (GetTemplateChild("TextBlockCurrentDate") is TextBlock dateBlock)
    {
      _textBlockCurrentDate = dateBlock;
    }

    StartClock();
  }

  /// <summary>
  /// 닫기 동작 처리 - 커맨드가 있으면 실행, 없으면 Close 호출
  /// </summary>
  private void WindowClose()
  {
    if (CloseCommand != null && CloseCommand.CanExecute(this))
    {
      CloseCommand.Execute(this);
    }
    else
    {
      Close();
    }
  }

  #region Date/Time
  private DispatcherTimer? _timer;
  private TextBlock? _textBlockCurrentTime;
  private TextBlock? _textBlockCurrentDate;

  /// <summary>
  /// 현재 시간과 날짜를 표시하는 타이머 시작
  /// </summary>
  private void StartClock()
  {
    _timer = new DispatcherTimer
    {
      Interval = TimeSpan.FromSeconds(1)
    };
    _timer.Tick += (s, e) =>
    {
      if (_textBlockCurrentTime != null)
      {
        var time = DateTime.Now.ToString("HH:mm:ss");

        if (_textBlockCurrentTime?.Inlines.LastInline is Run run)
        {
          run.Text = time;
        }
      }

      if (_textBlockCurrentDate != null)
      {
        var today = DateTime.Now;
        var dayOfWeek = GetKoreanDayOfWeek(today.DayOfWeek);

        if (_textBlockCurrentDate?.Inlines.LastInline is Run run)
        {
          run.Text = $"{today:yyyy-MM-dd} ({dayOfWeek})";
        }
      }
    };
    _timer.Start();
  }

  /// <summary>
  /// 요일을 한국어로 변환하는 메서드
  /// </summary>
  /// <param name="day"></param>
  /// <returns></returns>
  private string GetKoreanDayOfWeek(DayOfWeek day)
  {
    return day switch
    {
      DayOfWeek.Sunday => "일",
      DayOfWeek.Monday => "월",
      DayOfWeek.Tuesday => "화",
      DayOfWeek.Wednesday => "수",
      DayOfWeek.Thursday => "목",
      DayOfWeek.Friday => "금",
      DayOfWeek.Saturday => "토",
      _ => ""
    };
  }

  /// <summary>
  /// 창이 닫힐 때 타이머 중지
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosed(EventArgs e)
  {
    _timer?.Stop();
    base.OnClosed(e);
  }
  #endregion
}

