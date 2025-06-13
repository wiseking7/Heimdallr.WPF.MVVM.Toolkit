using System.Globalization;
using System.Windows;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// bool 값을 Visibility로 변환하는 ValueConverter.
/// 기본 동작은 true → Visible, false → Collapsed.
/// ConverterParameter에 "invert"를 주면 동작을 반전시켜 true → Collapsed, false → Visible.
/// ConverterParameter에 "hidden"을 주면 false일 때 Visibility.Hidden을 반환.
/// "invert;hidden" 같이 세미콜론(;)으로 여러 옵션 지정 가능.
/// </summary>
public class BooleanToVisibilityConverter : BaseValueConverter<BooleanToVisibilityConverter>
{
  /// <summary>
  /// bool → Visibility 변환 메서드
  /// </summary>
  /// <param name="value">입력 값 (bool 예상)</param>
  /// <param name="targetType">목표 타입 (Visibility 예상)</param>
  /// <param name="parameter">옵션 문자열, "invert", "hidden" 조합 가능</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>Visibility.Visible, Visibility.Collapsed, 또는 Visibility.Hidden 반환</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is bool boolValue)
    {
      // parameter가 여러 옵션을 세미콜론(;)으로 받을 수 있도록 분리
      string param = (parameter as string)?.ToLower() ?? string.Empty;
      var options = param.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

      // 옵션 중 invert(반전) 여부 체크
      bool invert = options.Contains("invert");
      // 옵션 중 hidden(숨김방식) 여부 체크
      bool useHidden = options.Contains("hidden");

      // bool 값에 invert 옵션 적용
      bool effectiveValue = boolValue ^ invert;

      // true면 항상 Visible
      if (effectiveValue)
        return Visibility.Visible;

      // false일 때, hidden 옵션에 따라 Hidden 또는 Collapsed 반환
      return useHidden ? Visibility.Hidden : Visibility.Collapsed;
    }

    // bool이 아닌 경우 기본 숨김(Collapsed)
    return Visibility.Collapsed;
  }

  /// <summary>
  /// Visibility → bool 역변환 메서드
  /// </summary>
  /// <param name="value">Visibility 값</param>
  /// <param name="targetType">목표 타입 (bool 예상)</param>
  /// <param name="parameter">옵션 문자열</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>bool 값 반환</returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is Visibility visibility)
    {
      string param = (parameter as string)?.ToLower() ?? string.Empty;
      var options = param.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

      bool invert = options.Contains("invert");
      // Hidden은 false로 간주 (역변환에서는 Hidden/Collapsed 구분 안함)
      bool result = visibility == Visibility.Visible;

      // invert 옵션 적용하여 결과 반전
      return result ^ invert;
    }

    // 기본 false 반환
    return false;
  }
}

