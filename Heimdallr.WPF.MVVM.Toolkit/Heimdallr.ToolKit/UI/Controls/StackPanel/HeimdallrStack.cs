using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// StackPanel을 확장한 커스텀 패널로, 
/// 자식 요소들 사이에 일정한 간격(Spacing)을 추가하여 배치할 수 있습니다.
/// 기본 StackPanel과 달리 자식 간격을 쉽게 조절할 수 있도록 ChildSpacing 프로퍼티 제공.
/// </summary>
public class HeimdallrStack : StackPanel
{
  /// <summary>
  /// 자식 요소들 사이 간격(Spacing)을 정의하는 의존성 속성입니다.
  /// 기본값은 10.0이며, 값이 변경되면 레이아웃이 다시 계산됩니다(AffectsArrange).
  /// </summary>
  public double ChildSpacing
  {
    get => (double)GetValue(ChildSpacingProperty);
    set => SetValue(ChildSpacingProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty ChildSpacingProperty =
      DependencyProperty.Register("ChildSpacing", typeof(double), typeof(HeimdallrStack),
          new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsArrange));

  /// <summary>
  /// 자식 요소들의 크기를 측정하는 단계.
  /// 모든 자식 요소를 Measure한 뒤, 자식들의 크기와 ChildSpacing을 고려하여
  /// 이 패널이 필요로 하는 총 크기를 계산하여 반환합니다.
  /// </summary>
  protected override Size MeasureOverride(Size availableSize)
  {
    double width = 0;
    double height = 0;
    double stackedWidth = 0;
    double stackedHeight = 0;

    foreach (UIElement child in InternalChildren)
    {
      if (child == null)
        continue;

      // 각 자식을 주어진 가용 크기 안에서 측정
      child.Measure(availableSize);

      Size childSize = child.DesiredSize;

      if (Orientation == Orientation.Vertical)
      {
        // 세로 정렬 시 높이를 누적하며 ChildSpacing도 포함
        stackedHeight += childSize.Height + ChildSpacing;

        // 가로 크기는 가장 넓은 자식 요소에 맞춤
        width = Math.Max(width, childSize.Width);
      }
      else
      {
        // 가로 정렬 시 너비 누적 + ChildSpacing 포함
        stackedWidth += childSize.Width + ChildSpacing;

        // 세로 크기는 가장 높은 자식 요소에 맞춤
        height = Math.Max(height, childSize.Height);
      }
    }

    // 마지막 자식 뒤의 간격은 제외해야 하므로 마지막 ChildSpacing만 빼줌
    if (InternalChildren.Count > 0)
    {
      if (Orientation == Orientation.Vertical)
        stackedHeight -= ChildSpacing;
      else
        stackedWidth -= ChildSpacing;
    }

    // Orientation에 따라 계산된 총 크기 반환
    return Orientation == Orientation.Vertical
        ? new Size(width, stackedHeight)
        : new Size(stackedWidth, height);
  }

  /// <summary>
  /// 자식 요소들을 최종 위치에 배치하는 단계.
  /// 누적 offset 값을 이용해 각 자식 요소의 위치와 크기를 지정하며,
  /// 자식 요소 사이에는 ChildSpacing 만큼의 공간을 둡니다.
  /// </summary>
  protected override Size ArrangeOverride(Size finalSize)
  {
    double offset = 0;

    foreach (UIElement child in InternalChildren)
    {
      if (child == null)
        continue;

      Size childSize = child.DesiredSize;

      if (Orientation == Orientation.Vertical)
      {
        // 세로 정렬: x=0, y=offset 위치에 자식 배치, 너비는 패널의 너비 전체로 설정
        child.Arrange(new Rect(0, offset, finalSize.Width, childSize.Height));

        // 다음 자식의 y 위치 offset 갱신 (자식 높이 + 간격)
        offset += childSize.Height + ChildSpacing;
      }
      else
      {
        // 가로 정렬: y=0, x=offset 위치에 자식 배치, 높이는 패널의 높이 전체로 설정
        child.Arrange(new Rect(offset, 0, childSize.Width, finalSize.Height));

        // 다음 자식의 x 위치 offset 갱신 (자식 너비 + 간격)
        offset += childSize.Width + ChildSpacing;
      }
    }

    // 마지막 자식 뒤에 추가된 ChildSpacing 제거
    if (InternalChildren.Count > 0)
      offset -= ChildSpacing;

    // 배치 후 실제 사용된 크기 반환
    return Orientation == Orientation.Vertical
        ? new Size(finalSize.Width, offset)
        : new Size(offset, finalSize.Height);
  }
}

/* 사용방법
  XAML에서 HeimdallrStack 사용하기

<local:HeimdallrStack Orientation="Horizontal" ChildSpacing="15" Width="300" Height="50">
  <Button Content="버튼1" Width="80" Height="40"/>
  <Button Content="버튼2" Width="80" Height="40"/>
  <Button Content="버튼3" Width="80" Height="40"/>
</local:HeimdallrStack>
Orientation 속성으로 가로/세로 배치를 결정

ChildSpacing으로 자식 사이 간격을 지정 (기본 10)

자식 요소들은 원하는 크기를 지정해주면 해당 크기로 배치되고, 간격도 적용됨
 */