using System.Globalization;
using System.Windows;

namespace Heimdallr.ToolKit.Converters;

// DurationSecondsSubtractionConverter.cs
// 역할: Duration 값에서 특정 초(seconds)를 빼서 새로운 Duration 반환
// 주로 애니메이션 시간 제어 등에 사용됨 (예: 원래 시간에서 일정 시간만큼 줄임)

/// <summary>
/// Duration에서 지정한 초(seconds) 만큼 뺀 Duration을 반환하는 컨버터입니다.
/// parameter로 뺄 초 단위를 전달받습니다.
/// </summary>
public class HoldSubmitButtonDurationSecondsSubtractionConverter : BaseValueConverter<HoldSubmitButtonDurationSecondsSubtractionConverter>
{
  /// <summary>
  /// 변환 메서드: Duration에서 parameter로 전달된 초(seconds)를 빼서 반환
  /// </summary>
  /// <param name="value">원본 Duration 값</param>
  /// <param name="targetType">목표 타입 (보통 Duration)</param>
  /// <param name="parameter">뺄 초를 나타내는 문자열 (예: "1.5")</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>초를 뺀 Duration, 실패 시 원본 Duration 반환</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // 1. value가 Duration 타입인지 확인
    // 2. parameter를 double로 변환 시도 (초 단위)
    if (value is Duration duration && double.TryParse(parameter?.ToString(), out double seconds))
    {
      // Duration에서 seconds 만큼 뺀 Duration 반환
      return duration.Subtract(TimeSpan.FromSeconds(seconds));
    }

    // 변환 실패 시 원본 값 그대로 반환 (fallback)
    return value;
  }

  /// <summary>
  /// ConvertBack은 이 컨버터에서 구현하지 않음 (단방향 변환 전용)
  /// </summary>
  /// <exception cref="NotImplementedException"></exception>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    => throw new NotImplementedException();
}
