using Heimdallr.ToolKit.Contracts.Viewing;
using Heimdallr.ToolKit.Infrastructure.AutoWiring;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/* WPF 애플리케이션에서 MVVM 아키텍처에 맞춰 View와 ViewModel을 자동 연결하고, 
   UI 조작에 유틸리티 기능을 제공하는 사용자 정의 Window입니다 
   IViewable (View 와 ViewModel 에 대한 참조 제공)*/
public class HeimdallrWindow : Window, IViewable
{
  private readonly AutoWireManager? _autoWireManager;

  // IViewable 상속 ViewModel이 IViewable 인터페이스를 통해 자신의 View 및 ViewModel에 접근할 수 있게 함
  public FrameworkElement View => _autoWireManager!.GetView();

  // IViewable 상속 ViewModel이 IViewable 인터페이스를 통해 자신의 View 및 ViewModel에 접근할 수 있게 함
  public INotifyPropertyChanged ViewModel => _autoWireManager!.GetDataContext()!;

  /// <summary>
  /// View와 ViewModel을 자동 연결(Auto-Wiring)해주는 DI 및 로딩 매니저
  /// </summary>
  public HeimdallrWindow()
  {
    _autoWireManager = new AutoWireManager();
    _autoWireManager.InitializeAutoWire(this);
  }

  /// <summary>
  /// Window 의 Content 를 지정된 FrameworkElement 로 설정
  /// </summary>
  /// <param name="frameworkElement"></param>
  /// <returns></returns>
  public HeimdallrWindow AddChild(FrameworkElement frameworkElement)
  {
    Content = frameworkElement;

    return this;
  }

  /// <summary>
  /// 컨텐츠 요소의 정렬을 VerticalAlignment · HorizontalAlignment 중앙 정렬로 설정
  /// 레이아웃 구조가 없는 경우 기본 정렬 제어에 유용
  /// </summary>
  /// <returns></returns>
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
  /// 문자열로 받은 색상 코드 ("#FFFFFF" 또는 "Red" 등)를 Color로 변환하여 WPF Brush로 적용
  /// WPF 테마 적용을 간단하게 할 수 있도록 함
  /// </summary>
  /// <param name="background"></param>
  /// <param name="borderBrush"></param>
  /// <param name="foreground"></param>
  /// <returns></returns>
  public HeimdallrWindow ApplyThemeColors(string background, string borderBrush, string foreground)
  {
    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(background));

    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(borderBrush));

    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(foreground));

    return this;
  }
}
