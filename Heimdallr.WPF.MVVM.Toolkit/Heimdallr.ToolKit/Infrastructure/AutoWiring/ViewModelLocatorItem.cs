namespace Heimdallr.ToolKit.Infrastructure.AutoWiring;

/// <summary>
/// View와 ViewModel 간의 연결 관계를 정의하는 단일 매핑 항목을 나타냅니다. 
/// 이 클래스는 보통 여러 개의 View-ViewModel 매핑을 관리하는 컬렉션(ViewModelLocatorCollection)의 개별 요소로 사용
/// </summary>
public class ViewModelLocatorItem
{
  /// <summary>
  /// 생성자 두 타입 정보를 받아 각각의 속성에 할당합니다
  /// </summary>
  /// <param name="viewType"></param>
  /// <param name="dataContextType"></param>
  public ViewModelLocatorItem(Type viewType, Type dataContextType)
  {
    ViewType = viewType;
    DataContextType = dataContextType;
  }

  /// <summary>
  /// View 클래스의 Type 정보입니다. 예: typeof(MainView)
  /// </summary>
  public Type ViewType { get; set; }

  /// <summary>
  /// ViewModel 클래스의 Type 정보입니다. 예: typeof(MainViewModel)
  /// </summary>
  public Type DataContextType { get; set; }
}
