namespace Heimdallr.ToolKit.Enums;
/// <summary>
/// 사용자 역할을 나타내는 열거형입니다.
/// </summary>
public enum UserRole
{
  /// <summary>
  /// 게스트 역할을 나타냅니다. 일반적으로 인증되지 않은 사용자를 의미합니다.
  /// </summary>
  Guest,

  /// <summary>
  /// 사용자 역할을 나타냅니다. 일반적으로 인증된 사용자를 의미합니다.
  /// </summary>
  User,

  /// <summary>
  /// 모더레이터 역할을 나타냅니다. 일반적으로 커뮤니티 관리나 콘텐츠 검토를 담당하는 사용자를 의미합니다.
  /// </summary>
  Moderator,

  /// <summary>
  /// 관리자 역할을 나타냅니다. 일반적으로 시스템 관리나 중요한 설정 변경을 담당하는 사용자를 의미합니다.
  /// </summary>
  Admin,
}
