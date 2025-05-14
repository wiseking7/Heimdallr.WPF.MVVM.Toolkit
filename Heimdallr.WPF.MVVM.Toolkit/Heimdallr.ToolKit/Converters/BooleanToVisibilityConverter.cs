using System.Globalization;
using System.Windows;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// bool 값을 Visibility로 변환하기 위한 ValueConverter입니다.
/// True일 때 보이도록(Visible), False일 때 숨기도록(Collapsed) 처리하는 단방향 또는 양방향 바인딩용 변환기입니다.
/// 싱글톤으로 동작함 (매번 인스턴스 만들지 않음), T 는 본인 Type 이므로 이 클래스는 자기 자신의 인스턴스를 생성해서 재사용합니다.
/// </summary>
public class BooleanToVisibilityConverter : BaseValueConverter<BooleanToVisibilityConverter>
{
  /// <summary>
  /// bool 값이 들어오면 true일 때 Visibility.Visible, false일 때 Visibility.Collapsed 반환
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is bool boolValue)
    {
      // boolValue 의 값이 True 면 Visible 을 반환하고 아니면 Collapsed 를 반환
      return boolValue ? Visibility.Visible : Visibility.Collapsed;
    }

    // value 가 bool 이 아니면 무조건 Collapsed 반환 (null 또는 잘못된 형)
    return Visibility.Collapsed;
  }

  /// <summary>
  /// UI의 Visibility 값을 bool로 되돌림
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetType"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is Visibility visibility)
    {
      return visibility == Visibility.Visible;
    }
    return false;
  }
}
