using Newtonsoft.Json;

namespace Heimdallr.ToolKit.Resources.Geometies;

public class GeometryItem
{
  [JsonProperty("name")]
  public string? Name { get; set; }

  [JsonProperty("data")]
  public string? Data { get; set; }
}
