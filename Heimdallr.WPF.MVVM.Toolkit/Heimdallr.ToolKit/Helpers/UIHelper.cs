using System.Windows;

namespace Heimdallr.ToolKit.Helpers;

/// <summary>
/// UI 스레드에서 안전하게 작업을 실행하기 위한 유틸리티 클래스입니다.
/// WPF에서는 UI 요소를 백그라운드 스레드에서 직접 조작할 수 없기 때문에,
/// 이 헬퍼를 통해 UI 스레드에서 안전하게 동작하도록 보장합니다.
/// </summary>
public static class UIHelper
{
  /// <summary>
  /// 주어진 작업(<paramref name="action"/>)을 UI 스레드에서 동기적으로 실행합니다.
  /// 현재 호출 스레드가 UI 스레드인 경우 바로 실행하고,
  /// 그렇지 않으면 Dispatcher를 통해 UI 스레드에서 실행합니다.
  /// </summary>
  /// <param name="action">UI 스레드에서 실행할 작업</param>
  public static void RunOnUIThread(Action action)
  {
    // 현재 스레드가 UI 스레드인지 확인
    if (Application.Current.Dispatcher.CheckAccess())
    {
      // UI 스레드이므로 바로 실행
      action();
    }
    else
    {
      // UI 스레드가 아니므로 Dispatcher를 통해 UI 스레드에서 실행
      Application.Current.Dispatcher.Invoke(action);
    }
  }

  /// <summary>
  /// 비동기 작업(<paramref name="asyncAction"/>)을 UI 스레드에서 실행합니다.
  /// 현재 호출 스레드가 UI 스레드인 경우 바로 실행하고,
  /// 그렇지 않으면 Dispatcher를 통해 UI 스레드에서 비동기 실행합니다.
  /// </summary>
  /// <param name="asyncAction">UI 스레드에서 실행할 비동기 작업</param>
  /// <returns>비동기 작업을 나타내는 Task</returns>
  public static async Task RunOnUIThreadAsync(Func<Task> asyncAction)
  {
    if (Application.Current.Dispatcher.CheckAccess())
    {
      // UI 스레드인 경우 바로 실행
      await asyncAction();
    }

    else
    {
      // UI 스레드가 아닌 경우 Dispatcher를 통해 비동기로 실행
      await Application.Current.Dispatcher.InvokeAsync(asyncAction);
    }
  }
}

