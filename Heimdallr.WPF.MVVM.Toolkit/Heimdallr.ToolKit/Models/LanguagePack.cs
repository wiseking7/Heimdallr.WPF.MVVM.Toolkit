using YamlDotNet.Serialization;

namespace Heimdallr.ToolKit.Models;

/// <summary>
/// 언어 
/// </summary>
public class LanguagePack
{
  [YamlMember(Alias = "key")]
  public string? Key { get; set; }
  [YamlMember(Alias = "items")]
  public LanguageUnit? Fonts { get; set; }
}
/* 사용예제
using YamlDotNet.Serialization;
using System.IO;

public class LanguageService
{
  public LanguagePack LoadLanguageSettings(string yamlFilePath)
  {
      var deserializer = new Deserializer();
      var yamlContent = File.ReadAllText(yamlFilePath);
      return deserializer.Deserialize<LanguagePack>(yamlContent);
  }
}
 */