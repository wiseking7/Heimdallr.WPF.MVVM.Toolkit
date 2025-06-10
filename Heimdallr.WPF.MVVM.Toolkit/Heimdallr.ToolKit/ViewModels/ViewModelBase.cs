namespace Heimdallr.ToolKit.ViewModels;

public abstract class ViewModelBase : BindableBase
{
  /// <summary>
  /// Title 프로터피(View 바인딩시 가능한 문자열)
  /// </summary>
  private string _title = string.Empty;
  public string Title
  {
    get => _title;
    set => SetProperty(ref _title, value);
  }

  /// <summary>
  /// Prism DI 컨테이너 저장용 필드, 개체를 런타임에 Resolve<T> 로 꺼낼 수 있음
  /// Protected 선언으로 하위 ViewModel 에시 직접 사용할 수 있게 했습니다.
  /// </summary>
  protected IContainerProvider Container { get; private set; }

  /// <summary>
  /// Prism EventAggregator 인스턴스 필드 + 프로퍼티 (ViewModel 간 이벤트 / 구독을 담당)
  /// 느슨한 결합(Loose Coupling)을 위해 사용하며, MVVM 구조에서 ViewModel 간 통신에 많이 씁니다.
  /// private set으로 외부에서 변경을 제한하고, 생성자에서 DI 컨테이너로부터 받아 초기화합니다.
  /// getter에서 null일 경우 예외를 던지도록 하여 런타임 안전성을 확보했습니다.
  /// </summary>
  private IEventAggregator? _eventAggregator;
  public IEventAggregator EventAggregator
  {
    get => _eventAggregator ?? throw new ArgumentNullException(nameof(_eventAggregator));
    private set => SetProperty(ref _eventAggregator, value);
  }

  /// <summary>
  /// 화면 내 여러 영역(Region)에 View를 동적으로 로드하거나 교체할 때 사용됩니다.
  /// 메뉴 클릭 시 특정 영역에 다른 화면(View)를 로드할 때 활용, DI 컨테이너에서 받아 초기화
  /// getter에서 null일 경우 예외를 던지도록 하여 런타임 안전성을 확보했습니다.
  /// </summary>
  private IRegionManager? _regionManager;
  public IRegionManager RegionManager
  {
    get => _regionManager ?? throw new ArgumentNullException(nameof(_regionManager));
    private set => SetProperty(ref _regionManager, value);
  }

  /// <summary>
  /// Resolve<T>() 메서드로 필요한 Prism 서비스들(IRegionManager, IEventAggregator)을 꺼내서 초기화합니다
  /// </summary>
  /// <param name="container">IContainerProvider를 생성자 인자로 받아서 내부 필드 Container에 저장하고,</param>
  /// <exception cref="ArgumentNullException">ArgumentNullException 검사로 DI 누락 방지</exception>
  public ViewModelBase(IContainerProvider container)
  {
    Container = container ?? throw new ArgumentNullException(nameof(container));

    RegionManager = Container.Resolve<IRegionManager>() ?? throw new ArgumentNullException(nameof(IRegionManager));
    EventAggregator = Container.Resolve<IEventAggregator>() ?? throw new ArgumentNullException(nameof(IEventAggregator));
  }
}

