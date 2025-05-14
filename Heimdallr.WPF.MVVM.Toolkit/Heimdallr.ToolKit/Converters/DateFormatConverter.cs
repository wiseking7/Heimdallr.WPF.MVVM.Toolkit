using System.Globalization;
using System.Windows.Data;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 날짜 형식 지정 변환기이며, WPF에서 XAML 데이터 바인딩 시 DateTime ↔ 문자열 간의 변환을 수행하는 데 사용
/// </summary>
public class DateFormatConverter : BaseValueConverter<DateFormatConverter>
{
  /// <summary>
  /// 날짜 형식을 지정하는 문자열입니다. 기본값은 "yyyy-MM-dd"입니다.
  /// </summary>
  public string Format { get; set; } = "yyyy-MM-dd";

  /// <summary>
  /// DateTime 값을 지정된 문자열 형식으로 변환합니다.
  /// </summary>
  /// <param name="value">DateTime 값</param>
  /// <param name="targetType">타겟 형식</param>
  /// <param name="parameter">추가 매개변수</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>형식화된 문자열</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is DateTime date)
      return date.ToString(Format, culture);

    return string.Empty;
  }

  /// <summary>
  /// 문자열 값을 DateTime으로 다시 변환합니다. 변환 실패 시 바인딩을 중단합니다.
  /// </summary>
  /// <param name="value">문자열 값</param>
  /// <param name="targetType">타겟 형식</param>
  /// <param name="parameter">추가 매개변수</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>변환된 DateTime 또는 Binding.DoNothing</returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is string dateString &&
        DateTime.TryParse(dateString, culture, DateTimeStyles.None, out var result))
    {
      return result;
    }

    // 입력이 유효하지 않으면 바인딩 무시
    return Binding.DoNothing;
  }
}

/* 사용예제
   public DateTime JoinDate { get; set; } = DateTime.Now;
 
   <TextBlock Text="{Binding JoinDate, Converter={StaticResource DateConverter}}" />

  사용자가 XAML에서 바꾸고 싶으면 이렇게 설정합니다:
  <local:DateFormatConverter Format="MM/dd/yyyy" />
 */
