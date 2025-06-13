using System.Globalization;
using System.Windows.Media;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// True/False 값에 따라 색상을 반환합니다. XAML에서 TrueColor, FalseColor를 설정할 수 있습니다.
/// </summary>
public class BoolToColorConverter : BaseValueConverter<BoolToColorConverter>
{
  /// <summary>
  /// True 값에 따라 색상을 반환
  /// </summary>
  public Brush TrueColor { get; set; } = Brushes.Green;

  /// <summary>
  /// False 값에 따라 색상을 반환
  /// </summary>
  public Brush FalseColor { get; set; } = Brushes.Red;

  /// <summary>
  /// True, False 값에 다라 색상변경
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value is bool b && b ? TrueColor : FalseColor;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value is SolidColorBrush brush && brush.Color == ((SolidColorBrush)TrueColor).Color;
  }
}
/* 사용예제
 <local:BoolToColorConverter x:Key="BoolColor"
                            TrueColor="LightGreen"
                            FalseColor="LightGray" />
 
 */
