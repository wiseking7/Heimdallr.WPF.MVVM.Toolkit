using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

// 파일 크기 등의 숫자 값을 사람이 읽기 쉬운 형식(B, KB 등)으로 변환하는 ValueConverter
public class SizeConverter : BaseValueConverter<SizeConverter>
{
  // Convert 메서드: 숫자(long 또는 int 등)를 "KB", "MB" 등의 문자열로 변환
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // null 값일 경우 0으로 대체
    value = value ?? 0;

    // 파일 크기 단위 배열
    string[] units = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

    // value를 double로 안전하게 파싱 (기존: long.Parse(value.ToString()))
    double size;

    if (!double.TryParse(value.ToString(), out size))
    {
      size = 0;
    }

    int unit = 0;

    // 파일 크기가 1024 이상이고, 최대 단위를 제한(여기서는 KB까지만, 즉 0(B) → 1(KB))
    while (size >= 1024 && unit < 1)
    {
      size /= 1024;
      ++unit;
    }

    // 크기가 0이면 "--" 표시
    if (size == 0)
    {
      return "--";
    }

    // 숫자를 천 단위 구분 쉼표와 함께 단위 표시
    return string.Format("{0:#,##0,#}{1}", size, units[unit]);
  }

  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}
