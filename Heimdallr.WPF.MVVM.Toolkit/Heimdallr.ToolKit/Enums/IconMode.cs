using System.ComponentModel;

namespace Heimdallr.ToolKit.Enums;

/// <summary>
/// 아이콘 모드 열거형
/// </summary>
public enum IconMode
{
  /// <summary>
  /// 없음
  /// </summary>
  [Description("없음")]
  None,

  /// <summary>
  /// 아이콘
  /// </summary>
  [Description("Icon")]
  Icon,

  /// <summary>
  /// 이미지
  /// </summary>
  [Description("Image")]
  Image,

  /// <summary>
  /// Path Data 아이콘
  /// </summary>
  [Description("Path Data")]
  PathIcon
}
