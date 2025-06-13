using Newtonsoft.Json;

namespace Heimdallr.ToolKit.Resources.Geometies;

/// <summary>
/// Geometry 위치
/// </summary>
public class GeometryRoot
{
  /// <summary>
  /// Item 위치
  /// </summary>
  [JsonProperty("geometries")]
  public List<GeometryItem>? Items { get; set; }
}
