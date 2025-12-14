using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Heimdallr.ToolKit.Helpers;

/// <summary>
/// <para>엔티티(Property) 또는 Enum 멤버에서 설명 문자열을 가져오는 범용 헬퍼 클래스입니다.</para>
/// <para>지원하는 Attribute:</para>
/// <list type="bullet">
/// <item><see cref="DisplayAttribute"/> - Description, Name</item>
/// <item>DescriptionAttribute</item>
/// <item><c>UnitInfoAttribute</c> (사용자 정의 Attribute, Name/Label 속성)</item>
/// </list>
/// <para>추가 Provider를 등록하면 확장 가능합니다.</para>
/// </summary>
public static class DescriptionHelper
{
  /// <summary>
  /// 현재 등록된 설명 Provider 목록.
  /// 순서대로 조회되며, 첫 번째 유효한 설명 문자열을 반환합니다.
  /// </summary>
  private static readonly List<IDescriptionProvider> Providers = new()
    {
        new DisplayAttributeProvider(),
        new DescriptionAttributeProvider(),
        new UnitInfoAttributeProvider()
    };

  /// <summary>
  /// 동적으로 Provider를 추가하거나 순서를 지정하여 삽입합니다.
  /// </summary>
  /// <param name="provider">추가할 <see cref="IDescriptionProvider"/> 구현체</param>
  /// <param name="order">
  /// 삽입 위치. 기본값(-1)인 경우 리스트 끝에 추가.
  /// 0 이상이면 해당 인덱스에 삽입하여 우선순위를 조정할 수 있음.
  /// </param>
  public static void RegisterProvider(IDescriptionProvider provider, int order = -1)
  {
    if (order >= 0 && order < Providers.Count)
      Providers.Insert(order, provider);
    else
      Providers.Add(provider);
  }

  /// <summary>
  /// 지정된 대상의 속성 또는 Enum 멤버에서 설명 문자열을 가져옵니다.
  /// <para>Provider 순서:</para>
  /// <list type="number">
  /// <item><see cref="DisplayAttribute.Description"/></item>
  /// <item><see cref = "System.ComponentModel.DescriptionAttribute.Description" />DescriptionAttribute.Description</item >
  /// <item><c>UnitInfoAttribute</c> Name/Label 속성</item>
  /// </list>
  /// <para>Fallback 순서:</para>
  /// <list type="number">
  /// <item>DisplayAttribute.Name</item>
  /// <item>target.ToString()</item>
  /// <item>member.Name</item>
  /// </list>
  /// </summary>
  /// <param name="target">
  /// 설명을 가져올 객체. 
  /// <para>- 엔티티 객체: 속성명을 지정하면 해당 속성의 설명을 조회</para>
  /// <para>- Enum 값: propertyName 생략</para>
  /// </param>
  /// <param name="propertyName">
  /// 조회할 속성 이름 (엔티티의 Property)
  /// <para>Enum 값 조회 시 null</para>
  /// </param>
  /// <returns>
  /// <para>등록된 Provider에서 찾은 설명 문자열</para>
  /// <para>없으면 DisplayAttribute.Name → target.ToString() → member.Name 순으로 fallback</para>
  /// </returns>
  /// <example>
  /// <code language="csharp">
  /// // 엔티티 속성 예시
  /// var product = new Product();
  /// var desc = DisplayHelper.GetDescription(product, nameof(Product.ImageData));
  /// // 반환: "제품에 대한 이미지 데이터 (옵션)"
  /// 설명 문자열 또는 검증 메시지
  /// // Enum 예시
  /// var role = UserRole.Admin;
  /// var roleDesc = DisplayHelper.GetDescription(role);
  /// // 반환: "관리자"
  /// </code>
  /// </example>
  public static string GetDescription(object target, string? propertyName = null)
  {
    if (target == null)
      return "[DisplayHelper] 대상 개체가 null입니다.";

    MemberInfo? member = null;

    if (target is Enum enumValue)
    {
      var type = enumValue.GetType();
      member = type.GetField(enumValue.ToString());
      if (member == null)
        return $"[DisplayHelper] Enum '{enumValue}'에 해당하는 필드를 찾을 수 없습니다.";
    }
    else if (!string.IsNullOrWhiteSpace(propertyName))
    {
      member = target.GetType().GetProperty(propertyName);
      if (member == null)
        return $"[DisplayHelper] '{propertyName}' 속성을 찾을 수 없습니다.";
    }
    else
    {
      return $"[DisplayHelper] 조회할 속성 이름을 지정해주세요.";
    }

    // 등록된 Provider 순서대로 설명 문자열 조회
    foreach (var provider in Providers)
    {
      var desc = provider.GetDescription(member);
      if (!string.IsNullOrWhiteSpace(desc))
        return desc;
    }

    // DisplayAttribute.Name fallback
    var displayAttr = member.GetCustomAttribute<DisplayAttribute>();
    if (!string.IsNullOrWhiteSpace(displayAttr?.Name))
      return displayAttr.Name;

    // 최종 fallback: Enum 이름 또는 멤버 이름
    return member.Name ?? target.ToString() ?? "[DisplayHelper] 설명 없음";
  }
}


/* 설명 
Provider 순회에서 이미 DisplayAttribute.Description → DescriptionAttribute.Description → UnitInfoAttribute(Name/Label) 순으로 확인합니다.

따라서 GetDescription의 fallback 부분에서 다시 DisplayAttribute.Name → target.ToString() → member.Name 을 체크하는 것은
Provider에서 처리하지 못한 경우만 보충하는 의미입니다.

즉 전체적으로 보면 3개가 아니라, 사실상 “Provider 3개 + fallback 2단계” 구조입니다.
단계	             처리 항목
1	               DisplayAttribute.Description (DisplayAttributeProvider)
2	               DescriptionAttribute.Description (DescriptionAttributeProvider)
3	               UnitInfoAttribute Name/Label (UnitInfoAttributeProvider)
4	               DisplayAttribute.Name (fallback)
5	               target.ToString() → member.Name (fallback) 
 
 
 */
