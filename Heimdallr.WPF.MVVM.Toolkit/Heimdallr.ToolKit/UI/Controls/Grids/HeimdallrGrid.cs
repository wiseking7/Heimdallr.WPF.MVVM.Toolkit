#if NETFRAMEWORK
// .NET Framework 인 경우에만 해당 블록을 컴파일하라는 의미
using WpfAutoGrid;

namespace Heimdallr.WPF.WPF.Controls;

public class HeimdallrGrid : AutoGrid
{
  public HeimdallrGrid()
  {

  }
}
#else
// .NET Core, NET 5/6/7/8 등 최신 .NET 인경우
using WpfAutoGrid;
namespace Heimdallr.ToolKit.UI.Controls;

public class HeimdallrGrid : AutoGrid
{
  public HeimdallrGrid()
  {

  }
}
#endif
// 이 방식은 멀티 타겟 프레임워크 프로젝트에서 동일 코드를 프레임워크별로 구분하여 작성할 때 유용합니다.
// 이 코드는 AutoGrid라는 UI 컨트롤을 기반으로 한 공통 사용자 정의 컨트롤 JamesGrid를 정의하며,
// 서로 다른 .NET 런타임 (.NET Framework vs .NET 5+)에서도 호환되도록 조건부 분기를 구성해 둔 것입니다.
