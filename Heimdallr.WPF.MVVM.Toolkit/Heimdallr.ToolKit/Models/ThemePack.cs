using YamlDotNet.Serialization;

namespace Heimdallr.ToolKit.Models;

/// <summary>
/// 테마색상 키
/// </summary>
public class ThemePack
{
  [YamlMember(Alias = "key")]
  public string? Key { get; set; }
  [YamlMember(Alias = "colors")]
  public SolidColorBrushUnit? Colors { get; set; }
}

/* 사용예제 
using YamlDotNet.Serialization;
using System.IO;

public class ThemeService
{
  public ThemePack LoadThemeSettings(string yamlFilePath)
  {
      var deserializer = new Deserializer();
      var yamlContent = File.ReadAllText(yamlFilePath);
      return deserializer.Deserialize<ThemePack>(yamlContent);
  }

  public string? GetColor(ThemePack themePack, string colorKey)
  {
      return themePack.Colors?.Get(colorKey);
  }
}
*/
