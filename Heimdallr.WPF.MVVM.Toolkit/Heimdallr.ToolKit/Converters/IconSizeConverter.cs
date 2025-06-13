using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// thumbSize 값을 받아서 아이콘 크기로 변환하는 컨버터
/// </summary>
public class IconSizeConverter : BaseValueConverter<IconSizeConverter>
{
  /// <summary>
  /// Convert 메서드: thumbSize를 받아 60% 크기로 축소하여 반환합니다.
  /// </summary>
  /// <param name="value">입력 값으로 보통 Thumb 컨트롤의 크기(double)입니다.</param>
  /// <param name="targetType">목표 타입, 보통 double입니다.</param>
  /// <param name="parameter">추가 매개변수로 보통 사용하지 않습니다.</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>thumbSize의 60% 값(double) 또는 기본값 15.0</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value가 double 타입인지 확인 (thumbSize)
    if (value is double thumbSize)
    {
      // thumbSize 크기의 60%를 계산하여 아이콘 크기로 반환
      return thumbSize * 0.6;
    }

    // value가 double 타입이 아니면 기본 아이콘 크기 15.0을 반환
    return 15.0;
  }

  /// <summary>
  /// ConvertBack은 구현하지 않음 (단방향 변환 전용)
  /// </summary>
  /// <param name="value">변환된 값</param>
  /// <param name="targetType">목표 타입</param>
  /// <param name="parameter">추가 매개변수</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>예외를 던짐</returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    => throw new NotImplementedException();
}
