namespace Heimdallr.ToolKit.Infrastructure.AutoWiring;

/// <summary>
/// MVVM 패턴의 View와 ViewModel 간의 자동 연결(Locator) 등록을 도와주는 컬렉션 클래스입니다. 
/// 내부적으로 리스트을 상속하고 있으며, Register 메서드를 통해 
/// View와 ViewModel 간 매핑을 등록하고 이를 기록합니다.
/// </summary>
public class ViewModelLocatorCollection : List<ViewModelLocatorItem>
{
  /// <summary>
  /// 제네릭 메서드를 통해 View와 ViewModel 간 자동 매핑을 등록합니다.
  /// ViewModelLocationProvider에 등록하여 View 생성 시 ViewModel을 자동으로 할당되도록 설정합니다.
  /// </summary>
  /// <typeparam name="T1">View 클래스 (UserControl 또는 Window)</typeparam>
  /// <typeparam name="T2">ViewModel 클래스</typeparam>
  public void Register<T1, T2>()
  {
    // Prism 라이브러리의 ViewModelLocationProvider를 사용하여
    // View 타입(T1)과 ViewModel 타입(T2)을 매핑합니다.
    // 이렇게 하면 View(T1)의 인스턴스가 생성될 때 자동으로 ViewModel(T2) 인스턴스가 생성되어
    // DataContext로 설정됩니다.
    ViewModelLocationProvider.Register<T1, T2>();

    // 위에서 등록한 View-ViewModel 매핑 정보를 추적 및 관리하기 위해 
    // ViewModelLocatorItem 객체를 생성하여 이 컬렉션(List<T>)에 추가합니다.
    // 이 기록은 디버깅, 설정 확인, 로그 출력 등에 사용할 수 있습니다.
    Add(new ViewModelLocatorItem(typeof(T1), typeof(T2)));
  }
}

