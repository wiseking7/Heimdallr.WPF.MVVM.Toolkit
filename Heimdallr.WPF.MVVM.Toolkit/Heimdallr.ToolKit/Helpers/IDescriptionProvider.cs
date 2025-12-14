using System.Reflection;

namespace Heimdallr.ToolKit.Helpers;

/// <summary>
/// 커스텀 Attribute 로부터 Description을 추출하는 공급자 인터페이스입니다.
/// </summary>
public interface IDescriptionProvider
{
  /// <summary>
  /// 지정된 MemberInfo 에서 설명 문자열을 가져옵니다.
  /// </summary>
  string? GetDescription(MemberInfo member);
}

/// <summary>
/// UnitInfo 속성 지정
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class UnitInfoAttribute : Attribute
{
  /// <summary>
  /// 이름
  /// </summary>
  public string Name { get; }

  /// <summary>
  /// 주문
  /// </summary>
  public int Order { get; }

  /// <summary>
  /// 생성자
  /// </summary>
  /// <param name="name"></param>
  /// <param name="order"></param>
  public UnitInfoAttribute(string name, int order)
  {
    Name = name;
    Order = order;
  }
}
