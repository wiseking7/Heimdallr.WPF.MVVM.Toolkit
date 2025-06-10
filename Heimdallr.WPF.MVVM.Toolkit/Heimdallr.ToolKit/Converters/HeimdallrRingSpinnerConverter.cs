using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Heimdallr.ToolKit.Converters;

// Convert 메서드는 여러 값을 하나로 변환하는 로직을 담고 있음
// IMultiValueConverter 인터페이스를 구현하며, 여러 값을 입력으로 받아 하나의 결과를 반환.
public class HeimdallrRingSpinnerConverter : IMultiValueConverter
{
  public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {
    // 1. 입력 값의 유효성 검사
    // values 배열의 길이가 2 이상이어야 하며, 첫 번째 값(diameter)과 두 번째 값(thickness)이 double로 변환될 수 있어야 함
    if (values.Length < 2 ||
               !double.TryParse(values[0].ToString(), out double diameter) ||
               !double.TryParse(values[1].ToString(), out double thickness))
    {
      // 값이 유효하지 않으면 기본 값으로 DoubleCollection에 0을 반환
      // 0은 원형 스피너의 선 길이나 간격이 없음을 의미
      return new DoubleCollection(new[] { 0.0 });
    }

    // 2. 지름(diameter)을 기반으로 원의 둘레(circumference) 계산
    double circumference = Math.PI * diameter;

    // 3. 선의 길이(lineLength)를 원둘레의 75%로 설정
    // Ring Spinner에서 표시되는 선의 길이를 원둘레의 75%로 설정
    double lineLength = circumference * 0.75;

    // 4. 빈 간격(gapLength)은 원둘레에서 선 길이를 빼서 구함
    // Ring Spinner에서 선과 선 사이의 빈 간격을 구함
    double gapLength = circumference - lineLength;

    // 5. 결과값을 DoubleCollection에 담아서 반환
    // lineLength / thickness는 선의 길이를 두께로 나눈 값을 반환
    // gapLength / thickness는 간격을 두께로 나눈 값을 반환
    return new DoubleCollection(new[] { lineLength / thickness, gapLength / thickness });
  }

  // ConvertBack 메서드는 이 변환기를 두 번의 변환(즉, 앞뒤 변환)을 할 때 사용되지만
  // 현재는 사용되지 않으므로 예외를 던짐
  public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}
