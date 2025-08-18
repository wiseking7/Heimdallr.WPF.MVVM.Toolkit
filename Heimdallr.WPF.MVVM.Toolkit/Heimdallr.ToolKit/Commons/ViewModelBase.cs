using System.Windows;

namespace Heimdallr.ToolKit.Commons;

/// <summary>
/// Prism 프레임워크를 기반으로 하는 기본 ViewModel 클래스입니다.
/// BindableBase를 상속하여 INotifyPropertyChanged 구현과
/// 속성 변경 알림(SetProperty 메서드)을 지원합니다.
/// </summary>
public abstract class ViewModelBase : BindableBase, IDestructible, INavigationAware
{
  #region Title 속성
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
  #endregion

  #region View에서 표시하기 위한 필수 패턴
  private bool _isBusy;

  /// <summary>
  /// API 호출, Navigation 중 로딩 상태를 View에서 표시하기 위한 필수 패턴입니다
  /// </summary>
  public bool IsBusy
  {
    get => _isBusy;
    set => SetProperty(ref _isBusy, value);
  }

  private string _busyMessage = string.Empty;
  /// <summary>
  /// View에서 로딩 중 메시지를 표시하기 위한 필수 패턴입니다.
  /// </summary>
  public string BusyMessage
  {
    get => _busyMessage;
    set => SetProperty(ref _busyMessage, value);
  }
  #endregion

  #region IContainerProvider
  /// <summary>
  /// Prism의 DI 컨테이너 인터페이스(컨테이너프로바이더)를 저장하는 필드, protected로 선언하여 상속받은 ViewModel에서 직접 접근 가능 런타임에 필요한 서비스,
  /// 개체를 리솔브제네릭 메서드로 꺼낼 수 있다.
  /// </summary>
  protected IContainerProvider Container { get; private set; }
  #endregion

  #region IEventAggregator
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
  #endregion

  #region IRegionManager
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
  #endregion

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

  #region Lazy<T>
  /// <summary>
  /// DI 컨테이너에서 지연 초기화(Lazy) 객체를 쉽게 생성할 수 있는 헬퍼 메서드입니다.
  /// </summary>
  /// <typeparam name="T">해당 서비스 타입</typeparam>
  /// <returns>Lazy로 감싼 서비스 인스턴스</returns>
  protected Lazy<T> ResolveLazy<T>() where T : class
  {
    // DI 컨테이너에서 T 타입의 인스턴스를 Lazy로 해석하여 반환
    return new Lazy<T>(() => Container.Resolve<T>());
  }
  #endregion

  #region RunOnUiThread
  /// <summary>
  /// 비동기 작업을 UI 스레드에서 실행하기 위한 헬퍼 메서드입니다.
  /// 비동기 콜백 등에서 UI 스레드 접근 시 유용합니다 
  /// UI 요소(예: ObservableCollection, Text, ListView.Items 등)**는 UI 스레드에서만 접근 가능합니다.
  /// </summary>
  /// <param name="action"></param>
  protected async Task RunOnUiThread(Func<Task> action)
  {
    if (Application.Current.Dispatcher.CheckAccess())
      // 현재 스레드가 UI 스레드이면 바로 실행
      await action();
    else
      // UI 스레드가 아니면 Dispatcher를 통해 실행
      await Application.Current.Dispatcher.InvokeAsync(action);
  }
  #endregion

  #region CancellationTokenSource
  private CancellationTokenSource _cts = new();

  /// <summary>
  /// 현재 ViewModel에 연결된 취소 토큰입니다.
  /// </summary>
  protected CancellationToken CancellationToken => _cts.Token;

  /// <summary>
  /// 현재 실행 중인 비동기 작업을 취소합니다.
  ///  _cts.Cancel(); 작업취소, _cts.Dispose(); 자원해제,  _cts = new CancellationTokenSource(); 새로운 토큰 생성 
  /// </summary>
  protected void CancelCurrentTask()
  {
    if (_cts.IsCancellationRequested)
      return;

    _cts.Cancel();
    _cts.Dispose();
    _cts = new CancellationTokenSource();
  }
  #endregion

  #region 메모리정리
  /// <summary>
  /// Disposable 패턴을 구현하여 ViewModel이 소멸될 때 CancellationTokenSource를 정리합니다.
  /// </summary>
  /// <exception cref="NotImplementedException"></exception>
  public void Destroy()
  {
    // ViewModel이 소멸될 때 CancellationTokenSource를 정리합니다.
    CancelCurrentTask();
    OnDestroying();
  }

  /// <summary>
  /// 자식 ViewModel 에서 필요한 경우 overrid
  /// EventAggregator 구독해제, 타이머 중단, IDisposable 자원해제, 내부 연결 또는 참조정리
  /// </summary>
  protected virtual void OnDestroying()
  {
    // 자식 ViewModel 에서 필요한 리소스 해제 등 처리
  }
  #endregion

  #region INavigationAware 기본 구현
  /// <summary>
  /// 이 View로 Navigation 되었을 때 호출됨, virtual로 선언하여 필요시 override 가능
  /// </summary>
  public virtual void OnNavigatedTo(NavigationContext navigationContext)
  {
    // 필요시 override하여 NavigationContext를 처리할 수 있습니다.
  }

  /// <summary>
  /// Navigation이 발생했을 때, View가 이 ViewModel을 재사용할지 여부 결정
  /// 기본값: true (재사용), virtual로 선언하여 필요시 override 가능
  /// </summary>
  public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;

  /// <summary>
  /// 다른 View로 이동되기 전 호출됨, virtual로 선언하여 필요시 override 가능
  /// </summary>
  public virtual void OnNavigatedFrom(NavigationContext navigationContext)
  {
    // 기본적으로 비동기 작업 취소
    CancelCurrentTask();
  }
  #endregion
}




