using System.Globalization;
using System.Windows;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 비밀번호 입력이나 특정 값의 상태에 따라 Visibility 값을 반환하는 WPF IValueConverter 구현입니다.
/// 주로 비밀번호가 입력되었는지 여부에 따라 메시지나 UI 요소의 Visibility를 제어할 때 사용됩니다.
/// 반환값은 메시지(`Message`)와 표시 상태(`Visibility`)를 포함한 익명 객체입니다.
/// </summary>
public class PasswordToVisibilityConverter : BaseValueConverter<PasswordToVisibilityConverter>
{
  /// <summary>
  /// Convert: 비밀번호 길이에 따라 메시지와 Visibility를 포함한 익명 객체를 반환합니다.
  /// - 길이 0 → "비밀번호가 비어 있습니다", Visibility.Visible
  /// - 길이 > 0 → "비밀번호가 입력되었습니다", Visibility.Hidden
  /// - int가 아닌 경우 → 빈 메시지, Visibility.Collapsed
  /// </summary>
  /// <param name="value">입력 값 (보통 Password의 길이인 int)</param>
  /// <param name="targetType">바인딩 대상 타입 (보통 object)</param>
  /// <param name="parameter">추가 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>
  /// 익명 객체 { Message = string, Visibility = System.Windows.Visibility } 형태.
  /// </returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is int passwordLength)
    {
      // 비밀번호가 입력되지 않았을 경우
      if (passwordLength == 0)
      {
        return new
        {
          Message = "비밀번호가 비어 있습니다",
          Visibility = Visibility.Visible // 화면에 표시
        };
      }
      else
      {
        return new
        {
          Message = "비밀번호가 입력되었습니다",
          Visibility = Visibility.Hidden // 공간은 유지하되 시각적으로 숨김
        };
      }
    }

    // 비정상적인 값일 경우 기본 처리
    return new
    {
      Message = string.Empty,
      Visibility = Visibility.Collapsed // 완전히 숨김 (공간도 제거)
    };
  }

  /// <summary>
  /// ConvertBack은 사용하지 않으며, 호출 시 NotImplementedException을 발생시킵니다.
  /// </summary>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}
/* 사용예제
  <Grid>
    <!-- 비밀번호 입력란 -->
    <PasswordBox Name="PasswordBox" Width="200" Height="30" VerticalAlignment="Top" HorizontalAlignment="Left" Margin="10" />

    <!-- 비밀번호 길이가 0일 때만 표시되는 UI 요소 -->
    <TextBlock Text="Password is empty" 
               Visibility="{Binding ElementName=PasswordBox, Path=Password.Length, 
               Converter={StaticResource PasswordToVisibilityConverter}}" 
               VerticalAlignment="Top" HorizontalAlignment="Left" Margin="10,50,0,0"/>
  </Grid> 
 
 */