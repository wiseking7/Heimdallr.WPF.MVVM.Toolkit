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
  - [x] EnumToTextConverter.cs [Enum 값을 문자열로 변환]
  - [x] HelperConverter.cs [여러 개의 소스 값을 변환해 하나의 타겟 값으로 바꾸거나 그 반대도 처리]
  - [x] IndexToNumberConverter.cs [ItmsContrl 상속(ListView, ListBox, ComboBox, WrapPanel 등에서 특정 아이템의 1-based 인덱스 번호를 반환(Number)]
  - [x] InverseComparisonConverter.cs [value != parameter일 때 true 반환하는 반전]
  - [x] MobileNumberConverter.cs [휴대전화번호 예-> 01012345678 → 010-1234-5678]
  - [x] MultiBooleanToVisibilityConverter.cs [로그인 시 사용 버튼을 화면에 표시 및 표시하지 않음]
  - [x] NullableBoolToTextConverter.cs [bool? (nullable bool)을 "예", "아니오", "모름" 등의 문자열로 변환합니다.]
  - [x] NullableIntConverter.cs [TextBox 에 Int 기본값 0 을 빈공간으로 처리]
  - [x] NumberCommaConverter.cs [숫자를 콤마 표기 천단위]
  - [x] PasswordToVisibilityConverter.cs [비밀번호 입력이나 특정 값의 상태에 따라 Visibility 값을 반환]
  - [x] ResourceBinding.cs [사용시점은 -> "리소스를 동적으로 바인딩하고 싶을 때]
  - [x] StringToVisibilityConverter.cs [문자열이 비어 있는지 여부에 따라 Visibility를 반환]
  - [x] ValidatingBorderBrushConverter.cs [유효성 검사 및 기본 색상 값이 정상적으로 전달되었는지 확인]
  - [x] ValidatingBorderThicknessConverter.cs [유효성 검사 실패 시 두 번째 값 반환 (기본 테두리 두께 등)]











**■ Resources [Folder]**
 - **Data [Folder]**
   - [x] geometries.json [Geometry Data]
   - [x] images.yaml [Image Data]
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

**■ Events [Folder]**
   - [x] SwitchLanguagePubsub.cs [string -> 변경언어 예제참조]
   - [x] SwitchLanguagePubsub.cs [string -> 테마변경언어]

**■ Animations [Folder]**
- **EasingFunctions [Folder]**
  - [x] EasingFunctionBaseMode.cs [Geometry Data]
  - [x] ColorAnimationItem.cs [UI 요소의 색상 속성(예: Background, BorderBrush, Foreground 등)을 부드럽게 전환, 예: 버튼 배경색이 점점 어두워지는 효과]
  - [x] DoubleAnimationItem.cs [수치형 속성(크기, 위치, 투명도 등)의 부드러운 애니메이션, 예: 버튼이 커지거나, 불투명도가 0 → 1로 변화]
  - [x] ThicknessAnimationItem.cs [Margin, Padding, BorderThickness처럼 Thickness 타입 속성에 부드러운 변화 적용, 예: 마우스를 올렸을 때 컨트롤의 여백이 커지는 효과]

**■ Contracts [Folder]**
   - [x] IViewable.cs [View와 ViewModel에 대한 참조를 제공하는 인터페이스]
   - [x] IViewInitializable.cs [MVVM 아키텍처에서 ViewModel이 View에 연결되었을 때 실행되는 초기화 작업을 정의하는 인터페이스 (ViewModel ↔ View 연결 직후	초기화, 바인딩, View 정보 활용)]
   - [x] IViewLoadable.cs [WPF MVVM 아키텍처에서 View의 Loaded 이벤트가 발생했을 때, ViewModel이 후처리 로직을 수행할 수 있도록 정의하는 인터페이스 (View의 Loaded 이벤트 발생 후	비동기 데이터 로딩, View 크기/상태 기반 처리)]






