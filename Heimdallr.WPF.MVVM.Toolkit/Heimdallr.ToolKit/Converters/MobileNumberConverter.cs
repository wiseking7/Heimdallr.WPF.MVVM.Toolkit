using System.Globalization;
using System.Text.RegularExpressions;

namespace Heimdallr.ToolKit.Converters;
public class MobileNumberConverter : BaseValueConverter<MobileNumberConverter>
{
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is string input)
    {
      var digits = Regex.Replace(input, @"[^\d]", "");
      if (digits.Length >= 10)
      {
        return $"{digits.Substring(0, 3)}-{digits.Substring(3, 4)}-{digits.Substring(7)}";
      }
      return digits; // 휴대폰번호가 10 자리 미만인 경우 포맷되지 않은 반환
    }
    return value; // 문자열이 아닌 경우 원본 반환
  }

  // // 이 방법은 데이터를 뷰 모델(ConvertBack)로 다시 보낼 때 형식을 스트립하는 데 사용됩니다.
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is string formatteedMobilNumber)
    {
      // 숫자가 아닌 문자제거 (하이픈 제거)
      return Regex.Replace(formatteedMobilNumber, @"[^\d]", "");
    }
    return value;
  }
}
