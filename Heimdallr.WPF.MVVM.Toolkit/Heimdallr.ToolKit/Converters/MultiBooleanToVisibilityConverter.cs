using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Heimdallr.ToolKit.Converters;
// 다중 값 bool 가시성 변환기
public class MultiBooleanToVisibilityConverter : MarkupExtension, IMultiValueConverter
{
  public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {
    // 입력 값의 유효성 검사  values.Length != 2: 입력된 값이 두 개가 아니면 조건이 참이 됩니다.
    // !(values[0] is bool isLoggedIn): 첫 번째 값이 bool 타입이 아니면 조건이 참이 됩니다.
    // !(values[1] is bool isLoginButton): 두 번째 값이 bool 타입이 아니면 조건이 참이 됩니다.
    if (values.Length != 2 || !(values[0] is bool isLoggedIn) || !(values[1] is bool isLoginButton))
    {
      // 위 조건 중 하나라도 참이면 Visibility.Collapsed를 반환하여 UI 요소가 보이지 않도록 합니다.
      return Visibility.Collapsed;
    }

    // 여기서는 isLoggedIn 이 false 이고 isLoginButton 이 true 인 경우, 
    // 즉 사용자가 로그인하지 않았고 로그인 버튼이 보이는 상황에서 Visibility.Visible을 반환합니다.
    if (!isLoggedIn && isLoginButton)
    {
      // 이 조건이 충족되지 않으면 아래의 return 문으로 넘어갑니다
      return Visibility.Visible;
    }
    // 위 조건이 충족되지 않는 모든 경우에 대해 Visibility.Collapsed 를 반환합니다. 
    // 이 경우 UI 요소는 보이지 않게 됩니다
    return Visibility.Collapsed;
  }

  public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }

  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return this;
  }
}

/* 이 컨버터는 XAML에서 사용되며, 다음과 같이 두 개의 bool 값을 Visibility로 변환할 수 있습니다 
  <TextBlock Text="Login Button" 
          Visibility="{Binding Path=IsLoginButton, 
                      Converter={StaticResource MultiBooleanToVisibilityConverter},
                        ConverterParameter={Binding Path=IsLoggedIn}}"/> 
 
   이 예시에서는 IsLoggedIn 과 IsLoginButton 이라는 두 개의 bool 속성을 바인딩하여 
   TextBlock 의 Visibility 를 결정합니다. */

