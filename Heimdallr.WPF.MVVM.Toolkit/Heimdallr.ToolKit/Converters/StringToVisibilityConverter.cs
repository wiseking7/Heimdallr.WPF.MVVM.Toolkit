using System.Globalization;
using System.Windows;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 문자열이 비었는지 여부에 따라 Visibility를 반환하는 범용 컨버터
/// </summary>
/// <remarks>
/// ConverterParameter:
/// - "Invert" 또는 "false" → 문자열이 있으면 Collapsed, 없으면 Visible
/// - 생략 또는 "true" → 문자열이 있으면 Visible, 없으면 Collapsed
/// </remarks>
public class StringToVisibilityConverter : BaseValueConverter<StringToVisibilityConverter>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // 문자열이 null 또는 빈 문자열인지 확인
    bool hasText = value is string str && !string.IsNullOrEmpty(str);

    // 파라미터 처리 (Invert 여부)
    string param = (parameter?.ToString()?.ToLowerInvariant()) ?? string.Empty;
    bool invert = param == "invert" || param == "false";

    // 동작 방식에 따라 Visibility 설정
    return invert
        ? (hasText ? Visibility.Collapsed : Visibility.Visible)
        : (hasText ? Visibility.Visible : Visibility.Collapsed);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      => throw new NotImplementedException();
}


