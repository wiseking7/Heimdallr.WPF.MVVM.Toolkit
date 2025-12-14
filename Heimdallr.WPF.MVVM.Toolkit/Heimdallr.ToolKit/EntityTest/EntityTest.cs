using System.ComponentModel.DataAnnotations;

namespace Heimdallr.ToolKit.EntityTest;

/// <summary>
/// 
/// </summary>
public class EntityTest
{
  /// <summary>
  /// 
  /// </summary>
  [Display(Name = "구매가", Description = "구매가격(부가별도)")]
  public decimal Buy { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [Display(Name = "판매가", Description = "판매가격(부가별도")]
  public decimal Sell { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [Display(Name = "종료일자", Description = "가격 적용 종료일 (null이면 현재 적용 중)")]

  public DateTime? EndDate { get; set; }

}
