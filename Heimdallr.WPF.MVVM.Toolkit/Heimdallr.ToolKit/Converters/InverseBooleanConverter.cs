namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// true → false, false → true
/// IsEnabled, IsChecked 등 bool 타입 속성의 값을 반전시켜 UI에 바인딩할 때 사용
/// </summary>
public class InverseBooleanConverter : BaseValueConverter<InverseBooleanConverter>
{
  /// <summary>
  /// bool 값을 반전시켜 반환합니다.
  /// </summary>
  /// <param name="value">입력 값 (bool 예상)</param>
  /// <param name="targetType">목표 타입 (bool 예상)</param>
  /// <param name="parameter">사용되지 않음</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>입력 값의 반전된 bool 값</returns>
  public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
  {
    if (value is bool boolValue)
    {
      return !boolValue;
    }

    // bool이 아닌 경우 기본값 false 반환
    return true;
  }

  /// <summary>
  /// ConvertBack: View → ViewModel (일반적으로 사용 안 함)
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
  {
    throw new NotSupportedException("ConvertBack은 지원되지 않습니다.");
  }
}
