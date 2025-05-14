namespace Heimdallr.ToolKit.Contracts.Viewing;

/// <summary>
/// WPF MVVM 아키텍처에서 View의 Loaded 이벤트가 발생했을 때,
/// ViewModel이 후처리 로직을 수행할 수 있도록 정의하는 인터페이스입니다.
///
/// ViewModel은 일반적으로 View의 생명 주기와 직접 연결되지 않지만, 
/// 이 인터페이스를 구현하면 View가 완전히 로드된 이후 필요한 초기화 또는 View 상태 의존 작업을 수행할 수 있습니다.
///
/// 주요 사용 목적:
/// - View 로딩 이후 비동기 데이터 로딩
/// - View 요소의 크기/배치 등의 정보 사용
/// - ViewModel 내 초기 값 계산 또는 명령 실행
/// - Lazy Initialization: 필요한 시점에만 데이터 바인딩하거나 초기화
/// 
/// ⚠ View가 아직 완전히 렌더링되지 않은 상태에서 ViewModel이 동작할 경우 문제를 방지할 수 있습니다.
/// 
/// AutoWireManager 또는 ViewModelLocator와 연계하여 자동으로 호출됩니다.
/// </summary>
public interface IViewLoadable
{
  /// <summary>
  /// View가 로드(Loaded 이벤트 발생)된 직후 호출됩니다.
  /// ViewModel이 View와 상호작용하거나 비동기 초기화 로직을 실행할 수 있는 지점입니다.
  /// </summary>
  /// <param name="view">
  /// IViewable 형식의 View 참조 - View와 ViewModel 모두 접근할 수 있습니다.
  /// </param>
  void OnLoaded(IViewable view);
}

/*사용예제
public class DashboardViewModel : IViewLoadable
{
  public void OnLoaded(IViewable view)
  {
      // View 요소의 상태를 활용한 초기 처리
      var actualHeight = view.View.ActualHeight;

      // 예: 화면에 맞춰 데이터를 지연 로드
      LoadDataAsync(actualHeight);
  }

  private async void LoadDataAsync(double height)
  {
      // View가 완전히 로드된 이후 실행해야 할 로직
      await Task.Delay(500); // 예시: API 호출 전에 약간의 지연
      // 데이터 로드 수행
  }
}
*/
