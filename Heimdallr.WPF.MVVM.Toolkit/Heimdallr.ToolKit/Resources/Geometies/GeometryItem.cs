using Newtonsoft.Json;

namespace Heimdallr.ToolKit.Resources.Geometies;

/// <summary>
/// Geometry Item
/// </summary>
public class GeometryItem
{
  /// <summary>
  /// Json 이름
  /// </summary>
  [JsonProperty("name")]
  public string? Name { get; set; }

  /// <summary>
  /// Json Data
  /// </summary>
  [JsonProperty("data")]
  public string? Data { get; set; }
}
