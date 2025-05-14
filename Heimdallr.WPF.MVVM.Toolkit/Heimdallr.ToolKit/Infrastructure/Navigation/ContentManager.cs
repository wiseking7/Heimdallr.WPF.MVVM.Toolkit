using Heimdallr.ToolKit.Contracts.Viewing;

namespace Heimdallr.ToolKit.Infrastructure.Navigation;

/// <summary>
/// Region 기반의 View 전환을 간결하게 처리하기 위한 헬퍼 유틸리티 클래스입니다.
/// 종속성 주입(DI)을 통해 구성되며, View의 동적 로딩, 활성화 및 비활성화를 담당합니다.
/// </summary>
public class ContentManager
{
  private readonly IContainerProvider _containerProvider;  // DI 컨테이너 제공자
  private readonly IRegionManager _regionManager;          // RegionManager, View 전환 관리

  /// <summary>
  /// ContentManager 생성자.
  /// Prism DI 컨테이너와 RegionManager를 주입받아 View 전환을 관리합니다.
  /// </summary>
  /// <param name="containerProvider">Prism DI 컨테이너 제공자</param>
  /// <param name="regionManager">RegionManager, 뷰 전환을 위한 Region 관리</param>
  public ContentManager(IContainerProvider containerProvider, IRegionManager regionManager)
  {
    _containerProvider = containerProvider ?? throw new ArgumentNullException(nameof(containerProvider));
    _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
  }

  /// <summary>
  /// 주어진 Region에 해당하는 View를 활성화합니다. 
  /// 이 메서드는 기본적으로 IViewable 인터페이스를 따르는 View를 활성화합니다.
  /// </summary>
  /// <param name="regionName">활성화할 View를 표시할 Region의 이름</param>
  /// <param name="contentName">활성화할 View의 이름 (DI 컨테이너에서 Resolve 됨)</param>
  public void ActiveContent(string regionName, string contentName)
  {
    ActiveContent<IViewable>(regionName, contentName);
  }

  /// <summary>
  /// 제네릭 타입으로 View를 활성화하는 메서드입니다.
  /// 뷰 타입을 명확히 지정하여 활성화할 수 있습니다.
  /// </summary>
  /// <typeparam name="T">활성화할 View의 타입 (예: UserControl)</typeparam>
  /// <param name="regionName">활성화할 Region의 이름</param>
  /// <param name="contentName">활성화할 View의 이름</param>
  public void ActiveContent<T>(string regionName, string contentName)
  {
    // 지정된 Region을 찾습니다.
    IRegion region = _regionManager.Regions[regionName];

    // DI 컨테이너에서 View를 가져옵니다.
    T content = _containerProvider.Resolve<T>(contentName);

    // Region에 해당 View가 없으면 추가합니다.
    if (!region.Views.Contains(content))
    {
      region.Add(content);
    }

    // View를 활성화합니다.
    region.Activate(content);
  }

  /// <summary>
  /// 주어진 Region에 해당하는 View를 비활성화합니다.
  /// 이 메서드는 기본적으로 IViewable 인터페이스를 따르는 View를 비활성화합니다.
  /// </summary>
  /// <param name="regionName">비활성화할 View를 포함한 Region의 이름</param>
  /// <param name="contentName">비활성화할 View의 이름 (DI 컨테이너에서 Resolve 됨)</param>
  public void DeactiveContent(string regionName, string contentName)
  {
    DeactiveContent<IViewable>(regionName, contentName);
  }

  /// <summary>
  /// 제네릭 타입으로 View를 비활성화하는 메서드입니다.
  /// 뷰 타입을 명확히 지정하여 비활성화할 수 있습니다.
  /// </summary>
  /// <typeparam name="T">비활성화할 View의 타입 (예: UserControl)</typeparam>
  /// <param name="regionName">비활성화할 Region의 이름</param>
  /// <param name="contentName">비활성화할 View의 이름</param>
  public void DeactiveContent<T>(string regionName, string contentName)
  {
    // 지정된 Region을 찾습니다.
    IRegion region = _regionManager.Regions[regionName];

    // DI 컨테이너에서 View를 가져옵니다.
    T content = _containerProvider.Resolve<T>(contentName);

    // Region에 해당 View가 있다면 비활성화합니다.
    if (region.Views.Contains(content))
    {
      region.Deactivate(content);
    }
  }
}

