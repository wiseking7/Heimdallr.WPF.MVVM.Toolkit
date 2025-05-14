namespace Heimdallr.ToolKit.Contracts.Viewing;

/// <summary>
/// MVVM 아키텍처에서 ViewModel이 View에 연결되었을 때 실행되는 
/// 초기화 작업을 정의하는 인터페이스입니다.
///
/// 이 인터페이스는 ViewModel의 수명 주기 중, View와 연결(ViewModelLocator 또는 AutoWireManager 등을 통해)된 직후에 
/// 한 번 실행되어야 하는 초기 설정 작업을 수행하기 위해 사용됩니다.
///
/// 주요 사용 목적:
/// - ViewModel이 View 인스턴스의 정보(예: View 크기, 상태 등)에 접근해야 할 때
/// - 초기 커맨드 바인딩, 이벤트 등록, View 관련 데이터 초기화 등
/// - ViewModel에서 View 요소와의 상호작용을 명시적으로 제어
///
/// 예: ViewModel에서 View에 포함된 특정 UI 요소의 초기 상태를 세팅할 수 있음
/// </summary>
public interface IViewInitializable
{
  /// <summary>
  /// ViewModel이 View와 연결된 직후 호출되는 메서드입니다.
  /// ViewModel이 View와 상호작용할 수 있도록 초기 설정 작업을 수행합니다.
  /// </summary>
  /// <param name="view">
  /// 연결된 View의 참조 (IViewable 형태) - View 인스턴스 및 ViewModel 모두 접근 가능
  /// </param>
  void OnViewWired(IViewable view);
}

/* 사용예제
public class MainViewModel : IViewInitializable
{
  public void OnViewWired(IViewable view)
  {
    // View의 실제 요소나 속성에 접근하여 초기값 설정
    var actualWidth = view.View.ActualWidth;

    // 초기 상태 설정
    this.WindowWidth = actualWidth;

    // 필요 시 View와의 직접 상호작용도 가능
    // ((MainView)view.View).SomeControl.Visibility = Visibility.Hidden;
  }
}
*/