using YamlDotNet.Serialization;

namespace Heimdallr.ToolKit.Models;
/// <summary>
/// 다국어 지원 기능을 구성하기 위해 설계된 클래스입니다. 
/// 주로 여러 언어에 대한 문자열 값을 하나의 객체로 관리하고, 코드에서 언어 코드로 값을 동적으로 가져올 수 있도록 하기 위한 목적
/// </summary>
public class LanguageUnit
{
  /// <summary>
  /// 언어 USA
  /// </summary>
  [YamlMember(Alias = "usa")]
  public string? USA { get; set; }

  /// <summary>
  /// 언어 Kor
  /// </summary>
  [YamlMember(Alias = "kor")]
  public string? KOR { get; set; }

  /// <summary>
  /// 언어 China
  /// </summary>
  [YamlMember(Alias = "chn")]
  public string? CHN { get; set; }

  /// <summary>
  /// 언어 Japan
  /// </summary>
  [YamlMember(Alias = "jpn")]
  public string? JPN { get; set; }

  /// <summary>
  /// 언어 베트남
  /// </summary>
  [YamlMember(Alias = "vnm")]
  public string? VNM { get; set; }

  /// <summary>
  /// 언어 스페인
  /// </summary>
  [YamlMember(Alias = "esp")]
  public string? ESP { get; set; }

  internal string? Get(string item)
  {
    switch (item.ToUpper())
    {
      case "USA": return USA;
      case "KOR": return KOR;
      case "CHN": return CHN;
      case "JPN": return JPN;
      case "VNM": return VNM;
      case "ESP": return ESP;
    }

    // // 일관성 있게 초기화할 수 있음
    return string.Empty;
  }
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

  public string? GetLanguageValue(LanguageUnit languageUnit, string languageCode)
  {
      return languageUnit.Get(languageCode);
  }
}

 */
