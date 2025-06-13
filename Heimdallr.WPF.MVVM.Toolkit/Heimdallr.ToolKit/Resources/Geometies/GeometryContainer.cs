using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Heimdallr.ToolKit.Resources.Geometies;

/// <summary>
/// GeometryItem 개체들을 관리하는 정적 클래스입니다. JSON 파일을 로드하고, 
/// GeometryItem을 Dictionary에 저장하여 효율적으로 접근할 수 있게 합니다.
/// </summary>
public static class GeometryContainer
{
  // GeometryItem 객체들을 저장하는 딕셔너리입니다.
  private static readonly Dictionary<string, GeometryItem> _items;

  // 정적 생성자: JSON 데이터를 로드하고 _items를 초기화합니다.
  static GeometryContainer()
  {
    _items = Build();
  }

  // JSON 파일을 로드하고 GeometryItem 객체들을 딕셔너리에 저장합니다.
  private static Dictionary<string, GeometryItem> Build()
  {
    string jsonData = LoadJson();

    var geometryRoot = JsonConvert.DeserializeObject<GeometryRoot>(jsonData);

    var items = new Dictionary<string, GeometryItem>();

    if (geometryRoot?.Items != null)
    {
      foreach (var item in geometryRoot.Items)
      {
        if (!string.IsNullOrWhiteSpace(item.Name))
        {
          items[item.Name] = item;
        }
      }
    }

    return items;
  }

  // 리소스에서 JSON 파일을 읽어오는 메서드입니다.
  private static string LoadJson()
  {
    Assembly assembly = Assembly.GetExecutingAssembly();
    string resourceName = "Heimdallr.ToolKit.Resources.Data.geometries.json";

    Debug.WriteLine($"Geometry Resource 경로: {resourceName}");

    using Stream? stream = assembly.GetManifestResourceStream(resourceName);
    if (stream == null)
    {
      // 디버깅을 돕기 위해 가능한 리소스 목록 출력
      string[] availableResources = assembly.GetManifestResourceNames();
      Debug.WriteLine("사용 가능한 리소스 목록:");
      foreach (string res in availableResources)
      {
        Debug.WriteLine($"- {res}");
      }

      throw new FileNotFoundException($"리소스 파일을 찾을 수 없습니다: {resourceName}");
    }

    using StreamReader reader = new StreamReader(stream);
    return reader.ReadToEnd();
  }

  /// <summary>
  /// 외부에서 GeometryItem 딕셔너리에 접근할 수 있도록 제공
  /// </summary>
  public static IReadOnlyDictionary<string, GeometryItem> Items => _items;
}

/*
 외부에서 GeometryContainer.Items 속성을 통해 _items 딕셔너리에 접근할 수 있게 됩니다.
 예제
  // GeometryContainer에서 Items 딕셔너리 가져오기
  var geometryItems = GeometryContainer.Items;

  // 딕셔너리에서 특정 GeometryItem을 가져오기 (예: 이름으로 검색)
  if (geometryItems != null && geometryItems.ContainsKey("ExampleName"))
  {
      var item = geometryItems["ExampleName"];
      // item을 사용한 작업 수행
  }
 */