using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Heimdallr.ToolKit.Resources.PathGeometries;

public static class PathGeometryContainer
{
  private static readonly Dictionary<string, PathGeometryItem>? _items;

  static PathGeometryContainer()
  {
    _items = Build();
  }

  private static Dictionary<string, PathGeometryItem>? Build()
  {
    string? jsonData = LoadJson();

    var root = JsonConvert.DeserializeObject<PathGeometryRoot>(jsonData!);

    var items = new Dictionary<string, PathGeometryItem>();

    if (root?.Items != null)
    {
      foreach (var item in root.Items)
      {
        if (!string.IsNullOrWhiteSpace(item.Name))
        {
          items[item.Name] = item;
        }
      }
    }

    return items;
  }

  private static string? LoadJson()
  {
    var assembly = Assembly.GetExecutingAssembly();

    string resourceName = "Heimdallr.ToolKit.Resources.Data.pathgeometries.json";

    Debug.WriteLine($"PathGeometry Resource 경로: {resourceName}");

    using Stream? stream = assembly.GetManifestResourceStream(resourceName);

    if (stream == null)
    {
      throw new FileNotFoundException($"Resource를 {resourceName} 을 찾을수 없습니다");
    }

    using StreamReader reader = new StreamReader(stream);

    return reader.ReadToEnd();
  }

  public static IReadOnlyDictionary<string, PathGeometryItem>? Items => _items;
}
