# Heimdallr.WPF.MVVM.Toolkit
Prism.DryIco, CommunityToolkit.Mvvm, WpfAutoGrid.Core  필수 Utility  지원하는 WPF 및 MVVM Toolkit
## Heimdallr.App [실행프로젝트]
- [x] 01 App.cs
- [x] 02 Starter.cs

## Heimdallr.ToolKit [Uitilty Class Library]
### CommunityToolkit.Mvvm (8.4.0), Prism.DryIco (9.0.537), WpfAutoGrid.Core (1.5.1), Newtonsoft.Json (13.0.3), YamlDotNet.NetCore (1.0.0) Nuget Package 설치
### Heimdallr.ToolKit Project Overview
**■ Properties [Folder]**
  - [x] AssemblyInfo.cs

**■ Animations [Folder]**
 - **EasingFunctions [Folder]**
    - [x] EasingFunctionBaseMode.cs [이징 함수(easing function) 의 다양한 모드를 정의하는데 사용(Enum)]
  - [x] ColorAnimationItem.cs [WPF에서 색상 애니메이션을 제어하는 ColorAnimation을 상속(적용할 대상과 속성, 이징 모드 등을 설정)]
  - [x] DoubleAnimationItem.cs [Storyboard에서 사용할 수 있도록 대상 속성, 대상 이름, 이징 함수 등을 설]
  - [x] ThicknessAnimationItem.cs [Margin, Padding, BorderThickness 등의 Thickness 속성에 애니메이션을 적용(타겟 요소 및 속성)]

**■ Applications [Folder]**
  - [x] HeimdallrApplication.cs [PrismApplication 을 상속받는 추상 클래스 (AddInversionModule[IoC 모듈을 애플리케이션에 추가], AddWireDataContext(ViewModelLocationScenario 는 View-ViewModel 매핑 설정))]

**■ Attributes [Folder]**
  - [x] UseDimmingAttribute.cs [특정 클래스에 대해 "디밍(Dimming) 효과를 사용할지 여부"를 지정, 뷰(View)나 윈도우(Window) 클래스에 적용하여, 해당 UI가 표시될 때 배경을 어둡게 처리할지 결정]

**■ Commons [Folder]** [공유]
  - [x] ObservableBase.cs [CommunityToolkit.Mvvm.ComponentModel 의 ObservableObject 상속]

**■ Contracts [Folder]** [계약]
  - **Viewing [Folder]** [보기]
   - [x] IViewable.cs [View와 ViewModel에 대한 참조를 제공]
   - [x] IViewInitializable.cs [MVVM 아키텍처에서 ViewModel이 View에 연결되었을 때 실행]
   - [x] IViewLoadable.cs [View의 Loaded 이벤트가 발생했을 때, ViewModel이 후처리 로직을 수행]

**■ Converters [Folder]** [계약]
  - [x] BaseValueConverter.cs [BaseValueConverter<T> 제네릭 클래스,  MarkupExtension 은 XAML에서 사용할 수 있도록 확장 기능]
  - [x] BooleanToVisibilityConverter.cs [bool 값을 Visibility로 변환]
  - [x] BoolToColorConverter.cs [True/False 값에 따라 색상을 반환]
  - [x] ComparisonConverter.cs [CheckBox, RadioButton, ComboBox, ListBox 등 선택 상태를 비교 기반으로 관리]
  - [x] ComparisonMultiConverter.cs [다중 값(MultiBinding) 비교]
  - [x] DateFormatConverter.cs [날짜 형식 지정 변환]
  - [x] DepthConverter.cs [하위자식 마진 구하기 예 TreeView]
  - [x] EnumToTextConverter.cs [Enum 값을 문자열로 변환]
  - [x] HeimdallrRingSpinnerConverter.cs [여러 값을 하나로 변환하는 로직을 담고 있음, 여러 값을 입력으로 받아 하나의 결과를 반환] 
  - [x] HelperConverter.cs [여러 개의 소스 값을 변환해 하나의 타겟 값으로 바꾸거나 그 반대도 처리]
  - [x] IconSizeConverter.cs [HeimdallrTreeViewItem 의 Icon 의 크기 조정]
  - [x] IndexToNumberConverter.cs [ItmsContrl 상속(ListView, ListBox, ComboBox, WrapPanel 등에서 특정 아이템의 1-based 인덱스 번호를 반환(Number)]
  - [x] InverseComparisonConverter.cs [value != parameter일 때 true 반환하는 반전]
  - [x] MobileNumberConverter.cs [휴대전화번호 예-> 01012345678 → 010-1234-5678]
  - [x] MultiBooleanToVisibilityConverter.cs [로그인 시 사용 버튼을 화면에 표시 및 표시하지 않음]
  - [x] NullableBoolToTextConverter.cs [bool? (nullable bool)을 "예", "아니오", "모름" 등의 문자열로 변환합니다.]
  - [x] NullableIntConverter.cs [TextBox 에 Int 기본값 0 을 빈공간으로 처리]
  - [x] NumberCommaConverter.cs [숫자를 콤마 표기 천단위]
  - [x] PasswordToVisibilityConverter.cs [비밀번호 입력이나 특정 값의 상태에 따라 Visibility 값을 반환]
  - [x] ResourceBinding.cs [사용시점은 -> "리소스를 동적으로 바인딩하고 싶을 때]
  - [x] SizeConverter.cs [파일 크기 등의 숫자 값을 사람이 읽기 쉬운 형식(B, KB 등)으로 변환하는 ValueConverter]
  - [x] StringToVisibilityConverter.cs [문자열이 비어 있는지 여부에 따라 Visibility를 반환]
  - [x] SwitchThumbOffsetConverter.cs [트랙의 실제 너비(ActualWidth)에서 Thumb의 이동 가능한 거리를 계산하는 컨버터]
  - [x] ValidatingBorderBrushConverter.cs [유효성 검사 및 기본 색상 값이 정상적으로 전달되었는지 확인]
  - [x] ValidatingBorderThicknessConverter.cs [유효성 검사 실패 시 두 번째 값 반환 (기본 테두리 두께 등)]

**■ Dialogs [Folder]**
 - **Base [Folder]**
   - [x] ObservableDialog.cs [Prism의 IDialogAware를 구현하여 다이얼로그 제어, UseDimmingAttribute를 기반으로 다이얼로그가 열릴 때 배경 디밍 효과를 자동 적용]

**■ Enums [Folder]**
  - [x] IconMode.cs [None, Icon, Image]
  - [x] IconType.cs [Geometry 에서 사용할 Icon Name]
  - [x] ImageType.cs [Image 에서 사용할 Image Name]
  - [x] JustifyEnum.cs [수평 레이아웃 배치에서 자식 요소들 사이의 간격을 어떻게 분배할지를 설정할 때 사용]
  - [x] PathIconType.cs [PathGeometry Data 열거형]

**■ Events [Folder]**
  - [x] SwitchLanguagePubsub.cs [문자열을 전달받아 언어 변경]
  - [x] SwitchThemePubsub.cs [문자열을 전달받아 Theme 변경]

**■ Extensions [Folder]** [확장]
  - [x] AnimationExtensions.cs [Background, Foreground, BorderBrush, Fill 애니메이션]
  - [x] DependencyExtensions.cs [유틸리티 확장 메서드를 제공하는 매우 실용적인 도우미 클래스]
  - [x] EnumExtensions.cs [Enum 값에 정의된 Attribute를 가져옵니다]

**■ Helpers [Folder]**
  - [x] DimmingManager.cs [전역적으로 ViewModel 또는 다른 코드에서 UI와의 의존성 없이 디밍 상태를 변경]
  - [x] ThemeManager.cs [테마의 색상변경]

**■ Indicators [Folder -> Spinner Type]**
 - **Extensions [Folder]**
   - [x] DispatcherExtension.cs [지정된 시간 이후에 UI 스레드에서 작업을 실행합니다.]
   - [x] EscaladeHelper.cs [Path 개체의 StrokeDashArray 속성을 제어하는 도우미 클래스] 
 - **Helpers [Folder]**
   - [x] EllipseHelper.cs [Ellipse 컨트롤의 StrokeDashArray 속성을 제어하는 제어하는 도우미 클래스] 
 - **Models [Folder]**
   - [x] LoadingIndicator.cs [Indicator(표기시) 의 형식을 지정 예:Twist, Bar, Ring...]
   - [x] LoadingSpinnerType.cs [열거형]
 - **Resources [Folder]**
   - [x] Bar, Blocks, BouncingDot, Cogs, Cupertino, Dashes, DotCircle, DoubleBounce, Ellipse, Escalade, FourDots, Grid, None, Piston, Pulse, Ring, Swirl, ThreeDots, Teist, Wave XAML 파일
 - **Views [Folder]**
   - [x] LoadingSpinner.cs [LoadingSpinner 컨트롤 정의 (로딩 중 표시하는 스피너)]
   - [x] VisualStates.cs [Visibility 관련 상태들] 
   
**■ Infrastructure [Folder]** [기본구조]
 - **AutoWiring [Folder]** [자동연결]
   - [x] AutoWireManager.cs [View와 ViewModel을 자동으로 연결(Auto-Wiring)]
   - [x] ViewModelLocationScenario.cs [ViewModel을 View에 연결하기 위한 시나리오 정의의 추상클래스]
   - [x] ViewModelLocatorCollection.cs [View와 ViewModel 간의 자동 연결(Locator) 등록을 도와주는 컬렉션]
   - [x] ViewModelLocatorItem.cs [View와 ViewModel 간의 연결 관계를 정의하는 단일 매핑 항목]
 - **Messaging [Folder]** 
   - [x] EventAggregatorHub.cs [ViewModel 간 이벤트 기반 통신]
   - [x] IEventHub.cs [Prism PubSubEvent 시스템의 추상화 컴포넌트 간 강한 결합 없이 메시지를 주고받기 위해 활용]
 - **Navigation [Folder]** 
   - [x] ContentManager.cs [Region 기반의 View 전환을 간결하게 처리하기 위한 헬퍼 유틸리티]

**■ Interfaces [Folder]**
  - [x] IResourceManager.cs [아무 멤버가 없는 인터페이스지만, 특정 타입을 구분하는 데 사용]
  - [x] IThemeManager.cs [아무 멤버가 없는 인터페이스지만, 특정 타입을 구분하는 데 사용]

**■ Models [Folder]**
 - **TreeViewTest [Folder]**
   - [x] FileCreator.cs [Text 폴더 생성]
   - [x] FileItem.cs [TreeView 하위 자식 항목]
   - [x] TreeViewInfo.cs [TreeView 정보]
   - [x] TreeViewInfoBase.cs [TreeView 의 이름, 깊이, 타입 설정]
  - [x] FontFamilyUnit.cs [폰트 설정을 구성하고 외부 YAML 파일과 매핑되도록 설계된 폰트]
  - [x] FontPack.cs [FontFamilyUnit 폰트 설정]
  - [x] LanguagePack.cs [LanguageUnit Lanuage 설정]
  - [x] LanguageUnit.cs [Lanuage 설정으 구성하고 외부 YAML 파일과 매핑대도록 설계된 Lanuage]
  - [x] SolidColorBrushUnit.cs [색상 테마 정의와 같은 기능을 위해 YAML로 정의된 색상 값을 불러와 사용]
  - [x] ThemeModel.cs [테마 코드, 테마 이름, 아이콘 타입, 값 등을 저장, 데이터를 관리하고, 사용자가 선택한 테마에 맞는 값을 활용]
  - [x] ThemePack.cs [테마의 색상 을 YAML에서 설계된 Color 설정]
  - [x] ThemeRoot.cs [Theme, Font, Lanuage YAML]

**■ Resources [Folder]**
 - **Data [Folder]**
   - [x] geometries.json [Geometry Data]
   - [x] images.yaml [Image Data]
   - [x] pathgeometries.json [PahIcon 지정시 PathGeometry Data Not Marked] 
   - [x] Summary.txt [리소스 사용방법] 
 - **Geometies [Folder]**
   - [x] GeometryItem.cs      [geometries.json 파일 자료 (name, data) 속성]
   - [x] GeometryRoot.cs      [geometries.json 파일 geometries Name]
   - [x] GeometryContainer.cs [GeometryItem 개체들을 관리하는 정적 클래스]
   - [x] GeometryConverter.cs [지정된 이름에 해당하는 GeometryItem의 데이터를 반환]
   - [x] GeometryData.cs      [GeometryConverter를 사용하여 각 아이콘에 대한 데이터를 반환]
 - **Images [Folder]**
   - [x] ImageItem.cs           [images.yaml 파일 자료 (name, data) 속성]
   - [x] ImageRoot.cs        [images.yaml 파일 images Name]
   - [x] ImageContainer.cs [ImageItem 개체들을 관리하는 정적 클래스]
   - [x] ImageConverter.cs [ImageContainer 에서 특정 이름의 ImageItem 을 조회, SVG 등 데이터 문자열을 반환]
   - [x] ImageData.cs        [ImageConverter 를 사용하여 각 Image에 대한 데이터를 반환]
- **Initialization [Folder]**
   - [x] BaseResourceInitializer.cs [WPF 또는 .NET 앱에서 테마/로케일 등의 리소스를 초기화하는 기반 추상 클래스]
   - [x] ResourceManager.cs [Prism 프레임워크 기반 WPF 애플리케이션에서 테마 및 언어 리소스를 로딩, 전환 및 관리하는 역할]
 - **PathDataStore [Folder]**
   - [x] PathGeometryStore.cs.cs  [정적으로 PathGeometry Data 값 입력]
 
**■ Themes [Folder]**
- **Controls [Folder]**
   - **Button [Folder]**
     - [x] CloseButton.xaml [닫기 Button]
     - [x] MaximizeButton.xaml [크게 Button]
     - [x] MinimizeButton.xaml [작게 Button]
   - **CheckBox [Folder]**
     - [x] HeimdallrSwitchCheckBox.xaml [Check Button 양옆으로 이동]
   - **Colors [Folder]**
     - [x] Color.xam [Color 항목]
  - **ContentConttrol [Folder]**
     - [x] SliderContentMenu.xam [슬라이드 메뉴(햄버거메뉴)]
  - **DatePicker [Folder]**
  - **Grid [Folder]**
  - **Icon [Folder]**
    - [x] HeimdallrIcon.xaml [Heimdallr Icon]
  - **ListBox [Folder]**
    - [x] MagicBar.xaml [수평 Menu 컨트롤]
  - **ListView [Folder]**
    - [x] HeimdallrGridView.xaml [GridViewColumnHeader Style]
    - [x] HeimdallrListView.xaml [ListView Style]
    - [x] HeimdallrListViewItem.xaml [ListViewItem Style]
  - **Region [Folder]**
    - [x] HeimdallrRegion.xaml [Region Style]
  - **Slider [Folder]**
    - [x] RiotSlider.xaml [Slider Style]
  - **SliderPanel [Folder]**
    - [x] HeimdallrSlidingPanel.xaml [Slider Style] 
  - **StackPanel [Folder]**
  - **Thumb [Folder]**
    - [x] HeimdallrThumb.xaml [Thumb Style]
  - **ToggleButton [Folder]**
    - [x] ChevronSwith.xaml [Up, Down Style]
    - [x] ThemeSwitchButton.xaml [Switch Style]
  - **TreeView [Folder]**
    - [x] HeimdallrTreeView.xaml [TreeView Style]
    - [x] HeimdallrTreeViewItem.xaml [TreeViewItem Style]  
  - **Window [Folder]**
    - [x] DarkThemeWindow.xaml [Window Style]
  - [x] Generic.xaml [Themes 의 Xaml 등록 -> DarkThemeWindow.xaml, HeimdallrIcon.xaml, Color.xaml,
                     CloseButton.xaml, MaximizeButton.xaml, MinimizeButton.xaml, HeimdallrRegion.xaml, HeimdallrSlidingPanel.xaml, MagicBar.xaml,
                     RiotSlider.xam, ThemeSwitchButton.xaml, HeimdallrListView.xaml, HeimdallrListViewItem.xaml, HeimdallrGridView.xaml, HeimdallrThumb.xaml,
                     HeimdallrTreeView.xaml, HeimdallrTreeViewItem.xaml, ChevronSwith.xaml, HeimdallrSwitchCheckBox.xamll

**■ UI [Folder]**
 - **Controls [Folder]**
  - **Base [Folder]**
    - [x] HeimdallrContents.cs [View와 ViewModel을 자동으로 연결(AutoWire)하는 컨트롤]
  - **Border [Folder]**
    - [x] DraggableBar.cs [Border Drag]
  - **Button [Folder]**
    - [x] CloseButton.cs [닫기 Button]
    - [x] MaximizeButton.cs [크게 Button]
    - [x] MinimizeButton.cs [작게 Button]
  - **CheckBox [Folder]**
    - [x] HeimdallrSwitchCheckBox.cs [Check Button 양옆으로 이동 Control]
   - **ContentConttrol [Folder]**
    - [x] SliderContentMenu.cs [Slider 메뉴 ContentControl, 열기, 에니메애션, 너비]
  - **DatePicker [Folder]**
  - **Grid [Folder]**
    - [x] HeimdallrGrid.cs [AutoGrid 를 확장]
  - **Icon [Folder]**
    - [x] HeimdallrIcon.cs [XAML 에서 사용가능한 Icon]
  - **ListBox [Folder]**
    - [x] HeimdallrListBox.cs [미완성]
    - [x] HeimdallrListBoxItem.cs [미완성]
    - [x] MagicBar.cs [ListView 상속으로 확인]
  - **ListView [Folder]**
    - [x] HeimdallrGridView.cs [GridViewColumnHeader Contrl]
    - [x] HeimdallrListView.cs [ListView Control]
    - [x] HeimdallrListViewItem.cs [ListViewItem Control] 
  - **Region [Folder]**
    - [x] HeimdallrRegion.cs [Region Control]
  - **Slider [Folder]**
    - [x] RiotSlider.cs [Slider Control] 
  - **SliderPanel [Folder]**
    - [x] HeimdallrSlidingPanel.cs [SlidingPanel Control]
  - **StackPanel [Folder]**
    - [x] HeimdallrPanel.cs [Panel Contrl]
    - [x] HeimdallrStack.cs [Stack Control]
    - [x] MagicStackPanel.cs [TreeView 에서 사용하는 StackPanel]
  - **Thumb [Folder]**
    - [x] HeimdallrThumb.cs [Thumb Contrl] 
  - **ToggleButton [Folder]**
    - [x] ChevronSwith.cs []
    - [x] HeimdallrToggleSwitch.cs []
    - [x] ThemeSwitchButton.cs [] 
  - **TreeView [Folder]**
    - [x] HeimdallrTreeView.cs []
    - [x] HeimdallrTreeViewItem.cs []  
  - **Window [Folder]**
    - [x] DarkThemeWindow.cs [DarkThemeWindow 클래스는 WPF 기반의 커스텀 창을 정의하며, 일반적인 기능(닫기, 최소화, 최대화, 드래그 이동)과함께 다크 테마, 디밍 처리(어두워짐), 태스크바 표시 여부 제어 같은 기능들을 포함 ]
    - [x] HeimdallrWindow.cs [WPF 애플리케이션에서 MVVM 아키텍처에 맞춰 View와 ViewModel을 자동 연결하고, UI 조작에 유틸리티 기능을 제공하는 사용자 정의 Window입니다 IViewable (View 와 ViewModel 에 대한 참조 제공)] 

**■ 기타**
 왜 Release로 빌드하나요?
 최적화: Release 모드는 코드 최적화가 적용되어, 실행 속도나 크기가 더 효율적입니다.
 디버깅 정보 제거: Debug 빌드는 디버깅 정보를 포함하지만, Release 빌드는 이러한 정보를 제거하고 실제 배포에 필요한 코드만 포함됩니다. 이는 패키지 크기를 줄이고, 실제 실행에 필요한 최적화된 코드를 제공합니다.
 배포 준비: NuGet 패키지는 일반적으로 실제 운영 환경에서 사용되므로 최적화된 Release 빌드로 패키지를 만드는 것이 표준입니다.

 .csproj 파일에 <GeneratePackageOnBuild>true</GeneratePackageOnBuild> 속성이 설정되어 있으면, 빌드가 완료되면 .nupkg 파일이 자동으로 생성됩니다.
 생성된 .nupkg 파일은 bin\Release 폴더 안에 위치합니다.

**■ NeGut 패키지 관리**
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




