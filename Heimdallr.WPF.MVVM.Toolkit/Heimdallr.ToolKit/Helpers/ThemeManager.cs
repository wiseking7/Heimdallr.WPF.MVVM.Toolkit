using System.Windows;

namespace Heimdallr.ToolKit.Helpers;

public static class ThemeManager
{
  public static void ChangedTehme(string themeName)
  {
    var appResources = Application.Current.Resources.MergedDictionaries;

    string themePath = $"Themes/Controls/Colors/{themeName}Colors.xaml";

    // 기본 색상 테마 제거
    var existingTheme = appResources.FirstOrDefault(d =>
        d.Source?.OriginalString.Contains("DarkColors.xaml") == true ||
        d.Source?.OriginalString.Contains("LightColors.xaml") == true);

    if (existingTheme != null)
      appResources.Remove(existingTheme);

    // 새 테마 추가
    var newTheme = new ResourceDictionary
    {
      Source = new Uri(themePath, UriKind.Relative)
    };

    appResources.Insert(0, newTheme);
  }
}

/* 사용방법
ThemeManager.ChangeTheme("Light"); // 또는 "Dark"
*/