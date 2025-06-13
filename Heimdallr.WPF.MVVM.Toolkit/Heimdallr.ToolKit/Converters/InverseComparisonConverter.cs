using System.Globalization;
using System.Windows.Data;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// value != parameter 일 때 true를 반환하는 반전 비교용 IValueConverter입니다.
/// 예: 특정 항목이 선택되지 않았을 때만 동작(Visible, IsChecked 등)을 수행하고 싶을 때 사용할 수 있습니다.
/// </summary>
public class InverseComparisonConverter : BaseValueConverter<InverseComparisonConverter>
{
  /// <summary>
  /// Convert: value와 parameter가 다르면 true, 같으면 false를 반환합니다.
  /// </summary>
  /// <param name="value">현재 바인딩된 값</param>
  /// <param name="targetType">바인딩 타겟 속성의 형식</param>
  /// <param name="parameter">비교 대상 값</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>true (value != parameter), false (value == parameter)</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value가 null일 경우: parameter가 null이 아니면 true (둘이 다르므로), 같으면 false
    if (value == null)
      return parameter != null;

    // value가 null이 아닐 경우: parameter와 같지 않으면 true 반환
    return !value.Equals(parameter);
  }

  /// <summary>
  /// ConvertBack: UI에서 true → parameter와 다르다는 뜻이므로 아무 일도 하지 않음 (DoNothing).
  /// false → parameter와 같다는 뜻이므로 parameter를 원래 값으로 돌려줍니다.
  /// </summary>
  /// <param name="value">현재 바인딩 값 (일반적으로 bool)</param>
  /// <param name="targetType">대상 타입</param>
  /// <param name="parameter">변환기에서 사용하는 비교 기준 값</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>
  /// - true일 경우: parameter와 같지 않다는 뜻이므로 DoNothing 반환 (바인딩 무시)
  /// - false일 경우: parameter와 같다고 보고 parameter를 반환
  /// </returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // false일 때만 parameter를 반환하고, 나머지는 변경하지 않음
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
