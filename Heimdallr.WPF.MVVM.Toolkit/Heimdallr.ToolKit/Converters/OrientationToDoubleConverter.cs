using System.Globalization;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// Orientation 값(Vertical 또는 Horizontal)에 따라
/// 사전에 설정된 두 개의 double 값 중 하나를 반환하는 컨버터입니다.
/// 예를 들어, Vertical이면 VerticalValue를, Horizontal이면 HorizontalValue를 반환합니다.
/// 
/// 이 컨버터는 주로 XAML 바인딩에서 Orientation에 따라 Width, Height 등
/// 특정 속성 값을 동적으로 변경할 때 사용됩니다.
/// </summary>
public class OrientationToDoubleConverter : BaseValueConverter<OrientationToDoubleConverter>
{
  /// <summary>
  /// Orientation이 Vertical일 때 반환할 double 값입니다.
  /// </summary>
  public double VerticalValue { get; set; }

  /// <summary>
  /// Orientation이 Horizontal일 때 반환할 double 값입니다.
  /// </summary>
  public double HorizontalValue { get; set; }

  /// <summary>
  /// Orientation 값에 따라 VerticalValue 또는 HorizontalValue를 반환합니다.
  /// value 파라미터는 Orientation 타입이어야 하며,
  /// Vertical이면 VerticalValue, Horizontal이면 HorizontalValue를 반환합니다.
  /// value가 Orientation 타입이 아니거나 null일 경우 VerticalValue를 기본 반환합니다.
  /// </summary>
  /// <param name="value">변환할 Orientation 값</param>
  /// <param name="targetType">목표 타입 (사용하지 않음)</param>
  /// <param name="parameter">변환에 사용하지 않음</param>
  /// <param name="culture">문화권 정보 (사용하지 않음)</param>
  /// <returns>VerticalValue 또는 HorizontalValue 중 하나</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is Orientation o)
    {
      return o == Orientation.Vertical ? VerticalValue : HorizontalValue;
    }
    return VerticalValue;
  }

  /// <summary>
  /// 이 컨버터는 단방향 변환만 지원하므로, ConvertBack은 구현하지 않고 예외를 던집니다.
  /// </summary>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    => throw new NotImplementedException();
}
