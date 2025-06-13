using System.Globalization;
using System.Windows.Data;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// Nullable bool (bool?) 값을 사용자 지정 문자열로 변환하는 WPF IValueConverter 구현입니다.
/// true, false, null 값 각각에 대해 사용자 정의 텍스트를 지정할 수 있으며,
/// UI에 "예", "아니오", "모름" 등과 같이 표시할 때 유용합니다.
/// </summary>
public class NullableBoolToTextConverter : BaseValueConverter<NullableBoolToTextConverter>
{
  /// <summary>
  /// bool? 값이 true일 때 반환할 문자열입니다. 기본값은 "예"입니다.
  /// </summary>
  public string? TrueText { get; set; } = "예";

  /// <summary>
  /// bool? 값이 false일 때 반환할 문자열입니다. 기본값은 "아니오"입니다.
  /// </summary>
  public string? FalseText { get; set; } = "아니오";

  /// <summary>
  /// bool? 값이 null일 때 반환할 문자열입니다. 기본값은 "모름"입니다.
  /// </summary>
  public string? NullText { get; set; } = "모름";

  /// <summary>
  /// Convert 메서드는 bool? 값을 대응되는 문자열(TrueText, FalseText, NullText)로 변환합니다.
  /// </summary>
  /// <param name="value">입력값 (nullable bool: true, false, 또는 null)</param>
  /// <param name="targetType">대상 바인딩 타입 (보통 string)</param>
  /// <param name="parameter">추가 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>해당 상태에 대응되는 텍스트 문자열</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value가 bool일 경우 true/false 텍스트 반환
    if (value is bool b)
      return b ? TrueText! : FalseText!;

    // value가 null이면 NullText 반환
    if (value is null)
      return NullText!;

    // 예상치 못한 타입은 NullText 처리
    return NullText!;
  }

  /// <summary>
  /// ConvertBack 메서드는 문자열 값을 bool? 값으로 역변환합니다.
  /// TrueText에 해당하면 true, FalseText에 해당하면 false, 그 외는 변환하지 않습니다.
  /// </summary>
  /// <param name="value">입력 문자열 (예: "예", "아니오", "모름")</param>
  /// <param name="targetType">대상 타입 (보통 bool?)</param>
  /// <param name="parameter">추가 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>bool? 값 또는 변환 불가능한 경우 Binding.DoNothing</returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    var str = value?.ToString();

    // 문자열이 TrueText와 일치할 경우 true 반환
    if (string.Equals(str, TrueText, StringComparison.Ordinal))
      return true;

    // 문자열이 FalseText와 일치할 경우 false 반환
    if (string.Equals(str, FalseText, StringComparison.Ordinal))
      return false;

    // 나머지는 바인딩을 무시 (예: NullText 또는 기타 예외적 값) return null! 표기도 가능
    return Binding.DoNothing;
  }
}
/* 사용예제
 <local:NullableBoolToTextConverter x:Key="NullableBoolToText"
                                   TrueText="예"
                                   FalseText="아니오"
                                   NullText="알 수 없음" />

 
 */
