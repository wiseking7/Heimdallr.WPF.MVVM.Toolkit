using System.ComponentModel;
using System.Windows;

namespace Heimdallr.ToolKit.Contracts.Viewing;

/// <summary>
/// View와 ViewModel에 대한 참조를 제공하는 인터페이스입니다.
/// MVVM 패턴을 사용하는 WPF 애플리케이션에서, 이 인터페이스는 
/// View (UserControl, Window, Page 등)와 해당 View에 연결된 ViewModel에 
/// 접근할 수 있는 표준화된 방법을 제공합니다.
/// 주요 사용 목적:
/// - ViewModel에서 View에 접근이 필요한 경우 (예: 초기화, 레이아웃 계산 등)
/// - AutoWireManager 또는 ViewModel 바인딩 시 View와 ViewModel을 함께 다루기 위함
/// - 공통 인터페이스를 통해 View와 ViewModel을 일반화된 방식으로 처리 가능
/// 예를 들어, ViewModel이 View의 특정 이벤트나 속성에 반응하거나,
/// View 로딩 이후 초기화 로직을 실행할 때 유용합니다.
/// </summary>
public interface IViewable
{
  /// <summary>
  /// 현재 View(UserControl, Window, Page 등 WPF의 시각적 요소)를 반환합니다.
  /// 이 View는 MVVM 구조에서 ViewModel과 데이터 바인딩되어 사용자 인터페이스를 구성합니다.
  /// </summary>
  FrameworkElement View { get; }

  /// <summary>
  /// 현재 View에 바인딩된 ViewModel을 반환합니다.
  /// ViewModel은 INotifyPropertyChanged를 구현하여,
  /// 속성 변경 시 View에 자동으로 변경사항을 알리고 반영되도록 합니다.
  /// </summary>
  INotifyPropertyChanged ViewModel { get; }
}

