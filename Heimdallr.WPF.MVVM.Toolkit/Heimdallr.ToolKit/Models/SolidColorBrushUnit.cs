using YamlDotNet.Serialization;

namespace Heimdallr.ToolKit.Models;

/// <summary>
/// 색상 테마 정의와 같은 기능을 위해 YAML로 정의된 색상 값을 불러와 사용할 수 있도록 구성된 단순 데이터 구조 클래스입니다
/// </summary>
public class SolidColorBrushUnit
{
  /// <summary>
  /// YAML 파일에서 black:이라는 키가 있으면, 이 값을 Black 속성에 매핑합니다
  /// </summary>
  [YamlMember(Alias = "black")]
  public string? Black { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [YamlMember(Alias = "light")]
  public string? Light { get; set; }

  internal string? Get(string item)
  {
    // 대소문자를 구분하지 않고 "black", "BLACK" 등의 입력을 처리합니다
    switch (item!.ToUpper())
    {
      case "BLACK": return Black;
      case "LIGHT": return Light;
    }
    return string.Empty;
  }
}

/* 사용예제
using YamlDotNet.Serialization;
using System.IO;

public class ColorService
{
  public SolidColorBrushUnit LoadColorSettings(string yamlFilePath)
  {
      var deserializer = new Deserializer();
      var yamlContent = File.ReadAllText(yamlFilePath);
      return deserializer.Deserialize<SolidColorBrushUnit>(yamlContent);
  }

  public string? GetColorValue(SolidColorBrushUnit colorUnit, string colorName)
  {
      return colorUnit.Get(colorName);
  }
}

 */
