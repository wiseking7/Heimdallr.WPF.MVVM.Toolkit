
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// Enum 값을 문자열로 변환하는 컨버터입니다.
/// DescriptionAttribute가 있으면 해당 속성의 Description 값을 반환하고,
/// 없으면 Enum의 기본 ToString() 값을 반환합니다.
/// </summary>
public class EnumToTextConverter : BaseValueConverter<EnumToTextConverter>
{
  /// <summary>
  /// Enum 값을 문자열로 변환합니다.
  /// </summary>
  /// <param name="value">변환할 Enum 값</param>
  /// <param name="targetType">목표 타입 (보통 string)</param>
  /// <param name="parameter">변환에 사용할 추가 매개변수 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보 (사용하지 않음)</param>
  /// <returns>Enum의 DescriptionAttribute가 있으면 그 Description 문자열, 없으면 Enum의 이름 문자열</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value가 Enum 타입인지 체크
    if (value is Enum enumValue)
    {
      // Enum 값에 해당하는 필드 정보를 가져옴
      var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

      // 필드에 붙은 DescriptionAttribute를 찾음
      var description = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();

      // Description이 있으면 그 값을, 없으면 기본 ToString() 값을 반환
      return description?.Description ?? enumValue.ToString();
    }

    // Enum 타입이 아니면 빈 문자열 반환
    return string.Empty;
  }

  /// <summary>
  /// 문자열을 다시 Enum 값으로 변환합니다.
  /// DescriptionAttribute의 Description 문자열 또는 Enum 이름과 일치하면 해당 Enum 값을 반환합니다.
  /// </summary>
  /// <param name="value">변환할 문자열 값</param>
  /// <param name="targetType">목표 Enum 타입</param>
  /// <param name="parameter">변환에 사용할 추가 매개변수 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보 (사용하지 않음)</param>
  /// <returns>일치하는 Enum 값 또는 Binding.DoNothing (변환 실패 시)</returns>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value가 문자열이고, targetType이 Enum인지 확인
    if (value is string str && targetType.IsEnum)
    {
      // targetType의 모든 필드(멤버)를 검사
      foreach (var field in targetType.GetFields())
      {
        // 각 필드에 붙은 DescriptionAttribute의 Description 값을 가져옴
        var desc = field.GetCustomAttribute<DescriptionAttribute>()?.Description;

        // 문자열이 Description과 같거나 필드 이름과 같으면 해당 Enum 값을 반환
        if (desc == str || field.Name == str)
          return Enum.Parse(targetType, field.Name);
      }
    }

    // 일치하는 값이 없으면 변환 실패 표시 (바인딩 무시)
    return Binding.DoNothing;
  }
}


/* 사용예제
public enum Status
{
  [Description("사용 중")]
  Active,
  [Description("사용 안 함")]
  Inactive
} */
