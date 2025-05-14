using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 다중 값(MultiBinding) 비교를 위한 컨버터입니다.
/// 첫 번째 값과 두 번째 값이 같은 경우 true, 그렇지 않으면 false를 반환합니다.
/// </summary>
public class ComparisonMultiConverter : MarkupExtension, IMultiValueConverter
{
  /// <summary>
  /// MultiBinding에서 전달된 첫 번째 값과 두 번째 값을 비교합니다.
  /// </summary>
  /// <param name="values">비교할 값 배열입니다. 최소 2개 이상이어야 합니다.</param>
  /// <param name="targetType">타겟 바인딩 타입 (사용되지 않음)</param>
  /// <param name="parameter">추가 파라미터 (사용되지 않음)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>값이 같으면 true, 다르면 false</returns>
  public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {
    if (values == null || values.Length < 2)
      return false;

    var first = values[0];
    var second = values[1];

    if (first == null || second == null)
      return false;

    return first.Equals(second);
  }

  /// <summary>
  /// ConvertBack은 지원하지 않습니다. (예외 발생)
  /// </summary>
  public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
  {
    throw new NotSupportedException("ComparisonMultiConverter는 ConvertBack을 지원하지 않습니다");
  }

  /// <summary>
  /// XAML에서 사용할 수 있도록 MarkupExtension을 통해 인스턴스를 반환합니다.
  /// </summary>
  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return this;
  }
}


/* 사용예제
 
  <Window.Resources>
    <local:ComparisonMultiConverter x:Key="ComparisonMultiConverter"/>
  </Window.Resources>

  <CheckBox Content="같은 경우 체크"
            IsChecked="{Binding RelativeSource={RelativeSource Self}, 
                        Path=IsChecked, 
                        Mode=TwoWay}">
      <CheckBox.IsChecked>
          <MultiBinding Converter="{StaticResource ComparisonMultiConverter}">
              <Binding Path="FirstValue" />
              <Binding Path="SecondValue" />
          </MultiBinding>
      </CheckBox.IsChecked>
  </CheckBox>


 */