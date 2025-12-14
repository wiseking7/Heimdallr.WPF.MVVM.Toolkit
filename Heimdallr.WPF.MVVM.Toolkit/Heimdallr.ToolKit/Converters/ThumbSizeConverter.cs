using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// Thumb 컨트롤의 크기 조절에 사용되는 컨버터입니다.
/// 
/// 입력 값으로 주로 Thumb의 Height 값을 받아서, 
/// 해당 값의 일정 비율(예: 70%)로 크기를 조정한 값을 반환합니다.
/// 이를 통해 Thumb 내부 요소나 다른 관련 UI 요소의 크기를 
/// 동적으로 조절할 때 활용할 수 있습니다.
/// 
/// 예를 들어, Thumb의 전체 Height가 40일 경우, 
/// 이 컨버터를 사용하면 40 * 0.7 = 28 크기로 변환되어 
/// 내부 Grip 요소나 표시 크기를 적절히 조정할 수 있습니다.
/// </summary>
public class ThumbSizeConverter : BaseValueConverter<ThumbSizeConverter>
{
  /// <summary>
  /// 입력된 Thumb 높이 값을 받아서 70% 크기로 변환하여 반환합니다.
  /// 입력 값이 double 타입이 아닐 경우 기본값 26.0을 반환합니다.
  /// </summary>
  /// <param name="value">Thumb의 Height 값 (double 타입 예상)</param>
  /// <param name="targetType">변환 대상 타입 (사용하지 않음)</param>
  /// <param name="parameter">변환 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보 (사용하지 않음)</param>
  /// <returns>Height의 70% 값 혹은 기본 26.0</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is double height)
    {
      return height * 0.70; // 예: 70% 크기 변환
    }
    return 26.0; // fallback 기본 크기
  }

  /// <summary>
  /// 단방향 변환만 지원하므로 ConvertBack은 구현하지 않고 예외를 던집니다.
  /// </summary>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}
