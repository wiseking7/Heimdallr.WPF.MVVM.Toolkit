using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;
/// <summary>
/// HeimdallrListViewBaseHeaderItem 클래스는 ListView의 헤더 아이템을 나타내는 사용자 지정 컨트롤입니다.
/// </summary>
public class HeimdallrListViewBaseHeaderItem : ContentControl
{
  /// <summary>
  /// HeimdallrListViewBaseHeaderItem 클래스의 새 인스턴스를 초기화합니다.
  /// </summary>
  public HeimdallrListViewBaseHeaderItem()
  {
  }

  #region UseSystemFocusVisuals
  /// <summary>
  /// 포커스 시각 효과를 시스템 기본값으로 사용할지 여부를 나타내는 종속성 속성입니다.
  /// </summary>
  public static readonly DependencyProperty UseSystemFocusVisualsProperty =
      FocusVisualHelper.UseSystemFocusVisualsProperty.AddOwner(typeof(HeimdallrListViewBaseHeaderItem));

  /// <summary>
  /// 포커스 시각 효과를 시스템 기본값으로 사용할지 여부를 나타내는 속성입니다.
  /// </summary>
  public bool UseSystemFocusVisuals
  {
    get => (bool)GetValue(UseSystemFocusVisualsProperty);
    set => SetValue(UseSystemFocusVisualsProperty, value);
  }
  #endregion

  #region CornerRadius
  /// <summary>
  /// 헤더 아이템의 모서리 반경을 나타내는 종속성 속성입니다.
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
      ControlHelper.CornerRadiusProperty.AddOwner(typeof(HeimdallrListViewBaseHeaderItem));

  /// <summary>
  /// 헤더 아이템의 모서리 반경을 나타내는 속성입니다.
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }
  #endregion
}

