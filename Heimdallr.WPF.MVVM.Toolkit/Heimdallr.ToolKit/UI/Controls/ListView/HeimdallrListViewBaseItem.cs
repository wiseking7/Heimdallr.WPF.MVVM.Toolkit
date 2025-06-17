using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Heimdallr UI 스타일에 맞춘 사용자 지정 ListView/ListBox 항목 컨트롤입니다.
/// 포커스 시각 효과, 멀티 선택 상태, 클릭 처리를 확장하여 보다 세밀한 사용자 경험을 제공합니다.
/// </summary>
public class HeimdallrListViewBaseItem : ListViewItem
{
  /// <summary>
  /// HeimdallrListViewBaseItem 클래스의 새 인스턴스를 초기화합니다.
  /// </summary>
  protected HeimdallrListViewBaseItem()
  {
    Loaded += HeimdallrListViewBaseItem_Loaded;
    Unloaded += HeimdallrListViewBaseItem_Unloaded;
  }
  /// <summary>
  /// 템플릿이 적용된 후 호출되는 메서드입니다.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void HeimdallrListViewBaseItem_Loaded(object sender, RoutedEventArgs e)
  {
    if (ParentListViewBase is HeimdallrListViewBase listViewBase)
    {
      SubscribeToMultiSelectEnabledChanged(listViewBase);
    }
  }
  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void HeimdallrListViewBaseItem_Unloaded(object sender, RoutedEventArgs e)
  {
    if (ParentListViewBase is HeimdallrListViewBase listViewBase)
    {
      UnsubscribeFromMultiSelectEnabledChanged(listViewBase);
    }
  }

  #region UseSystemFocusVisuals

  /// <summary>
  /// 포커스 시각 효과를 시스템 기본값으로 사용할지 여부를 나타내는 속성입니다.
  /// </summary>
  public static readonly DependencyProperty UseSystemFocusVisualsProperty =
      FocusVisualHelper.UseSystemFocusVisualsProperty.AddOwner(typeof(HeimdallrListViewBaseItem));

  /// <summary>
  /// 포커스 시각 효과를 시스템 기본값으로 사용할지 여부를 나타내는 속성입니다.
  /// </summary>
  public bool UseSystemFocusVisuals
  {
    get => (bool)GetValue(UseSystemFocusVisualsProperty);
    set => SetValue(UseSystemFocusVisualsProperty, value);
  }

  #endregion

  #region FocusVisualMargin
  /// <summary>
  /// 포커스 시각 효과의 여백을 설정하는 속성입니다.
  /// </summary>
  public static readonly DependencyProperty FocusVisualMarginProperty =
      FocusVisualHelper.FocusVisualMarginProperty.AddOwner(typeof(HeimdallrListViewBaseItem));

  /// <summary>
  /// 포커스 시각 효과의 여백을 설정하는 속성입니다.
  /// </summary>
  public Thickness FocusVisualMargin
  {
    get => (Thickness)GetValue(FocusVisualMarginProperty);
    set => SetValue(FocusVisualMarginProperty, value);
  }

  #endregion

  #region FocusVisualPrimaryBrush
  /// <summary>
  /// 포커스 시각 효과의 기본 브러시를 설정하는 속성입니다.
  /// </summary>
  public static readonly DependencyProperty FocusVisualPrimaryBrushProperty =
      FocusVisualHelper.FocusVisualPrimaryBrushProperty.AddOwner(typeof(HeimdallrListViewBaseItem));

  /// <summary>
  /// 포커스 시각 효과의 기본 브러시를 설정하는 속성입니다.
  /// </summary>
  public Brush FocusVisualPrimaryBrush
  {
    get => (Brush)GetValue(FocusVisualPrimaryBrushProperty);
    set => SetValue(FocusVisualPrimaryBrushProperty, value);
  }

  #endregion

  #region FocusVisualPrimaryThickness
  /// <summary>
  /// 포커스 시각 효과의 기본 두께를 설정하는 속성입니다.
  /// </summary>
  public static readonly DependencyProperty FocusVisualPrimaryThicknessProperty =
      FocusVisualHelper.FocusVisualPrimaryThicknessProperty.AddOwner(typeof(HeimdallrListViewBaseItem));

  /// <summary>
  /// 포커스 시각 효과의 기본 두께를 설정하는 속성입니다.
  /// </summary>
  public Thickness FocusVisualPrimaryThickness
  {
    get => (Thickness)GetValue(FocusVisualPrimaryThicknessProperty);
    set => SetValue(FocusVisualPrimaryThicknessProperty, value);
  }

  #endregion

  #region FocusVisualSecondaryBrush
  /// <summary>
  /// 포커스 시각 효과의 보조 브러시를 설정하는 속성입니다.
  /// </summary>
  public static readonly DependencyProperty FocusVisualSecondaryBrushProperty =
      FocusVisualHelper.FocusVisualSecondaryBrushProperty.AddOwner(typeof(HeimdallrListViewBaseItem));

  /// <summary>
  /// 포커스 시각 효과의 보조 브러시를 설정하는 속성입니다.
  /// </summary>
  public Brush FocusVisualSecondaryBrush
  {
    get => (Brush)GetValue(FocusVisualSecondaryBrushProperty);
    set => SetValue(FocusVisualSecondaryBrushProperty, value);
  }

  #endregion

  #region FocusVisualSecondaryThickness
  /// <summary>
  /// 포커스 시각 효과의 보조 두께를 설정하는 속성입니다.
  /// </summary>
  public static readonly DependencyProperty FocusVisualSecondaryThicknessProperty =
      FocusVisualHelper.FocusVisualSecondaryThicknessProperty.AddOwner(typeof(HeimdallrListViewBaseItem));

  /// <summary>
  /// 포커스 시각 효과의 보조 두께를 설정하는 속성입니다.
  /// </summary>
  public Thickness FocusVisualSecondaryThickness
  {
    get => (Thickness)GetValue(FocusVisualSecondaryThicknessProperty);
    set => SetValue(FocusVisualSecondaryThicknessProperty, value);
  }

  #endregion

  #region CornerRadius
  /// <summary>
  /// ListViewBase 항목의 모서리 반경을 설정하는 속성입니다.
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
      ControlHelper.CornerRadiusProperty.AddOwner(typeof(HeimdallrListViewBaseItem));

  /// <summary>
  /// ListViewBase 항목의 모서리 반경을 설정하는 속성입니다.
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }

  /// <summary>
  /// 항목이 클릭되었을 때 발생하는 이벤트입니다.
  /// </summary>
  public event RoutedEventHandler Click
  {
    add => AddHandler(ClickEvent, value);
    remove => RemoveHandler(ClickEvent, value);
  }
  /// <summary>
  /// 항목이 클릭되었을 때 발생하는 라우팅 이벤트입니다.
  /// </summary>
  public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
    nameof(Click), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(HeimdallrListViewBaseItem));

  #endregion

  /// <summary>
  /// 템플릿이 적용된 후 호출되는 메서드입니다.
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    // 멀티 선택 기능이 활성화된 경우 상태를 업데이트합니다.
    if (ParentListViewBase is HeimdallrListViewBase listViewBase)
    {
      UpdateMultiSelectStates(listViewBase, false);
    }
  }

  /// <summary>
  /// 마우스 왼쪽 버튼이 눌렸을 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
  {
    if (!e.Handled)
    {
      _isPointerPressed = true;
    }
    base.OnMouseLeftButtonDown(e);
  }

  /// <summary>
  /// 마우스 왼쪽 버튼이 떼어졌을 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
  {
    if (!e.Handled)
    {
      HandleMouseUp(e);
      _isPointerPressed = false;
    }
    base.OnMouseLeftButtonUp(e);
  }

  /// <summary>
  /// 마우스가 항목을 떠날 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseLeave(MouseEventArgs e)
  {
    if (!e.Handled)
    {
      _isPointerPressed = false;
    }
    base.OnMouseLeave(e);
  }

  /// <summary>
  /// 키가 눌렸을 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="e"></param>
  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);

    if (e.Key == Key.Enter)
    {
      OnClick();
      e.Handled = true;
    }
  }

  /// <summary>
  /// 멀티 선택 기능이 활성화되었을 때 상태를 업데이트하고, 이벤트를 구독합니다.
  /// </summary>
  /// <param name="parent"></param>
  internal void SubscribeToMultiSelectEnabledChanged(HeimdallrListViewBase parent)
  {
    parent.MultiSelectEnabledChanged += OnMultiSelectEnabledChanged;
    UpdateMultiSelectStates(parent);
  }

  /// <summary>
  /// 멀티 선택 기능이 비활성화되었을 때 상태를 업데이트하고, 이벤트 구독을 해제합니다.
  /// </summary>
  /// <param name="parent"></param>
  internal void UnsubscribeFromMultiSelectEnabledChanged(HeimdallrListViewBase parent)
  {
    parent.MultiSelectEnabledChanged -= OnMultiSelectEnabledChanged;
    UpdateMultiSelectStates(parent);
  }

  /// <summary>
  /// 멀티 선택 기능이 활성화되었을 때 상태를 업데이트합니다.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnMultiSelectEnabledChanged(object? sender, EventArgs e)
  {
    if (sender is HeimdallrListViewBase parent)
    {
      UpdateMultiSelectStates(parent);
    }
  }

  /// <summary>
  /// 멀티 선택 상태를 업데이트합니다.
  /// </summary>
  /// <param name="parent"></param>
  /// <param name="useTransitions"></param>
  private void UpdateMultiSelectStates(HeimdallrListViewBase parent, bool useTransitions = true)
  {
    if (parent != null)
    {
      bool enabled = parent.MultiSelectEnabled && parent.IsMultiSelectCheckBoxEnabled;
      VisualStateManager.GoToState(this, enabled ? "MultiSelectEnabled" : "MultiSelectDisabled", useTransitions);
    }
  }

  /// <summary>
  /// 마우스 왼쪽 버튼이 떼어졌을 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="e"></param>
  private void HandleMouseUp(MouseButtonEventArgs e)
  {
    if (_isPointerPressed)
    {
      Rect r = new Rect(new Point(), RenderSize);

      if (r.Contains(e.GetPosition(this)))
      {
        OnClick();
      }
    }
  }

  /// <summary>
  /// 항목이 클릭되었을 때 호출되는 메서드입니다.
  /// </summary>
  protected virtual void OnClick()
  {
    RaiseEvent(new RoutedEventArgs(ClickEvent, this));
    ParentListViewBase?.NotifyListItemClicked(this);
  }

  /// <summary>
  /// 현재 항목이 속한 HeimdallrListViewBase 컨트롤을 가져옵니다.
  /// </summary>
  private HeimdallrListViewBase? ParentListViewBase => ItemsControl.ItemsControlFromItemContainer(this) as HeimdallrListViewBase;

  /// <summary>
  /// 현재 항목이 눌려진 상태인지 여부를 나타내는 플래그입니다.
  /// </summary>
  private bool _isPointerPressed;
}

