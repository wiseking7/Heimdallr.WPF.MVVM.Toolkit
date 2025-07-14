using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrDropdownContentMenu는 드롭다운 방식의 콘텐츠 컨트롤입니다.
/// 내부에 Popup과 Toggle(CheckBox)을 포함하며, 사용자가 토글 버튼을 클릭하면 Popup이 열립니다.
/// </summary>
[TemplatePart(Name = PART_POPUP_NAME, Type = typeof(Popup))]
[TemplatePart(Name = PART_TOGGLE_NAME, Type = typeof(CheckBox))]
public class HeimdallrDropdownContentMenu : ContentControl
{
  // 템플릿에서 사용할 PART 이름 정의
  private const string PART_POPUP_NAME = "PART_Popup";
  private const string PART_TOGGLE_NAME = "PART_Toggle";

  // 실제 템플릿 내부 요소 참조
  private Popup? _popup;
  private CheckBox? _toggle;

  /// <summary>
  /// 정적 생성자: 기본 스타일 키를 설정하여 Generic.xaml 스타일과 연결합니다.
  /// </summary>
  static HeimdallrDropdownContentMenu()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrDropdownContentMenu),
      new FrameworkPropertyMetadata(typeof(HeimdallrDropdownContentMenu)));
  }

  /// <summary>
  /// 템플릿이 적용된 후 호출됩니다.
  /// Popup과 Toggle(CheckBox)을 템플릿에서 가져와 초기화하고, 이벤트를 설정합니다.
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    // Popup 참조 획득 및 이벤트 연결
    _popup = Template.FindName(PART_POPUP_NAME, this) as Popup;
    if (_popup != null)
    {
      // 중복 등록 방지
      _popup.Closed -= Popup_Closed;
      _popup.Closed += Popup_Closed;

      // 바인딩된 Placement 초기 적용
      _popup.Placement = Placement;
    }

    // 토글 버튼 (CheckBox) 참조
    _toggle = Template.FindName(PART_TOGGLE_NAME, this) as CheckBox;
  }

  /// <summary>
  /// Popup이 닫힐 때 호출되는 이벤트 핸들러입니다.
  /// 필요 시 IsOpen을 false로 설정하고 ClosedCommand를 실행합니다.
  /// </summary>
  private void Popup_Closed(object? sender, EventArgs e)
  {
    if (StaysOpenWhenMouseOver && _toggle != null && !_toggle.IsMouseOver)
    {
      // 마우스가 토글 위에 있으면 닫지 않음
      return;
    }

    IsOpen = false;

    if (ClosedCommand?.CanExecute(null) == true)
    {
      ClosedCommand.Execute(null);
    }
  }

  #region IsOpen
  /// <summary>
  /// 드롭다운 팝업이 열려 있는지 여부를 나타냅니다.
  /// 외부 바인딩 또는 내부 이벤트로 값을 제어합니다.
  /// </summary>
  public bool IsOpen
  {
    get { return (bool)GetValue(IsOpenProperty); }
    set { SetValue(IsOpenProperty, value); }
  }

  /// <summary>
  /// 의존 속성 등록: IsOpen
  /// </summary>
  public static readonly DependencyProperty IsOpenProperty =
      DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(HeimdallrDropdownContentMenu),
          new PropertyMetadata(false));
  #endregion

  #region PathIconType
  /// <summary>
  /// HeimdallrIcon에 사용할 아이콘 PathGeometry 유형을 지정합니다.
  /// (예: 펼침 화살표, 사용자 아이콘 등)
  /// </summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }

  /// <summary>
  /// 의존 속성 등록: PathIcon
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrDropdownContentMenu),
          new PropertyMetadata(PathIconType.None));
  #endregion

  #region Fill
  /// <summary>
  /// HeimdallrIcon의 색상을 지정합니다. 기본색상는 회색(Gray)입니다.
  /// </summary>
  public Brush Fill
  {
    get { return (Brush)GetValue(FillProperty); }
    set { SetValue(FillProperty, value); }
  }

  /// <summary>
  /// 의존 속성 등록: Fill
  /// </summary>
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrDropdownContentMenu),
        new PropertyMetadata(Brushes.Gray));
  #endregion

  #region Placement
  /// <summary>
  /// 팝업이 표시될 위치입니다. 기본값은 Bottom입니다.
  /// </summary>
  public PlacementMode Placement
  {
    get => (PlacementMode)GetValue(PlacementProperty);
    set => SetValue(PlacementProperty, value);
  }
  /// <summary>
  /// 의존 속성 등록: Placement
  /// 변경 시 Popup의 실제 Placement 속성도 업데이트됩니다.
  /// </summary>
  public static readonly DependencyProperty PlacementProperty =
      DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(HeimdallrDropdownContentMenu),
          new PropertyMetadata(PlacementMode.Bottom, OnPlacementChanged));

  private static void OnPlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is HeimdallrDropdownContentMenu menu && menu._popup != null)
    {
      // Popup의 Placement 속성을 변경합니다.
      menu._popup.Placement = (PlacementMode)e.NewValue;
    }
  }
  #endregion

  #region StaysOpenWhenMouseOver
  /// <summary>
  /// 마우스가 Toggle 위에 있을 때 Popup을 닫지 않도록 유지할지 여부를 지정합니다.
  /// </summary>
  public bool StaysOpenWhenMouseOver
  {
    get => (bool)GetValue(StaysOpenWhenMouseOverProperty);
    set => SetValue(StaysOpenWhenMouseOverProperty, value);
  }
  /// <summary>
  /// 의존 속성 등록: StaysOpenWhenMouseOver
  /// </summary>
  public static readonly DependencyProperty StaysOpenWhenMouseOverProperty =
      DependencyProperty.Register(nameof(StaysOpenWhenMouseOver), typeof(bool), typeof(HeimdallrDropdownContentMenu),
          new PropertyMetadata(false));
  #endregion

  #region ClosedCommand
  /// <summary>
  /// 팝업이 닫힐 때 실행할 ICommand입니다.
  /// </summary>
  public ICommand? ClosedCommand
  {
    get => (ICommand?)GetValue(ClosedCommandProperty);
    set => SetValue(ClosedCommandProperty, value);
  }
  /// <summary>
  /// 의존 속성 등록: ClosedCommand
  /// </summary>
  public static readonly DependencyProperty ClosedCommandProperty =
      DependencyProperty.Register(nameof(ClosedCommand), typeof(ICommand), typeof(HeimdallrDropdownContentMenu),
          new PropertyMetadata(null));
  #endregion

  #region 문자열표기
  /// <summary>
  /// 버튼 영역에 표시할 텍스트입니다 (아이콘 오른쪽)
  /// </summary>
  public string Label
  {
    get => (string)GetValue(LabelProperty);
    set => SetValue(LabelProperty, value);
  }
  /// <summary>
  /// 기본값은 빈 문자열입니다.
  /// </summary>
  public static readonly DependencyProperty LabelProperty =
      DependencyProperty.Register(nameof(Label), typeof(string), typeof(HeimdallrDropdownContentMenu),
          new PropertyMetadata(string.Empty));
  #endregion

  #region 체크박스 백그라운드
  /// <summary>
  /// 드롭다운 토글 버튼(체크박스)의 배경 브러시입니다.
  /// </summary>
  public Brush ToggleBackground
  {
    get => (Brush)GetValue(ToggleBackgroundProperty);
    set => SetValue(ToggleBackgroundProperty, value);
  }
  /// <summary>
  /// 기본값은 투명(Transparent)입니다.
  /// </summary>
  public static readonly DependencyProperty ToggleBackgroundProperty =
      DependencyProperty.Register(nameof(ToggleBackground), typeof(Brush), typeof(HeimdallrDropdownContentMenu),
          new PropertyMetadata(Brushes.Transparent));
  #endregion
}
