using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// WPF의 멀티 바인딩에서 사용하는 인터페이스로,
/// 여러 개의 소스 값을 변환해 하나의 타겟 값으로 바꾸거나 그 반대도 처리할 수 있게 해줍니다.
/// 이 클래스는 여러 값을 입력받아 하나의 객체로 반환하는 Convert 메서드를 구현합니다
/// </summary>
public class HelperConverter : MarkupExtension, IMultiValueConverter
{
  // HelperConverter 클래스의 싱글턴(static instance) 역할을 합니다.
  // 어딘가에서 인스턴스를 생성하지 않고도 HelperConverter.Current로 접근할 수 있어 XAML에서 쉽게 바인딩에 사용 가능합니다.
  public static readonly HelperConverter Current = new HelperConverter();

  /// <summary>
  /// Convert 메서드
  /// </summary>
  /// <param name="values">values[] 바인딩된 여러 값이 배열로 들어옵니다. 예: { TextBox.Text, Button.IsEnabled }</param>
  /// <param name="targetType">변환 후 결과가 바인딩될 대상의 타입 (거의 사용 안 됨)</param>
  /// <param name="parameter">XAML 에서 바인딩 시 설정한 ConverterParameter 값. 여기서는 DependencyProperty로 캐스팅되어 사용됩니다.</param>
  /// <param name="culture">지역화/국제화를 위한 CultureInfo 객체입니다. 날짜/숫자 등의 포맷팅에 사용될 수 있으나 여기서는 사용 안함</param>
  /// <returns></returns>
  public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {
    // 첫 번째 값 (values[0])과 전달된 매개변수 (parameter, DependencyProperty 타입)를
    // 하나의 Tuple<object, DependencyProperty>로 묶어서 반환합니다.
    return Tuple.Create(values[0], (DependencyProperty)parameter);
  }

  /// <summary>
  /// 양방향 바인딩 시 사용되지만 현재는 한 방향 바인딩만 허용하고 예외를 던지도록 되어 있습니다.
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetTypes"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }

  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return this;
  }
}
// 전체 목적 요약
/* 이 컨버터는 멀티 바인딩을 통해 특정 DependencyObject의 인스턴스와 해당 속성(DependencyProperty) 을 
   한 번에 바인딩 컨텍스트에 넘기고, 이후 이를 기반으로 동적으로 속성 접근 또는 설정을 할 수 있도록 돕습니다. */

/* 사용 방법 
 * 
<MultiBinding Converter="{x:Static local:HelperConverter.Current}" ConverterParameter="{x:Static TextBox.TextProperty}">
<Binding ElementName="myTextBox" />
</MultiBinding>

   이 경우 반환되는 값은 다음과 같을 수 있습니다.

   Tuple.Create(myTextBox, TextBox.TextProperty)

   그 후 다른 로직에서 이 Tuple을 이용하여 동적으로 속성 값을 설정하거나 가져올 수 있습니다.

   에제 시나리오
   버튼을 클릭하면, 바인딩된 TextBox의 Text 속성을 동적으로 설정합니다.
 
<Window.Resources>
  <local:HelperConverter x:Key="HelperConverter" />
</Window.Resources>

  <StackPanel Margin="20">
    <TextBox x:Name="MyTextBox" Width="200" Height="30" />
    <Button Content="텍스트 설정" Click="Button_Click" Margin="0,10,0,0">
      <Button.Tag>
          <MultiBinding Converter="{StaticResource HelperConverter}" ConverterParameter="{x:Static TextBox.TextProperty}">
              <Binding ElementName="MyTextBox" />
          </MultiBinding>
      </Button.Tag>
    </Button>
  </StackPanel>

private void Button_Click(object sender, RoutedEventArgs e)
{
  if (sender is Button btn && btn.Tag is Tuple<object, DependencyProperty> tuple)
  {
    var targetObject = tuple.Item1 as DependencyObject;
    var targetProperty = tuple.Item2;

    if (targetObject != null && targetProperty != null)
    {
      // 동적으로 텍스트 설정
      targetObject.SetValue(targetProperty, "안녕하세요, WPF!");
    }
  }
}

결과

버튼을 클릭하면 TextBox의 Text 속성이 "안녕하세요, WPF!"로 설정됩니다.
이 과정에서 바인딩된 객체와 속성 정보가 Tuple로 전달되어 활용됩니다.

 */

