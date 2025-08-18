using System.Globalization;
using System.Windows;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// double 값을 받아 Thickness의 왼쪽 여백으로 변환하는 컨버터입니다.
/// 예를 들어 40.0이 들어오면, Thickness(40, 0, 0, 0)으로 변환합니다.
/// 일반적으로 Margin 바인딩 시 사용됩니다.
/// </summary>
public class DoubleToLeftMarginConverter : BaseValueConverter<DoubleToLeftMarginConverter>
{
  /// <summary>
  /// ViewModel에서 View로 데이터가 전달될 때 호출되는 변환 메서드입니다.
  /// double 값을 Thickness로 변환합니다.
  /// </summary>
  /// <param name="value">바인딩된 값 (예: double 타입의 값, 예: 40.0)</param>
  /// <param name="targetType">바인딩 대상 속성 타입 (보통 Thickness)</param>
  /// <param name="parameter">추가 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보 (국제화 고려용)</param>
  /// <returns>Thickness(d, 0, 0, 0)</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value가 double 타입일 경우만 변환 수행
    if (value is double d)
    {
      // 왼쪽 여백만 d로 지정하고 나머지는 0
      return new Thickness(d, 0, 0, 0);
    }

    // 그 외 타입이거나 null인 경우, 기본 여백(모두 0) 반환
    return new Thickness(0);
  }

  /// <summary>
  /// View에서 ViewModel로 데이터가 전달될 때 호출되는 역변환 메서드입니다.
  /// 하지만 이 컨버터는 OneWay 바인딩 용도이므로 역변환은 구현하지 않습니다.
  /// </summary>
  /// <param name="value">변환된 값</param>
  /// <param name="targetType">타겟 타입</param>
  /// <param name="parameter">추가 파라미터</param>
  /// <param name="culture">문화권</param>
  /// <returns>NotImplementedException 예외 발생</returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // 역변환은 사용하지 않음 (OneWay 전용 컨버터)
    throw new NotImplementedException();
  }
}
