using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

public class IconSizeConverter : BaseValueConverter<IconSizeConverter>
{
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is double thumbSize)
    {
      return thumbSize * 0.6;
    }

    return 15.0;
  }

  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    => throw new NotImplementedException();
}
