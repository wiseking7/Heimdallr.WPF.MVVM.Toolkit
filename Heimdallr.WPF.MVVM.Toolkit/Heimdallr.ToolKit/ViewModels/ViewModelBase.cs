namespace Heimdallr.ToolKit.ViewModels;

/// <summary>
/// Prism 프레임워크를 기반으로 하는 기본 ViewModel 클래스입니다.
/// BindableBase를 상속하여 INotifyPropertyChanged 구현과
/// 속성 변경 알림(SetProperty 메서드)을 지원합니다.
/// </summary>
public abstract class ViewModelBase : BindableBase
{
  private string _title = string.Empty;

  /// <summary>
  /// View에 바인딩 가능한 Title 속성 (주로 윈도우 타이틀이나 화면 제목용)
  /// 내부 필드 _title이 변경되면 OnPropertyChanged 이벤트 발생
  /// </summary>
  public string Title
  {
    get => _title;
    set => SetProperty(ref _title, value);
  }

  /// <summary>
  /// Prism의 DI 컨테이너 인터페이스(컨테이너프로바이더)를 저장하는 필드, protected로 선언하여 상속받은 ViewModel에서 직접 접근 가능 런타임에 필요한 서비스,
  /// 개체를 리솔브제네릭 메서드로 꺼낼 수 있다.
  /// </summary>
  protected IContainerProvider Container { get; private set; }


  private IEventAggregator? _eventAggregator;

  /// <summary>
  /// Prism의 이벤트 집합체인 이벤트에그리에터 인스턴스 (느슨한 결합을 위한 Pub/Sub 이벤트 통신)
  /// 여러 ViewModel 간 메시지 전달 및 구독에 활용
  /// private set으로 외부에서 수정 불가, 생성자에서 DI 컨테이너로부터 주입받음
  /// null일 경우 예외 발생(런타임 안전성 확보)
  /// </summary>
  public IEventAggregator EventAggregator
  {
    get => _eventAggregator ?? throw new ArgumentNullException(nameof(_eventAggregator));
    private set => SetProperty(ref _eventAggregator, value);
  }

  private IRegionManager? _regionManager;

  /// <summary>
  /// Prism의 RegionManager 인스턴스 (화면 내 여러 영역(Region)에 View를 동적으로 로드/교체)
  /// 메뉴 클릭 등으로 특정 Region에 View를 전환할 때 사용
  /// private set으로 외부 변경 제한, 생성자에서 DI 컨테이너로부터 초기화됨
  /// null일 경우 예외 발생하여 런타임 오류 방지
  /// </summary>
  public IRegionManager RegionManager
  {
    get => _regionManager ?? throw new ArgumentNullException(nameof(_regionManager));
    private set => SetProperty(ref _regionManager, value);
  }

  /// <summary>
  /// 생성자: Prism의 DI 컨테이너 IContainerProvider를 인자로 받음
  /// 내부 필드 Container에 저장하며,
  /// DI 컨테이너를 통해 IRegionManager와 IEventAggregator 인스턴스를 Resolve하여 초기화한다.
  /// null 체크를 통해 DI 누락 시 즉시 예외를 던져 런타임 안전성 확보
  /// </summary>
  /// <param name="container">DI 컨테이너 인스턴스 (IContainerProvider)</param>
  /// <exception cref="ArgumentNullException">container, IRegionManager, IEventAggregator가 null일 때 발생</exception>
  public ViewModelBase(IContainerProvider container)
  {
    // DI 컨테이너가 null이면 즉시 예외 발생
    Container = container ?? throw new ArgumentNullException(nameof(container));

    // RegionManager를 DI 컨테이너에서 해석(Resolve)하고 null이면 예외 발생
    RegionManager = Container.Resolve<IRegionManager>() ?? throw new ArgumentNullException(nameof(IRegionManager));

    // EventAggregator를 DI 컨테이너에서 해석(Resolve)하고 null이면 예외 발생
    EventAggregator = Container.Resolve<IEventAggregator>() ?? throw new ArgumentNullException(nameof(IEventAggregator));
  }
}


