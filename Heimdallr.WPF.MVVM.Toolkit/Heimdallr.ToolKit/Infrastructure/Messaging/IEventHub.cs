using System.Diagnostics;

namespace Heimdallr.ToolKit.Infrastructure;

/// <summary>
/// Prism PubSubEvent 시스템을 추상화한 인터페이스입니다.
/// 컴포넌트 간 강한 결합 없이 이벤트를 발행(Publish)하고 구독(Subscribe)할 수 있도록 지원합니다.
///
/// 주요 변경 사항(권장 방식):
/// 1. 구독(Subscribe, SubscribeAsync)은 SubscriptionToken을 반환합니다.
/// 2. 구독 해지(UnSubscribe, UnSubscribeAsync)는 SubscriptionToken을 사용합니다.
/// 3. Action{T}, Func{T, Task} 같은 델리게이트 타입 불일치 문제를 제거했습니다.
/// 4. Prism EventAggregator의 권장 패턴과 일치하도록 설계되었습니다.
/// </summary>
public interface IEventHub
{
  /// <summary>
  /// 동기 이벤트 발행.
  /// </summary>
  /// <typeparam name="TEvent">이벤트 타입 (PubSubEvent)</typeparam>
  /// <typeparam name="TPayload">이벤트 데이터 타입</typeparam>
  /// <param name="value">전달할 데이터</param>
  void Publish<TEvent, TPayload>(TPayload value) where TEvent : PubSubEvent<TPayload>, new();


  /// <summary>
  /// 동기 이벤트 구독 메서드입니다.
  /// 이벤트가 발생하면 지정된 Action{TPayload}가 호출됩니다.
  /// </summary>
  /// <typeparam name="TEvent">이벤트 타입(PubSubEvent)</typeparam>
  /// <typeparam name="TPayload">이벤트 데이터 타입</typeparam>
  /// <param name="action">이벤트 발생 시 실행될 콜백</param>
  /// <returns>이 구독을 해제하기 위한 SubscriptionToken</returns>
  void Subscribe<TEvent, TPayload>(Action<TPayload> action) where TEvent : PubSubEvent<TPayload>, new();

  /// <summary>
  /// 동기 구독 해지.
  /// 반드시 Subscribe/SubscribeAsync에서 반환된 SubscriptionToken을 사용해야 합니다.
  /// </summary>
  /// <typeparam name="TEvent">이벤트 타입</typeparam>
  /// <typeparam name="TPayload">이벤트 데이터 타입</typeparam>
  /// <param name="action">구독 시 획득한 SubscriptionToken</param>
  void UnSubscribe<TEvent, TPayload>(Action<TPayload> action) where TEvent : PubSubEvent<TPayload>, new();


  /// <summary>
  /// 디버깅 및 로깅을 위해 이벤트 발행 시 호출되는 스택트레이스를 제공하는 콜백.
  /// </summary>
  Action<StackTrace>? Publising { get; set; }
}

