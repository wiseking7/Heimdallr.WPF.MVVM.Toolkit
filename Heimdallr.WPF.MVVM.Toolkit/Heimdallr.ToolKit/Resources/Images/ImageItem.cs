using Newtonsoft.Json;

namespace Heimdallr.ToolKit.Resources.Images;

/// <summary>
/// 이미지 Item
/// </summary>
public class ImageItem
{
  /// <summary>
  /// Json 이름
  /// </summary>
  [JsonProperty("name")]
  public string? Name { get; set; }

  /// <summary>
  /// Json 데이터
  /// </summary>

  [JsonProperty("data")]
  public string? Data { get; set; }
}
