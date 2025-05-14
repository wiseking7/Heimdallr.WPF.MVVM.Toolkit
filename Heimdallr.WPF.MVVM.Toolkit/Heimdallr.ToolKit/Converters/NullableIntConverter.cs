using System.Globalization;

namespace Heimdallr.ToolKit.Converters;
// TextBox 에 Int 기본값 0 을 빈공간으로 처리
public class NullableIntConverter : BaseValueConverter<NullableIntConverter>
{
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value == null)
    {
      return string.Empty;
    }
    return value.ToString()!;
  }

  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (string.IsNullOrWhiteSpace(value as string))
    {
      return null!;
    }

    if (int.TryParse(value as string, out int result))
    {
      return result;
    }

    // 입력이 유효한지 않은 경우 예외를 던집니다.
    return null!;
  }
}

