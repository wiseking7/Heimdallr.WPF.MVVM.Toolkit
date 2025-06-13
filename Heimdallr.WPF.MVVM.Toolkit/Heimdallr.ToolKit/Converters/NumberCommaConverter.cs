using System.Globalization;
using System.Text.RegularExpressions;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 숫자(int 또는 숫자 문자열)를 천 단위 콤마 형식 문자열로 변환하는 WPF IValueConverter입니다.
/// 예: 12345 → "12,345"
/// - Convert: int 또는 string 값을 "12,345" 형식의 문자열로 변환
/// - ConvertBack: 콤마가 포함된 문자열을 정수(int)로 역변환
/// - 값이 0 또는 null이면 빈 문자열을 반환함
/// </summary>
public class NumberCommaConverter : BaseValueConverter<NumberCommaConverter>
{
  /// <summary>
  /// 숫자(int 또는 숫자 문자열)를 천 단위 콤마 문자열로 변환합니다.
  /// </summary>
  /// <param name="value">입력 값 (int 또는 string)</param>
  /// <param name="targetType">변환 대상 형식 (보통 string)</param>
  /// <param name="parameter">추가 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보 (콤마/마침표 등 지역 포맷에 영향)</param>
  /// <returns>천 단위 콤마가 포함된 문자열, 또는 빈 문자열</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // null인 경우 빈 문자열 반환
    if (value == null) return string.Empty;

    // value가 정수형(int)일 경우
    if (value is int number)
    {
      // 0이면 빈 문자열 처리
      if (number == 0) return string.Empty;

      // 천 단위 콤마 문자열 반환 (예: "12,345")
      return number.ToString("N0", culture);
    }

    // value가 문자열이고, 정수로 파싱 가능한 경우
    if (value is string strValue && int.TryParse(strValue, out int parsed))
    {
      // 0이면 빈 문자열
      if (parsed == 0) return string.Empty;

      // 천 단위 콤마 문자열 반환
      return parsed.ToString("N0", culture);
    }

    // 처리 불가능한 값은 빈 문자열 반환
    return string.Empty;
  }

  /// <summary>
  /// 콤마가 포함된 문자열을 정수(int)로 역변환합니다.
  /// 예: "12,345" → 12345
  /// </summary>
  /// <param name="value">콤마 포함 문자열</param>
  /// <param name="targetType">변환 대상 타입 (보통 int)</param>
  /// <param name="parameter">추가 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보 (사용자 지역 설정)</param>
  /// <returns>정수 값, 또는 null</returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // 입력값이 null이거나 문자열이 아닌 경우 "" 처리
    string input = value?.ToString() ?? string.Empty;

    // 숫자만 남기기 위해 모든 숫자가 아닌 문자 제거 (콤마, 공백 등 제거)
    string numericOnly = Regex.Replace(input, "[^0-9]", string.Empty);

    // 정수로 파싱 가능하면 반환
    if (int.TryParse(numericOnly, NumberStyles.Any, culture, out int result))
      return result;

    // 파싱 실패 시 null 반환
    return null!;
  }
}
