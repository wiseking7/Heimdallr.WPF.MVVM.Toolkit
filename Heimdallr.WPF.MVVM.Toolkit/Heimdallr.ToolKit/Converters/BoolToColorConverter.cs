using System.Globalization;
using System.Windows.Media;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// True/False 값에 따라 색상을 반환합니다. XAML에서 TrueColor, FalseColor를 설정할 수 있습니다.
/// </summary>
public class BoolToColorConverter : BaseValueConverter<BoolToColorConverter>
{
  public Brush TrueColor { get; set; } = Brushes.Green;
  public Brush FalseColor { get; set; } = Brushes.Red;

  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value is bool b && b ? TrueColor : FalseColor;
  }

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
