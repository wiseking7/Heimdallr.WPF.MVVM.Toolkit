using System.Diagnostics;

namespace Heimdallr.ToolKit.Infrastructure.Messaging;


/// <summary>
/// Prism PubSubEvent 시스템의 추상화 컴포넌트 간 강한 결합 없이 메시지를 주고받기 위해 활용됩니다. 
/// </summary>
public interface IEventHub
{
  // T1 발행한 이벤트 타입 (예: MyCustomEvent : PubSubEvent<string>), 
  // T2 전달할 데이터의 타입
  void Publish<T1, T2>(T2 value) where T1 : PubSubEvent<T2>, new();

  // 특정 이벤트 타입 T1을 구독합니다
  // 이벤트가 발행될 때 action이 호출됩니다
  void Subscribe<T1, T2>(Action<T2> action) where T1 : PubSubEvent<T2>, new();

  // Subscribe로 등록했던 핸들러를 해지합니다
  // 메모리 누수 방지, 뷰가 닫힐 때 등 중요합니다.
  void UnSubscribe<T1, T2>(Action<T2> action) where T1 : PubSubEvent<T2>, new();

  // 이벤트가 발행될 때 호출되는 콜백 (후킹용) 속성입니다
  // 디버깅이나 로깅 목적으로 사용 가능 (예: 어떤 경로에서 이벤트가 발행됐는지 추적)
  Action<StackTrace>? Publising { get; set; }
}
