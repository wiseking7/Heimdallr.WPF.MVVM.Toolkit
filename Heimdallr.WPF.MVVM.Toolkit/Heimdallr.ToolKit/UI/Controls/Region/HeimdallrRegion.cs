using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// MVVM(Prism) 프레임워크에서 영역(Region) 역할을 하는 ContentControl 기반 커스텀 컨트롤.
/// RegionName 속성을 통해 영역 이름을 지정하고, RegionManager와 연결하여 영역 관리 기능 제공.
/// </summary>
public class HeimdallrRegion : ContentControl
{
  /// <summary>
  /// RegionName 의존 속성
  /// 영역 이름을 문자열로 저장하며, 값이 변경되면 콜백으로 RegionManager를 설정함.
  /// </summary>
  public static readonly DependencyProperty RegionNameProperty =
    DependencyProperty.Register(
      nameof(RegionName),                 // 속성 이름 "RegionName"
      typeof(string),                    // 속성 타입 string
      typeof(HeimdallrRegion),           // 소유 타입 HeimdallrRegion
      new PropertyMetadata(ContentNamePropertyChanged)); // 값 변경 시 호출되는 콜백

  /// <summary>
  /// 영역 이름 프로퍼티 래퍼
  /// </summary>
  public string RegionName
  {
    get => (string)GetValue(RegionNameProperty);
    set => SetValue(RegionNameProperty, value);
  }

  /// <summary>
  /// RegionName 속성 값이 변경될 때 실행되는 콜백 메서드
  /// </summary>
  /// <param name="d">변경이 발생한 DependencyObject (여기서는 HeimdallrRegion 인스턴스)</param>
  /// <param name="e">속성 변경 정보</param>
  private static void ContentNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // 새 값이 null이 아니고 빈 문자열도 아닌 경우에만 처리
    if (e.NewValue is string str && str != "")
    {
      // 현재 애플리케이션의 MainWindow에서 RegionManager 인스턴스 가져오기
      IRegionManager rm = RegionManager.GetRegionManager(Application.Current.MainWindow);

      // 변경된 영역 이름을 현재 HeimdallrRegion 컨트롤에 등록
      RegionManager.SetRegionName((HeimdallrRegion)d, str);

      // 해당 컨트롤에 RegionManager 인스턴스 연결
      RegionManager.SetRegionManager(d, rm);
    }
  }

  /// <summary>
  /// 정적 생성자: 스타일 키를 이 컨트롤 타입으로 재정의
  /// 이 컨트롤에 대한 기본 스타일을 찾아서 적용 가능하도록 함.
  /// </summary>
  static HeimdallrRegion()
  {
    DefaultStyleKeyProperty.OverrideMetadata(
      typeof(HeimdallrRegion),
      new FrameworkPropertyMetadata(typeof(HeimdallrRegion)));
  }

  /// <summary>
  /// 기본 생성자 (현재는 별도 초기화 코드 없음)
  /// </summary>
  public HeimdallrRegion()
  {
  }
}


