using YamlDotNet.Serialization;

namespace Heimdallr.ToolKit.Models;

/// <summary>
/// 폰트
/// </summary>
public class FontPack
{
  /// <summary>
  /// 폰트 Key
  /// </summary>
  [YamlMember(Alias = "key")]
  public string? Key { get; set; }

  /// <summary>
  /// 폰트
  /// </summary>
  [YamlMember(Alias = "fonts")]
  public FontFamilyUnit? Fonts { get; set; }
}
/* 사용예제
using YamlDotNet.Serialization;
using System.IO;

public class FontService
{
  public FontPack LoadFontSettings(string yamlFilePath)
  {
      var deserializer = new Deserializer();
      var yamlContent = File.ReadAllText(yamlFilePath);
      return deserializer.Deserialize<FontPack>(yamlContent);
  }
}

 
 
 */
