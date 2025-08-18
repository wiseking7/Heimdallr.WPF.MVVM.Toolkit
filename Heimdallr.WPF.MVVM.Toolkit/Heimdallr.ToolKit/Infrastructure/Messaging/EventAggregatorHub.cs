using System.Diagnostics;

namespace Heimdallr.ToolKit.Infrastructure;

/// <summary>
/// Prism의 IEventAggregator를 감싸는 유틸리티 클래스입니다.
/// 
/// MVVM 패턴에서 ViewModel 간 이벤트 기반 통신을 편리하게 처리하도록 도와줍니다.
///
/// 주요 기능:
/// - 타입 안전성 보장: 제네릭을 통해 이벤트 타입 구분 명확
/// - 느슨한 결합: ViewModel 간 직접 참조 없이 메시지 전달 가능
/// - 디버깅 향상: 이벤트 발행 시 호출 위치 추적 가능
/// </summary>
public class EventAggregatorHub : IEventHub
{
  private IEventAggregator _eventAggregator;

  /// <summary>
  /// EventAggregatorHub 생성자 (Prism의 EventAggregator 인스턴스를 주입)
  /// </summary>
  /// <param name="eventAggregator">Prism의 IEventAggregator</param>
  /// <exception cref="ArgumentNullException">null 주입 시 예외 발생</exception>
  public EventAggregatorHub(IEventAggregator eventAggregator)
  {
    Debug.WriteLine("new EventAggregator");

    _eventAggregator = eventAggregator ??
      throw new ArgumentNullException(nameof(eventAggregator), "EventAggregator 인스턴스는 null 일 수 없습니다.");
  }

  /// <summary>
  /// 이벤트 발생 시 호출 스택 정보를 외부에서 받을 수 있는 디버깅용 액션입니다.
  /// 
  /// 사용 예시:
  /// <code>
  /// eventHub.Publising = trace => Debug.WriteLine(trace);
  /// </code>
  /// </summary>
  public Action<StackTrace>? Publising { get; set; }

  /// <summary>
  /// 특정 이벤트 타입 T1에 값을 발행(Publish)합니다.
  /// T1은 PubSubEvent&lt;T2&gt; 형식의 Prism 이벤트 타입입니다.
  ///
  /// 사용 예시:
  /// 1. 먼저 이벤트 정의:
  /// <code>
  /// public class UserLoginEvent : PubSubEvent&lt;string&gt; { }
  /// </code>
  ///
  /// 2. 이벤트 발행:
  /// <code>
  /// eventHub.Publish&lt;UserLoginEvent, string&gt;("admin");
  /// </code>
  /// </summary>
  /// <typeparam name="T1">이벤트 타입 (PubSubEvent)</typeparam>
  /// <typeparam name="T2">전달할 데이터 타입</typeparam>
  /// <param name="value">이벤트와 함께 전달할 값</param>
  public void Publish<T1, T2>(T2 value) where T1 : PubSubEvent<T2>, new()
  {
    StackTrace stackTrace = new StackTrace(skipFrames: 1, fNeedFileInfo: true);
    var callingMethod = stackTrace.GetFrame(0)?.GetMethod()?.Name ?? "알 수 없음";

    Debug.WriteLine($"[EventAggregatorHub] Publish 호출: {callingMethod}");

    Publising?.Invoke(stackTrace);

    _eventAggregator.GetEvent<T1>().Publish(value);
  }

  /// <summary>
  /// 특정 이벤트에 구독자 등록합니다. 이벤트 발생 시 자동으로 콜백 메서드가 호출됩니다.
  ///
  /// 사용 예시:
  /// <code>
  /// eventHub.Subscribe&lt;UserLoginEvent, string&gt;(username =>
  /// {
  ///     Debug.WriteLine($"로그인 사용자: {username}");
  /// });
  /// </code>
  /// </summary>
  /// <typeparam name="T1">이벤트 타입 (PubSubEvent)</typeparam>
  /// <typeparam name="T2">전달되는 데이터 타입</typeparam>
  /// <param name="action">구독 콜백 메서드</param>
  public void Subscribe<T1, T2>(Action<T2> action) where T1 : PubSubEvent<T2>, new()
  {
    _eventAggregator.GetEvent<T1>().Subscribe(action);
  }

  /// <summary>
  /// 특정 이벤트에 대한 구독을 해제합니다.
  /// 동일한 Action 델리게이트를 넘겨야 정확히 해제됩니다.
  ///
  /// 사용 예시:
  /// <code>
  /// Action&lt;string&gt; onLogin = username => { ... };
  /// eventHub.Subscribe&lt;UserLoginEvent, string&gt;(onLogin);
  /// 
  /// // 필요 시 해제
  /// eventHub.UnSubscribe&lt;UserLoginEvent, string&gt;(onLogin);
  /// </code>
  /// </summary>
  /// <typeparam name="T1">이벤트 타입</typeparam>
  /// <typeparam name="T2">데이터 타입</typeparam>
  /// <param name="action">해제할 콜백</param>
  public void UnSubscribe<T1, T2>(Action<T2> action) where T1 : PubSubEvent<T2>, new()
  {
    _eventAggregator.GetEvent<T1>().Unsubscribe(action);
  }
}
