using System.Globalization;
using System.Windows.Data;

namespace Heimdallr.ToolKit.Converters;
/// <summary>
/// value != parameter일 때 true 반환하는 반전된 버전입니다.
/// 예를 들어 어떤 항목이 선택되지 않았을 때만 체크되도록 할 수 있습니다.
/// </summary>
public class InverseComparisonConverter : BaseValueConverter<InverseComparisonConverter>
{
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value == null)
      return parameter != null;

    return !value.Equals(parameter);
  }

  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // true일 때는 parameter와 같지 않다는 뜻이므로 DoNothing
    // false일 때는 parameter와 같다고 보고 parameter 반환
    return value?.Equals(false) == true ? parameter : Binding.DoNothing;
  }
}

/* 사용예제 
 <Window.Resources>
    <local:InverseComparisonConverter x:Key="InverseComparisonConverter"/>
 </Window.Resources>

 <RadioButton Content="선택 안 되었을 때 체크"
              IsChecked="{Binding SelectedValue, 
                         Converter={StaticResource InverseComparisonConverter}, 
                         ConverterParameter=Admin}" />

 */
