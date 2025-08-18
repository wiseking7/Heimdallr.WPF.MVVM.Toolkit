using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;
/// <summary>
/// 교차 색상 줄무늬(Border)들을 반복 생성하여 표시하는 StackPanel.
/// 지정된 높이(ItemHeight)를 기준으로 36px 간격으로 교차 스트라이프를 출력.
/// </summary>
public class HeimdallrStackPanel : StackPanel
{
  #region ▒ Dependency Properties ▒

  #region ▶ StripeColor1Property - 스트라이프 색상 1
  /// <summary>
  /// 스트라이프의 첫 번째 색상 (짝수 인덱스 줄).
  /// </summary>
  public Brush StripeColor1
  {
    get => (Brush)GetValue(StripeColor1Property);
    set => SetValue(StripeColor1Property, value);
  }
  /// <summary>
  /// StripeColor1Property 속성
  /// </summary>
  public static readonly DependencyProperty StripeColor1Property =
      DependencyProperty.Register(nameof(StripeColor1), typeof(Brush), typeof(HeimdallrStackPanel),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#393E46"))));
  #endregion

  #region ▶ StripeColor2Property - 스트라이프 색상 2
  /// <summary>
  /// 스트라이프의 두 번째 색상 (홀수 인덱스 줄).
  /// </summary>
  public Brush StripeColor2
  {
    get => (Brush)GetValue(StripeColor2Property);
    set => SetValue(StripeColor2Property, value);
  }
  /// <summary>
  /// StripeColor2Property 속성
  /// </summary>
  public static readonly DependencyProperty StripeColor2Property =
      DependencyProperty.Register(nameof(StripeColor2), typeof(Brush), typeof(HeimdallrStackPanel),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#948979"))));
  #endregion

  #region ▶ ItemHeightProperty - 전체 영역의 높이
  /// <summary>
  /// 전체 높이. 내부 스트라이프 수를 계산하는 기준이 됨.
  /// 외부에서 직접 설정하거나 SizeChanged 이벤트로 자동 갱신됨.
  /// </summary>
  public double ItemHeight
  {
    get => (double)GetValue(ItemHeightProperty);
    set => SetValue(ItemHeightProperty, value);
  }
  /// <summary>
  /// 기본값 0, OnItemHeightChanged
  /// </summary>
  public static readonly DependencyProperty ItemHeightProperty =
      DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(HeimdallrStackPanel),
          new PropertyMetadata(0.0, OnItemHeightChanged));
  #endregion

  #endregion

  #region 생성자 및 이벤트 핸들러

  /// <summary>
  /// 생성자. 크기 변경 이벤트 등록을 통해 ItemHeight 자동 설정.
  /// </summary>
  public HeimdallrStackPanel()
  {
    this.SizeChanged += (s, e) =>
    {
      ItemHeight = e.NewSize.Height;
    };
  }

  /// <summary>
  /// ItemHeight 변경 시 내부 스트라이프를 다시 생성.
  /// </summary>
  private static void OnItemHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is not HeimdallrStackPanel panel)
      return;

    panel.UpdateStripes();
  }
  #endregion

  #region 내부 스트라이프 생성 
  /// <summary>
  /// 현재 ItemHeight에 맞춰 36픽셀 간격으로 스트라이프 Border 요소를 생성.
  /// 기존 Children을 모두 제거한 후 다시 추가함.
  /// </summary>
  private void UpdateStripes()
  {
    this.Children.Clear();

    if (ItemHeight <= 0)
      return;

    int stripeCount = (int)(ItemHeight / 36.0);

    for (int i = 0; i < stripeCount; i++)
    {
      Border border = new()
      {
        Height = 36,
        Background = (i % 2 == 0) ? StripeColor1 : StripeColor2
      };

      this.Children.Add(border);
    }
  }
  #endregion
}
