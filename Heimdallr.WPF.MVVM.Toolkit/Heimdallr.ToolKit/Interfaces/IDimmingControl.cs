namespace Heimdallr.ToolKit.Interfaces;

/// <summary>
/// 재사용 가능한 윈도우에 다이밍 처리
/// </summary>
public interface IDimmingControl
{
  /// <summary>
  /// 다이밍
  /// </summary>
  bool Dimming { get; set; }
}
