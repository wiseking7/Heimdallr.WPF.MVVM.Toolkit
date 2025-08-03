using Heimdallr.ToolKit.Enums;
using System.Globalization;
using System.Windows;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// PathIconType 값을 기반으로 Visibility 값을 반환하는 변환기(Converter).
/// 
/// - PathIconType이 None이 아니면 → Visibility.Visible
/// - PathIconType이 None이면 → Visibility.Collapsed
///
/// 이 변환기는 주로 XAML에서 PathIcon 속성이 설정되어 있는 경우에만
/// 아이콘을 표시하기 위한 용도로 사용됩니다.
///   /// </summary>
public class PathIconToVisibilityConverter : BaseValueConverter<PathIconToVisibilityConverter>
{
  /// <summary>
  /// PathIconType → Visibility 변환 함수.
  /// 
  /// 예:
  ///   - value = PathIconType.Barcode → Visible
  ///   - value = PathIconType.None 또는 null → Collapsed
  /// </summary>
  /// <param name="value">바인딩된 값 (PathIconType enum)</param>
  /// <param name="targetType">변환할 대상 형식 (보통 Visibility)</param>
  /// <param name="parameter">추가 매개변수 (사용 안 함)</param>
  /// <param name="culture">문화권 정보 (사용 안 함)</param>
  /// <returns>Visibility.Visible 또는 Visibility.Collapsed</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // PathIconType 타입인지 확인하고, None이 아니면 Visible 반환
    if (value is PathIconType icon && icon != PathIconType.None)
      return Visibility.Visible;

    // null 이거나 None이면 아이콘 숨김
    return Visibility.Collapsed;
  }

  /// <summary>
  /// ConvertBack은 구현하지 않음. UI에서 역변환은 필요 없기 때문.
  /// </summary>
  /// <exception cref="NotImplementedException">
  /// 역변환이 호출되면 예외를 던집니다. OneWay 바인딩에서만 사용됩니다.
  /// </exception>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}
