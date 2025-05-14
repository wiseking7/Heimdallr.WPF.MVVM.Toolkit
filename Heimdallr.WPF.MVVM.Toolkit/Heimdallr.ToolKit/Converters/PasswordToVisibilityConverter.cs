using System.Globalization;
using System.Windows;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 비밀번호 입력이나 특정 값의 상태에 따라 Visibility 값을 반환하는 역할을 합니다. 
/// 이 변환기는 비밀번호의 상태나 특정 조건에 따라 UI 요소의 표시 여부를 제어할 때 유용하게 사용됩니다.
/// </summary>
public class PasswordToVisibilityConverter : BaseValueConverter<PasswordToVisibilityConverter>
{
  // Convert: 비밀번호 길이에 따라 텍스트 변경 및 Visibility 설정
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is int passwordLength)
    {
      // 비밀번호 길이가 0일 때
      if (passwordLength == 0)
      {
        // 비밀번호가 비어있을 때 메시지와 Visibility 설정
        return new { Message = "비밀번호가 비어 있습니다", Visibility = Visibility.Visible };
      }
      else
      {
        // 비밀번호가 있을 때 다른 메시지와 Visibility 설정
        return new { Message = "비밀번호가 입력되었습니다", Visibility = Visibility.Hidden };  // 레이아웃에는 영향을 주지 않음
      }
    }
    return new { Message = string.Empty, Visibility = Visibility.Collapsed };  // 기본값: 비어 있을 때는 Collapsed
  }

  // ConvertBack은 구현하지 않음
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