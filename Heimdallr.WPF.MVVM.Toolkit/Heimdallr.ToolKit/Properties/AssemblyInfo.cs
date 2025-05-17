using System.Runtime.InteropServices;
using System.Windows.Markup;

[assembly: System.Windows.ThemeInfo(System.Windows.ResourceDictionaryLocation.None, System.Windows.ResourceDictionaryLocation.SourceAssembly)]

// 이와 같은 SDK 스타일 프로젝트에서는 이 파일에 지금까지 정의된 여러 어셈블리 특성이 이제 빌드 중에 자동으로 추가되고 프로젝트 속성에 정의된
// 값으로 채워집니다. 포함된 특성에 대한 자세한 내용과 이 프로세스를 사용자 지정하는 방법에 대해서는 https://aka.ms/assembly-info-properties를
// 참조하세요.

// ComVisible을 false로 설정하면 이 어셈블리의 형식이 COM 구성 요소에 표시되지 않습니다.  COM에서 이 어셈블리의 형식에 액세스하려면
// 해당 형식에 대해 ComVisible 특성을 true로 설정하세요. (COM 노출 여부)

[assembly: ComVisible(false)]

// 이 프로젝트가 COM에 노출되는 경우 다음 GUID는 typelib의 ID를 나타냅니다.

[assembly: Guid("6b685f3b-6b8d-4f71-89c6-1d4f018a6731")]

// 여러 네임스페이스를 하나의 URI로 묶어서 정의
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Animations")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Animations.EasingFunctions")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Attributes")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Commons")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Converters")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Dialogs.Base")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Enums")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Events")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Helpers")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Infrastructure.AutoWiring")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Models")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Resources.Geometies")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Resources.Images")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.Resources.PathGeometries")]
[assembly: XmlnsDefinition("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "Heimdallr.ToolKit.UI.Controls")]

// 하나의 접두사로 묶기
[assembly: XmlnsPrefix("https://Heimdallr.WPF.MVVM.ToolKit/xaml", "heimdallr")]