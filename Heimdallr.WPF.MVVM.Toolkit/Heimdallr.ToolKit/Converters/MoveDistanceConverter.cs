using System.Globalization;

namespace Heimdallr.ToolKit.Converters;
/// <summary>
/// 
/// </summary>
public class MoveDistanceConverter : BaseValueConverter<MoveDistanceConverter>
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
    if (value is double width)
    {
      double barWidth = 20; // 애니메이션 대상 Rectangle 너비와 동일하게 맞춤
      return Math.Max(0, width - barWidth);
    }
    return 0;
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
