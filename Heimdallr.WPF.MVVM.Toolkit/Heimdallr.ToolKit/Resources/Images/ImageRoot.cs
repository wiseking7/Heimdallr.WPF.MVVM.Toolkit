using Newtonsoft.Json;

namespace Heimdallr.ToolKit.Resources.Images;

/// <summary>
/// 이미지 위치
/// </summary>
public class ImageRoot
{
  /// <summary>
  /// 이미지 항목
  /// </summary>
  [JsonProperty("images")]
  public List<ImageItem>? Items { get; set; }
}
