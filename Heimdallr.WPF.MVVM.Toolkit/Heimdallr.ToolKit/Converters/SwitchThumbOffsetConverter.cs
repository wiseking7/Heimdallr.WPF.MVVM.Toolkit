using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 트랙의 실제 너비(ActualWidth)에서 Thumb의 이동 가능한 거리를 계산하는 컨버터
/// </summary>
public class SwitchThumbOffsetConverter : BaseValueConverter<SwitchThumbOffsetConverter>
{
  // 여유 마진 (테두리나 시각적으로 여백을 줄 때 사용)
  private const double MarginBuffer = 4;

  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is double trackWidth && parameter is string thumbWidthString && double.TryParse(thumbWidthString, out double thumbWidth))
    {
      return Math.Max(0, trackWidth - thumbWidth - MarginBuffer);
    }

    // 디버깅이나 예외 상황 방지를 위해 0 반환
    return 0.0;
  }

  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    => throw new NotImplementedException();

}
