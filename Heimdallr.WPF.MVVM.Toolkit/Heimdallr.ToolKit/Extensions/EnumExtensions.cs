using System.ComponentModel;

namespace Heimdallr.ToolKit.Extensions;

public static class EnumExtensions
{
  /// <summary>
  /// Enum 값에 정의된 Attribute를 가져옵니다.
  /// </summary>
  /// <typeparam name="T"> 가져올 Attribute 타입</typeparam>
  /// <param name="value"> Enum 값</param>
  /// <returns> Attribute 또는 null</returns>
  public static T? GetAttribute<T>(this Enum value) where T : Attribute
  {
    var type = value.GetType();
    var memberInfo = type.GetMember(value.ToString());

    if (memberInfo.Length == 0)
    {
      return null;
    }

    var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

    return attributes.Length > 0
        ? attributes[0] as T
        : null;
  }

  /// <summary>
  /// Enum에 붙은 DescriptionAttribute 값을 반환합니다. 없으면 이름을 반환합니다.
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  public static string ToName(this Enum value)
  {
    var attribute = value.GetAttribute<DescriptionAttribute>();
    return attribute == null ? value.ToString() : attribute.Description;
  }
}
