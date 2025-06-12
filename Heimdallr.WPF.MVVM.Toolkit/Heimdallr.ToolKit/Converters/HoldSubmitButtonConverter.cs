using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

// HoldSubmitButtonConverter.cs
// 역할: Width/Height 값을 일정 비율로 계산하여 반환
public class HoldSubmitButtonConverter : BaseValueConverter<HoldSubmitButtonConverter>
{
  // Convert: 값(value)과 파라미터(parameter)를 double로 파싱해 곱셈 결과 반환
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return double.TryParse(value.ToString(), out double num1) &&
      double.TryParse(parameter.ToString(), out double num2) ?
      num1 * num2 : 0;
  }

  // 사용하지 않음
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  => throw new NotImplementedException();
}
