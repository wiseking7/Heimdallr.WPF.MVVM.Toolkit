using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;
/// <summary>
/// HeimdallrListViewBase 클래스는 ListView를 확장하여 멀티 선택 기능과 항목 클릭 이벤트를 지원하는 사용자 지정 ListView 컨트롤입니다.
/// </summary>
public class HeimdallrListViewBase : ListView
{
  /// <summary>
  /// 멀티 선택이 활성화되었는지 여부를 나타내는 종속성 속성입니다.
  /// </summary>
  public static readonly DependencyProperty MultiSelectEnabledProperty =
      DependencyProperty.Register(nameof(MultiSelectEnabled), typeof(bool),
          typeof(HeimdallrListViewBase), new PropertyMetadata(false, OnMultiSelectEnabledChanged));

  /// <summary>
  /// 멀티 선택이 활성화되었는지 여부를 나타내는 속성입니다.
  /// </summary>
  public bool MultiSelectEnabled
  {
    get => (bool)GetValue(MultiSelectEnabledProperty);
    private set => SetValue(MultiSelectEnabledProperty, value);
  }

  /// <summary>
  /// MultiSelectEnabled 속성이 변경될 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnMultiSelectEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // 1. MultiSelectEnabled 속성 변경 시 이벤트 알림
    if (d is HeimdallrListViewBase control)
    {
      // MultiSelectEnabled 속성이 변경되면 이벤트를 발생시킵니다.
      control.MultiSelectEnabledChanged?.Invoke(control, EventArgs.Empty);
    }
  }

  /// <summary>
  /// 멀티 선택이 활성화되었을 때 발생하는 이벤트입니다.
  /// </summary>
  public event EventHandler? MultiSelectEnabledChanged;

  /// <summary>
  /// HeimdallrListViewBase 클래스의 새 인스턴스를 초기화합니다.
  /// </summary>
  static HeimdallrListViewBase()
  {
    // 1. 기본 스타일 키 설정
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrListViewBase),
        new FrameworkPropertyMetadata(typeof(HeimdallrListViewBase)));

    // 2. SelectionMode 속성 변경 시 MultiSelectEnabled 업데이트
    SelectionModeProperty.OverrideMetadata(typeof(HeimdallrListViewBase),
        new FrameworkPropertyMetadata(SelectionMode.Extended, OnSelectionModePropertyChanged));
  }

  /// <summary>
  /// SelectionMode 속성이 변경될 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnSelectionModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // 2. SelectionMode 변경 시 MultiSelectEnabled 업데이트
    if (d is HeimdallrListViewBase listView)
    {
      // SelectionMode가 Multiple 또는 Extended일 때 MultiSelectEnabled를 true로 설정
      listView.UpdateMultiSelectEnabled();
    }
  }

  /// <summary>
  /// 멀티 선택 체크박스가 활성화되어 있는지 여부를 나타내는 속성입니다.
  /// </summary>
  public bool IsMultiSelectCheckBoxEnabled { get; set; } = true;

  /// <summary>
  /// 선택 모드가 Multiple 또는 Extended일 때 멀티 선택을 허용하는지 여부를 나타내는 속성입니다.
  /// </summary>
  public bool IsSelectionEnabled { get; set; } = true;

  /// <summary>
  /// 멀티 선택이 활성화되어 있는지 여부를 업데이트합니다.
  /// </summary>
  private void UpdateMultiSelectEnabled()
  {
    // 2. SelectionMode에 따라 MultiSelectEnabled 업데이트
    MultiSelectEnabled = IsSelectionEnabled &&
                         SelectionMode == SelectionMode.Multiple &&
                         IsMultiSelectCheckBoxEnabled;
  }

  /// <summary>
  /// 선택이 변경될 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="e"></param>
  protected override void OnSelectionChanged(SelectionChangedEventArgs e)
  {
    // 3. 멀티 선택이 비활성화된 경우 선택 해제
    if (!IsSelectionEnabled)
    {
      // 선택 모드가 Multiple 또는 Extended인 경우 선택 해제
      if (SelectedItems.Count > 0)
        UnselectAll();
      return;
    }

    base.OnSelectionChanged(e);
  }

  /// <summary>
  /// 항목 컨테이너를 준비합니다.
  /// </summary>
  /// <param name="element"></param>
  /// <param name="item"></param>
  protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
  {
    // 4. 항목 컨테이너가 HeimdallrListViewItem인 경우 클릭 이벤트 핸들러 등록
    if (element is HeimdallrListViewBaseItem container)
    {
      // 클릭 이벤트 핸들러 등록
      container.Click += OnListItemClicked;
    }

    // 기본 컨테이너 준비 메서드 호출
    base.PrepareContainerForItemOverride(element, item);
  }

  /// <summary>
  /// 항목 컨테이너를 해제합니다.
  /// </summary>
  /// <param name="element"></param>
  /// <param name="item"></param>
  protected override void ClearContainerForItemOverride(DependencyObject element, object item)
  {
    // 4. 항목 컨테이너가 HeimdallrListViewItem인 경우 클릭 이벤트 핸들러 해제
    if (element is HeimdallrListViewBaseItem container)
    {
      // 클릭 이벤트 핸들러 해제
      container.Click -= OnListItemClicked;
    }

    base.ClearContainerForItemOverride(element, item);
  }

  /// <summary>
  /// 항목이 클릭되었을 때 호출되는 이벤트 핸들러입니다.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnListItemClicked(object? sender, RoutedEventArgs e)
  {
    if (sender is HeimdallrListViewItem listItem)
    {
      NotifyListItemClicked(listItem.Content);
    }
  }

  /// <summary>
  /// 항목 클릭 이벤트에 대한 인수 클래스입니다.
  /// </summary>
  public class ItemClickEventArgs : EventArgs
  {
    /// <summary>
    /// 
    /// </summary>
    public object? ClickedItem { get; set; }

    /// <summary>
    /// ItemClickEventArgs 클래스의 새 인스턴스를 초기화합니다.
    /// </summary>
    /// <param name="item"></param>
    public ItemClickEventArgs(object? item)
    {
      ClickedItem = item;
    }
  }

  /// <summary>
  /// 항목이 클릭되었을 때 발생하는 이벤트입니다.
  /// </summary>
  public event EventHandler<ItemClickEventArgs>? ItemClick;

  /// <summary>
  /// 항목이 클릭되었을 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="clickedItem"></param>
  internal virtual void NotifyListItemClicked(object? clickedItem)
  {
    ItemClick?.Invoke(this, new ItemClickEventArgs(clickedItem));
  }
}

