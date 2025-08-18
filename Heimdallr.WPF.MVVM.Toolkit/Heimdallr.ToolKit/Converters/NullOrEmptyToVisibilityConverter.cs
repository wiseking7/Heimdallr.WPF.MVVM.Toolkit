using System.Globalization;
using System.Windows;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 문자열이 null 또는 빈 문자열일 경우 Visibility.Collapsed,
/// 값이 존재할 경우 Visibility.Visible을 반환하는 컨버터입니다.
/// 'invert' 파라미터를 사용하면 결과를 반대로 반환할 수 있습니다.
/// </summary>
public class NullOrEmptyToVisibilityConverter : BaseValueConverter<NullOrEmptyToVisibilityConverter>
{
  /// <summary>
  /// ViewModel -> View 방향의 값 변환을 수행합니다.
  /// 문자열이 null, 빈 문자열, 혹은 공백으로만 구성되어 있으면
  /// Visibility.Collapsed를 반환합니다.
  /// 그렇지 않으면 Visibility.Visible을 반환합니다.
  /// </summary>
  /// <param name="value">검사할 값 (예: 문자열)</param>
  /// <param name="targetType">타겟 속성 타입 (보통 Visibility)</param>
  /// <param name="parameter">"invert" 문자열을 넘기면 결과를 반전함</param>
  /// <param name="culture">문화권 정보 (국제화 대응용)</param>
  /// <returns>Visibility.Visible 또는 Visibility.Collapsed</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // parameter가 문자열 "invert"인 경우 결과를 반전 처리
    bool invert = (parameter as string)?.Equals("invert", StringComparison.OrdinalIgnoreCase) == true;

    // value가 null이거나 공백 문자열인지 확인
    bool isNullOrEmpty = string.IsNullOrWhiteSpace(value?.ToString());

    // invert가 true이면 결과를 반대로 반환
    // isNullOrEmpty = true → Visible
    // isNullOrEmpty = false → Collapsed
    if (invert)
      return isNullOrEmpty ? Visibility.Visible : Visibility.Collapsed;
    else
      return isNullOrEmpty ? Visibility.Collapsed : Visibility.Visible;
  }

  /// <summary>
  /// View -> ViewModel 방향의 역변환은 지원하지 않으며,
  /// 본 컨버터는 OneWay 바인딩에만 사용됩니다.
  /// 호출 시 예외가 발생합니다.
  /// </summary>
  /// <param name="value">View에서 전달된 값</param>
  /// <param name="targetType">대상 속성 타입</param>
  /// <param name="parameter">추가 파라미터</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>미구현 - 예외 발생</returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // ConvertBack은 사용하지 않도록 NotImplementedException 발생
    throw new NotImplementedException();
  }
}
