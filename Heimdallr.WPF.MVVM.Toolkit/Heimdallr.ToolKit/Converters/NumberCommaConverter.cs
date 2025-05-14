using System.Globalization;
using System.Text.RegularExpressions;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 숫자를 콤마 표기 천단위
/// </summary>
public class NumberCommaConverter : BaseValueConverter<NumberCommaConverter>
{
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value == null) return string.Empty;

    if (value is int number)
    {
      // 0일 경우 빈 문자열
      if (number == 0) return string.Empty;

      return number.ToString("N0", culture); // 천 단위 콤마
    }

    if (value is string strValue && int.TryParse(strValue, out int parsed))
    {
      if (parsed == 0) return string.Empty;

      return parsed.ToString("N0", culture);
    }

    return string.Empty;
  }

  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    string input = value?.ToString() ?? "";

    string numericOnly = Regex.Replace(input, "[^0-9]", "");

    if (int.TryParse(numericOnly, NumberStyles.Any, culture, out int result))
      return result;

    return null!; // 숫자 파싱 실패 → null 반환
  }
}
