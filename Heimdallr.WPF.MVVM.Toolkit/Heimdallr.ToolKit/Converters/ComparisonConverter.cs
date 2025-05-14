using System.Globalization;
using System.Windows.Data;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 이 변환기는 CheckBox, RadioButton, ComboBox, ListBox 등 선택 상태를 비교 기반으로 관리할 때 매우 유용
/// 체크박스의 IsChecked 속성을 특정 조건에 따라 true 또는 false 로 설정하고 싶을때 사용
/// </summary>
public class ComparisonConverter : BaseValueConverter<ComparisonConverter>
{
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value == null)
    {
      // value 가 null 일 경우 false 반환
      return false;
    }

    // parameter 가 같으면 True
    return value.Equals(parameter);
  }

  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value: 컨트롤에서 발생한 값 (true인 경우만 의미 있음), parameter: 선택된 경우 반환할 값
    // Binding.DoNothing: 값이 선택되지 않았을 경우 아무 값도 변경하지 않음
    return value?.Equals(true) == true ? parameter : Binding.DoNothing;
  }
}
/* 사용예제
- RadioButton 
  public enum Gender
  {
    Male,
    Female
  }

  public class PersonViewModel
  {
    public Gender SelectedGender { get; set; }
  }

  <StackPanel>
      <RadioButton Content="남자"
                   IsChecked="{Binding SelectedGender, Converter={StaticResource ComparisonConverter}, 
                   ConverterParameter={x:Static local:Gender.Male}, Mode=TwoWay}" />

      <RadioButton Content="여자"
                   IsChecked="{Binding SelectedGender, Converter={StaticResource ComparisonConverter}, 
                   ConverterParameter={x:Static local:Gender.Female}, Mode=TwoWay}" />
  </StackPanel>
 
  SelectedGender == Gender.Male → 첫 번째 라디오 버튼 체크됨
  Radiobutton 클릭 시 → SelectedGender 값이 변경됨 (TwoWay 덕분)

- CheckBox 

  public string Option { get; set; } = "A";

  <CheckBox Content="옵션 A"
          IsChecked="{Binding Option, Converter={StaticResource ComparisonConverter}, 
          ConverterParameter=A, Mode=TwoWay}" />

 Option == "A" → 체크됨
 체크박스 선택 해제 시 → 아무것도 안 함 (ConvertBack: DoNothing)
 다시 체크 → Option = "A" 설정됨
 */
