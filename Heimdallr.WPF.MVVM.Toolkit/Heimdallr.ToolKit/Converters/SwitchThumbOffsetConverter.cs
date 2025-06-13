using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 스위치 컨트롤의 트랙(Track) 실제 너비(ActualWidth)에서
/// Thumb(움직이는 버튼)의 이동 가능한 거리를 계산해 반환하는 컨버터입니다.
/// </summary>
public class SwitchThumbOffsetConverter : BaseValueConverter<SwitchThumbOffsetConverter>
{
  // Thumb가 트랙 안에서 움직일 때 여유 공간(마진)으로 확보할 픽셀 수
  // 보통 테두리 두께나 시각적 여백을 위해 설정
  private const double MarginBuffer = 4;

  /// <summary>
  /// Convert 메서드:
  /// 트랙 너비와 Thumb 너비를 입력받아 Thumb가 움직일 수 있는 최대 거리를 계산합니다.
  /// </summary>
  /// <param name="value">트랙의 ActualWidth (double)</param>
  /// <param name="targetType">대상 타입 (일반적으로 double)</param>
  /// <param name="parameter">Thumb의 너비를 나타내는 문자열 (string)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>Thumb가 이동 가능한 최대 거리(double)</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value가 double 타입의 트랙 너비이고,
    // parameter가 Thumb 너비를 의미하는 string이며 double로 변환 가능할 때
    if (value is double trackWidth && parameter is string thumbWidthString && double.TryParse(thumbWidthString, out double thumbWidth))
    {
      // Thumb가 움직일 수 있는 거리 = 트랙 너비 - Thumb 너비 - 여유 마진
      // Math.Max(0, ...)를 사용하여 음수가 나오지 않도록 방지
      return Math.Max(0, trackWidth - thumbWidth - MarginBuffer);
    }

    // 입력 값이 올바르지 않거나 변환 실패 시, 안전하게 0 반환
    return 0.0;
  }

  /// <summary>
  /// ConvertBack은 구현하지 않음 (단방향 변환)
  /// </summary>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    => throw new NotImplementedException();
}

