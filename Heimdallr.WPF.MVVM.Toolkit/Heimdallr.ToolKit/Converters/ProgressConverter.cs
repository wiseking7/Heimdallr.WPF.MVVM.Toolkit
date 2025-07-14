using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 진행률 계산용 컨버터 (전체 너비, 진행률 값을 받아 진행 영역 너비를 계산)
/// </summary>
public class ProgressConverter : BaseMultiValueConverter<ProgressConverter>
{
  /// <summary>
  /// MultiBinding으로 전달된 값을 바탕으로 진행률에 맞는 너비를 계산합니다.
  /// </summary>
  /// <param name="values">
  /// values[0]: 전체 컨트롤 또는 부모 요소의 실제 너비(double)
  /// values[1]: 현재 진행률 값(double), 일반적으로 0에서 100 사이의 값으로 기대됨
  /// </param>
  /// <param name="targetType">목표 속성 타입, 보통 double</param>
  /// <param name="parameter">추가 매개변수, 현재 사용하지 않음</param>
  /// <param name="culture">문화권 정보, 지역별 숫자 형식 등에 활용 가능</param>
  /// <returns>
  /// 전체 너비에 진행률 비율을 곱한 값을 반환합니다.
  /// 즉, 진행률에 해당하는 막대의 너비를 계산하여 반환합니다.
  /// </returns>
  public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {
    if (values.Length != 3)
      return 0d;

    if (values[0] is double totalWidth &&
        values[1] is double value &&
        values[2] is double maximum)
    {
      if (maximum <= 0)
        return 0d;

      value = Math.Max(0, Math.Min(maximum, value));
      return totalWidth * (value / maximum);
    }

    return 0d;
  }

  /// <summary>
  /// ConvertBack은 이 컨버터에서 구현하지 않으며, 호출 시 예외를 발생시킵니다.
  /// </summary>
  /// <param name="value">역변환할 값, 사용하지 않음</param>
  /// <param name="targetTypes">목표 타입 배열, 사용하지 않음</param>
  /// <param name="parameter">추가 매개변수, 사용하지 않음</param>
  /// <param name="culture">문화권 정보, 사용하지 않음</param>
  /// <returns>예외를 발생시킴</returns>
  /// <exception cref="NotImplementedException">항상 발생</exception>
  public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}
