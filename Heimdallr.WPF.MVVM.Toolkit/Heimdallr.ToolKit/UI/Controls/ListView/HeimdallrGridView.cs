using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 기본 커스터마이징
/// </summary>
public class HeimdallrGridView : GridView
{
  /*
   * GridView는 Control이 아닌 ViewBase의 파생 클래스이므로, DefaultStyleKeyProperty.OverrideMetadata를 직접 사용할 수 없습니다.
   * HeimdallrGridView의 스타일을 오버라이드하려면, GridView를 사용하는 XAML 파일에서 스타일을 정의하고, 
     HeimdallrGridView에 적절하게 적용해야 합니다
   * GridViewColumnHeader와 같은 특정 요소의 스타일을 설정하려면 해당 요소에 대한 Style을 XAML에서 정의하고 사용합니다.
   */
}

