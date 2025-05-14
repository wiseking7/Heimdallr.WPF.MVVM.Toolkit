# Heimdallr.WPF.MVVM.Toolkit
Prism.DryIco, CommunityToolkit.Mvvm, WpfAutoGrid.Core  필수 Utility  지원하는 WPF 및 MVVM Toolkit
## Heimdallr.App [실행프로젝트]
- [x] 01 App.cs
- [x] 02 Starter.cs

## Heimdallr.ToolKit [Uitilty Class Library]
### CommunityToolkit.Mvvm (8.4.0), Prism.DryIco (9.0.537), WpfAutoGrid.Core (1.5.1), Newtonsoft.Json (13.0.3), YamlDotNet.NetCore (1.0.0) Nuget Package 설치
### Heimdallr.ToolKit Project Overview
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
   - [x] ImageItem.cs        [images.yaml 파일 자료 (name, data) 속성]
   - [x] GeometryRoot.cs        [images.yaml 파일 geometries Name]
   - [x] GeometryContainer.cs [GeometryItem 개체들을 관리하는 정적 클래스]
   - [x] GeometryConverter.cs [지정된 이름에 해당하는 GeometryItem의 데이터를 반환]
   - [x] GeometryData.cs        [GeometryConverter를 사용하여 각 아이콘에 대한 데이터를 반환]
