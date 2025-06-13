using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrPanel은 StackPanel을 확장한 커스텀 패널로,
/// Spacing(자식 요소 간 간격)과 Justify(정렬 방식) 기능을 추가 구현함.
/// Justify는 Flexbox의 justify-content와 비슷한 역할을 하며,
/// SpaceAround, SpaceBetween, SpaceEvenly 등의 정렬 옵션을 지원.
/// </summary>
public class HeimdallrPanel : StackPanel
{
  /// <summary>
  /// 자식 요소들 사이의 간격을 설정하는 의존성 속성 (기본값 0.0)
  /// </summary>
  public double Spacing
  {
    get { return (double)GetValue(SpacingProperty); }
    set { SetValue(SpacingProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty SpacingProperty =
      DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(HeimdallrPanel),
        new PropertyMetadata(0.0));

  /// <summary>
  /// 자식 요소들을 정렬하는 방법을 설정하는 의존성 속성.
  /// JustifyEnum 열거형으로 None, SpaceAround, SpaceBetween, SpaceEvenly 값을 가짐.
  /// 기본값은 None
  /// </summary>
  public JustifyEnum Justify
  {
    get { return (JustifyEnum)GetValue(JustifyProperty); }
    set { SetValue(JustifyProperty, value); }
  }

  /// <summary>
  /// 종송성주입
  /// </summary>
  public static readonly DependencyProperty JustifyProperty =
      DependencyProperty.Register(nameof(Justify), typeof(JustifyEnum), typeof(HeimdallrPanel),
        new PropertyMetadata(JustifyEnum.None));

  /// <summary>
  /// Measure 단계에서 레이아웃을 계산하는 메서드 오버라이드
  /// Justify 옵션이 활성화 되어있으면 justifyLayout()으로 정렬 처리 후 기본 MeasureOverride 실행
  /// 아니면 PerformLayout()으로 Spacing 기반 레이아웃 처리 후 기본 MeasureOverride 호출
  /// </summary>
  protected override Size MeasureOverride(Size constraint)
  {
    if (IsJustifyUseOption())
    {
      justifyLayout();
      return base.MeasureOverride(constraint);
    }
    PerformLayout();
    return base.MeasureOverride(constraint);
  }

  /// <summary>
  /// 의존성 속성 변경 시 호출됨
  /// Spacing 또는 Justify가 변경되면 적절히 레이아웃을 다시 수행
  /// </summary>
  protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
  {
    base.OnPropertyChanged(e);

    string propertyName = e.Property.Name;

    if (propertyName == nameof(Spacing) || propertyName == nameof(Justify))
    {
      if (IsJustifyUseOption())
      {
        justifyLayout();
        return;
      }

      PerformLayout();
    }
  }

  /// <summary>
  /// Justify 속성이 활성화 되어있고, ActualWidth/Height가 정상값인지 검사
  /// Justify가 None이거나 너비, 높이가 NaN, Infinity면 false 반환
  /// </summary>
  private bool IsJustifyUseOption()
  {
    if (IsNan(this.ActualWidth) || IsNan(this.ActualHeight))
      return false;

    if (Justify == JustifyEnum.None)
      return false;

    return true;
  }

  /// <summary>
  /// 값이 NaN 또는 Infinity인지 검사
  /// </summary>
  private bool IsNan(double value)
  {
    if (double.IsInfinity(value))
      return true;

    if (double.IsNaN(value))
      return true;

    return false;
  }

  /// <summary>
  /// 생성자: Loaded 이벤트 핸들러 등록
  /// </summary>
  public HeimdallrPanel()
  {
    this.Loaded += HeimdallrPanel_Loaded;
  }

  /// <summary>
  /// Loaded 이벤트 핸들러
  /// 부모 요소의 ActualWidth, ActualHeight 값을 이 패널의 Width, Height에 설정
  /// 부모 크기에 맞춰 자식 요소 배치에 활용됨
  /// </summary>
  private void HeimdallrPanel_Loaded(object sender, RoutedEventArgs e)
  {
    var parent = VisualTreeHelper.GetParent(this) as FrameworkElement;

    this.Width = parent!.ActualWidth;
    this.Height = parent.ActualHeight;
  }

  /// <summary>
  /// Justify 속성에 따른 레이아웃 처리
  /// SpaceAround, SpaceBetween, SpaceEvenly 옵션을 지원하며,
  /// 자식 요소들에 Margin을 적절히 부여하여 정렬 구현
  /// </summary>
  private void justifyLayout()
  {
    double allChildSize = 0;

    // 모든 자식의 너비 또는 높이를 합산
    foreach (UIElement child in base.Children)
    {
      allChildSize += (Orientation == Orientation.Horizontal ? (double)child.GetValue(WidthProperty)
      : (double)child.GetValue(HeightProperty));
    }

    if (allChildSize == 0)
      return;

    if (Width == 0 && Orientation == Orientation.Horizontal)
      return;

    if (Height == 0 && Orientation == Orientation.Vertical)
      return;

    // 부모 요소 크기 (너비 또는 높이)
    var parent = VisualTreeHelper.GetParent(this) as FrameworkElement;

    var parentSize = (Orientation == Orientation.Horizontal
      ? (double)parent!.ActualWidth : (double)parent!.ActualHeight);

    if (Justify == JustifyEnum.SpaceAround)
    {
      // SpaceAround: 자식 요소들 앞뒤로 동일한 여백 부여
      double aroundMargin = (parentSize - (double)allChildSize) / ((double)(base.Children.Count * 2));

      foreach (UIElement child in base.Children)
      {
        Thickness thick = Orientation == Orientation.Horizontal
          ? new Thickness(Math.Truncate(aroundMargin), 0, Math.Truncate(aroundMargin), 0)
          : new Thickness(0, Math.Truncate(aroundMargin), 0, Math.Truncate(aroundMargin));

        child.SetValue(MarginProperty, thick);
      }
    }

    else if (Justify == JustifyEnum.SpaceBetween)
    {
      // SpaceBetween: 첫번째와 마지막 자식은 여백 없이, 나머지 자식 사이에 균등 여백 부여
      double aroundMargin = (parentSize - (double)allChildSize) / (double)(base.Children.Count - 1);

      int lastIdx = base.Children.Count - 1;
      int idx = 0;
      foreach (UIElement child in base.Children)
      {
        child.SetValue(MarginProperty, new Thickness(0, 0, 0, 0)); // 초기화
        if (lastIdx == idx)
          break;
        Thickness thick = Orientation == Orientation.Horizontal
          ? new Thickness(0, 0, aroundMargin, 0)
          : new Thickness(0, 0, 0, aroundMargin);

        child.SetValue(MarginProperty, thick);

        idx++;
      }
    }

    else if (Justify == JustifyEnum.SpaceEvenly)
    {
      // SpaceEvenly: 모든 자식 요소 앞에 균등한 여백 부여
      double aroundMargin = (parentSize - (double)allChildSize) / (double)(base.Children.Count + 1);

      int lastIdx = base.Children.Count - 1;
      int idx = 0;

      foreach (UIElement child in base.Children)
      {
        child.SetValue(MarginProperty, new Thickness(0, 0, 0, 0)); // 초기화

        Thickness thick = Orientation == Orientation.Horizontal
          ? new Thickness(aroundMargin, 0, 0, 0)
          : new Thickness(0, aroundMargin, 0, 0);

        child.SetValue(MarginProperty, thick);

        idx++;
      }
    }
  }

  /// <summary>
  /// Spacing 프로퍼티가 사용될 경우 각 자식 요소 간 간격을 설정하는 메서드
  /// Spacing 값이 0 이상이고 자식 요소가 2개 이상일 때만 적용
  /// Orientation에 따라 오른쪽 또는 아래쪽 여백으로 간격 부여
  /// 마지막 자식 요소는 간격을 적용하지 않음
  /// </summary>
  private void PerformLayout()
  {
    if (Spacing < 0)
      return;

    if (base.Children.Count <= 1)
      return;

    Thickness thick = Orientation == Orientation.Horizontal
      ? new Thickness(0, 0, Spacing, 0)
      : new Thickness(0, 0, 0, Spacing);

    int lastIdx = base.Children.Count - 1;
    int idx = 0;

    foreach (UIElement child in base.Children)
    {
      child.SetValue(MarginProperty, new Thickness(0, 0, 0, 0)); // 초기화
      if (lastIdx == idx)
        break;
      child.SetValue(MarginProperty, thick);
      idx++;
    }
  }
}

/* HeimdallrPanel 사용하는 방법
1. XAML에서 HeimdallrPanel 선언 및 자식 요소 배치

<local:HeimdallrPanel Orientation="Horizontal" Spacing="10" Justify="SpaceBetween" Width="400" Height="50">
    <Button Content="Button 1" Width="80" Height="40"/>
    <Button Content="Button 2" Width="80" Height="40"/>
    <Button Content="Button 3" Width="80" Height="40"/>
</local:HeimdallrPanel>

Orientation : 기본 StackPanel처럼 수평(Horizontal) 또는 수직(Vertical) 배치 설정

Spacing : 자식 간 고정 간격을 픽셀 단위로 지정 (예: 10)

Justify : 자식 요소 정렬 방법 설정

None : 정렬 없이 Spacing만 적용

SpaceAround : 양쪽에 균등 여백 포함하여 배치

SpaceBetween : 첫/마지막 요소 끝 정렬, 나머지 사이 균등 여백

SpaceEvenly : 요소 간과 양 끝에 균등 여백

Width, Height : 부모 요소 크기에 맞춰 정렬 가능하도록 크기 지정
 */