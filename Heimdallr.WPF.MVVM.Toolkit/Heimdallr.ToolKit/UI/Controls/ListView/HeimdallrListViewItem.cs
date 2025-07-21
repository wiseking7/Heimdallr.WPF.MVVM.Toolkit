using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Heimdallr 스타일에 맞춘 커스터마이징 가능한 ListViewItem 컨트롤입니다.
/// </summary>
public class HeimdallrListViewItem : ListViewItem
{
  static HeimdallrListViewItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrListViewItem),
        new FrameworkPropertyMetadata(typeof(HeimdallrListViewItem)));
  }

  #region 1. 선택 시 배경색 지정
  /// <summary>
  /// 선택 시 배경색을 지정하는 의존성 속성입니다.
  /// </summary>
  public Brush SelectedBackground
  {
    get { return (Brush)GetValue(SelectedBackgroundProperty); }
    set { SetValue(SelectedBackgroundProperty, value); }
  }
  /// <summary>
  /// 기본색은 회색입니다.
  /// </summary>
  public static readonly DependencyProperty SelectedBackgroundProperty =
      DependencyProperty.Register(nameof(SelectedBackground), typeof(Brush), typeof(HeimdallrListViewItem),
          new PropertyMetadata(Brushes.Gray));
  #endregion
}
