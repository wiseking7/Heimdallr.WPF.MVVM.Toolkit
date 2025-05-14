using Newtonsoft.Json;

namespace Heimdallr.ToolKit.Resources.Images;

public class ImageItem
{
  [JsonProperty("name")]
  public string? Name { get; set; }

  [JsonProperty("data")]
  public string? Data { get; set; }
}
