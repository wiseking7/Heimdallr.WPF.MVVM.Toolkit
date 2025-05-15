using Newtonsoft.Json;

namespace Heimdallr.ToolKit.Resources.PathGeometries;

public class PathGeometryRoot
{
  [JsonProperty("paths")]
  public List<PathGeometryItem>? Items { get; set; }
}
