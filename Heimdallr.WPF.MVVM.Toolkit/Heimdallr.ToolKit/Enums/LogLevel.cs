namespace Heimdallr.ToolKit.Enums;

/// <summary>
/// 로그 레벨을 나타내는 열거형입니다.
/// </summary>
public enum LogLevel
{
  /// <summary>
  /// 가장 상세한 로그 레벨로, 모든 로그 메시지를 기록합니다.
  /// </summary>
  Trace,

  /// <summary>
  /// 디버깅 목적으로 사용되는 로그 레벨입니다.
  /// </summary>
  Degug,

  /// <summary>
  /// 정보성 메시지를 기록하는 로그 레벨입니다.
  /// </summary>
  Info,

  /// <summary>
  /// 경고 메시지를 기록하는 로그 레벨입니다.
  /// </summary>
  Warnign,

  /// <summary>
  /// 오류 메시지를 기록하는 로그 레벨입니다.
  /// </summary>
  Error,

  /// <summary>
  /// 치명적인 오류 메시지를 기록하는 로그 레벨입니다.
  /// </summary>
  Critical,

  /// <summary>
  /// 로그 레벨이 설정되지 않았거나 사용되지 않는 경우를 나타냅니다.
  /// </summary>
  None,
}
