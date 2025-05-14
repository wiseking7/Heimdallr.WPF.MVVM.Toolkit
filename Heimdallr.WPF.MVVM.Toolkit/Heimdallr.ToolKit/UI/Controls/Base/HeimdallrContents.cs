using Heimdallr.ToolKit.Contracts.Viewing;
using Heimdallr.ToolKit.Infrastructure.AutoWiring;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// View와 ViewModel을 자동으로 연결(AutoWire)하는 컨트롤.
/// MVVM 구조에서 View와 ViewModel을 자동으로 바인딩하도록 구성된 ContentControl 파생 클래스.
/// </summary>
public abstract class HeimdallrContents : ContentControl, IViewable
{
  // View와 ViewModel을 자동으로 연결해주는 AutoWire 관리자
  private readonly AutoWireManager _autoWireManager;

  /// <summary>
  /// 현재 바인딩된 View를 가져옴.
  /// 내부적으로 AutoWireManager가 관리하는 FrameworkElement 반환.
  /// null일 수 있으므로 null 병합 연산자로 안전 처리.
  /// </summary>
  public FrameworkElement View => _autoWireManager.GetView()
    ?? throw new InvalidOperationException("View 가 null 입니다.");

  /// <summary>
  /// 현재 View에 연결된 ViewModel(INotifyPropertyChanged)을 가져옴.
  /// null일 수 있으므로 예외 처리 혹은 null 조건 연산자 사용 가능.
  /// </summary>
  public INotifyPropertyChanged ViewModel => _autoWireManager.GetDataContext()
    ?? throw new InvalidOperationException("ViewModel이 null 입니다.");

  /// <summary>
  /// 생성자: AutoWireManager를 초기화하고 현재 컨트롤에 대해 자동 연결을 수행함.
  /// </summary>
  public HeimdallrContents()
  {
    _autoWireManager = new AutoWireManager();
    _autoWireManager.InitializeAutoWire(this);
  }
}
