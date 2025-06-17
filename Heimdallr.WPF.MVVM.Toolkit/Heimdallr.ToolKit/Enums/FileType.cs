namespace Heimdallr.ToolKit.Enums;

/// <summary>
/// 파일 유형을 나타내는 열거형입니다.
/// </summary>
public enum FileType
{
  /// <summary>
  /// XML 파일 형식을 나타냅니다.
  /// </summary>
  Xml,

  /// <summary>
  /// JSON 파일 형식을 나타냅니다.
  /// </summary>
  Json,

  /// <summary>
  /// CSV 파일 형식을 나타냅니다.
  /// </summary>
  Csv,

  /// <summary>
  /// 텍스트 파일 형식을 나타냅니다. 일반적으로 텍스트 기반의 데이터를 포함하는 파일입니다.
  /// </summary>
  Text,

  /// <summary>
  /// 바이너리 파일 형식을 나타냅니다. 일반적으로 이진 데이터를 포함하는 파일입니다.
  /// </summary>
  Binary,

  /// <summary>
  /// 이미지 파일 형식을 나타냅니다. 일반적으로 JPEG, PNG, GIF 등의 이미지 데이터를 포함하는 파일입니다.
  /// </summary>
  PDF
}
