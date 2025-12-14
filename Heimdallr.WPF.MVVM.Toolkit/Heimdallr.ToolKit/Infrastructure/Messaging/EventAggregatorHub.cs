using System.Diagnostics;

namespace Heimdallr.ToolKit.Infrastructure;

/// <summary>
/// Prism의 IEventAggregator를 감싸는 유틸리티 클래스.
/// MVVM 패턴에서 ViewModel 간 이벤트 기반 통신을 안전하게 처리하도록 설계되었습니다.
/// 
/// 주요 기능:
/// - 타입 안전성 보장: 제네릭을 통해 이벤트 타입 구분 명확
/// - 느슨한 결합: ViewModel 간 직접 참조 없이 메시지 전달 가능
/// - SubscriptionToken 사용: Prism 권장 구독 해제 패턴
/// - 비동기 Subscribe에서 발생한 예외 안전하게 외부로 전달
/// - 여러 이벤트 동시 발생에도 예외를 안전하게 집계
/// - DEBUG 모드에서 이벤트 발행 시 호출 위치 추적
/// </summary>
public class EventAggregatorHub : IEventHub
{
  private readonly IEventAggregator _eventAggregator;

  /// <summary>
  /// 이벤트 발행 시 호출 스택 정보를 외부에서 받을 수 있는 디버깅용 콜백
  /// </summary>
  public Action<StackTrace>? Publising { get; set; }

  /// <summary>
  /// 생성자: Prism EventAggregator 인스턴스를 주입받습니다.
  /// </summary>
  /// <param name="eventAggregator">Prism의 IEventAggregator 인스턴스</param>
  public EventAggregatorHub(IEventAggregator eventAggregator)
  {
    _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator), "EventAggregator 인스턴스는 null일 수 없습니다.");
  }

  #region Publish Methods

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="TEvent"></typeparam>
  /// <typeparam name="TPayload"></typeparam>
  /// <param name="value"></param>
  public void Publish<TEvent, TPayload>(TPayload value) where TEvent : PubSubEvent<TPayload>, new()
  {
    StackTrace stackTrace = new StackTrace(1, fNeedFileInfo: true);
    _ = stackTrace.GetFrame(0)?.GetMethod()?.Name;
    Publising?.Invoke(stackTrace);
    _eventAggregator.GetEvent<TEvent>().Publish(value);
  }
  #endregion

  #region Subscribe Methods

  /// <summary>
  /// 동기 이벤트 구독
  /// </summary>
  public void Subscribe<TEvent, TPayload>(Action<TPayload> action) where TEvent : PubSubEvent<TPayload>, new()
  {
    _eventAggregator.GetEvent<TEvent>().Subscribe(action);
  }
  #endregion

  #region Unsubscribe Methods
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="TEvent"></typeparam>
  /// <typeparam name="TPayload"></typeparam>
  /// <param name="action"></param>
  public void UnSubscribe<TEvent, TPayload>(Action<TPayload> action) where TEvent : PubSubEvent<TPayload>, new()
  {
    _eventAggregator.GetEvent<TEvent>().Unsubscribe(action);
  }
  #endregion
}




