using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// StackPanel을 상속받은 사용자 정의 패널 클래스입니다.
/// </summary>
// ItemHeight 속성을 바탕으로 일정 높이의 Border 요소들을 자동 생성하여 추가합니다.
public class MagicStackPanel : StackPanel
{
  // 두 가지 색상을 정의합니다. 번갈아가며 Border 배경색으로 사용됩니다.
  private SolidColorBrush color1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#393E46"));
  private SolidColorBrush color2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#948979"));

  /// <summary>
  /// 사용자 정의 의존성 속성: ItemHeight, 이 속성을 설정하면 그 값에 따라 내부에 Border 요소가 생성됩니다.
  /// </summary>
  public double ItemHeight
  {
    get { return (double)GetValue(ItemHeightProperty); }
    set { SetValue(ItemHeightProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty ItemHeightProperty =
      DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(MagicStackPanel),
        new PropertyMetadata(0.0, ItemHeightPropertyChanged));

  /// <summary>
  /// ItemHeight 속성이 변경되었을 때 호출되는 콜백 메서드입니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void ItemHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // 변경된 객체를 MagicStackPanel로 캐스팅
    MagicStackPanel panel = (MagicStackPanel)d;

    // 기존에 있던 자식 요소들을 모두 제거합니다.
    panel.Children.Clear();

    // 전체 패널 높이를 ItemHeight 값으로 설정
    panel.Height = panel.ItemHeight;

    // ItemHeight 값을 기준으로 36 단위로 몇 개의 Border를 생성할지 계산
    int index = (int)panel.ItemHeight / 36;

    // index 수만큼 Border를 생성하여 패널에 추가합니다.
    for (int i = 0; i < index; i++)
    {
      Border border = new();

      // 각 Border의 높이는 고정값 36
      border.Height = 36;

      // 색상은 짝수/홀수에 따라 교대로 설정 (스트라이프 느낌)
      border.Background = i % 2 == 0 ? panel.color1 : panel.color2;

      // StackPanel의 자식으로 추가
      panel.Children.Add(border);
    }
  }
}
