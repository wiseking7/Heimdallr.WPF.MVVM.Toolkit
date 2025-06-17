using System.Windows;

namespace Heimdallr.ToolKit.UI.Controls;
/// <summary>
/// 항목이 클릭되었을 때 발생하는 이벤트를 처리하기 위한 델리게이트입니다.
/// </summary>
/// <param name="sender">이벤트를 발생시킨 컨트롤 또는 개체입니다.</param>
/// <param name="e">클릭된 항목에 대한 정보가 포함된 <see cref="ItemClickEventArgs"/> 인스턴스입니다.</param>
public delegate void ItemClickEventHandler(object sender, ItemClickEventArgs e);

/// <summary>
/// 항목 클릭 이벤트에 대한 데이터를 포함하는 이벤트 인수 클래스입니다.
/// </summary>
public sealed class ItemClickEventArgs : RoutedEventArgs
{
  /// <summary>
  /// <see cref="ItemClickEventArgs"/> 클래스의 새 인스턴스를 초기화합니다.
  /// </summary>
  public ItemClickEventArgs()
  {
  }

  /// <summary>
  /// 클릭된 항목을 지정하여 <see cref="ItemClickEventArgs"/> 클래스의 새 인스턴스를 초기화합니다.
  /// </summary>
  /// <param name="clickedItem">사용자가 클릭한 항목입니다.</param>
  public ItemClickEventArgs(object? clickedItem)
  {
    ClickedItem = clickedItem;
  }

  /// <summary>
  /// 지정된 라우팅 이벤트와 클릭 항목을 사용하여 인스턴스를 초기화합니다.
  /// </summary>
  /// <param name="routedEvent">라우팅 이벤트입니다.</param>
  /// <param name="clickedItem">클릭된 항목입니다.</param>
  public ItemClickEventArgs(RoutedEvent routedEvent, object? clickedItem)
      : base(routedEvent)
  {
    ClickedItem = clickedItem;
  }

  /// <summary>
  /// 사용자가 클릭한 항목에 대한 개체입니다.
  /// </summary>
  public object? ClickedItem { get; internal set; }
}
