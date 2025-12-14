using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace Heimdallr.ToolKit.Applications;

/// 실제 Default 테마 초기화 클래스
public class HeimdallrDefaultResource : BaseResourceInitializer
{
  /// <summary>
  /// 재정의 추상화 메서드
  /// </summary>
  public override void Initialize()
  {
    var dict = new ResourceDictionary
    {
      Source = new Uri(
            "pack://application:,,,/Heimdallr.ToolKit;component/Themes/Generic.xaml", UriKind.Absolute)
    };
    Application.Current.Resources.MergedDictionaries.Add(dict);

    // HeimdallrApplication 에의 생성자에서 확인
    Debug.WriteLine($"[{nameof(HeimdallrDefaultResource)}.{MethodBase.GetCurrentMethod()?.Name}] Themes(Generi) Load 완료");
  }
}

