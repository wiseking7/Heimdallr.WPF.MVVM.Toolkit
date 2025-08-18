
using Heimdallr.ToolKit.Contracts;

namespace Heimdallr.ToolKit.Infrastructure;

/// <summary>
/// Region 기반의 View 전환을 간결하게 처리하기 위한 헬퍼 유틸리티 클래스입니다.
/// 종속성 주입(DI)을 통해 구성되며, View의 동적 로딩, 활성화 및 비활성화를 담당합니다.
/// <para />
/// <b>사용 예제:</b>
/// <code>
/// // View를 활성화
/// contentManager.ActiveContent("MainRegion", "HomeView");
///
/// // View를 비활성화
/// contentManager.DeactiveContent("MainRegion", "HomeView");
///
/// // 특정 타입으로 View 활성화
/// contentManager.ActiveContent&lt;UserControl&gt;("MainRegion", "CustomView");
/// </code>
/// <para />
/// <b>전제 조건:</b>
/// - View는 DI 컨테이너에 등록되어 있어야 하며 이름으로 Resolve 가능해야 합니다.
/// - View가 Region에 등록되지 않았다면 자동으로 Add 후 Activate 처리됩니다.
/// - Region 이름은 XAML에 정의된 Region 이름과 일치해야 합니다.
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
  /// 기본 인터페이스 타입(IViewable)으로 View를 Region에 활성화합니다.
  ///
  /// 사용 예:
  ///   ActiveContent("MainRegion", "HomeView");
  ///   => "MainRegion"에 "HomeView"를 DI 컨테이너에서 Resolve 후 표시
  /// </summary>
  /// <param name="regionName">View를 표시할 Region의 이름 (XAML의 RegionManager.RegionName 속성과 일치)</param>
  /// <param name="contentName">DI 컨테이너에 등록된 View 이름</param>
  public void ActiveContent(string regionName, string contentName)
  {
    ActiveContent<IViewable>(regionName, contentName);
  }

  /// <summary>
  /// 제네릭으로 View의 타입을 지정하여 Region에 View를 활성화합니다.
  /// <para />
  /// <b>사용 예:</b>
  /// <code>
  /// // "CustomView"라는 이름으로 등록된 UserControl을 MainRegion에 표시
  /// ActiveContent&lt;UserControl&gt;("MainRegion", "CustomView");
  /// </code>
  /// </summary>
  /// <typeparam name="T">View의 타입 (예: UserControl, IViewable 등)</typeparam>
  /// <param name="regionName">활성화할 Region의 이름</param>
  /// <param name="contentName">DI 컨테이너에 등록된 View 이름</param>
  public void ActiveContent<T>(string regionName, string contentName)
  {
    IRegion region = _regionManager.Regions[regionName];
    T content = _containerProvider.Resolve<T>(contentName);

    // Region에 View가 없는 경우 추가
    if (!region.Views.Contains(content))
    {
      region.Add(content);
    }

    // View 활성화
    region.Activate(content);
  }

  /// <summary>
  /// 기본 타입(IViewable)으로 Region에서 View를 비활성화합니다.
  ///
  /// 사용 예:
  ///   DeactiveContent("MainRegion", "HomeView");
  ///   => "HomeView"를 Region에서 비활성화
  /// </summary>
  /// <param name="regionName">비활성화할 Region의 이름</param>
  /// <param name="contentName">DI 컨테이너에 등록된 View 이름</param>
  public void DeactiveContent(string regionName, string contentName)
  {
    DeactiveContent<IViewable>(regionName, contentName);
  }

  /// <summary>
  /// 제네릭으로 View 타입을 명시하여 Region에서 해당 View를 비활성화합니다.
  /// <para />
  /// <b>사용 예:</b>
  /// <code>
  /// // "CustomView"라는 이름으로 등록된 UserControl을 MainRegion에서 비활성화
  /// DeactiveContent&lt;UserControl&gt;("MainRegion", "CustomView");
  /// </code>
  /// </summary>
  /// <typeparam name="T">View의 타입 (예: UserControl, IViewable 등)</typeparam>
  /// <param name="regionName">비활성화할 Region의 이름</param>
  /// <param name="contentName">DI 컨테이너에 등록된 View 이름</param>
  public void DeactiveContent<T>(string regionName, string contentName)
  {
    IRegion region = _regionManager.Regions[regionName];
    T content = _containerProvider.Resolve<T>(contentName);

    // Region에 View가 존재할 경우 비활성화
    if (region.Views.Contains(content))
    {
      region.Deactivate(content);
    }
  }
}

