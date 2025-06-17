using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// IsGridViewConverter는 GridView 여부를 확인하는 ValueConverter입니다.
/// </summary>
public class IsGridViewConverter : BaseValueConverter<IsGridViewConverter>
{
  /// <summary>
  /// Convert 메서드는 GridView 타입인지 여부를 확인하여 true 또는 false를 반환합니다.
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value가 null이거나 GridView 타입이 아닌 경우 false 반환
    return value is System.Windows.Controls.GridView;
  }

  /// <summary>
  /// ConvertBack 메서드는 IsGridViewConverter에서 사용되지 않습니다.
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
