using System.Runtime.CompilerServices;

namespace Heimdallr.ToolKit.Resources.Images;

/// <summary>
/// ImageContainer 에서 특정 이름의 ImageItem 을 조회하고,
/// 그에 해당하는 SVG 등 데이터 문자열을 반환하는 유틸리티 클래스입니다.
/// </summary>
public static class ImageConverter
{
  /// <summary>
  /// 호출한 프로퍼티나 메서드의 이름을 기반으로 ImageContainer에서 데이터를 조회하여 반환합니다.
  /// </summary>
  /// <param name="name">호출자 이름 (자동으로 CallerMemberName으로 채워짐)</param>
  /// <returns>ImageItem의 Data 속성 값</returns>
  /// <exception cref="ArgumentNullException">name이 null인 경우</exception>
  /// <exception cref="KeyNotFoundException">해당 name에 해당하는 이미지 항목이 존재하지 않는 경우</exception>
  /// <exception cref="InvalidOperationException">해당 이미지 항목에 Data가 존재하지 않는 경우</exception>
  public static string GetData([CallerMemberName] string? name = null)
  {
    // 호출 이름 누락 시 예외 발생
    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentNullException(nameof(name), "호출자 이름이 null 또는 비어 있습니다.");

    // Items가 null이거나 키가 존재하지 않으면 예외
    if (ImageContainer.Items == null || !ImageContainer.Items.ContainsKey(name))
      throw new KeyNotFoundException($"이미지 항목을 찾을 수 없습니다: '{name}'");

    var item = ImageContainer.Items[name];

    // item의 Data가 비어있거나 null이면 예외
    if (string.IsNullOrWhiteSpace(item.Data))
      throw new InvalidOperationException($"이미지 항목 '{name}'에 유효한 Data가 없습니다.");

    return item.Data;
  }
}
