using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 다중 바인딩에서 유효성 검사 결과와 기본 두께 값을 받아,
/// 유효할 경우 두께를 2로, 그렇지 않으면 기본 두께를 반환하는 컨버터입니다.
/// </summary>
public class ValidatingBorderThicknessConverter : IMultiValueConverter
{
  /// <summary>
  /// 변환 메서드
  /// </summary>
  /// <param name="values">
  /// values[0]: bool isValid - 유효성 검사 결과
  /// values[1]: object defaultThickness - 기본 테두리 두께 (옵션)
  /// </param>
  /// <param name="targetType">대상 타입 (Thickness)</param>
  /// <param name="parameter">변환기 파라미터 (사용 안 함)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>유효하면 Thickness(2), 아니면 기본 두께 또는 Thickness(1)</returns>
  public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {
    // values[0]가 bool 타입이고 true라면 (유효성 검사 성공)
    if (values[0] is bool isValid && isValid)
    {
      // 테두리 두께를 2로 반환
      return new Thickness(2);
    }

    // 유효하지 않으면 두 번째 값이 있으면 반환, 없으면 기본 Thickness(1)
    return values.Length > 1 ? values[1] : new Thickness(1);
  }

  /// <summary>
  /// ConvertBack 미구현 (단방향 변환 전용)
  /// </summary>
  public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
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
