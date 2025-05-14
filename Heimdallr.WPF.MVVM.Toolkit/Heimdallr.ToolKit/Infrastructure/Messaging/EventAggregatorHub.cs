using System.Diagnostics;

namespace Heimdallr.ToolKit.Infrastructure.Messaging;

/// <summary>
/// Prism의 IEventAggregator를 감싸는 유틸리티 클래스입니다.
/// MVVM 패턴에서 ViewModel 간 이벤트 기반 통신을 편리하게 처리하도록 도와줍니다.
/// 이 메서드는 타입의 안전성(제네릭을 사용) Type 을 명확하게구분
/// 중앙 이벤트 허브 : ViewModel 사이에 강한 결합 없이 메시지 전달 가능
/// 디버깅 향상 : StackTrac 로 어떤 곳에서 발행했는지 추적 가능
/// </summary>
public class EventAggregatorHub : IEventHub
{
  private IEventAggregator _eventAggregator;

  public EventAggregatorHub(IEventAggregator eventAggregator)
  {
    Debug.WriteLine("new EventAggregator");

    _eventAggregator = eventAggregator ??
      throw new ArgumentNullException(nameof(eventAggregator), "EventAggregator 인스턴스는 null 일 수 없습니다.");
  }

  /// <summary>
  /// 이벤트 발생시 디버깅용으로 호출 스택을 외부에 알림
  /// </summary>
  public Action<StackTrace>? Publising { get; set; }

  /// <summary>
  /// 특정 이벤트 타입 T1에 값을 발행
  /// </summary>
  /// <typeparam name="T1"></typeparam>
  /// <typeparam name="T2"></typeparam>
  /// <param name="value"></param>
  public void Publish<T1, T2>(T2 value) where T1 : PubSubEvent<T2>, new()
  {
    StackTrace stackTrace = new StackTrace(skipFrames: 1, fNeedFileInfo: true);
    var callingMethod = stackTrace.GetFrame(0)?.GetMethod()?.Name ?? "알 수 없음";

    Debug.WriteLine($"[EventAggregatorHub] Publish 호출: {callingMethod}");

    Publising?.Invoke(stackTrace);

    _eventAggregator.GetEvent<T1>().Publish(value);
  }

  /// <summary>
  /// 특정 이벤트에 구독자 등록
  /// </summary>
  /// <typeparam name="T1"></typeparam>
  /// <typeparam name="T2"></typeparam>
  /// <param name="action"></param>
  public void Subscribe<T1, T2>(Action<T2> action) where T1 : PubSubEvent<T2>, new()
  {
    _eventAggregator.GetEvent<T1>().Subscribe(action);
  }

  /// <summary>
  /// 이벤트 구독 해제
  /// </summary>
  /// <typeparam name="T1"></typeparam>
  /// <typeparam name="T2"></typeparam>
  /// <param name="action"></param>
  public void UnSubscribe<T1, T2>(Action<T2> action) where T1 : PubSubEvent<T2>, new()
  {
    _eventAggregator.GetEvent<T1>().Unsubscribe(action);
  }
}
