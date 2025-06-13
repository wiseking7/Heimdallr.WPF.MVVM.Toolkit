using YamlDotNet.Serialization;

namespace Heimdallr.ToolKit.Models;

/// <summary>
/// 테마, 폰트, 언어
/// </summary>
public class ThemeRoot
{
  /// <summary>
  /// 테마
  /// </summary>
  [YamlMember(Alias = "themes")]
  public List<ThemePack>? Themes { get; set; }

  /// <summary>
  /// 테마폰트
  /// </summary>
  [YamlMember(Alias = "fonts")]
  public List<FontPack>? Fonts { get; set; }

  /// <summary>
  /// 테마언어
  /// </summary>
  [YamlMember(Alias = "languages")]
  public List<LanguagePack>? Languages { get; set; }
}
/* 사요예제
using YamlDotNet.Serialization;
using System.IO;

public class ThemeService
{
  public ThemeRoot LoadAllSettings(string yamlFilePath)
  {
      var deserializer = new Deserializer();
      var yamlContent = File.ReadAllText(yamlFilePath);
      return deserializer.Deserialize<ThemeRoot>(yamlContent);
  }

  public ThemePack GetTheme(ThemeRoot themeRoot, string themeKey)
  {
      return themeRoot.Themes?.FirstOrDefault(theme => theme.Key == themeKey);
  }

  public FontPack GetFont(ThemeRoot themeRoot, string fontKey)
  {
      return themeRoot.Fonts?.FirstOrDefault(font => font.Key == fontKey);
  }

  public LanguagePack GetLanguage(ThemeRoot themeRoot, string languageKey)
  {
      return themeRoot.Languages?.FirstOrDefault(language => language.Key == languageKey);
  }
}
*/
