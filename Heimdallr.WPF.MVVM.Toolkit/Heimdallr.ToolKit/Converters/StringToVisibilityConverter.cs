using System.Globalization;
using System.Windows;

namespace Heimdallr.ToolKit.Converters;
/// <summary>
/// 문자열이 비어 있는지 여부에 따라 Visibility를 반환하는 컨버터입니다.
/// 문자열이 비어 있으면 Visibility.Visible을 반환하고, 그렇지 않으면 Visibility.Collapsed를 반환합니다.
/// </summary>
public class StringToVisibilityConverter : BaseValueConverter<StringToVisibilityConverter>
{
  /// <summary>
  /// 문자열이 비어 있으면 Visibility.Visible, 그렇지 않으면 Visibility.Collapsed를 반환합니다.
  /// </summary>
  /// <param name="value">바인딩되는 값 (string)</param>
  /// <param name="targetType">타겟 타입 (Visibility)</param>
  /// <param name="parameter">추가 매개변수 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>문자열이 비어 있으면 Visibility.Visible, 아니면 Visibility.Collapsed</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value가 문자열일 경우
    if (value is string strValue)
    {
      // 문자열이 비어 있으면 Visibility.Visible, 아니면 Visibility.Collapsed 반환
      return string.IsNullOrEmpty(strValue) ? Visibility.Visible : Visibility.Collapsed;
    }

    // value가 문자열이 아닐 경우 기본값으로 Collapsed 반환
    return Visibility.Collapsed;
  }

  /// <summary>
  /// ConvertBack 메서드는 구현되지 않음 (단방향 바인딩만 사용)
  /// </summary>
  /// <param name="value">바인딩되는 값</param>
  /// <param name="targetType">타겟 타입</param>
  /// <param name="parameter">추가 매개변수</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>NotImplementedException 예외를 던짐</returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}

/* 사용예제
  <Grid>
    <!-- 문자열이 비어 있을 경우만 표시되도록 설정 -->
    <TextBlock Text="이 텍스트는 문자열이 비어 있을 때만 보입니다."
               Visibility="{Binding YourStringProperty, 
                Converter={StaticResource StringToVisibilityConverter}}" />
  </Grid>

1. 사용자 비밀번호 입력란
2. 유효성 검사 메시지 표시
3. 텍스트 박스 입력 후 안내 메시지 변경
4. 로그인 화면의 "비밀번호를 입력하세요" 메시지
5. 배너 또는 알림 메시지 표시
6. 다국어 지원에서 빈 번역 처리
 
 */
