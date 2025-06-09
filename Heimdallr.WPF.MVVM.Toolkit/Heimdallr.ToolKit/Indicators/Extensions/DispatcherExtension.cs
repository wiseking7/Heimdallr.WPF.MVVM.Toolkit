using System.Windows.Threading;

namespace Heimdallr.ToolKit.Indicators;

public static class DispatcherExtension
{
  /// <summary>
  /// 지정된 시간 이후에 UI 스레드에서 작업을 실행합니다.
  /// 이 메서드는 UI 스레드와 다른 스레드에서 작업을 안전하게 실행할 수 있도록 
  /// Dispatcher를 사용합니다.
  /// </summary>
  /// <param name="dispatcher"></param>
  /// <param name="delay">지연시간(밀리초단위)</param>
  /// <param name="action">지연 후 실행할 Action</param>
  /// <param name="parm">Action에 전달될 인수(기본값 null)</param>
  public static async Task Delay(this Dispatcher dispatcher, int delay,
      Action<object?> action, object? parm = null)
  {
    // 지연 후 실행을 위해 Task.Delay를 비동기로 호출
    await Task.Delay(delay);

    // Dispatcher를 사용하여 UI 스레드에서 action을 실행
    dispatcher.Invoke(action, parm);
  }

  /// <summary>
  /// 지정된 시간 이후에 UI 스레드에서 리턴 값을 처리하는 작업을 실행합니다.
  /// </summary>
  /// <param name="dispatcher"></param>
  /// <param name="delay">지연시간(밀리초단위)</param>
  /// <param name="action">지연 후 실행할 Func (결과값을 반환하는 함수)</param>
  /// <param name="parm">Func에 전달될 인수(기본값 null)</param>
  public static async Task<TResult?> Delay<TResult>(this Dispatcher dispatcher, int delay,
      Func<object?, TResult?> action, object? parm = null)
  {
    // 지연 후 실행을 위해 Task.Delay를 비동기로 호출
    await Task.Delay(delay);

    // Dispatcher를 사용하여 UI 스레드에서 action을 실행하고 결과를 반환
    return await dispatcher.InvokeAsync(() => action(parm));
  }

  /// <summary>
  /// 지정된 시간 이후에 UI 스레드에서 작업을 실행하며, 취소 가능하도록 처리합니다.
  /// </summary>
  /// <param name="dispatcher"></param>
  /// <param name="delay">지연시간(밀리초단위)</param>
  /// <param name="action">지연 후 실행할 Action</param>
  /// <param name="parm">Action에 전달될 인수(기본값 null)</param>
  /// <param name="cancellationToken">취소 토큰</param>
  public static async Task Delay(this Dispatcher dispatcher, int delay,
      Action<object?> action, object? parm = null, CancellationToken cancellationToken = default)
  {
    // 취소 토큰을 처리하면서 지연 후 실행
    await Task.Delay(delay, cancellationToken);

    // 취소되지 않은 경우 Dispatcher를 사용하여 UI 스레드에서 action을 실행
    if (!cancellationToken.IsCancellationRequested)
    {
      dispatcher.Invoke(action, parm);
    }
  }

  /// <summary>
  /// 지정된 시간 이후에 UI 스레드에서 리턴 값을 처리하는 작업을 실행하며, 취소 가능하도록 처리합니다.
  /// </summary>
  /// <param name="dispatcher"></param>
  /// <param name="delay">지연시간(밀리초단위)</param>
  /// <param name="action">지연 후 실행할 Func (결과값을 반환하는 함수)</param>
  /// <param name="parm">Func에 전달될 인수(기본값 null)</param>
  /// <param name="cancellationToken">취소 토큰</param>
  public static async Task<TResult?> Delay<TResult>(this Dispatcher dispatcher, int delay,
      Func<object?, TResult?> action, object? parm = null, CancellationToken cancellationToken = default)
  {
    // 취소 토큰을 처리하면서 지연 후 실행
    await Task.Delay(delay, cancellationToken);

    // 취소되지 않은 경우 Dispatcher를 사용하여 UI 스레드에서 action을 실행하고 결과를 반환
    if (!cancellationToken.IsCancellationRequested)
    {
      return await dispatcher.InvokeAsync(() => action(parm));
    }
    return default; // 취소된 경우 기본값 반환
  }

  /// <summary>
  /// Dispatcher를 통해 UI 스레드에서 작업을 실행하는 비동기 메서드.
  /// </summary>
  public static Task<TResult> InvokeAsync<TResult>(this Dispatcher dispatcher, Func<TResult> func)
  {
    var tcs = new TaskCompletionSource<TResult>();
    dispatcher.Invoke(() =>
    {
      try
      {
        var result = func();
        tcs.SetResult(result);
      }
      catch (Exception ex)
      {
        tcs.SetException(ex);
      }
    });
    return tcs.Task;
  }
}
