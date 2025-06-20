using System.Globalization;
using System.Windows.Data;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 
/// </summary>
public class ProgressWidthConverter : IMultiValueConverter
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="values"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {
    if (values.Length >= 2 &&
                values[0] is double actualWidth &&
                values[1] is double value)
    {
      if (parameter is string maxStr && double.TryParse(maxStr, out var max))
      {
        if (max > 0)
        {
          return actualWidth * value / max;
        }
      }
    }
    return 0.0;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetTypes"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    => throw new NotImplementedException();

}
