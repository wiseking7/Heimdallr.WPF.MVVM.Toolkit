using Heimdallr.ToolKit.UI.Controls;
using System.Windows;

namespace Heimdallr.ToolKit.Helpers;

/// <summary>
/// 디밍 상태를 제어하는 헬퍼 클래스입니다.
/// 전역적으로 ViewModel 또는 다른 코드에서 UI와의 의존성 없이 디밍 상태를 변경할 수 있도록 도와줍니다.
/// </summary>
public class DimmingManager
{
  /// <summary>
  /// 생성자: DimmingManager 인스턴스를 생성합니다.
  /// </summary>
  public DimmingManager() { }

  /// <summary>
  /// 디밍 상태를 설정합니다.
  /// 해당 메서드는 UI (특히 DarkThemeWindow)에서 디밍 상태를 동적으로 변경하는 기능을 제공합니다.
  /// </summary>
  /// <param name="isDimming">디밍 상태 설정. true이면 디밍, false이면 디밍 해제.</param>
  public void Dimming(bool isDimming)
  {
    // 현재 애플리케이션의 MainWindow가 DarkThemeWindow 타입인지 확인합니다.
    if (Application.Current.MainWindow is DarkThemeWindow window)
    {
      // MainWindow가 DarkThemeWindow라면, 해당 윈도우의 Dimming 속성을 동적으로 설정합니다.
      window.Dimming = isDimming;
    }
  }
}
