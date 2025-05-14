using Heimdallr.ToolKit.Contracts.Viewing;
using System.ComponentModel;
using System.Windows;

namespace Heimdallr.ToolKit.Infrastructure.AutoWiring;

/// <summary>
/// MVVM 패턴을 사용하는 WPF 애플리케이션에서
/// View와 ViewModel을 자동으로 연결(Auto-Wiring)해주는 관리자 클래스입니다.
/// 
/// - ViewModelLocator를 활용하여 View에 알맞은 ViewModel을 자동 주입하고
/// - ViewModel의 초기화(OnViewWired) 및 로딩 후처리(OnLoaded) 이벤트를 연결합니다.
/// </summary>
public class AutoWireManager
{
  /// <summary>
  /// 현재 Auto-Wiring 대상이 되는 View 인스턴스를 저장합니다.
  /// 초기화 시점에만 설정되므로 readonly로는 선언하지 않습니다.
  /// </summary>
  private FrameworkElement? _view;

  /// <summary>
  /// ViewModel 자동 연결 프로세스를 시작합니다.
  /// 
  /// 내부적으로 ViewModelLocator에 연결하여 ViewModel을 자동 주입받고,
  /// 연결 후 콜백 메서드(AutoWireViewModelChanged)가 호출됩니다.
  /// </summary>
  /// <param name="view">View 인스턴스 (예: UserControl, Window)</param>
  public void InitializeAutoWire(FrameworkElement view)
  {
    _view = view ?? throw new ArgumentNullException(nameof(view));

    ViewModelLocationProvider.AutoWireViewModelChanged(view, AutoWireViewModelChanged);
  }

  /// <summary>
  /// ViewModelLocator에 의해 ViewModel이 주입되었을 때 호출되는 콜백입니다.
  /// ViewModel의 수명 주기 인터페이스(IViewInitializable, IViewLoadable)를 활용하여
  /// 초기화 및 로딩 이벤트를 처리합니다.
  /// </summary>
  /// <param name="view">연결된 View</param>
  /// <param name="dataContext">ViewModel 인스턴스</param>
  public virtual void AutoWireViewModelChanged(object view, object dataContext)
  {
    if (_view == null || dataContext == null)
      return;

    // View에 ViewModel(DataContext)을 설정
    _view.DataContext = dataContext;

    // ViewModel 초기화 이벤트 호출
    if (dataContext is IViewInitializable viewModel && view is IViewable viewable)
    {
      viewModel.OnViewWired(viewable);
    }

    // View 로드 이벤트 후처리 연결
    if (dataContext is IViewLoadable && view is FrameworkElement frameworkElement)
    {
      frameworkElement.Loaded += HeimdallrContent_Loaded;
    }
  }

  /// <summary>
  /// View가 완전히 로드되었을 때 호출되는 이벤트 핸들러입니다.
  /// IViewLoadable 인터페이스를 구현한 ViewModel에게 후처리 로직을 전달합니다.
  /// </summary>
  private void HeimdallrContent_Loaded(object sender, RoutedEventArgs e)
  {
    if (sender is FrameworkElement frameworkElement &&
        frameworkElement.DataContext is IViewLoadable viewmodel)
    {
      // 이벤트 중복 방지
      frameworkElement.Loaded -= HeimdallrContent_Loaded;

      if (frameworkElement is IViewable viewable)
        viewmodel.OnLoaded(viewable);
    }
  }

  /// <summary>
  /// 현재 연결된 View 인스턴스를 반환합니다.
  /// 초기화 이전에 호출할 경우 예외를 발생시킵니다.
  /// </summary>
  /// <returns>FrameworkElement (예: UserControl, Window)</returns>
  public FrameworkElement GetView()
  {
    return _view ?? throw new InvalidOperationException("View 가 초기화되지 않았습니다.");
  }

  /// <summary>
  /// View에 연결된 ViewModel(INotifyPropertyChanged 구현체)을 반환합니다.
  /// ViewModel의 존재 여부에 따라 null이 반환될 수 있습니다.
  /// </summary>
  /// <returns>INotifyPropertyChanged 또는 null</returns>
  public INotifyPropertyChanged? GetDataContext()
  {
    return _view?.DataContext as INotifyPropertyChanged;
  }
}


