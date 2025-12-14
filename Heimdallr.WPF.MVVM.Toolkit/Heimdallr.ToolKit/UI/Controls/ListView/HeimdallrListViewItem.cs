using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Heimdallr 스타일에 맞춘 커스텀 ListViewItem
/// - 선택 시 배경색(SelectedBackground)
/// - 마우스 오버 시 배경색(MouseOverBackground)
/// - 코너 라디우스(CornerRadius)
/// - 확장 가능한 스타일 적용 가능
/// </summary>
public class HeimdallrListViewItem : ListViewItem
{
  static HeimdallrListViewItem()
  {
    // Generic.xaml 등에서 DefaultStyleKey 적용
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrListViewItem),
        new FrameworkPropertyMetadata(typeof(HeimdallrListViewItem)));
  }

  #region 선택 시 배경색 지정/// <summary>

  /// <summary>
  /// 선택시 배경색
  /// </summary>
  public Brush SelectedBackground
  {
    get => (Brush)GetValue(SelectedBackgroundProperty);
    set => SetValue(SelectedBackgroundProperty, value);
  }

  /// <summary>
  /// 종속성 속성: 선택시 배경색 기본값은 Gray 입니다.
  /// </summary>
  public static readonly DependencyProperty SelectedBackgroundProperty =
      DependencyProperty.Register(nameof(SelectedBackground), typeof(Brush), typeof(HeimdallrListViewItem),
          new PropertyMetadata(Brushes.Gray));
  #endregion

  #region CornerRadius Property
  /// <summary>
  /// 코너라디우스
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }

  /// <summary>
  /// 종속성 속성: 기본값 0
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius),
          typeof(HeimdallrListViewItem),
          new FrameworkPropertyMetadata(new CornerRadius(0)));
  #endregion

  #region 마우스오버 시 배경색 지정
  /// <summary>
  /// 마우스 오버 시 배경색
  /// </summary>
  public Brush MouseOverBackground
  {
    get => (Brush)GetValue(MouseOverBackgroundProperty);
    set => SetValue(MouseOverBackgroundProperty, value);
  }

  /// <summary>
  /// 종속성 속성: 마우스 오버 시 배경색 기본값은 #334155 입니다.
  /// </summary>
  public static readonly DependencyProperty MouseOverBackgroundProperty =
      DependencyProperty.Register(nameof(MouseOverBackground), typeof(Brush), typeof(HeimdallrListViewItem),
          new PropertyMetadata(new SolidColorBrush(Color.FromRgb(51, 65, 85)))); // #334155
  #endregion
}

