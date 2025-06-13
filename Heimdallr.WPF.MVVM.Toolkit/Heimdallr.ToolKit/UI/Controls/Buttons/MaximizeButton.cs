using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 최대화/복원 상태를 전환하는 버튼 컨트롤입니다.
/// IsMaximize 값에 따라 아이콘을 변경하며, HeimdallrIcon을 통해 시각적으로 표시합니다.
/// </summary>
public class MaximizeButton : Button
{
  #region DependencyProperty - IsMaximize

  /// <summary>
  /// 현재 최대화 상태 여부를 나타냅니다.
  /// true면 Restore 아이콘, false면 Maximize 아이콘이 표시됩니다.
  /// </summary>
  public bool IsMaximize
  {
    get => (bool)GetValue(IsMaximizeProperty);
    set => SetValue(IsMaximizeProperty, value);
  }

  /// <summary>
  /// IsMaximize 속성 등록 - 변경 시 아이콘을 자동 변경하도록 콜백 지정
  /// </summary>
  public static readonly DependencyProperty IsMaximizeProperty =
      DependencyProperty.Register(
        nameof(IsMaximize),              // 속성명
        typeof(bool),                    // 속성 타입
        typeof(MaximizeButton),          // 소유자 타입
        new PropertyMetadata(
          false,                         // 기본값: false (즉, Maximize 상태)
          MaximizePropertyChanged        // 변경 콜백
        ));

  /// <summary>
  /// IsMaximize 값이 변경되었을 때 호출되는 콜백 메서드
  /// 아이콘을 Maximize 또는 Restore로 전환합니다.
  /// </summary>
  private static void MaximizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var btn = (MaximizeButton)d;

    // 템플릿 내부의 아이콘 컨트롤이 존재할 경우 아이콘을 갱신
    if (btn.img != null)
    {
      btn.img.Icon = btn.IsMaximize ? IconType.Restore : IconType.Maximize;
    }
  }

  #endregion

  #region 생성자 및 템플릿 설정

  /// <summary>
  /// 정적 생성자: 커스텀 컨트롤 스타일 연결
  /// Themes/Generic.xaml에 정의된 기본 스타일을 적용할 수 있도록 설정합니다.
  /// </summary>
  static MaximizeButton()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(MaximizeButton),
      new FrameworkPropertyMetadata(typeof(MaximizeButton)));
  }

  /// <summary>
  /// 생성자: 특별한 로직 없음 (기본 생성자)
  /// </summary>
  public MaximizeButton()
  {
  }

  #endregion

  #region 템플릿 적용 및 아이콘 처리

  /// <summary>
  /// 컨트롤 템플릿이 적용된 후 호출되는 메서드
  /// 템플릿에서 HeimdallrIcon을 찾아서 내부 필드에 보관합니다.
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    // 컨트롤 템플릿 내의 PART_IMG 요소(HeimdallrIcon 타입)를 찾아 img 필드에 저장
    if (GetTemplateChild("PART_IMG") is HeimdallrIcon maxbtn)
    {
      img = maxbtn;

      // 초기 상태에서도 아이콘을 반영
      img.Icon = IsMaximize ? IconType.Restore : IconType.Maximize;
    }
  }

  #endregion

  /// <summary>
  /// 템플릿 내부에서 사용하는 아이콘 컨트롤입니다.
  /// 템플릿의 PART_IMG 파트로 연결되며, IsMaximize 상태에 따라 아이콘이 변경됩니다.
  /// </summary>
  private HeimdallrIcon? img;
}

