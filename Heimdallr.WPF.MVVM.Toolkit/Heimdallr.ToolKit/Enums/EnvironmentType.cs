namespace Heimdallr.ToolKit.Enums;

/// <summary>
/// 환경 유형을 나타내는 열거형입니다.
/// </summary>
public enum EnvironmentType
{
  /// <summary>
  /// 개발 환경을 나타냅니다. 일반적으로 개발 중인 애플리케이션이나 서비스가 실행되는 환경입니다.
  /// </summary>
  Development,

  /// <summary>
  /// 테스트 환경을 나타냅니다. 일반적으로 애플리케이션이나 서비스의 기능을 테스트하는 데 사용되는 환경입니다.
  /// </summary>
  Staging,

  /// <summary>
  /// 운영 환경을 나타냅니다. 일반적으로 실제 사용자에게 제공되는 애플리케이션이나 서비스가 실행되는 환경입니다.
  /// </summary>
  Production,
}
