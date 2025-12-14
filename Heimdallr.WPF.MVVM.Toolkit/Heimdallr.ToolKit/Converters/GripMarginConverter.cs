using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// Grip 요소들의 Margin을 Orientation과 Grip의 위치(첫번째, 중간, 마지막)에 따라 동적으로 결정하는 컨버터입니다.
/// 수직(Vertical)일 경우 각 Grip 사이에 아래쪽 여백을 주고,
/// 수평(Horizontal)일 경우 오른쪽 여백을 줍니다.
/// 마지막 Grip은 여백이 0이 됩니다.
/// 
/// 주로 Thumb 컨트롤 내의 여러 개의 Grip(Border) 요소 간 간격 조절에 사용됩니다.
/// </summary>
public class GripMarginConverter : BaseValueConverter<GripMarginConverter>
{
  /// <summary>
  /// Orientation과 Grip 위치 정보에 따라 적절한 Margin(두께)를 반환합니다.
  /// </summary>
  /// <param name="value">Orientation 값 (Vertical 또는 Horizontal)</param>
  /// <param name="targetType">목표 타입 (Thickness)</param>
  /// <param name="parameter">
  /// Grip 위치를 나타내는 문자열. 보통 "First", "Middle", "Last" 등의 값을 받습니다.
  /// 첫번째와 중간 Grip은 여백이 적용되고, 마지막은 0입니다.
  /// </param>
  /// <param name="culture">문화권 정보 (사용하지 않음)</param>
  /// <returns>
  /// Orientation과 위치에 따른 적절한 Thickness (Margin) 값.
  /// Vertical이면 아래쪽에 여백(0,0,0,2), Horizontal이면 오른쪽에 여백(0,0,2,0).
  /// 마지막 Grip이면 여백 없음 (0).
  /// </returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is Orientation o && parameter is string position)
    {
      // 수직이면 아래쪽 여백, 수평이면 오른쪽 여백을 줌
      if (o == Orientation.Vertical)
      {
        if (position == "First" || position == "Middle")
          return new Thickness(0, 0, 0, 2);
        else
          return new Thickness(0);
      }
      else
      {
        if (position == "First" || position == "Middle")
          return new Thickness(0, 0, 2, 0);
        else
          return new Thickness(0);
      }
    }
    // 잘못된 값이 들어온 경우 기본 여백 0 반환
    return new Thickness(0);
  }

  /// <summary>
  /// 이 컨버터는 단방향 변환만 지원하므로, ConvertBack은 구현하지 않고 예외를 던집니다.
  /// </summary>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    => throw new NotImplementedException();
}
