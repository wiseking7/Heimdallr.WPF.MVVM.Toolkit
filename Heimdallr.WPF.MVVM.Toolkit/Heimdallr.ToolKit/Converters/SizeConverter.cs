using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 파일 크기 등의 숫자 값을 사람이 읽기 쉬운 형식(예: B, KB)으로 변환하는 ValueConverter입니다.
/// </summary>
public class SizeConverter : BaseValueConverter<SizeConverter>
{
  /// <summary>
  /// 소스 값을 사람이 읽기 쉬운 크기 단위 문자열로 변환합니다.
  /// </summary>
  /// <param name="value">변환할 원본 값 (숫자형, null 가능)</param>
  /// <param name="targetType">목적지 타입 (대부분 string)</param>
  /// <param name="parameter">옵션 매개변수 (미사용)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>읽기 쉬운 문자열 (예: "10 KB", "2 MB") 또는 값이 0이면 "--"</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // null 입력 시 0으로 간주
    value = value ?? 0;

    // 파일 크기 단위 배열 (Byte, KB, MB, ...)
    string[] units = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

    // 입력값을 double로 변환 시도 (예외 방지를 위해 TryParse 사용)
    double size;
    if (!double.TryParse(value.ToString(), out size))
    {
      size = 0;
    }

    // 단위 인덱스 초기화 (0: B)
    int unit = 0;

    // size가 1024 이상이고, 단위 인덱스가 1(KB)까지만 증가하도록 제한
    // 즉, B에서 KB까지만 변환하고 MB 이상은 하지 않음
    while (size >= 1024 && unit < 1)
    {
      size /= 1024;
      ++unit;
    }

    // 크기가 0일 경우 "--" 표시
    if (size == 0)
    {
      return "--";
    }

    // 숫자를 천 단위 구분 쉼표와 함께 출력하고, 단위 문자열 붙임
    // {0:#,##0,#} 형식은 소수점 없이 천 단위 쉼표를 표시하는 포맷
    return string.Format("{0:#,##0,#}{1}", size, units[unit]);
  }

  /// <summary>
  /// 역변환은 구현하지 않음 (일방향 변환)
  /// </summary>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}

