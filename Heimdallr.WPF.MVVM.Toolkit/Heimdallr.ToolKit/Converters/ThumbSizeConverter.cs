using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 
/// </summary>
public class ThumbSizeConverter : BaseValueConverter<ThumbSizeConverter>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is double height)
    {
      return height * 0.70; // 예: 60% 크기
    }
    return 26.0; // fallback
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}
