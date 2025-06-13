using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 값(Int)을 문자열로 변환하거나, 문자열 값을 int? 로 변환하는 WPF IValueConverter입니다.
/// - null 값을 TextBox에 표시할 때는 빈 문자열로 처리합니다.
/// - 사용자 입력이 비어 있거나 공백일 경우 null로 변환합니다.
/// </summary>
public class NullableIntConverter : BaseValueConverter<NullableIntConverter>
{
  /// <summary>
  /// Convert: int? 값을 문자열로 변환합니다.
  /// - null일 경우 빈 문자열 반환
  /// - 값이 있을 경우 해당 값을 문자열로 반환
  /// </summary>
  /// <param name="value">입력 값 (보통 int? 형식)</param>
  /// <param name="targetType">변환 대상 형식 (string)</param>
  /// <param name="parameter">추가 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>문자열 표현 또는 빈 문자열</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // 값이 null인 경우 빈 문자열로 반환 (예: TextBox에서 비워 보이게 함)
    if (value == null)
    {
      return string.Empty;
    }

    // 값이 존재하면 문자열로 반환
    return value.ToString()!;
  }

  /// <summary>
  /// ConvertBack: 문자열을 int? 로 변환합니다.
  /// - 입력이 null, 빈 문자열, 또는 공백일 경우 null 반환
  /// - 유효한 정수 문자열이면 int로 변환
  /// - 그 외에는 null 반환 (또는 예외 처리로 확장 가능)
  /// </summary>
  /// <param name="value">입력 문자열</param>
  /// <param name="targetType">변환 대상 형식 (int?)</param>
  /// <param name="parameter">추가 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>정수 값 또는 null</returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // 문자열이 비어 있거나 null/공백이면 null 반환
    if (string.IsNullOrWhiteSpace(value as string))
    {
      return null!;
    }

    // 문자열을 정수로 파싱
    if (int.TryParse(value as string, out int result))
    {
      return result;
    }

    // 유효하지 않은 입력인 경우 null 반환 (예외 처리로 바꿀 수도 있음)
    return null!;
  }
}


