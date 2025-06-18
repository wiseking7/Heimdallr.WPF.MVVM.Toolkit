namespace Heimdallr.ToolKit.Attributes;
/// <summary>
/// 제품 단위에 대한 정보를 제공하는 어트리뷰트입니다.
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class UnitInfoAttribute : Attribute
{
  /// <summary>
  /// 제품 단위의 설명과 실제 수량을 나타내는 어트리뷰트입니다. (예: "다스", "상자")
  /// </summary>
  public string Description { get; }

  /// <summary>
  /// 제품 단위가 의미하는 실제 수량을 나타내는 어트리뷰트입니다. (예: 12, 24)
  /// </summary>
  public int Quantity { get; }

  /// <summary>
  /// 제품 단위에 대한 설명과 실제 수량을 지정하는 생성자입니다.
  /// </summary>
  /// <param name="description"></param>
  /// <param name="quantity"></param>
  public UnitInfoAttribute(string description, int quantity)
  {
    Description = description;
    Quantity = quantity;
  }
}