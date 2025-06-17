namespace Heimdallr.ToolKit.Enums;
/// <summary>
/// 연결 상태를 나타내는 열거형입니다.
/// </summary>
public enum ConnectionStatus
{
  /// <summary>
  /// 연결되지 않은 상태를 나타냅니다. 일반적으로 초기 상태이거나 연결이 끊어진 상태입니다.
  /// </summary>
  Disconnected,

  /// <summary>
  /// 연결을 시도하는 상태를 나타냅니다. 서버에 연결을 시도하거나 네트워크를 통해 연결을 설정하는 중입니다.
  /// </summary>
  Connecting,

  /// <summary>
  /// 연결된 상태를 나타냅니다. 서버와의 연결이 성공적으로 이루어진 상태입니다.
  /// </summary>
  Connected,

  /// <summary>
  /// 
  /// </summary>
  Disconnecting,

  /// <summary>
  /// 연결 실패 상태를 나타냅니다. 서버와의 연결이 실패했거나 네트워크 오류가 발생한 상태입니다.
  /// </summary>
  Failed,
}
