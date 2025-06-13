using System.Windows;

namespace Heimdallr.ToolKit.Helpers;

/// <summary>
/// 테마(색상 리소스) 전환을 위한 ThemeManager 클래스
/// </summary>
public static class ThemeManager
{
  /// <summary>
  /// 지정된 테마 이름에 따라 리소스 딕셔너리(XAML 파일)를 교체하여 색상 테마를 변경합니다.
  /// </summary>
  /// <param name="themeName">적용할 테마 이름 (예: "Dark", "Light")</param>
  public static void ChangedTehme(string themeName)
  {
    // 현재 애플리케이션의 리소스 딕셔너리 목록 (App.xaml에 정의된 ResourceDictionary들)
    var appResources = Application.Current.Resources.MergedDictionaries;

    // 새로 적용할 테마의 XAML 경로 지정 (예: Themes/Controls/Colors/DarkColors.xaml)
    string themePath = $"pack://application:,,,/Heimdallr.ToolKit;component/Themes/Controls/Colors/{themeName}Colors.xaml";

    // 기존 테마를 찾음: 현재 머지된 리소스들 중에서 'DarkColors.xaml' 또는 'LightColors.xaml'이 포함된 것
    var existingTheme = appResources.FirstOrDefault(d =>
        d.Source?.OriginalString.Contains("DarkColors.xaml") == true ||
        d.Source?.OriginalString.Contains("LightColors.xaml") == true);

    // 기존 테마가 존재하면 제거
    if (existingTheme != null)
      appResources.Remove(existingTheme);

    // 새 테마 리소스 딕셔너리를 생성하고 해당 XAML 경로를 Source로 설정
    var newTheme = new ResourceDictionary
    {
      Source = new Uri(themePath, UriKind.Relative) // 상대 경로 사용
    };

    // 새 테마를 가장 앞에 삽입하여 우선순위를 높임
    appResources.Insert(0, newTheme);
  }
}


/* 사용방법
ThemeManager.ChangeTheme("Light"); // 또는 "Dark"
*/