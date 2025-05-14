namespace Heimdallr.ToolKit.Attributes;

/// <summary>
/// 이 특성(Attribute)은 특정 클래스에 대해 "디밍(Dimming) 효과를 사용할지 여부"를 지정하는 데 사용됩니다.
/// 예를 들어 뷰(View)나 윈도우(Window) 클래스에 적용하여, 해당 UI가 표시될 때 배경을 어둡게 처리할지 결정할 수 있습니다.
/// 클래스에만 적용되도록 제한, Inherited = false -> 상속되지 않도록 지정
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class UseDimmingAttribute : Attribute
{
  /// <summary>
  /// 디밍 효과를 사용할지 여부를 나타냅니다.
  /// true이면 디밍 효과가 활성화되고, false이면 사용되지 않습니다.
  /// </summary>
  public bool UseDimming { get; private set; }

  /// <summary>
  /// 기본 생성자. 디밍 효과를 기본값으로 사용(true)하도록 설정합니다.
  /// 이 생성자를 사용하는 경우 [UseDimming] 과 같이 매개변수 없이 선언하면 UseDimming = true로 처리됩니다.
  /// </summary>
  public UseDimmingAttribute()
  {
    UseDimming = true;
  }

  /// <summary>
  /// 디밍 사용 여부를 지정할 수 있는 생성자입니다.
  /// 예: [UseDimming(false)] 처럼 디밍 효과를 사용하지 않도록 지정할 수 있습니다.
  /// </summary>
  /// <param name="useDimming">디밍 효과를 사용할지 여부</param>
  public UseDimmingAttribute(bool useDimming)
  {
    UseDimming = useDimming;
  }
}
