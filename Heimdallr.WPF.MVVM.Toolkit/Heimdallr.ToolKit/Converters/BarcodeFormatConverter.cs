using System.Globalization;
using System.Text.RegularExpressions;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 바코드 문자열을 UI용 포맷으로 변환하거나
/// 사용자 입력을 ViewModel용 순수 숫자 형태로 변환하는 컨버터
/// </summary>
public class BarcodeFormatConverter : BaseValueConverter<BarcodeFormatConverter>
{
  /// <summary>
  /// ViewModel에서 가져온 순수 바코드 숫자를 UI 표시용 문자열로 변환
  /// 예: "8801236123457" -> "880-1236-1234-7"
  /// </summary>
  /// <param name="value">ViewModel에 저장된 바코드 문자열 (숫자만)</param>
  /// <param name="targetType">바인딩 대상의 타입 (여기서는 string)</param>
  /// <param name="parameter">바인딩 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보 (사용하지 않음)</param>
  /// <returns>UI에 표시할 포맷된 바코드 문자열</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    string numeric = value as string ?? string.Empty;

    // 숫자가 아닌 문자 제거
    numeric = Regex.Replace(numeric, @"[^\d]", "");

    // 3자리씩 잘라 하이픈 추가
    var chunks = Enumerable.Range(0, (numeric.Length + 2) / 3)
                           .Select(i => numeric.Substring(i * 3, Math.Min(3, numeric.Length - i * 3)));

    return string.Join("-", chunks);
  }

  /// <summary>
  /// UI에서 입력한 포맷된 바코드 문자열을 ViewModel용 순수 숫자 형태로 변환
  /// 예: "880-1236-1234-7" -> "8801236123457"
  /// </summary>
  /// <param name="value">UI에서 입력된 문자열</param>
  /// <param name="targetType">바인딩 대상 타입 (string)</param>
  /// <param name="parameter">바인딩 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보 (사용하지 않음)</param>
  /// <returns>ViewModel에 저장할 순수 숫자 문자열</returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    string input = value?.ToString() ?? string.Empty;

    // 하이픈 제거 후 숫자만 반환
    return Regex.Replace(input, @"[^\d]", "");
  }
}

