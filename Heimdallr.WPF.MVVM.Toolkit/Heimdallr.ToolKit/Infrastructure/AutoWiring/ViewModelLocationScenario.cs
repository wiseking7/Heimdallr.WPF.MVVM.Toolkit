namespace Heimdallr.ToolKit.Infrastructure.AutoWiring;

/// <summary>
/// MVVM 패턴에서 ViewModel을 View에 연결하기 위한 시나리오 정의의 추상 베이스 클래스입니다. 
/// 주로 WPF에서 ViewModel 자동 연결(Auto-Wiring) 구조를 구성할 때 사용되며, View와 ViewModel 간의 매핑 규칙을 정의하는 데 목적
/// </summary>
public abstract class ViewModelLocationScenario
{
  /// <summary>
  /// View와 ViewModel 간의 매핑 정보를 담은 컬렉션(ViewModelLocatorCollection)을 반환합니다.
  /// 실제 매핑 로직은 추상 메서드인 Match()에서 구현되며, 자식 클래스가 override해야 합니다.
  /// 즉, 매핑을 구성하고 Items에 추가한 뒤 그 결과를 반환하는 구조입니다
  /// </summary>
  /// <returns></returns>
  public ViewModelLocatorCollection Publish()
  {
    ViewModelLocatorCollection Items = new ViewModelLocatorCollection();
    Match(Items);
    return Items;
  }

  /// <summary>
  /// 별도의 로직 없이 객체 초기화 용도입니다
  /// 필요 시 확장 가능 (ex: 로깅, DI 주입 등).
  /// </summary>
  public ViewModelLocationScenario()
  {
  }

  /// <summary>
  /// 추상 메서드 -> View와 ViewModel의 매핑 로직을 정의하는 핵심 함수입니다
  /// 자식 클래스에서 다음과 같이 override하게 됩니다
  /// </summary>
  /// <param name="items"></param>
  protected abstract void Match(ViewModelLocatorCollection items);
}
/* 추상메서드 오버라이드 예제
protected override void Match(ViewModelLocatorCollection items)
{
    items.Add("LoginView", typeof(LoginViewModel));
    items.Add("DashboardView", typeof(DashboardViewModel));
} */
