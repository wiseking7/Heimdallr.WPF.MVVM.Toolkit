using System.Globalization;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// HeimdallrScrollBar의 하단 스크롤 제한을 판단하는 컨버터입니다.
/// 스크롤 위치가 ScrollableHeight 이상인 경우 true를 반환하여
/// ScrollBar의 Down 버튼을 비활성화할 수 있도록 도와줍니다.
/// </summary>
public class ScrollLimitConverter : BaseMultiValueConverter<ScrollLimitConverter>
{
  /// <summary>
  /// MultiBinding을 통해 전달된 두 값(VerticalOffset, ScrollableHeight)을 비교합니다.
  /// 스크롤 위치가 최하단에 도달했는지 여부를 bool로 반환합니다.
  /// </summary>
  /// <param name="values">
  /// [0] VerticalOffset: 현재 스크롤 위치
  /// [1] ScrollableHeight: 스크롤 가능한 최대 높이
  /// </param>
  /// <param name="targetType">바인딩 대상 형식</param>
  /// <param name="parameter">추가 파라미터 (미사용)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>
  /// VerticalOffset >= ScrollableHeight 인 경우 true (스크롤이 맨 아래)
  /// 아니면 false
  /// </returns>
  public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {
    if (values.Length == 2 &&
        values[0] is double offset &&
        values[1] is double scrollableHeight)
    {
      return offset >= scrollableHeight;
    }

    return false;
  }

  /// <summary>
  /// ConvertBack은 사용하지 않으므로 NotImplementedException을 발생시킵니다.
  /// </summary>
  /// <param name="value">바인딩 값</param>
  /// <param name="targetTypes">타겟 형식 배열</param>
  /// <param name="parameter">추가 파라미터</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>예외 발생</returns>
  public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
      => throw new NotImplementedException();
}

