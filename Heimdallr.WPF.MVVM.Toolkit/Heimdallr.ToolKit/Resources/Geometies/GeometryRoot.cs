using Newtonsoft.Json;

namespace Heimdallr.ToolKit.Resources.Geometies;

public class GeometryRoot
{
  [JsonProperty("geometries")]
  public List<GeometryItem>? Items { get; set; }
}
