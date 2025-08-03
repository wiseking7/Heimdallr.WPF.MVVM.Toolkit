using System.Globalization;
using System.Windows;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 문자열이 null이거나 빈 문자열일 경우 Visibility.Collapsed를 반환하고,
/// 그렇지 않으면 Visibility.Visible을 반환하는 값 변환기입니다.
/// Invert 속성을 사용하면 결과를 반대로 바꿀 수 있습니다.
/// </summary>
public class NullOrEmptyToVisibilityConverter : BaseValueConverter<NullOrEmptyToVisibilityConverter>
{
  /// <summary>
  /// 변환 결과를 반전할지 여부를 설정하는 속성입니다.
  /// true로 설정하면, null 또는 빈 문자열일 때 Visible을 반환하고,
  /// 값이 있을 때 Collapsed를 반환합니다.
  /// </summary>
  public bool Invert { get; set; } = false;

  /// <summary>
  /// 값 변환 메서드
  /// ViewModel -> View 로 데이터가 전달될 때 호출됩니다.
  /// </summary>
  /// <param name="value">바인딩된 값 (예: 문자열)</param>
  /// <param name="targetType">대상 속성 타입 (예: Visibility)</param>
  /// <param name="parameter">추가 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>Visibility.Visible 또는 Visibility.Collapsed</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // 문자열이 null이거나 빈 문자열인지 확인
    bool isNullOrEmpty = string.IsNullOrEmpty(value as string);

    // Invert가 true일 경우 결과 반전
    if (Invert)
      isNullOrEmpty = !isNullOrEmpty;

    // 조건에 따라 Visibility 값을 반환
    return isNullOrEmpty ? Visibility.Collapsed : Visibility.Visible;
  }

  /// <summary>
  /// View -> ViewModel 방향 변환이 필요할 경우 구현되지만,
  /// 본 컨버터는 OneWay 바인딩 용도이므로 예외 처리
  /// </summary>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}
