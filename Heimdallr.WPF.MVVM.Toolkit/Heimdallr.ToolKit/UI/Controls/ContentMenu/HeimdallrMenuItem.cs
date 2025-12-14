using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 
/// </summary>
public class HeimdallrMenuItem : MenuItem
{
  static HeimdallrMenuItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrMenuItem),
        new FrameworkPropertyMetadata(typeof(HeimdallrMenuItem)));
  }

  /// <summary>
  /// HeimdallrIcon에 표시될 PathGeometry 아이콘 타입
  /// </summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrMenuItem),
          new PropertyMetadata(PathIconType.None));

  /// <summary>
  /// 아이콘 색상 (HeimdallrIcon의 Fill과 바인딩됨)
  /// </summary>
  public Brush Fill
  {
    get => (Brush)GetValue(FillProperty);
    set => SetValue(FillProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrMenuItem),
        new PropertyMetadata(Brushes.Gray));

  /// <summary>
  /// 아이콘 사이즈크기조정
  /// </summary>
  public double IconSize
  {
    get => (double)GetValue(IconSizeProperty);
    set => SetValue(IconSizeProperty, value);
  }

  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty IconSizeProperty =
    DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(HeimdallrMenuItem), new PropertyMetadata(14.0));

  /// ShortcutKeyText (단축키 텍스트)
  public string ShortcutKeyText
  {
    get => (string)GetValue(ShortcutKeyTextProperty);
    set => SetValue(ShortcutKeyTextProperty, value);
  }

  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty ShortcutKeyTextProperty =
      DependencyProperty.Register(nameof(ShortcutKeyText), typeof(string), typeof(HeimdallrMenuItem), new PropertyMetadata(string.Empty));

  // CommandParameter 기본값 자동 설정 (PlacementTarget.DataContext 등에서)
  // 이 기능은 ContextMenu 쪽에서 자동 처리하는 경우가 많지만, 필요시 여기에 구현 가능

}
