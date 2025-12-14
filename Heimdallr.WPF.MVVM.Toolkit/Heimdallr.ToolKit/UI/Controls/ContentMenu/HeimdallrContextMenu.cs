using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 사용자 정의 ContextMenu로, MVVM 패턴과 테마 커스터마이징을 지원합니다.
/// </summary>
public class HeimdallrContextMenu : ContextMenu
{
  static HeimdallrContextMenu()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrContextMenu),
        new FrameworkPropertyMetadata(typeof(HeimdallrContextMenu)));
  }

  /// <summary>
  /// 생성자
  /// </summary>
  public HeimdallrContextMenu()
  {
    // DataContext 자동 바인딩
    this.Opened += (s, e) =>
    {
      if (PlacementTarget is FrameworkElement target)
      {
        this.DataContext = target.DataContext;

        // CommandParameter 자동 설정
        foreach (var item in Items.OfType<MenuItem>())
        {
          if (item.CommandParameter == null)
            item.CommandParameter = target.DataContext;
        }
      }
    };

    // 닫힐 때 리소스 해제 또는 상태 초기화
    this.Closed += (s, e) =>
    {
      this.DataContext = null;
    };

    // 키보드 ESC 닫기 지원
    this.PreviewKeyDown += (s, e) =>
    {
      if (e.Key == Key.Escape)
      {
        this.IsOpen = false;
        e.Handled = true;
      }
    };

    // 기본 위치를 마우스 포인트로 설정
    this.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
  }

  /// <summary>
  /// 배경색 사용자 지정 속성 (테마 연동)
  /// </summary>
  public Brush BackgroundColor
  {
    get => (Brush)GetValue(BackgroundColorProperty);
    set => SetValue(BackgroundColorProperty, value);
  }

  /// <summary>
  /// 종속성 속성 등록
  /// </summary>
  public static readonly DependencyProperty BackgroundColorProperty =
      DependencyProperty.Register(nameof(BackgroundColor), typeof(Brush), typeof(HeimdallrContextMenu),
         new PropertyMetadata(null));
}
