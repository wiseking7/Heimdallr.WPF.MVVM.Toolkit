using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// Convert 메서드는 여러 입력값을 받아 하나의 출력값으로 변환하는 로직을 구현합니다.
/// IMultiValueConverter 인터페이스를 구현하여 XAML의 MultiBinding에서 사용됩니다.
/// </summary>
public class HeimdallrRingSpinnerConverter : IMultiValueConverter
{
  /// <summary>
  /// 여러 입력값을 받아, 링 스피너의 DashArray 값을 계산하여 반환합니다.
  /// 입력값: diameter (지름), thickness (선 두께)
  /// 반환값: DoubleCollection (DashArray에 사용됨)
  /// </summary>
  /// <param name="values">입력값 배열 (지름과 두께 기대)</param>
  /// <param name="targetType">변환 목표 타입 (보통 DoubleCollection)</param>
  /// <param name="parameter">부가 매개변수 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보 (사용하지 않음)</param>
  /// <returns>DashArray로 사용할 DoubleCollection</returns>
  public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {
    // 1. 입력값 유효성 검사
    // values 길이가 2 이상인지, 그리고 첫 번째와 두 번째 값이 double로 변환 가능한지 확인
    if (values.Length < 2 ||
        !double.TryParse(values[0].ToString(), out double diameter) ||
        !double.TryParse(values[1].ToString(), out double thickness))
    {
      // 유효하지 않으면 길이가 1인 DoubleCollection(0.0) 반환
      // 0.0은 선 길이나 간격이 없음을 의미하며, 스피너가 그려지지 않음
      return new DoubleCollection(new[] { 0.0 });
    }

    // 2. 지름을 기준으로 원의 둘레 계산 (circumference = π * diameter)
    double circumference = Math.PI * diameter;

    // 3. 선 길이(lineLength)를 원둘레의 75%로 설정
    // 링 스피너의 시각적 선 길이를 둘레의 3/4로 정함
    double lineLength = circumference * 0.75;

    // 4. 선 길이를 제외한 나머지는 빈 간격(gapLength)으로 설정
    double gapLength = circumference - lineLength;

    // 5. 선 길이와 빈 간격을 선 두께로 나누어 DashArray 값 생성
    // DashArray는 선과 간격의 상대적 길이를 정의함
    return new DoubleCollection(new[] { lineLength / thickness, gapLength / thickness });
  }

  /// <summary>
  /// ConvertBack 메서드는 Convert와 반대 방향 변환용이나, 이 컨버터에서는 미구현입니다.
  /// </summary>
  /// <param name="value">입력값</param>
  /// <param name="targetTypes">목표 타입 배열</param>
  /// <param name="parameter">부가 매개변수</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>미구현으로 예외 발생</returns>
  public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
  {
    // 이 변환기는 단방향 변환만 지원하므로 예외 던짐
    throw new NotImplementedException();
  }
}
