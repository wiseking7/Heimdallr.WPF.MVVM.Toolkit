using System.Globalization;
using System.Text.RegularExpressions;

namespace Heimdallr.ToolKit.Converters;
/// <summary>
/// 휴대폰 번호를 문자열로 변환하는 WPF용 ValueConverter
/// BaseValueConverter<T>를 상속받아 XAML에서 사용 가능하게 합니다.
/// </summary>
public class MobileNumberConverter : BaseValueConverter<MobileNumberConverter>
{
  /// <summary>
  /// ViewModel → View로 값을 전달할 때 호출됨 (예: 휴대폰 번호를 보기 좋게 포맷)
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value가 문자열(string) 타입인지 확인
    if (value is string input)
    {
      // 숫자가 아닌 문자(공백, 하이픈 등)를 모두 제거하여 숫자만 남김
      var digits = Regex.Replace(input, @"[^\d]", "");

      // 숫자 길이가 10자리 이상인 경우 포맷팅 진행
      if (digits.Length >= 10)
      {
        // 휴대폰 번호를 'XXX-XXXX-XXXX' 형식으로 반환
        return $"{digits.Substring(0, 3)}-{digits.Substring(3, 4)}-{digits.Substring(7)}";
      }

      // 10자리 미만일 경우 그냥 숫자만 반환 (포맷 생략)
      return digits;
    }

    // value가 문자열이 아니면 변환하지 않고 그대로 반환
    return value;
  }

  /// <summary>
  /// View → ViewModel로 값을 되돌릴 때 호출됨 (예: 사용자 입력을 순수 숫자로 복원)
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // 입력값이 문자열이라면
    if (value is string formatteedMobilNumber)
    {
      // 숫자가 아닌 문자를 모두 제거하여 숫자만 반환 (하이픈 등 제거)
      return Regex.Replace(formatteedMobilNumber, @"[^\d]", "");
    }

    // 문자열이 아닌 경우 원본 값 그대로 반환
    return value;
  }
}
