using System.ComponentModel;

namespace Heimdallr.ToolKit.Extensions;

/// <summary>
/// Enum 타입에 대해 확장 메서드를 제공하는 클래스입니다.
/// </summary>
public static class EnumExtensions
{
  /// <summary>
  /// Enum 값에 정의된 특정 Attribute를 가져옵니다.
  /// 예를 들어 DescriptionAttribute 같은 커스텀 어트리뷰트를 쉽게 조회할 수 있습니다.
  /// </summary>
  /// <typeparam name="T">가져올 Attribute 타입 (Attribute를 상속한 클래스)</typeparam>
  /// <param name="value">Attribute를 가져올 Enum 값</param>
  /// <returns>해당 Attribute 인스턴스 또는 없으면 null 반환</returns>
  public static T? GetAttribute<T>(this Enum value) where T : Attribute
  {
    // Enum 타입 정보 가져오기
    var type = value.GetType();

    // Enum 값의 이름에 해당하는 멤버 정보(MemberInfo) 배열 가져오기
    var memberInfo = type.GetMember(value.ToString());

    // 멤버 정보가 없으면 null 반환
    if (memberInfo.Length == 0)
    {
      return null;
    }

    // 첫 번째 멤버의 지정한 타입(T)의 Attribute 배열을 가져옴 (상속 검사 안 함)
    var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

    // Attribute가 하나 이상 있으면 첫 번째를 T 타입으로 변환하여 반환, 없으면 null 반환
    return attributes.Length > 0
        ? attributes[0] as T
        : null;
  }

  /// <summary>
  /// Enum 값에 붙은 DescriptionAttribute의 Description 문자열을 반환합니다.
  /// DescriptionAttribute가 없으면 Enum 값의 이름(ToString())을 반환합니다.
  /// </summary>
  /// <param name="value">Description을 조회할 Enum 값</param>
  /// <returns>Description 문자열 또는 Enum 이름</returns>
  public static string ToName(this Enum value)
  {
    // DescriptionAttribute를 가져옴
    var attribute = value.GetAttribute<DescriptionAttribute>();

    // Description이 없으면 Enum 이름을 반환, 있으면 Description 반환
    return attribute == null ? value.ToString() : attribute.Description;
  }
}

/* 사용방법
 Enum 값에 사용자 친화적 이름을 표시할 때
 Enum 자체 이름은 코드용으로 적합하지만, UI나 로그 등에 그대로 노출하면 사용자가 이해하기 어려울 수 있습니다.

 DescriptionAttribute 같은 어트리뷰트를 Enum 값에 붙여놓고, 화면이나 메시지에 표시할 때 이 확장 메서드 ToName()을 써서 사람에게 보기 좋은 문자열을 얻을 수 있습니다.
 
 public enum Status
{
  [Description("작동 중")]
  Running,
  [Description("중지됨")]
  Stopped,
  Unknown
}

string displayText = Status.Running.ToName();  // "작동 중"

2. Enum 값에 부가 정보를 어트리뷰트로 저장해 조회할 때
Enum 값마다 추가적인 메타데이터(예: 코드, 설명, 설정값)를 Attribute로 붙여놓고, 실행 시에 그 값을 리플렉션으로 꺼내 써야 할 때 사용합니다.

csharp
복사
편집
public class CodeAttribute : Attribute
{
  public string Code { get; }
  public CodeAttribute(string code) => Code = code;
}

public enum ErrorCode
{
  [Code("ERR001")]
  NetworkError,
  [Code("ERR002")]
  FileNotFound
}

string code = ErrorCode.NetworkError.GetAttribute<CodeAttribute>()?.Code;  // "ERR001"
 */
