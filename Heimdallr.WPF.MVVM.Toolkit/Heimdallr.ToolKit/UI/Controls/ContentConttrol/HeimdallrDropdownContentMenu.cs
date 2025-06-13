using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
  // 템플릿에서 참조할 PART 이름 상수 정의
  private const string PART_POPUP_NAME = "PART_Popup";
  private const string PART_TOGGLE_NAME = "PART_Toggle";

  // 실제 템플릿 내부 요소 참조
  private Popup? _popup;
  private CheckBox? _toggle;

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
  /// ※ 등록 대상 타입이 잘못되어 있어 수정 필요
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrDropdownContentMenu),
          new PropertyMetadata(PathIconType.None));

  #endregion

  #region Fill

  /// <summary>
  /// HeimdallrIcon의 색상을 지정합니다. (아이콘 브러시)
  /// </summary>
  public Brush Fill
  {
    get { return (Brush)GetValue(FillProperty); }
    set { SetValue(FillProperty, value); }
  }

  /// <summary>
  /// 의존 속성 등록: Fill
  /// ※ 등록 대상 타입이 잘못되어 있어 수정 필요
  /// </summary>
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrDropdownContentMenu),
        new PropertyMetadata(Brushes.Gray));

  #endregion

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
    }

    // 토글 버튼 (CheckBox) 참조
    _toggle = Template.FindName(PART_TOGGLE_NAME, this) as CheckBox;
  }

  /// <summary>
  /// 팝업이 닫힐 때 호출되는 이벤트 핸들러
  /// Toggle에 마우스가 올라가 있지 않으면 IsOpen을 false로 설정하여 완전히 닫습니다.
  /// </summary>
  private void Popup_Closed(object? sender, EventArgs e)
  {
    if (_toggle != null && !_toggle.IsMouseOver)
    {
      IsOpen = false;
    }
  }
}
