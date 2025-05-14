using YamlDotNet.Serialization;

namespace Heimdallr.ToolKit.Models;

/// <summary>
/// 폰트 설정을 구성하고 외부 YAML 파일과 매핑되도록 설계된 폰트 그룹 클래스입니다. 
/// 주로 여러 굵기(weight)의 폰트를 지정하고 필요 시 해당 값을 반환하는 데 사용됩니다
/// </summary>
public class FontFamilyUnit
{
  /// <summary>
  /// BLACK" 스타일의 폰트 이름. 예: "NotoSansKR-Black
  /// </summary>
  [YamlMember(Alias = "black")]
  public string? Black { get; set; }

  /// <summary>
  /// LIGHT" 스타일의 폰트 이름. 예: "NotoSansKR-Light
  /// </summary>
  [YamlMember(Alias = "light")]
  public string? Light { get; set; }

  public string? Get(string item)
  {
    switch (item.ToUpper())
    {
      case "BLACK": return Black;
      case "LIGHT": return Light;
    }
    return "";
  }
}
/* 사용예제
using YamlDotNet.Serialization;
using System.IO;

public class FontService
{
  public FontFamilyUnit LoadFontSettings(string yamlFilePath)
  {
      var deserializer = new Deserializer();
      var yamlContent = File.ReadAllText(yamlFilePath);
      return deserializer.Deserialize<FontFamilyUnit>(yamlContent);
  }
}
 */
