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






