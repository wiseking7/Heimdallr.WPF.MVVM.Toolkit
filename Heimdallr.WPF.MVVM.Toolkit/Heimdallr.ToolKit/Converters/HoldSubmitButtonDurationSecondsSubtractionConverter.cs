using System.Globalization;
using System.Windows;

namespace Heimdallr.ToolKit.Converters;

// DurationSecondsSubtractionConverter.cs
// 역할: Duration에서 일정 시간을 뺀 Duration 반환 (애니메이션 시간 제어용)
public class HoldSubmitButtonDurationSecondsSubtractionConverter : BaseValueConverter<HoldSubmitButtonDurationSecondsSubtractionConverter>
{
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is Duration duration && double.TryParse(parameter.ToString(), out double seconds))
    {
      return duration.Subtract(TimeSpan.FromSeconds(seconds));
    }

    // 기본 fallback
    return value;
  }

  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  => throw new NotImplementedException();
}
