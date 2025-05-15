using Newtonsoft.Json;

namespace Heimdallr.ToolKit.Resources.PathGeometries;

public class PathGeometryItem
{
  [JsonProperty("name")]
  public string? Name { get; set; }

  [JsonProperty("figures")]
  public string? Figures { get; set; }
}
