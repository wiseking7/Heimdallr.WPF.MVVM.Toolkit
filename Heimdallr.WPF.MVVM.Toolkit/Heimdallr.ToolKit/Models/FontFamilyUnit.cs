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

  /// <summary>
  /// 주어진 문자열(item)에 따라 내부에 정의된 색상 문자열 값을 반환합니다.
  /// 입력 문자열을 대문자로 변환한 후 조건에 맞는 값을 반환하며,
  /// "BLACK"이면 Black 프로퍼티 값을, "LIGHT"이면 Light 프로퍼티 값을 반환합니다.
  /// 그 외의 값에 대해서는 빈 문자열("")을 반환합니다.
  /// </summary>
  /// <param name="item">검색할 문자열 (예: "BLACK", "LIGHT")</param>
  /// <returns>
  /// 해당하는 문자열이 있으면 대응하는 색상 문자열을 반환하고,
  /// 없으면 빈 문자열을 반환합니다.
  /// </returns>
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
