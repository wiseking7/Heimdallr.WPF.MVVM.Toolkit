namespace Heimdallr.ToolKit.Indicators;

/// <summary>
/// 표시기 형식
/// </summary>
public enum LoadingSpinnerType
{
  /// <summary>
  /// 수평 바 형태의 로딩 애니메이션		
  /// </summary>
  Bar,

  /// <summary>
  /// 여러 개의 블록(사각형)을 사용하는 로딩 애니메이션		  
  /// </summary>
  Blocks,

  /// <summary>
  /// 튕기는 점을 사용한 로딩 애니메이션		
  /// </summary>
  BouncingDot,

  /// <summary>
  /// 톱니바퀴 모양을 사용한 로딩 애니메이션		 
  /// </summary>
  Cogs,

  /// <summary>
  /// Apple의 Cupertino 스타일을 사용하는 로딩 애니메이션		 
  /// </summary>
  Cupertino,

  /// <summary>
  /// 대시 모양의 로딩 애니메이션	
  /// </summary>
  Dashes,

  /// <summary>
  /// 원 모양의 점을 사용하는 로딩 애니메이션		 
  /// </summary>
  DotCircle,

  /// <summary>
  /// 두 개의 점이 튕기는 로딩 애니메이션		 
  /// </summary>
  DoubleBounce,

  /// <summary>
  /// 타원형 애니메이션		 
  /// </summary>
  Ellipse,

  /// <summary>
  /// Escalade 스타일의 로딩 애니메이션	
  /// </summary>
  Escalade,

  /// <summary>
  /// 네 개의 점이 순차적으로 나타나는 애니메이션	
  /// </summary>
  FourDots,

  /// <summary>
  /// 격자 형태의 로딩 애니메이션		
  /// </summary>
  Grid,

  /// <summary>
  /// 없음
  /// </summary>
  None,

  /// <summary>
  /// 피스톤처럼 움직이는 애니메이션	
  /// </summary>
  Piston,

  /// <summary>
  /// '펄스' 스타일의 로딩 애니메이션 (확장 및 축소)		
  /// </summary>
  Pulse,

  /// <summary>
  /// 원 형태로 돌아가는 로딩 애니메이션		
  /// </summary>
  Ring,

  /// <summary>
  /// 나선형 형태로 회전하는 로딩 애니메이션		
  /// </summary>
  Swirl,

  /// <summary>
  /// 세 개의 점이 순차적으로 나타나는 애니메이션		
  /// </summary>
  ThreeDots,

  /// <summary>
  /// 트위스트 형태로 움직이는 로딩 애니메이션		
  /// </summary>
  Twist,

  /// <summary>
  /// 파도 모양의 애니메이션		 
  /// </summary>
  Wave
}
