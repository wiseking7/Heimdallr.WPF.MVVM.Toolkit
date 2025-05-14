using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Heimdallr.ToolKit.Converters;
public class ValidatingBorderBrushConverter : IMultiValueConverter
{
  public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {
    // 유효성 검사 및 기본 색상 값이 정상적으로 전달되었는지 확인
    if (values == null || values.Length < 2)
    {
      throw new ArgumentException("Invalid number of values provided to the converter.");
    }

    // 첫 번째 값이 bool 타입인지 체크
    bool isValid = values[0] is bool valid && valid;

    // 두 번째 값이 Hex 색상 코드인지 받기
    string? hexColor = values[1] as string;

    // Hex 색상 코드가 유효하다면 SolidColorBrush로 변환
    Brush defaultBrush = Brushes.Transparent;
    if (!string.IsNullOrEmpty(hexColor))
    {
      try
      {
        // Hex 색상 코드가 유효하면 SolidColorBrush로 변환
        defaultBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
      }
      catch
      {
        // 예외가 발생하면 기본값을 사용
        defaultBrush = Brushes.Transparent;
      }
    }

    // 유효성 검사 실패 시 빨간색 테두리, 아니면 기본 색상
    return isValid ? Brushes.Red : defaultBrush;
  }

  // ConvertBack은 구현되지 않음
  public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}
/* 사용예제
   <TextBox>
    <TextBox.BorderBrush>
      <MultiBinding Converter="{StaticResource ValidatingBorderBrushConverter}">
        <!-- 유효성 검사 결과 값 (bool 타입) -->
        <Binding Path="IsValid" />
        <!-- 기본 BorderBrush 값 -->
        <Binding Path="HexColor" />
      </MultiBinding>
    </TextBox.BorderBrush>
  </TextBox>

 IsValid: 이 값은 모델에서 바인딩되는 유효성 검사 결과입니다. 
          예를 들어, 텍스트 박스의 값이 올바르지 않으면 IsValid가 false로 설정될 수 있습니다.

 BorderBrush: 기본 BorderBrush 색상을 바인딩합니다. 
              유효성 검사에 실패한 경우 빨간색(Brushes.Red)으로 변경되고, 그렇지 않으면 기존 색상으로 유지됩니다.

 public class MyViewModel : INotifyPropertyChanged
{
  private bool isValid;
  private Brush borderBrush;

  public bool IsValid
  {
    get { return isValid; }
    set
    {
      isValid = value;
      OnPropertyChanged(nameof(IsValid));
    }
  }

  public string HexColor
  {
    get { return hexColor; }
    set
    {
      hexColor = value;
      OnPropertyChanged(nameof(HexColor));
    }
  }

  // INotifyPropertyChanged 구현
  public event PropertyChangedEventHandler PropertyChanged;
  protected void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}
 

 */

