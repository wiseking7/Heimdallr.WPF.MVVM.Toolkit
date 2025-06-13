using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// RiotSlider는 WPF의 기본 Slider 컨트롤을 상속받아 만든 커스텀 슬라이더입니다.
/// 기본 스타일 대신 이 컨트롤 타입에 맞는 별도의 스타일이 적용될 수 있도록
/// DefaultStyleKeyProperty를 오버라이드 하여 스타일 연결을 자동화합니다.
/// </summary>mmary>
public class RiotSlider : Slider
{
  static RiotSlider()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(RiotSlider),
      new FrameworkPropertyMetadata(typeof(RiotSlider)));
  }
}

