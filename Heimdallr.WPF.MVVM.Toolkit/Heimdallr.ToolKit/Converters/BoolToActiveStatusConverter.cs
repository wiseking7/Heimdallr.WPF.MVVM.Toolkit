using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// IsActive 변환기
/// </summary>
public class BoolToActiveStatusConverter : BaseValueConverter<BoolToActiveStatusConverter>
{
  /// <summary>
  /// true -> "사용", false -> "불가"
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is bool isActive)
    {
      return isActive ? "사용" : "미사용";
    }

    return "미사용"; // 기본값
  }

  /// <summary>
  /// "사용" -> true, "불가" -> false
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is string status)
    {
      return status == "사용";
    }

    return false; // 기본값
  }
}
