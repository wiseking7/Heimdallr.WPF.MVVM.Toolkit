using System.ComponentModel;
using System.Reflection;

namespace Heimdallr.ToolKit.Helpers;

/// <summary>
/// Enum 타입에 대해 공통적으로 사용 가능한 헬퍼 메서드를 제공하는 클래스입니다.
/// </summary>
/// <example>
/// 사용 예시:
/// var desc = EnumHelper.GetDescription(RoleTitle.Developer); // "Developer(개발자)"
/// var role = EnumHelper.GetEnumFromDescription&lt;RoleTitle&gt;("Manager(책임자)"); // RoleTitle.Manager
/// var role2 = EnumHelper.GetEnumFromName&lt;RoleTitle&gt;("Admin"); // RoleTitle.Admin
/// </example>
public static class EnumHelper
{
  /// <summary>
  /// 지정된 Enum 값에 대해 [Description] 특성에 정의된 설명을 반환합니다.
  /// 만약 Description 특성이 없으면 Enum 이름을 문자열로 반환합니다.
  /// </summary>
  /// <param name="value">설명을 얻고자 하는 Enum 값</param>
  /// <returns>Description 문자열 또는 Enum 이름</returns>
  public static string GetDescription(Enum value)
  {
    // Enum 값의 타입에서 해당 멤버 필드 정보를 가져옵니다.
    var field = value.GetType().GetField(value.ToString());

    // 필드에 붙은 DescriptionAttribute를 가져와서 첫 번째 것을 선택합니다.
    var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                         .Cast<DescriptionAttribute>()
                         .FirstOrDefault();

    // DescriptionAttribute가 존재하면 그 Description을 반환하고,
    // 없으면 Enum 멤버 이름(value.ToString())을 반환합니다.
    return attribute?.Description ?? value.ToString();
  }

  /// <summary>
  /// 지정한 Enum 타입의 모든 값과 각 값에 대한 설명(Description) 문자열을 Dictionary로 반환합니다.
  /// </summary>
  /// <typeparam name="TEnum">Enum 타입 (제한 조건으로 Enum 타입이어야 함)</typeparam>
  /// <returns>Enum 값(Key)와 Description 또는 이름(Value) 쌍의 Dictionary</returns>
  public static Dictionary<TEnum, string> GetEnumDictionary<TEnum>() where TEnum : Enum
  {
    // Enum 타입의 모든 값을 가져와서
    // 각 값을 키로, GetDescription() 결과를 값으로 하는 딕셔너리를 생성합니다.
    return Enum.GetValues(typeof(TEnum))
               .Cast<TEnum>()
               .ToDictionary(e => e, e => GetDescription(e as Enum));
  }

  /// <summary>
  /// Description 문자열로부터 해당하는 Enum 값을 찾아 반환합니다.
  /// 지정된 Description이 없으면 null을 반환합니다.
  /// </summary>
  /// <typeparam name="T">Enum 타입</typeparam>
  /// <param name="description">매칭할 Description 문자열</param>
  /// <param name="ignoreCase">대소문자 구분 여부 (기본값: false)</param>
  /// <returns>매칭된 Enum 값, 실패 시 null</returns>
  public static T? GetEnumFromDescription<T>(string description, bool ignoreCase = false) where T : struct, Enum
  {
    var type = typeof(T);
    foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
    {
      var attr = field.GetCustomAttribute<DescriptionAttribute>();
      var desc = attr?.Description ?? field.Name;

      if (string.Equals(desc, description, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
      {
        return (T)field.GetValue(null)!;
      }
    }

    return null;
  }

  /// <summary>
  /// 문자열 형태의 Enum 이름을 실제 Enum 값으로 변환합니다.
  /// 예: "Admin" → RoleTitle.Admin
  /// </summary>
  /// <typeparam name="T">Enum 타입</typeparam>
  /// <param name="name">Enum 이름 문자열</param>
  /// <param name="ignoreCase">대소문자 무시 여부</param>
  /// <returns>변환된 Enum 값, 실패 시 null</returns>
  public static T? GetEnumFromName<T>(string name, bool ignoreCase = false) where T : struct, Enum
  {
    return Enum.TryParse<T>(name, ignoreCase, out var result) ? result : null;
  }
}
