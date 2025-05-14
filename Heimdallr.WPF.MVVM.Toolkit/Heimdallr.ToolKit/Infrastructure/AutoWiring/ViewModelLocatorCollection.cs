namespace Heimdallr.ToolKit.Infrastructure.AutoWiring;

/// <summary>
/// MVVM 패턴의 View와 ViewModel 간의 자동 연결(Locator) 등록을 도와주는 컬렉션 클래스입니다. 
/// 내부적으로 List<ViewModelLocatorItem>을 상속하고 있으며, Register<T1, T2>() 메서드를 통해 View와 ViewModel 간 매핑을 등록하고 기록
/// </summary>
public class ViewModelLocatorCollection : List<ViewModelLocatorItem>
{
  public void Register<T1, T2>()
  {
    // 제네릭 타입 T1(View), T2(ViewModel)을 Prism의 ViewModelLocationProvider에 등록하고, 동시에 이 매핑을 컬렉션에 추가
    // View(T1)의 인스턴스가 생성될 때 자동으로 ViewModel(T2) 타입이 DataContext에 설정되도록 등록
    // 즉, View와 ViewModel 간 **자동 연결(Auto-Wiring)**을 설정
    ViewModelLocationProvider.Register<T1, T2>();

    // 방금 등록한 View와 ViewModel 매핑 정보를 ViewModelLocatorItem으로 만들어 컬렉션에 저장
    // 추후에 디버깅하거나 매핑 정보를 출력하거나 제거할 때 활용 가능
    Add(new ViewModelLocatorItem(typeof(T1), typeof(T2)));
  }
}
