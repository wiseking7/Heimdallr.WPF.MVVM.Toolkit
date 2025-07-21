using System.ComponentModel;

namespace Heimdallr.ToolKit.Helpers;
/// <summary>
/// Enum 제네릭으로 재사용 
/// </summary>
public static class EnumHelper
{
  /// <summary>
  /// 지정된 Enum 값에 대한 DescriptionAttribute 또는 이름 반환
  /// </summary>
  public static string GetDescription(Enum value)
  {
    var field = value.GetType().GetField(value.ToString());
    var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                         .Cast<DescriptionAttribute>()
                         .FirstOrDefault();
    return attribute?.Description ?? value.ToString();
  }

  /// <summary>
  /// Enum의 모든 값과 해당 Description 문자열을 Dictionary로 반환
  /// </summary>
  public static Dictionary<TEnum, string> GetEnumDictionary<TEnum>() where TEnum : Enum
  {
    return Enum.GetValues(typeof(TEnum))
               .Cast<TEnum>()
               .ToDictionary(e => e, e => GetDescription(e));
  }
}