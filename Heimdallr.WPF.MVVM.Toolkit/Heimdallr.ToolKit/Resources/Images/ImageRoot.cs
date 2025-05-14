using Newtonsoft.Json;

namespace Heimdallr.ToolKit.Resources.Images;

public class ImageRoot
{
  [JsonProperty("images")]
  public List<ImageItem>? Items { get; set; }
}
