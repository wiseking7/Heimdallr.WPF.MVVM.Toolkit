using Heimdallr.ToolKit.Enums;

namespace Heimdallr.ToolKit.Extensions;

/// <summary>
/// 제품 단위(ProductUnit) 열거형에 대한 확장 메서드를 제공합니다.
/// </summary>
public static class ProductUnitExtensions
{
  private static readonly Dictionary<ProductUnit, string> _displayNames = new()
    {
        { ProductUnit.Each, "낱개" },
        { ProductUnit.Pack, "팩" },
        { ProductUnit.Box, "박스" },
        { ProductUnit.Dozen, "다스" },
        { ProductUnit.Gram, "그램" },
        { ProductUnit.Kilogram, "킬로그램" },
        { ProductUnit.Milliliter, "밀리리터" },
        { ProductUnit.Liter, "리터" },
        { ProductUnit.Meter, "미터" },
        { ProductUnit.SquareMeter, "제곱미터" },
        { ProductUnit.CubicMeter, "세제곱미터" },
        { ProductUnit.Piece, "조각" },
        { ProductUnit.Set, "세트" },
        { ProductUnit.Roll, "롤" },
        { ProductUnit.Bundle, "묶음" },
        { ProductUnit.Bottle, "병" },
        { ProductUnit.Bag, "봉지" },
        { ProductUnit.Can, "캔" },
        { ProductUnit.Tube, "튜브" },
        { ProductUnit.Sheet, "시트" },
        { ProductUnit.Unit, "단위" }
    };

  /// <summary>
  /// 한글 표시 문자열을 키로 하는 ProductUnit 열거형 값의 매핑을 저장합니다.
  /// </summary>
  private static readonly Dictionary<string, ProductUnit> _displayToEnum = new(StringComparer.OrdinalIgnoreCase);

  static ProductUnitExtensions()
  {
    foreach (var pair in _displayNames)
    {
      _displayToEnum[pair.Value] = pair.Key;
    }
  }

  /// <summary>
  /// ProductUnit 열거형 값을 사용자 친화적인 한글 표시 문자열로 변환합니다.
  /// </summary>
  public static string ToDisplayName(this ProductUnit unit)
  {
    return _displayNames.TryGetValue(unit, out var name)
        ? name
        : unit.ToString();
  }

  /// <summary>
  /// 한글 표시 문자열을 ProductUnit 열거형 값으로 변환합니다.
  /// 예: "병" → ProductUnit.Bottle
  /// </summary>
  public static ProductUnit FromDisplayName(string name)
  {
    if (_displayToEnum.TryGetValue(name, out var unit))
      return unit;

    throw new ArgumentException($"알 수 없는 제품 단위 이름입니다: {name}", nameof(name));
  }

  /// <summary>
  /// 한글 표시 문자열을 ProductUnit으로 안전하게 변환합니다.
  /// 변환에 실패하면 false를 반환합니다.
  /// </summary>
  public static bool TryFromDisplayName(string name, out ProductUnit unit)
  {
    return _displayToEnum.TryGetValue(name, out unit);
  }

  /// <summary>
  /// 전체 제품 단위 목록을 한글 표시 문자열로 반환합니다. ComboBox 바인딩 등에 사용됩니다.
  /// </summary>
  public static IEnumerable<string> GetAllDisplayNames()
  {
    return _displayNames.Values;
  }

  /// <summary>
  /// 전체 열거형과 한글 표시 문자열 쌍을 반환합니다. ComboBox 바인딩에 유용합니다.
  /// </summary>
  public static IEnumerable<KeyValuePair<ProductUnit, string>> GetUnitPairs()
  {
    return _displayNames;
  }
}