using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Heimdallr.ToolKit.Converters;

public class ValidatingBorderThicknessConverter : IMultiValueConverter
{
  public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {
    // values[0]이 bool? 타입이고, true이면 Thickness(2)를 반환
    if (values[0] is bool isValid && isValid)
    {
      // 유효성 검사 통과 => 테두리 두께를 2로 설정
      return new Thickness(2);
    }

    // 유효성 검사 실패 시 두 번째 값 반환 (기본 테두리 두께 등)
    return values.Length > 1 ? values[1] : new Thickness(1); // values[1]이 없을 경우 기본 두께 1 설정
  }

  public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException(); // 반대로 변환하는 것은 구현하지 않음
  }
}
/* 사용예제
 <TextBox>
  <TextBox.BorderBrush>
    <Binding Path="ValidationResult" Converter="{StaticResource ValidatingBorderThicknessConverter}">
      <Binding.TargetNullValue>
        <Thickness>1</Thickness> <!-- 기본 테두리 두께 -->
      </Binding.TargetNullValue>
    </Binding>
  </TextBox.BorderBrush>
</TextBox>
 */
