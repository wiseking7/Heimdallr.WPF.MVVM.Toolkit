using Heimdallr.ToolKit.Contracts.Viewing;
using Heimdallr.ToolKit.Infrastructure.AutoWiring;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrWindow 클래스는 WPF의 기본 Window를 상속하여,
/// MVVM 패턴에서 View와 ViewModel을 자동으로 연결해주는 기능과,
/// UI 조작에 도움이 되는 유틸리티 메서드를 포함하는 사용자 정의 윈도우입니다.
/// </summary>
public class HeimdallrWindow : Window, IViewable
{
  // 내부에서 View와 ViewModel 자동 연결을 관리하는 매니저 객체
  // AutoWireManager는 View와 ViewModel을 DI(Dependency Injection) 혹은 네이밍 규칙 등으로 연결해주는 역할을 한다고 추정
  private readonly AutoWireManager? _autoWireManager;

  /// <summary>
  /// IViewable 인터페이스 구현: 이 Window에 연결된 View(FrameworkElement)를 반환
  /// ViewModel이 자신의 View에 접근할 수 있도록 참조 제공
  /// </summary>
  public FrameworkElement View => _autoWireManager!.GetView();

  /// <summary>
  /// IViewable 인터페이스 구현: 이 Window에 연결된 ViewModel(INotifyPropertyChanged)을 반환
  /// ViewModel이 자신의 ViewModel 인스턴스(자기 자신일 수도 있음)에 접근할 수 있도록 참조 제공
  /// </summary>
  public INotifyPropertyChanged ViewModel => _autoWireManager!.GetDataContext()!;

  /// <summary>
  /// 기본 생성자
  /// 생성 시 AutoWireManager 인스턴스를 생성하고,
  /// 현재 Window(this)와 ViewModel을 자동 연결(Auto-Wiring) 하도록 초기화한다.
  /// </summary>
  public HeimdallrWindow()
  {
    _autoWireManager = new AutoWireManager();

    // 이 Window에 ViewModel 바인딩 및 초기화 작업 수행
    _autoWireManager.InitializeAutoWire(this);
  }

  /// <summary>
  /// 윈도우의 Content 프로퍼티에 지정한 FrameworkElement를 설정한다.
  /// 이 메서드는 체이닝 메서드 스타일을 지원하여 연속 호출 가능하다.
  /// </summary>
  /// <param name="frameworkElement">윈도우에 표시할 UI 요소</param>
  /// <returns>자기 자신을 반환하여 메서드 체이닝 지원</returns>
  public HeimdallrWindow AddChild(FrameworkElement frameworkElement)
  {
    Content = frameworkElement;
    return this;
  }

  /// <summary>
  /// 윈도우 Content가 FrameworkElement인 경우, Content 내부 요소의 정렬 속성을
  /// 수평 및 수직 모두 중앙 정렬(Center)로 설정한다.
  /// 레이아웃 구조가 없거나 기본 배치 정렬이 필요할 때 유용하다.
  /// </summary>
  /// <returns>자기 자신을 반환하여 메서드 체이닝 지원</returns>
  public HeimdallrWindow CenterAlignContent()
  {
    if (Content is FrameworkElement content)
    {
      content.HorizontalAlignment = HorizontalAlignment.Center;
      content.VerticalAlignment = VerticalAlignment.Center;
    }

    return this;
  }

  /// <summary>
  /// 문자열 형태의 색상 코드 또는 색상 이름 (예: "#FFFFFF", "Red")를 받아
  /// WPF의 Color 타입으로 변환한 후, SolidColorBrush로 감싸서
  /// 윈도우의 Background, BorderBrush, Foreground 색상을 각각 지정한다.
  /// 
  /// 이를 통해 쉽게 테마 색상 세트를 적용할 수 있다.
  /// </summary>
  /// <param name="background">배경색 문자열</param>
  /// <param name="borderBrush">테두리 색상 문자열</param>
  /// <param name="foreground">전경색(글자색) 문자열</param>
  /// <returns>자기 자신을 반환하여 메서드 체이닝 지원</returns>
  public HeimdallrWindow ApplyThemeColors(string background, string borderBrush, string foreground)
  {
    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(background));
    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(borderBrush));
    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(foreground));

    return this;
  }
}

