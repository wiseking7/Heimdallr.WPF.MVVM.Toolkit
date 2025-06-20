using System.ComponentModel;

namespace Heimdallr.ToolKit.Enums;
/// <summary>
/// 각각의 표시기(Indicator) 형식 입니다. 
/// </summary>
public enum IndicatorType
{
  /// <summary>
  /// 막대가 회전하거나 움직이며 로딩을 표시합니다.
  /// </summary>
  [Description("막대")]
  Bar,

  /// <summary>
  /// 여러 개의 블록이 애니메이션으로 깜빡이며 로딩을 나타냅니다.
  /// </summary>
  [Description("블록")]
  Blocks,

  /// <summary>
  /// 점이 튀어오르는 형태의 부드러운 로딩 애니메이션입니다.
  /// </summary>
  [Description("튕기는 점")]
  BouncingDot,

  /// <summary>
  /// 기어가 회전하는 듯한 형태로 로딩을 시각화합니다.
  /// </summary>
  [Description("기어")]
  Cogs,

  /// <summary>
  /// iOS 스타일의 부드러운 회전 애니메이션입니다.
  /// </summary>
  [Description("쿠퍼티노")]
  Cupertino,

  /// <summary>
  /// 대시(줄무늬)가 순서대로 나타나는 형태의 애니메이션입니다.
  /// </summary>
  [Description("대시")]
  Dashes,

  /// <summary>
  /// 원형으로 배열된 점들이 순차적으로 깜빡이는 스타일입니다.
  /// </summary>
  [Description("점 원")]
  DotCircle,

  /// <summary>
  /// 두 개의 점이 교차하며 커지고 작아지는 애니메이션입니다.
  /// </summary>
  [Description("이중 점")]
  DoubleBounce,

  /// <summary>
  /// 타원 형태가 회전하며 로딩 상태를 표시합니다.
  /// </summary>
  [Description("타원")]
  Ellipse,

  /// <summary>
  /// 계단을 오르내리는 듯한 움직임의 애니메이션입니다.
  /// </summary>
  [Description("에스컬레이드")]
  Escalade,

  /// <summary>
  /// 네 개의 점이 번갈아가며 움직이며 로딩을 표시합니다.
  /// </summary>
  [Description("네 점")]
  FourDots,

  /// <summary>
  /// 격자 모양 블록이 깜빡이며 로딩을 나타냅니다.
  /// </summary>
  [Description("격자")]
  Grid,

  /// <summary>
  /// 없음
  /// </summary>
  [Description("없음")]
  None,

  /// <summary>
  /// 실린더가 위아래로 움직이는 애니메이션입니다.
  /// </summary>
  [Description("피스톤")]
  Piston,

  /// <summary>
  /// 맥박처럼 크기가 변하는 원형 로딩 애니메이션입니다.
  /// </summary>
  [Description("펄스")]
  Pules,

  /// <summary>
  /// 원형 테두리가 회전하는 기본적인 로딩 인디케이터입니다.
  /// </summary>
  [Description("링")]
  Ring,

  /// <summary>
  /// 소용돌이치는 듯한 패턴의 애니메이션입니다.
  /// </summary>
  [Description("소용돌이")]
  Swirl,

  /// <summary>
  /// 세 개의 점이 반복적으로 깜빡이며 로딩을 표시합니다.
  /// </summary>
  [Description("세 점")]
  ThreeDots,

  /// <summary>
  /// 꼬인 선이 회전하는 형태의 독특한 애니메이션입니다.
  /// </summary>
  [Description("꼬임")]
  Twist,

  /// <summary>
  /// 파도처럼 좌우로 퍼지는 점 애니메이션입니다.
  /// </summary>
  [Description("파도")]
  Wave
}
