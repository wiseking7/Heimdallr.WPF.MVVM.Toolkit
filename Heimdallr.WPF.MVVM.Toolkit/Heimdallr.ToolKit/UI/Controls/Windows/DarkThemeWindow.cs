using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// DarkThemeWindow 클래스는 WPF 기반의 커스텀 창을 정의하며, 일반적인 기능(닫기, 최소화, 최대화, 드래그 이동)과 
/// 함께 다크 테마, 디밍 처리(어두워짐), 태스크바 표시 여부 제어 같은 기능들을 포함
/// </summary>
public class DarkThemeWindow : HeimdallrWindow
{
  // WPF의 바인딩/스타일/트리거에 활용될 수 있도록 선언된 속성
  // 주로 뷰(View)와 XAML 리소스 바인딩을 위한 의존 속성
  public static readonly DependencyProperty PopupOpenProperty;
  public static readonly DependencyProperty TitleHeaderBackgroundProperty;
  public static readonly DependencyProperty CloseCommandProperty;
  public static readonly new DependencyProperty TitleProperty;
  public static readonly DependencyProperty IsShowTaskBarProperty;

  /// <summary>
  /// 윈도우 타이틀 (WPF Window.Title 숨김 처리)
  /// </summary>
  public new object Title
  {
    get => GetValue(TitleProperty);
    set => SetValue(TitleProperty, value);
  }

  /// <summary>
  /// 닫기 버튼 클릭 시 실행할 명령
  /// </summary>
  public ICommand CloseCommand
  {
    get => (ICommand)GetValue(CloseCommandProperty);
    set => SetValue(CloseCommandProperty, value);
  }

  /// <summary>
  /// 타이틀 헤더 배경색
  /// </summary>
  public Brush TitleHeaderBackground
  {
    get => (Brush)GetValue(TitleHeaderBackgroundProperty);
    set => SetValue(TitleHeaderBackgroundProperty, value);
  }

  /// <summary>
  /// 최대화 시 태스크바 위로 창이 올라올지 여부
  /// </summary>
  public bool IsShowTaskBar
  {
    get { return (bool)GetValue(IsShowTaskBarProperty); }
    set { SetValue(IsShowTaskBarProperty, value); }
  }

  /// <summary>
  /// 팝업 열기/닫기 상태
  /// </summary>
  public bool PopupOpen
  {
    get => (bool)GetValue(PopupOpenProperty);
    set => SetValue(PopupOpenProperty, value);
  }

  #region Dimming
  public static readonly DependencyProperty DimmingProperty;
  public static readonly DependencyProperty DimmingColorProperty;
  public static readonly DependencyProperty DimmingOpacityProperty;

  /// <summary>
  /// 디밍 효과 여부 (배경 어둡게 처리)
  /// </summary>
  public bool Dimming
  {
    get => (bool)GetValue(DimmingProperty);
    set => SetValue(DimmingProperty, value);
  }

  /// <summary>
  /// 디밍 배경색
  /// </summary>
  public Brush DimmingColor
  {
    get => (Brush)GetValue(DimmingColorProperty);
    set => SetValue(DimmingColorProperty, value);
  }

  /// <summary>
  /// 디밍 투명도
  /// </summary>
  public double DimmingOpacity
  {
    get => (double)GetValue(DimmingOpacityProperty);
    set => SetValue(DimmingOpacityProperty, value);
  }
  #endregion

  private MaximizeButton? maximBtn;

  /// <summary>
  /// 투명창 설정 (AllowsTransparency) + 타이틀바 제거 (WindowStyle.None) → 커스텀 타이틀 영역 사용 가능
  /// 최대화 상태 변화 감지: StateChanged 이벤트로 최대화 버튼 상태 연동
  /// </summary>
  static DarkThemeWindow()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(DarkThemeWindow),
      new FrameworkPropertyMetadata(typeof(DarkThemeWindow)));

    CloseCommandProperty = DependencyProperty.Register(nameof(CloseCommand), typeof(ICommand), typeof(DarkThemeWindow),
      new PropertyMetadata(null));

    TitleProperty = DependencyProperty.Register(nameof(Title), typeof(object), typeof(DarkThemeWindow),
      new UIPropertyMetadata(null));

    TitleHeaderBackgroundProperty = DependencyProperty.Register(nameof(TitleHeaderBackground), typeof(Brush), typeof(DarkThemeWindow),
      new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#252525"))));

    DimmingProperty = DependencyProperty.Register(nameof(Dimming), typeof(bool), typeof(DarkThemeWindow),
      new PropertyMetadata(false, (e, a) =>
      {
        //Console.WriteLine ("");
      }));

    DimmingColorProperty = DependencyProperty.Register(nameof(DimmingColor), typeof(Brush), typeof(DarkThemeWindow),
      new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#141414"))));

    DimmingOpacityProperty = DependencyProperty.Register(nameof(DimmingOpacity), typeof(double), typeof(DarkThemeWindow),
      new PropertyMetadata(0.8, OnDimmingOpacityChanged));

    IsShowTaskBarProperty = DependencyProperty.Register("IsShowTaskBar", typeof(bool), typeof(DarkThemeWindow),
      new PropertyMetadata(true, (d, e) =>
      {
        var win = (DarkThemeWindow)d;
        win.MaxHeightSet();
      }));

    PopupOpenProperty = DependencyProperty.Register("PopupOpen", typeof(bool), typeof(DarkThemeWindow),
      new PropertyMetadata(false, OnPopupOpenChanged));
  }

  private static void OnDimmingOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is DarkThemeWindow window && window._dimmingEffect != null)
    {
      window._dimmingEffect.Radius = (double)e.NewValue;
    }
  }

  private static void OnPopupOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is DarkThemeWindow window)
    {
      bool isOpen = (bool)e.NewValue;
      // 예시: 팝업 열기/닫기 처리
      if (isOpen)
      {
        // 팝업 열기 로직
      }
      else
      {
        // 팝업 닫기 로직
      }
    }
  }

  public DarkThemeWindow()
  {
    MaxHeightSet();

    this.AllowsTransparency = true;
    this.WindowStyle = WindowStyle.None;
    this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

    // 이벤트 핸들러 등록 StateChanged 이벤트는 창의 상태가 변경될 때 발생하는 이벤트
    // WindowState.Normal – 기본 상태, WindowState.Minimized – 최소화, WindowState.Maximized – 최대화
    this.StateChanged += DarkThemeWindow_StateChanged;

  }

  private void DarkThemeWindow_StateChanged(object? sender, EventArgs e)
  {
    if (maximBtn != null)
    {
      maximBtn.IsMaximize = this.WindowState == WindowState.Maximized;
    }
  }

  /// <summary>
  /// ControlTemplate에 정의된 버튼(PART_CloseButton, PART_MinButton, PART_MaxButton, PART_DragBar)에 이벤트 바인딩
  /// 사용자 지정 UI에 윈도우 기능 버튼 바인딩하는 핵심 메서드, 드래그 이동, 창 상태 변경 구현
  /// </summary>
  private BlurEffect? _dimmingEffect;
  public override void OnApplyTemplate()
  {
    if (GetTemplateChild("PART_CloseButton") is CloseButton btn)
    {
      btn.Click += (s, e) => WindowClose();
    }

    if (GetTemplateChild("PART_MinButton") is MinimizeButton minbtn)
    {
      minbtn.Click += (s, e) => WindowState = WindowState.Minimized;
    }

    if (GetTemplateChild("PART_MaxButton") is MaximizeButton maxbtn)
    {
      maximBtn = maxbtn;
      maxbtn.Click += (s, e) =>
      {
        this.WindowState = maxbtn.IsMaximize ? WindowState.Normal : WindowState.Maximized;
      };
    }

    if (GetTemplateChild("PART_DragBar") is DraggableBar bar)
    {
      bar.MouseDown += WindowDragMove;
    }

    // 여기 BlurEffect 처리 추가
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
  /// 닫기 명령처리 Command 없으면 기본 Close 호출
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

  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
  }

  /// <summary>
  /// 드래그바 클릭 시 창 이동 기능
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void WindowDragMove(object sender, MouseButtonEventArgs e)
  {
    if (e.LeftButton == MouseButtonState.Pressed)
    {
      GetWindow(this).DragMove();
    }
  }

  /// <summary>
  /// 창이 최대화될 때 태스크바 포함 여부 설정 (SystemParameters)
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