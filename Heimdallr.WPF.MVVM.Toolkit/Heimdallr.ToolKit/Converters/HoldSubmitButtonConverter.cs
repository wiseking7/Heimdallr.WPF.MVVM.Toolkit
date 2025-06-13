using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// Width/Height 값을 일정 비율로 계산하여 반환
/// </summary>
public class HoldSubmitButtonConverter : BaseValueConverter<HoldSubmitButtonConverter>
{
  /// <summary>
  /// Convert: 값(value)과 파라미터(parameter)를 double로 파싱해 곱셈 결과 반환
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return double.TryParse(value.ToString(), out double num1) &&
      double.TryParse(parameter.ToString(), out double num2) ?
      num1 * num2 : 0;
  }

  /// <summary>
  /// 사용하지 않음
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
