using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// DarkThemeWindow 클래스는 WPF 기반의 커스텀 창을 정의하며,
/// 기본 윈도우 기능(닫기, 최소화, 최대화, 드래그 이동)뿐 아니라
/// 다크 테마, 디밍(어두워짐) 처리, 태스크바 표시 여부 제어 기능을 포함합니다.
/// HeimdallrWindow를 상속하여 커스텀 스타일과 동작을 확장한 윈도우입니다.
/// </summary>
public class DarkThemeWindow : HeimdallrWindow
{
  #region 의존성 속성(DependencyProperty) 선언
  // WPF의 바인딩과 스타일, 트리거에 활용할 수 있는 의존성 속성 선언 부분입니다.

  /// <summary>
  /// 팝업 열림 상태를 나타내는 속성 (bool)
  /// XAML에서 바인딩하여 팝업 UI 상태 제어에 활용 가능
  /// </summary>
  public static readonly DependencyProperty PopupOpenProperty;

  /// <summary>
  /// 타이틀 헤더 영역의 배경색을 설정하는 Brush 타입 속성
  /// 다크 테마에 맞는 기본 색상을 지정 가능
  /// </summary>
  public static readonly DependencyProperty TitleHeaderBackgroundProperty;

  /// <summary>
  /// 닫기 버튼 클릭 시 실행할 ICommand 명령 속성
  /// MVVM 패턴에서 ViewModel 명령과 연결하여 동작 처리 가능
  /// </summary>
  public static readonly DependencyProperty CloseCommandProperty;

  /// <summary>
  /// 윈도우 타이틀을 커스텀 오브젝트(보통 문자열)로 지정하는 속성
  /// Window.Title 속성을 숨기고 대신 사용
  /// </summary>
  public static readonly new DependencyProperty TitleProperty;

  /// <summary>
  /// 최대화 시 태스크바를 포함하여 창 크기 제한 여부를 지정하는 bool 속성
  /// </summary>
  public static readonly DependencyProperty IsShowTaskBarProperty;

  /// <summary>
  /// 디밍(어두워짐) 효과 활성화 여부
  /// </summary>
  public static readonly DependencyProperty DimmingProperty;

  /// <summary>
  /// 디밍 배경색 Brush 속성
  /// </summary>
  public static readonly DependencyProperty DimmingColorProperty;

  /// <summary>
  /// 디밍 효과의 투명도(double) 설정
  /// </summary>
  public static readonly DependencyProperty DimmingOpacityProperty;
  #endregion

  #region CLR 래퍼 프로퍼티
  // 의존성 속성을 편리하게 사용할 수 있도록 하는 일반 프로퍼티 래퍼

  /// <summary>
  /// 윈도우 타이틀을 나타냅니다.
  /// 기본 Window.Title 대신 이 속성을 사용하여 XAML 바인딩 가능
  /// </summary>
  public new object Title
  {
    get => GetValue(TitleProperty);
    set => SetValue(TitleProperty, value);
  }

  /// <summary>
  /// 닫기 버튼 클릭 시 실행할 커맨드
  /// 커맨드가 없으면 기본 닫기 동작 실행
  /// </summary>
  public ICommand CloseCommand
  {
    get => (ICommand)GetValue(CloseCommandProperty);
    set => SetValue(CloseCommandProperty, value);
  }

  /// <summary>
  /// 타이틀 헤더 배경색 Brush
  /// </summary>
  public Brush TitleHeaderBackground
  {
    get => (Brush)GetValue(TitleHeaderBackgroundProperty);
    set => SetValue(TitleHeaderBackgroundProperty, value);
  }

  /// <summary>
  /// 최대화 시 태스크바 포함 여부를 지정 (true: 포함, false: 태스크바 영역 무한 확장)
  /// </summary>
  public bool IsShowTaskBar
  {
    get => (bool)GetValue(IsShowTaskBarProperty);
    set => SetValue(IsShowTaskBarProperty, value);
  }

  /// <summary>
  /// 팝업 열기/닫기 상태 (bool)
  /// 팝업 UI의 상태를 바인딩하여 제어하는데 사용
  /// </summary>
  public bool PopupOpen
  {
    get => (bool)GetValue(PopupOpenProperty);
    set => SetValue(PopupOpenProperty, value);
  }

  /// <summary>
  /// 디밍 효과 활성화 여부
  /// </summary>
  public bool Dimming
  {
    get => (bool)GetValue(DimmingProperty);
    set => SetValue(DimmingProperty, value);
  }

  /// <summary>
  /// 디밍 배경색 Brush
  /// </summary>
  public Brush DimmingColor
  {
    get => (Brush)GetValue(DimmingColorProperty);
    set => SetValue(DimmingColorProperty, value);
  }

  /// <summary>
  /// 디밍 효과 투명도 (0.0 ~ 1.0)
  /// </summary>
  public double DimmingOpacity
  {
    get => (double)GetValue(DimmingOpacityProperty);
    set => SetValue(DimmingOpacityProperty, value);
  }
  #endregion

  // 최대화 버튼 참조 (템플릿에서 찾음)
  private MaximizeButton? maximBtn;

  /// <summary>
  /// 정적 생성자: 의존성 속성 등록, 기본 스타일 키 설정
  /// AllowsTransparency와 WindowStyle을 조합해 투명하고 커스텀 타이틀 바를 구현
  /// </summary>
  static DarkThemeWindow()
  {
    // 이 타입에 대한 기본 스타일 키를 설정하여 Themes/Generic.xaml에서 스타일을 찾게 함
    DefaultStyleKeyProperty.OverrideMetadata(typeof(DarkThemeWindow),
      new FrameworkPropertyMetadata(typeof(DarkThemeWindow)));

    // 의존성 속성 등록
    CloseCommandProperty = DependencyProperty.Register(nameof(CloseCommand), typeof(ICommand), typeof(DarkThemeWindow),
      new PropertyMetadata(null));

    TitleProperty = DependencyProperty.Register(nameof(Title), typeof(object), typeof(DarkThemeWindow),
      new UIPropertyMetadata(null));

    TitleHeaderBackgroundProperty = DependencyProperty.Register(nameof(TitleHeaderBackground), typeof(Brush), typeof(DarkThemeWindow),
      new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#252525"))));

    DimmingProperty = DependencyProperty.Register(nameof(Dimming), typeof(bool), typeof(DarkThemeWindow),
      new PropertyMetadata(false, (d, e) =>
      {
        // 디밍 활성화 상태 변경 시 필요한 동작을 넣을 수 있음
      }));

    DimmingColorProperty = DependencyProperty.Register(nameof(DimmingColor), typeof(Brush), typeof(DarkThemeWindow),
      new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#141414"))));

    DimmingOpacityProperty = DependencyProperty.Register(nameof(DimmingOpacity), typeof(double), typeof(DarkThemeWindow),
      new PropertyMetadata(0.8, OnDimmingOpacityChanged));

    IsShowTaskBarProperty = DependencyProperty.Register(nameof(IsShowTaskBar), typeof(bool), typeof(DarkThemeWindow),
      new PropertyMetadata(true, (d, e) =>
      {
        var win = (DarkThemeWindow)d;
        win.MaxHeightSet(); // 값 변경 시 최대 높이 재설정
      }));

    PopupOpenProperty = DependencyProperty.Register(nameof(PopupOpen), typeof(bool), typeof(DarkThemeWindow),
      new PropertyMetadata(false, OnPopupOpenChanged));
  }

  /// <summary>
  /// 디밍 투명도 변경시 호출되는 콜백
  /// BlurEffect의 반경(radius)를 변경함으로써 디밍 효과 조절
  /// </summary>
  private static void OnDimmingOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is DarkThemeWindow window && window._dimmingEffect != null)
    {
      window._dimmingEffect.Radius = (double)e.NewValue;
    }
  }

  /// <summary>
  /// 팝업 열림 상태 변경 시 호출되는 콜백
  /// 실제 팝업 열기/닫기 동작을 여기서 구현 가능
  /// </summary>
  private static void OnPopupOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is DarkThemeWindow window)
    {
      bool isOpen = (bool)e.NewValue;
      if (isOpen)
      {
        // 팝업 열기 관련 로직 삽입 가능
      }
      else
      {
        // 팝업 닫기 관련 로직 삽입 가능
      }
    }
  }

  /// <summary>
  /// 생성자: 창의 기본 특성 설정 및 이벤트 핸들러 등록
  /// </summary>
  public DarkThemeWindow()
  {
    // 최대 높이를 태스크바 포함 여부에 따라 설정
    MaxHeightSet();

    // 투명창 + 스타일 없음 => 커스텀 타이틀 영역을 XAML에서 만들 수 있음
    this.AllowsTransparency = true;
    this.WindowStyle = WindowStyle.None;

    // 창 위치를 화면 중앙으로 초기화
    this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

    // 창 상태 변화 이벤트 등록 (최대화 버튼 상태 동기화용)
    this.StateChanged += DarkThemeWindow_StateChanged;
  }

  /// <summary>
  /// 창 상태가 변경될 때마다 최대화 버튼 상태(IsMaximize)를 변경해 UI 동기화
  /// </summary>
  private void DarkThemeWindow_StateChanged(object? sender, EventArgs e)
  {
    if (maximBtn != null)
    {
      maximBtn.IsMaximize = this.WindowState == WindowState.Maximized;
    }
  }

  private BlurEffect? _dimmingEffect;

  /// <summary>
  /// ControlTemplate이 적용된 후 템플릿 내부 요소를 찾아 이벤트 및 효과 바인딩 처리
  /// PART_ 접두어가 붙은 이름은 템플릿 파트 규칙에 따라 XAML에서 정의된 요소 이름
  /// </summary>
  public override void OnApplyTemplate()
  {
    // 닫기 버튼 찾고 클릭 이벤트 등록 (CloseCommand 실행 또는 기본 Close 호출)
    if (GetTemplateChild("PART_CloseButton") is CloseButton btn)
    {
      btn.Click += (s, e) => WindowClose();
    }

    // 최소화 버튼 클릭 시 창 상태를 최소화로 변경
    if (GetTemplateChild("PART_MinButton") is MinimizeButton minbtn)
    {
      minbtn.Click += (s, e) => WindowState = WindowState.Minimized;
    }

    // 최대화 버튼 클릭 시 창 상태 토글 (최대화/기본)
    if (GetTemplateChild("PART_MaxButton") is MaximizeButton maxbtn)
    {
      maximBtn = maxbtn;
      maxbtn.Click += (s, e) =>
      {
        this.WindowState = maxbtn.IsMaximize ? WindowState.Normal : WindowState.Maximized;
      };
    }

    // 드래그 바(MouseDown 시 창 이동)
    if (GetTemplateChild("PART_DragBar") is DraggableBar bar)
    {
      bar.MouseDown += WindowDragMove;
    }

    // 디밍 효과 처리: 템플릿 내 PART_Dimming 요소에 BlurEffect 적용
    if (this.Template.FindName("PART_Dimming", this) is UIElement dimmingElement)
    {
      _dimmingEffect = new BlurEffect
      {
        Radius = this.DimmingOpacity,
        KernelType = KernelType.Gaussian
      };

      dimmingElement.Effect = _dimmingEffect;
    }
  }

  /// <summary>
  /// 닫기 버튼 클릭 시 호출, CloseCommand가 있으면 실행, 없으면 기본 Close 호출
  /// </summary>
  private void WindowClose()
  {
    if (CloseCommand == null)
    {
      Close();
    }
    else
    {
      CloseCommand.Execute(this);
    }
  }

  /// <summary>
  /// 드래그바 클릭 후 마우스 왼쪽 버튼을 누른 상태에서 창 이동(DragMove)
  /// </summary>
  private void WindowDragMove(object sender, MouseButtonEventArgs e)
  {
    if (e.LeftButton == MouseButtonState.Pressed)
    {
      GetWindow(this).DragMove();
    }
  }

  /// <summary>
  /// 최대화 시 창 최대 높이를 태스크바 영역 포함 여부에 따라 설정
  /// true면 최대화 시 태스크바 위로 창이 올라가지 않고, false면 무한 확장
  /// </summary>
  private void MaxHeightSet()
  {
    this.MaxHeight = IsShowTaskBar ? SystemParameters.MaximizedPrimaryScreenHeight : Double.PositiveInfinity;
  }
}

/* BlurEffect 와 DimmingOpacity가 필요한가? 
 
1. DarkThemeWindow는 다크 테마 기반의 사용자 정의 WPF 창입니다.
   이 안에서 Dimming이라는 기능은:
   * 창의 콘텐츠 위에 어두운 반투명 레이어를 씌워 배경을 흐리게 보이게 하는 효과입니다.
   * 팝업 창이 열릴 때 배경을 흐릿하게 처리하여 집중 효과를 주는 UI 패턴으로 자주 사용됩니다.
   * BlurEffect는 시각적 흐림 효과를 추가하는 WPF 내장 이펙트입니다.

💡 즉, Dimming 기능은 사용자가 어떤 작업(예: 팝업, 모달 대화상자 등)에 집중하도록 UI를 흐리게 만들기 위해 사용합니다.

2. BlurEffect와 DimmingOpacity의 관계
   * BlurEffect.Radius: 흐림 정도를 숫자로 조절합니다 (값이 클수록 더 흐림).
   * DimmingOpacity: 레이어의 투명도 (0.0 = 투명, 1.0 = 완전 불투명)
   * 일반적으로 이 두 값을 조합해서 어두워지고 흐려진 배경 효과를 만들 수 있습니다.

3. 왜 XAML에서 바인딩하지 않고 코드(C#)에서 처리해야 하는가?
   * XAML에서는 문제가 발생함
     - BlurEffect는 Freezable 개체입니다.
     - Freezable 은 DataContext 또는 RelativeSource, ElementName 같은 바인딩에 제약이 있어 XAML에서 동적 바인딩이 거의 불가능합니다.
       <!-- 이런 건 안 됨: 런타임 오류 발생 -->
       <BlurEffect Radius="{Binding Opacity, ElementName=PART_Dimming}" />

4. 해결: C# 코드에서 직접 적용
   * 그래서 우리는 코드에서 다음과 같이 처리해야 합니다:
     - 템플릿이 적용된 후 (OnApplyTemplate)에 PART_Dimming를 찾고
       BlurEffect를 생성해 거기에 DimmingOpacity를 반영하고
       나중에 DimmingOpacity가 변경되면 다시 Radius 값을 업데이트합니다.

 */