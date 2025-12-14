using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Heimdallr.ToolKit.Helpers;

/// <summary>
/// <para><see cref="DisplayAttribute"/>의 <see cref="DisplayAttribute.Description"/> 값을 추출하는 Provider 입니다.</para>
/// <para>주로 엔티티(모델) **속성(Property)** 에 부여된 메타데이터를 UI 표기용 설명 문자열로 사용하려는 경우에 활용합니다.</para>
/// </summary>
/// <remarks>
/// <para>지원 대상: <see cref="PropertyInfo"/> (엔티티/뷰모델의 속성)</para>
/// <para>미지원: <see cref="FieldInfo"/> (일반 필드/enum 멤버). enum 설명에는 <see cref="DescriptionAttributeProvider"/> 사용을 권장합니다.</para>
/// <para>우선순위: 이 Provider는 <see cref="DisplayAttribute.Description"/> 만 읽습니다. 값이 비어있으면 <c>null</c>을 반환하여 다음 Provider로 넘어갑니다.</para>
/// </remarks>
/// <example>
/// <code language="csharp">
/// public class Product
/// {
///     [Display(Name = "제품 이미지", Description = "제품에 대한 이미지 데이터 (옵션)")]
///     public byte[]? ImageData { get; set; }
/// }
///
/// // member: typeof(Product).GetProperty(nameof(Product.ImageData))
/// // 반환: "제품에 대한 이미지 데이터 (옵션)"
/// </code>
/// </example>
public class DisplayAttributeProvider : IDescriptionProvider
{
  /// <summary>
  /// 지정된 멤버에서 <see cref="DisplayAttribute.Description"/> 값을 읽어 반환합니다.
  /// </summary>
  /// <param name="member">설명을 조회할 멤버. 주로 <see cref="PropertyInfo"/>.</param>
  /// <returns>
  /// <para><see cref="DisplayAttribute.Description"/> 문자열, 없거나 공백이면 <c>null</c>.</para>
  /// </returns>
  public string? GetDescription(MemberInfo member)
  {
    var attr = member.GetCustomAttribute<DisplayAttribute>();
    return string.IsNullOrWhiteSpace(attr?.Description) ? null : attr.Description;
  }
}

/// <summary>
/// <para><see cref="DescriptionAttribute"/>의 <see cref="DescriptionAttribute.Description"/> 값을 추출하는 Provider 입니다.</para>
/// <para>주로 **열거형(enum) 멤버** 또는 간단한 설명이 필요한 멤버에 붙은 Description을 UI 표기에 사용합니다.</para>
/// </summary>
/// <remarks>
/// <para>지원 대상: <see cref="FieldInfo"/> (특히 enum 멤버), <see cref="PropertyInfo"/></para>
/// <para>권장 사용: enum 값 설명 - 예) <c>[Description("관리자")] UserRole.Admin</c></para>
/// <para>우선순위: 이 Provider는 <see cref="DescriptionAttribute.Description"/> 만 읽습니다. 값이 비어있으면 <c>null</c>을 반환하여 다음 Provider로 넘어갑니다.</para>
/// </remarks>
/// <example>
/// <code language="csharp">
/// public enum UserRole
/// {
///     [Description("관리자")]
///     Admin
/// }
///
/// // member: typeof(UserRole).GetField(nameof(UserRole.Admin))
/// // 반환: "관리자"
/// </code>
/// </example>
/// 
public class DescriptionAttributeProvider : IDescriptionProvider
{
  /// <summary>
  /// 지정된 멤버에서 <see cref="DescriptionAttribute.Description"/> 값을 읽어 반환합니다.
  /// </summary>
  /// <param name="member">설명을 조회할 멤버. enum 멤버의 경우 <see cref="FieldInfo"/>.</param>
  /// <returns>
  /// <para><see cref="DescriptionAttribute.Description"/> 문자열, 없거나 공백이면 <c>null</c>.</para>
  /// </returns>
  public string? GetDescription(MemberInfo member)
  {
    var attr = member.GetCustomAttribute<DescriptionAttribute>();
    return string.IsNullOrWhiteSpace(attr?.Description) ? null : attr.Description;
  }
}

/// <summary>
/// <para>사용자 정의 Attribute(예: <c>UnitInfoAttribute</c>)에서 **사전에 약속한 속성들**을 찾아 설명 문자열로 반환하는 범용 Provider 입니다.</para>
/// <para>프로젝트마다 커스텀 Attribute 구조가 다르므로, 대표 후보 속성명(<c>Name</c>, <c>Label</c> 등)을 리플렉션으로 탐색합니다.</para>
/// </summary>
/// <remarks>
/// <para>지원 대상: <see cref="FieldInfo"/> (enum 멤버), <see cref="PropertyInfo"/></para>
/// <para>탐색 우선순위(기본): <c>"Name"</c> → <c>"Label"</c> → 없으면 <see cref="object.ToString"/> 결과</para>
/// <para>확장 방법: 후보 속성을 늘리거나, 전용 커스텀 Provider를 만들어 <c>DisplayHelper.RegisterProvider</c> 로 등록하세요.</para>
/// </remarks>
/// <example>
/// <code language="csharp">
/// [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
/// public class UnitInfoAttribute : Attribute
/// {
///     public string Name { get; }
///     public int Order { get; }
///     public UnitInfoAttribute(string name, int order) { Name = name; Order = order; }
/// }
///
/// public enum UserRole
/// {
///     [UnitInfo("관리자", 1)]
///     Admin
/// }
///
/// // member: typeof(UserRole).GetField(nameof(UserRole.Admin))
/// // 1) UnitInfo.Name 존재 → "관리자"
/// // 2) Name/Label 미존재 → attr.ToString() (Attribute의 기본 문자열)
/// </code>
/// </example>
public class UnitInfoAttributeProvider : IDescriptionProvider
{
  /// <summary>
  /// 지정된 멤버에서 <c>UnitInfoAttribute</c> (또는 동일한 이름의 커스텀 Attribute)를 읽고,
  /// 대표 속성(<c>Name</c> 또는 <c>Label</c>) 값을 설명 문자열로 반환합니다.
  /// </summary>
  public string? GetDescription(MemberInfo member)
  {
    if (member == null) return null;

    var attr = member.GetCustomAttribute<UnitInfoAttribute>();
    if (attr != null)
    {
      // Name 속성이 있으면 우선 반환, 없으면 Label, 없으면 null
      var nameProp = attr.GetType().GetProperty("Name");
      if (nameProp != null)
        return nameProp.GetValue(attr)?.ToString();

      var labelProp = attr.GetType().GetProperty("Label");
      if (labelProp != null)
        return labelProp.GetValue(attr)?.ToString();
    }

    // Attribute가 없거나 속성이 없으면 Enum 이름 반환
    return member.Name;
  }
}

