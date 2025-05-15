using System.Diagnostics;
using System.Windows.Media;

namespace Heimdallr.ToolKit.Resources.PathGeometries;

public class PathGeometryConverter
{
  // PathGeometry 데이터를 이름을 통해 가져오는 메서드
  public static string GetData(string name)
  {
    if (string.IsNullOrEmpty(name))
    {
      throw new ArgumentNullException(nameof(name), "이름은 null 또는 빈 문자열일 수 없습니다.");
    }

    // PathGeometryContainer.Items가 null인 경우 예외 처리
    if (PathGeometryContainer.Items == null)
    {
      throw new InvalidOperationException("PathGeometryContainer.Items가 null입니다.");
    }

    // PathGeometryContainer에서 이름에 해당하는 항목이 있는지 확인
    if (!PathGeometryContainer.Items.ContainsKey(name))
    {
      Debug.WriteLine($"[PathGeometryConverter] '{name}' Key 찾을 수 없습니다");
      throw new KeyNotFoundException($"PathGeometry '{name}'를 찾을 수 없습니다.");
    }

    // 항목 가져오기
    var item = PathGeometryContainer.Items[name];

    if (string.IsNullOrWhiteSpace(item.Figures))
    {
      Debug.WriteLine($"[PathGeometryConverter] '{name}'의 Figures가 비어 있음."); // ✅ 로그
      throw new InvalidOperationException($"'{name}'의 Figures가 없습니다.");
    }

    Debug.WriteLine($"[PathGeometryConverter] '{name}'의 경로 데이터: {item.Figures}");

    // PathGeometry 객체 생성
    PathGeometry geometry = PathGeometry.CreateFromGeometry(Geometry.Parse(item.Figures));

    // PathGeometry를 문자열로 반환 (예: Geometry의 Figures 속성)
    return geometry.ToString(); // 또는 geometry.Figures.ToString()
  }
}
