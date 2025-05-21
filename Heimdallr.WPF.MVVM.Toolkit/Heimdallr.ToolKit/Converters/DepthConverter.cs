using System.Globalization;
using System.Windows;

namespace Heimdallr.ToolKit.Converters;

// DepthConverter 클래스는 BaseValueConverter<T>를 상속한 클래스입니다.
// BaseValueConverter는 일반적으로 IValueConverter를 구현하는 제네릭 기반 클래스입니다.
// TreeView 의 하위자식 마진 구하기
public class DepthConverter : BaseValueConverter<DepthConverter>
{
  // Convert 메서드는 뷰모델의 값(예: 계층 깊이)을 UI 요소의 속성(예: Margin)으로 변환할 때 사용됩니다.
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value는 바인딩된 값으로, 여기서는 depth(계층 깊이)를 의미합니다. 
    int depth = (int)value;     // int로 캐스팅

    // 왼쪽 여백을 깊이에 따라 설정합니다.
    // 예: 깊이 0 → Margin(0,0,0,0), 깊이 1 → Margin(20,0,0,0), 깊이 2 → Margin(40,0,0,0) 등
    Thickness margin = new Thickness(depth * 20, 0, 0, 0);

    // 결과로 Margin(Thickness 객체)를 반환
    return margin;
  }

  // ConvertBack은 일반적으로 양방향 바인딩에 필요하지만,
  // 여기서는 단방향 변환이므로 구현하지 않고 예외를 던집니다.
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // 역변환은 사용하지 않음
    throw new NotImplementedException();
  }
}
