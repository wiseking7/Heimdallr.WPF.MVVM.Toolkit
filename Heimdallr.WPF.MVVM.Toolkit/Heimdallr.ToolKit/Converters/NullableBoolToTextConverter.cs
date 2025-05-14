using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// bool? (nullable bool)을 "예", "아니오", "모름" 등의 문자열로 변환합니다.
/// </summary>
public class NullableBoolToTextConverter : BaseValueConverter<NullableBoolToTextConverter>
{
  public string TrueText { get; set; } = "예";
  public string FalseText { get; set; } = "아니오";
  public string NullText { get; set; } = "모름";

  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value switch
    {
      true => TrueText,
      false => FalseText,
      null => NullText,
      _ => NullText
    };
  }

  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    var str = value?.ToString();
    if (str == TrueText) return true;
    if (str == FalseText) return false;
    return null!;
  }
}
/* 사용예제
 <local:NullableBoolToTextConverter x:Key="NullableBoolToText"
                                   TrueText="예"
                                   FalseText="아니오"
                                   NullText="알 수 없음" />

 
 */
