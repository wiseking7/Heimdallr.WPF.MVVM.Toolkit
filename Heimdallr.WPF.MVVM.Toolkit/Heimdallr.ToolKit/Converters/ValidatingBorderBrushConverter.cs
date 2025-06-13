using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Heimdallr.ToolKit.Converters;
/// <summary>
/// 다중 바인딩에서 유효성 검사 결과와 기본 색상 문자열을 받아
/// 유효하지 않을 경우 빨간색 테두리를, 그렇지 않으면 기본 색상을 반환하는 컨버터입니다.
/// </summary>
public class ValidatingBorderBrushConverter : IMultiValueConverter
{
  /// <summary>
  /// values 배열에서 첫 번째 값은 유효성 검사 결과(bool),
  /// 두 번째 값은 기본 테두리 색상(Hex 문자열)입니다.
  /// </summary>
  /// <param name="values">[0]: bool isValid, [1]: string hexColor</param>
  /// <param name="targetType">바인딩 대상 타입 (Brush)</param>
  /// <param name="parameter">옵션 파라미터 (미사용)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>유효하지 않으면 빨간색 브러시, 유효하면 기본 색상 브러시</returns>
  public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {
    // 입력값이 null이거나 2개 미만이면 예외 발생
    if (values == null || values.Length < 2)
    {
      throw new ArgumentException("Converter 에 제공된 값의 수가 잘못되었습니다.");
    }

    // 첫 번째 값이 bool 타입이고 true인지 확인 (유효성 검사 결과)
    bool isValid = values[0] is bool valid && valid;

    // 두 번째 값은 기본 색상으로 쓰일 Hex 문자열 (예: "#FF00FF")
    string? hexColor = values[1] as string;

    // 기본 브러시 초기값 (투명색)
    Brush defaultBrush = Brushes.Transparent;

    // 유효한 Hex 색상 문자열이면 SolidColorBrush로 변환 시도
    if (!string.IsNullOrEmpty(hexColor))
    {
      try
      {
        defaultBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
      }
      catch
      {
        // 변환 실패 시 기본값 유지 (투명색)
        defaultBrush = Brushes.Transparent;
      }
    }

    // 유효성 검사 결과가 false면 빨간색 테두리 반환, true면 기본 브러시 반환
    return isValid ? defaultBrush : Brushes.Red;
  }

  /// <summary>
  /// ConvertBack은 구현하지 않음 (단방향 변환)
  /// </summary>
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

