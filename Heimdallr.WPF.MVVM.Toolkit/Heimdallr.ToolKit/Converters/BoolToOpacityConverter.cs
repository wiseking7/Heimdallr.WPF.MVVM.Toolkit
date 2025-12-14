using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// HeimdallrMenuItem 아이콘 Disabled 시 불투명도 조절용
/// </summary>
public class BoolToOpacityConverter : BaseValueConverter<BoolToOpacityConverter>
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
    if (value is bool b)
      return b ? 1.0 : 0.3;
    return 1.0;
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
    => throw new NotImplementedException();
}
