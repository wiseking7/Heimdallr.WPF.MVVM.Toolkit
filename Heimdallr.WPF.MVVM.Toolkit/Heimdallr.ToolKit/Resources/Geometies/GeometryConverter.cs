using System.Runtime.CompilerServices;

namespace Heimdallr.ToolKit.Resources.Geometies;

public static class GeometryConverter
{
  /// <summary>
  /// 지정된 이름에 해당하는 GeometryItem의 데이터를 반환합니다.
  /// </summary>
  /// <param name="name">현재 메서드를 호출한 멤버의 이름을 자동으로 전달받습니다.</param>
  /// <returns>GeometryItem의 Data 문자열</returns>
  /// <exception cref="ArgumentNullException">name이 null일 경우 발생</exception>
  /// <exception cref="KeyNotFoundException">해당 GeometryItem을 찾을 수 없을 경우 발생</exception>
  /// <exception cref="InvalidOperationException">해당 GeometryItem에 대한 Data가 없을 경우 발생</exception>
  public static string GetData([CallerMemberName] string? name = null)
  {
    // name 매개변수는 CallerMemberName으로 자동으로 채워집니다. 
    // null이 전달되면 예외를 발생시킴으로써, 호출한 멤버 이름이 반드시 필요함을 명확히 합니다.
    if (name == null)
    {
      throw new ArgumentNullException(nameof(name), "CallerMemberName 는 null 일 수 없습니다.");
    }

    // GeometryContainer에서 해당 이름의 GeometryItem을 찾기
    if (GeometryContainer.Items == null || !GeometryContainer.Items.ContainsKey(name))
    {
      throw new KeyNotFoundException($"Geometry Item 을 찾을 수 없습니다: '{name}'.");
    }

    // GeometryItem을 가져옴
    var item = GeometryContainer.Items[name];

    // GeometryItem이 존재하지만 Data가 없을 경우 예외 처리
    if (item == null || string.IsNullOrEmpty(item.Data))
    {
      throw new InvalidOperationException($"Geometry Item '{name}'에 대한 Data 를 사용할 수 없습니다.");
    }

    // GeometryItem의 Data를 반환
    return item.Data;
  }
}