using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;
/// <summary>
/// 외곽선 색상 및 두께, 내부 테두리 색상 및 두께, 여백(Margin) 설정
/// 시스템 스타일로 사용할지 여부 결정
/// FrameworkElement를 포커스 타겟으로 지정
/// 이 클래스는 WPF에서 사용자 정의 포커스 시각 요소(Focus Visual)를 구현하기 위한 헬퍼 클래스입니다.
/// 접근성 및 키보드 탐색 시 외곽선 스타일을 더 세밀하게 제어할 수 있습니다.
/// </summary>
public static class FocusVisualHelper
{
  #region FocusVisualPrimaryBrush
  /// <summary>
  /// ocusVisualPrimaryBrush getter
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public static Brush GetFocusVisualPrimaryBrush(FrameworkElement element)
  {
    return (Brush)element.GetValue(FocusVisualPrimaryBrushProperty);
  }

  /// <summary>
  /// FocusVisualPrimaryBrush setter
  /// </summary>
  /// <param name="element"></param>
  /// <param name="value"></param>
  public static void SetFocusVisualPrimaryBrush(FrameworkElement element, Brush value)
  {
    element.SetValue(FocusVisualPrimaryBrushProperty, value);
  }

  /// <summary>
  /// 포커스 시 표시되는 기본 외곽선 색상 브러시를 설정합니다. 
  /// </summary>
  public static readonly DependencyProperty FocusVisualPrimaryBrushProperty =
    DependencyProperty.RegisterAttached("FocusVisualPrimaryBrush", typeof(Brush), typeof(FocusVisualHelper));
  #endregion

  #region FocusVisualSecondaryBrush
  /// <summary>
  /// FocusVisualSecondaryBrush getter
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public static Brush GetFocusVisualSecondaryBrush(FrameworkElement element)
  {
    return (Brush)element.GetValue(FocusVisualSecondaryBrushProperty);
  }

  /// <summary>
  /// FocusVisualSecondaryBrush setter
  /// </summary>
  /// <param name="element"></param>
  /// <param name="value"></param>
  public static void SetFocusVisualSecondaryBrush(FrameworkElement element, Brush value)
  {
    element.SetValue(FocusVisualSecondaryBrushProperty, value);
  }

  /// <summary>
  /// 보조 외곽선의 색상 브러시를 설정합니다.
  /// </summary>
  public static readonly DependencyProperty FocusVisualSecondaryBrushProperty =
    DependencyProperty.RegisterAttached("FocusVisualSecondaryBrush", typeof(Brush), typeof(FocusVisualHelper));

  #endregion

  #region FocusVisualPrimaryThickness
  /// <summary>
  /// GetFocusVisualPrimaryThickness
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public static Thickness GetFocusVisualPrimaryThickness(FrameworkElement element)
  {
    return (Thickness)element.GetValue(FocusVisualPrimaryThicknessProperty);
  }

  /// <summary>
  /// SetFocusVisualPrimaryThickness
  /// </summary>
  /// <param name="element"></param>
  /// <param name="value"></param>
  public static void SetFocusVisualPrimaryThickness(FrameworkElement element, Thickness value)
  {
    element.SetValue(FocusVisualPrimaryThicknessProperty, value);
  }

  /// <summary>
  /// 보조 외곽선의 두께를 설정합니다.
  /// </summary>
  public static readonly DependencyProperty FocusVisualPrimaryThicknessProperty =
    DependencyProperty.RegisterAttached("FocusVisualPrimaryThickness", typeof(Thickness), typeof(FocusVisualHelper),
        new FrameworkPropertyMetadata(new Thickness(2)));

  #endregion

  #region FocusVisualSecondaryThickness
  /// <summary>
  /// GetFocusVisualSecondaryThickness get
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public static Thickness GetFocusVisualSecondaryThickness(FrameworkElement element)
  {
    return (Thickness)element.GetValue(FocusVisualSecondaryThicknessProperty);
  }

  /// <summary>
  ///  SetFocusVisualSecondaryThickness set
  /// </summary>
  /// <param name="element"></param>
  /// <param name="value"></param>
  public static void SetFocusVisualSecondaryThickness(FrameworkElement element, Thickness value)
  {
    element.SetValue(FocusVisualSecondaryThicknessProperty, value);
  }

  /// <summary>
  /// Thickness 기본값 1
  /// </summary>
  public static readonly DependencyProperty FocusVisualSecondaryThicknessProperty =
    DependencyProperty.RegisterAttached("FocusVisualSecondaryThickness", typeof(Thickness),
      typeof(FocusVisualHelper), new FrameworkPropertyMetadata(new Thickness(1)));

  #endregion

  #region FocusVisualMargin
  /// <summary>
  /// GetFocusVisualMargin get
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public static Thickness GetFocusVisualMargin(FrameworkElement element)
  {
    return (Thickness)element.GetValue(FocusVisualMarginProperty);
  }

  /// <summary>
  /// SetFocusVisualMargin set
  /// </summary>
  /// <param name="element"></param>
  /// <param name="value"></param>
  public static void SetFocusVisualMargin(FrameworkElement element, Thickness value)
  {
    element.SetValue(FocusVisualMarginProperty, value);
  }

  /// <summary>
  /// Margin 값은 Thickness()
  /// </summary>
  public static readonly DependencyProperty FocusVisualMarginProperty =
    DependencyProperty.RegisterAttached("FocusVisualMargin", typeof(Thickness),
      typeof(FocusVisualHelper), new FrameworkPropertyMetadata(new Thickness()));

  #endregion

  #region UseSystemFocusVisuals
  /// <summary>
  /// 기본값 false
  /// </summary>
  public static readonly DependencyProperty UseSystemFocusVisualsProperty =
    DependencyProperty.RegisterAttached("UseSystemFocusVisuals", typeof(bool),
      typeof(FocusVisualHelper), new PropertyMetadata(false));

  /// <summary>
  /// GetUseSystemFocusVisuals get 
  /// </summary>
  /// <param name="control"></param>
  /// <returns></returns>
  public static bool GetUseSystemFocusVisuals(Control control)
  {
    return (bool)control.GetValue(UseSystemFocusVisualsProperty);
  }

  /// <summary>
  /// SetUseSystemFocusVisuals saet
  /// </summary>
  /// <param name="control"></param>
  /// <param name="value"></param>
  public static void SetUseSystemFocusVisuals(Control control, bool value)
  {
    control.SetValue(UseSystemFocusVisualsProperty, value);
  }

  #endregion

  #region IsTemplateFocusTarget
  /// <summary>
  /// OnIsTemplateFocusTargetChanged 콜백 메서드
  /// </summary>
  public static readonly DependencyProperty IsTemplateFocusTargetProperty =
    DependencyProperty.RegisterAttached("IsTemplateFocusTarget", typeof(bool),
      typeof(FocusVisualHelper), new PropertyMetadata(OnIsTemplateFocusTargetChanged));

  /// <summary>
  /// GetIsTemplateFocusTarget
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public static bool GetIsTemplateFocusTarget(FrameworkElement element)
  {
    return (bool)element.GetValue(IsTemplateFocusTargetProperty);
  }

  /// <summary>
  /// SetIsTemplateFocusTarget
  /// </summary>
  /// <param name="element"></param>
  /// <param name="value"></param>
  public static void SetIsTemplateFocusTarget(FrameworkElement element, bool value)
  {
    element.SetValue(IsTemplateFocusTargetProperty, value);
  }

  /// <summary>
  /// IsTemplateFocusTarget 의 콜백 메서드
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnIsTemplateFocusTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var element = (FrameworkElement)d;
    if (element.TemplatedParent is Control control)
    {
      if ((bool)e.NewValue)
      {
        SetTemplateFocusTarget(control, element);
      }
      else
      {
        control.ClearValue(TemplateFocusTargetProperty);
      }
    }
  }

  #endregion

  #region IsSystemFocusVisual
  /// <summary>
  /// GetIsSystemFocusVisual get
  /// </summary>
  /// <param name="control"></param>
  /// <returns></returns>
  public static bool GetIsSystemFocusVisual(Control control)
  {
    return (bool)control.GetValue(IsSystemFocusVisualProperty);
  }

  /// <summary>
  /// SetIsSystemFocusVisual set
  /// </summary>
  /// <param name="control"></param>
  /// <param name="value"></param>
  public static void SetIsSystemFocusVisual(Control control, bool value)
  {
    control.SetValue(IsSystemFocusVisualProperty, value);
  }

  /// <summary>
  /// 콜백 OnIsSystemFocusVisualChanged 메서드
  /// </summary>
  public static readonly DependencyProperty IsSystemFocusVisualProperty =
    DependencyProperty.RegisterAttached("IsSystemFocusVisual", typeof(bool),
      typeof(FocusVisualHelper), new PropertyMetadata(OnIsSystemFocusVisualChanged));

  /// <summary>
  /// IsSystemFocusVisual 속성의 변경 시 호출되는 메서드입니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnIsSystemFocusVisualChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // d가 Control 타입인지 확인
    var control = (Control)d;

    // ShowFocusVisual 속성을 설정합니다.
    if ((bool)e.NewValue)
    {
      // ShowFocusVisual 속성을 true로 설정합니다.
      control.IsVisibleChanged += OnFocusVisualIsVisibleChanged;
    }
    else
    {
      // ShowFocusVisual 속성을 false로 설정합니다.
      control.IsVisibleChanged -= OnFocusVisualIsVisibleChanged;
    }
  }

  #endregion

  #region ShowFocusVisual
  /// <summary>
  /// ShowFocusVisual 속성은 포커스 시각 요소를 표시할지 여부를 결정합니다.
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public static bool GetShowFocusVisual(FrameworkElement element)
  {
    // ShowFocusVisualProperty 속성 값을 가져옵니다.
    return (bool)element.GetValue(ShowFocusVisualProperty);
  }

  /// <summary>
  /// ShowFocusVisual 속성은 포커스 시각 요소를 표시할지 여부를 설정합니다.
  /// </summary>
  /// <param name="element"></param>
  /// <param name="value"></param>
  private static void SetShowFocusVisual(FrameworkElement element, bool value)
  {
    // ShowFocusVisualPropertyKey 속성 값을 설정합니다.
    element.SetValue(ShowFocusVisualPropertyKey, value);
  }

  /// <summary>
  /// ShowFocusVisualPropertyKey는 ShowFocusVisual 속성의 읽기 전용 키입니다.
  /// </summary>
  private static readonly DependencyPropertyKey ShowFocusVisualPropertyKey =
    DependencyProperty.RegisterAttachedReadOnly("ShowFocusVisual", typeof(bool),
      typeof(FocusVisualHelper), new PropertyMetadata(OnShowFocusVisualChanged));

  /// <summary>
  /// ShowFocusVisualProperty는 포커스 시각 요소를 표시할지 여부를 결정하는 속성입니다.
  /// </summary>
  public static readonly DependencyProperty ShowFocusVisualProperty =
      ShowFocusVisualPropertyKey.DependencyProperty;

  /// <summary>
  /// ShowFocusVisual 속성의 변경 시 호출되는 메서드입니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnShowFocusVisualChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // ShowFocusVisual 속성이 변경되면 호출되는 메서드입니다.
    if (d is Control control && GetTemplateFocusTarget(control) is { } target)
    {
      // ShowFocusVisual 속성이 true로 설정되면 포커스 시각 요소를 표시합니다.
      if ((bool)e.NewValue)
      {
        // ShowFocusVisual 속성이 true인 경우, 포커스 시각 요소를 표시합니다.
        bool shouldShowFocusVisual = true;

        // GetUseSystemFocusVisuals 메서드를 사용하여 시스템 포커스 시각 요소를 사용할지 여부를 결정합니다.
        if (target is Control targetAsControl)
        {
          // GetUseSystemFocusVisuals 메서드를 사용하여 시스템 포커스 시각 요소를 사용할지 여부를 결정합니다.
          shouldShowFocusVisual = GetUseSystemFocusVisuals(targetAsControl);
        }

        // 포커스 시각 요소를 표시할지 여부를 결정합니다.
        if (shouldShowFocusVisual)
        {
          // 시스템 포커스 시각 요소를 사용하여 포커스 시각 요소를 표시합니다.
          ShowFocusVisual(control, target);
        }
      }
      // ShowFocusVisual 속성이 false로 설정되면 포커스 시각 요소를 숨깁니다.
      else
      {
        // ShowFocusVisual 속성이 false인 경우, 포커스 시각 요소를 숨깁니다.
        HideFocusVisual();
      }
    }

    // ShowFocusVisual 속성이 변경되면 FocusedElement 속성도 업데이트합니다.
    static void HideFocusVisual()
    {
      // 포커스 시각 요소를 숨깁니다.
      if (_focusVisualAdornerCache != null)
      {
        // 부모에서 AdornerLayer를 안전하게 가져옵니다.
        if (VisualTreeHelper.GetParent(_focusVisualAdornerCache) is AdornerLayer adornerLayer)
        {
          // AdornerLayer에서 _focusVisualAdornerCache 제거
          adornerLayer.Remove(_focusVisualAdornerCache);
        }

        // 캐시 초기화
        _focusVisualAdornerCache = null!;
      }
    }

    // ShowFocusVisual 메서드는 포커스 시각 요소를 표시합니다.
    static void ShowFocusVisual(Control control, FrameworkElement target)
    {
      // ShowFocusVisual 메서드는 포커스 시각 요소를 표시합니다.
      HideFocusVisual();

      // ShowFocusVisual 메서드는 포커스 시각 요소를 표시합니다.
      AdornerLayer adornerlayer = AdornerLayer.GetAdornerLayer(target);

      // adornerlayer가 null인 경우, 포커스 시각 요소를 표시하지 않습니다.
      if (adornerlayer == null)
        return;

      // ShowFocusVisual 메서드는 포커스 시각 요소를 표시합니다.
      Style fvs = target.FocusVisualStyle;

      if (fvs != null && fvs.BasedOn == null && fvs.Setters.Count == 0)
      {
        // fvs가 null이 아니고, BasedOn이 null이며, Setters가 비어있지 않은 경우
        fvs = target.TryFindResource(SystemParameters.FocusVisualStyleKey) as Style ?? new Style();
      }

      // fvs가 null이 아닌 경우, 포커스 시각 요소를 표시합니다.
      if (fvs != null)
      {
        // fvs가 null이 아닌 경우, 포커스 시각 요소를 표시합니다.
        _focusVisualAdornerCache = new FocusVisualAdorner(control, target, fvs);

        // adornerlayer에 _focusVisualAdornerCache를 추가합니다.
        adornerlayer.Add(_focusVisualAdornerCache);

        // ShowFocusVisual 속성을 true로 설정합니다.
        control.IsVisibleChanged += OnControlIsVisibleChanged;
      }
    }


    static void OnControlIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      // IsVisibleChanged 이벤트 핸들러입니다.
      ((Control)sender).IsVisibleChanged -= OnControlIsVisibleChanged;

      // ShowFocusVisual 속성이 false로 설정되면 포커스 시각 요소를 숨깁니다.
      Debug.Assert((bool)e.NewValue == false);

      // ShowFocusVisual 속성이 false로 설정되면 포커스 시각 요소를 숨깁니다.
      if (_focusVisualAdornerCache != null && _focusVisualAdornerCache.FocusedElement == sender)
      {
        // ShowFocusVisual 속성이 false로 설정되면 포커스 시각 요소를 숨깁니다.
        HideFocusVisual();
      }
    }
  }

  #endregion

  #region FocusedElement
  /// FocusedElement 속성은 포커스가 있는 요소를 나타냅니다.
  private static FrameworkElement GetFocusedElement(Control focusVisual)
  {
    // FocusedElementProperty 속성 값을 가져옵니다.
    return (FrameworkElement)focusVisual.GetValue(FocusedElementProperty);
  }

  /// <summary>
  /// FocusedElement 속성은 포커스가 있는 요소를 설정합니다.
  /// </summary>
  /// <param name="focusVisual"></param>
  /// <param name="value"></param>
  private static void SetFocusedElement(Control focusVisual, FrameworkElement value)
  {
    // FocusedElementProperty 속성 값을 설정합니다.
    focusVisual.SetValue(FocusedElementProperty, value);
  }

  /// <summary>
  /// FocusedElementProperty는 포커스가 있는 요소를 나타내는 종속성 속성입니다.
  /// </summary>
  private static readonly DependencyProperty FocusedElementProperty =
    DependencyProperty.RegisterAttached("FocusedElement", typeof(FrameworkElement),
      typeof(FocusVisualHelper));

  #endregion

  #region TemplateFocusTarget
  /// <summary>
  /// TemplateFocusTargetProperty는 Control의 템플릿 포커스 대상 요소를 나타내는 종속성 속성입니다.
  /// </summary>
  private static readonly DependencyProperty TemplateFocusTargetProperty =
    DependencyProperty.RegisterAttached("TemplateFocusTarget", typeof(FrameworkElement),
      typeof(FocusVisualHelper));

  /// <summary>
  /// GetTemplateFocusTarget 메서드는 Control의 템플릿 포커스 대상 요소를 가져옵니다.
  /// </summary>
  /// <param name="control"></param>
  /// <returns></returns>
  private static FrameworkElement GetTemplateFocusTarget(Control control)
  {
    // TemplateFocusTargetProperty 속성 값을 가져옵니다.
    return (FrameworkElement)control.GetValue(TemplateFocusTargetProperty);
  }

  /// <summary>
  /// SetTemplateFocusTarget 메서드는 Control의 템플릿 포커스 대상 요소를 설정합니다.
  /// </summary>
  /// <param name="control"></param>
  /// <param name="value"></param>
  private static void SetTemplateFocusTarget(Control control, FrameworkElement value)
  {
    // TemplateFocusTargetProperty 속성 값을 설정합니다.
    control.SetValue(TemplateFocusTargetProperty, value);
  }

  #endregion
  /// <summary>
  /// OnFocusVisualIsVisibleChanged 메서드는 FocusVisual이 표시될 때 호출됩니다.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void OnFocusVisualIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
  {
    // sender가 Control 타입인지 확인합니다.
    var focusVisual = (Control)sender;

    // ShowFocusVisual 속성이 변경되면 호출되는 메서드입니다.
    if ((bool)e.NewValue)
    {
      // ShowFocusVisual 속성이 true로 설정되면 포커스 시각 요소를 표시합니다.
      if ((VisualTreeHelper.GetParent(focusVisual) as Adorner)?.AdornedElement is FrameworkElement focusedElement)
      {
        // ShowFocusVisual 속성이 true인 경우, 포커스 시각 요소를 표시합니다.
        SetShowFocusVisual(focusedElement, true);

        // 포커스 시각 요소의 스타일을 설정합니다.
        if (focusedElement is Control focusedControl &&
            (!GetUseSystemFocusVisuals(focusedControl) || GetTemplateFocusTarget(focusedControl) != null))
        {
          // UseSystemFocusVisuals가 false이거나, 템플릿 포커스 대상이 null이 아닌 경우
          focusVisual.Template = null;
        }
        else
        {
          // UseSystemFocusVisuals가 true이거나, 템플릿 포커스 대상이 null인 경우
          TransferValue(focusedElement, focusVisual, FocusVisualPrimaryBrushProperty);

          // FocusVisualPrimaryThicknessProperty, FocusVisualSecondaryBrushProperty, FocusVisualSecondaryThicknessProperty 속성을 설정합니다.
          TransferValue(focusedElement, focusVisual, FocusVisualPrimaryThicknessProperty);

          // FocusVisualSecondaryBrushProperty, FocusVisualSecondaryThicknessProperty 속성을 설정합니다.
          TransferValue(focusedElement, focusVisual, FocusVisualSecondaryBrushProperty);

          // FocusVisualSecondaryThicknessProperty 속성을 설정합니다.
          TransferValue(focusedElement, focusVisual, FocusVisualSecondaryThicknessProperty);

          // ControlHelper.CornerRadiusProperty 속성을 설정합니다.
          TransferValue(focusedElement, focusVisual, ControlHelper.CornerRadiusProperty);

          // FocusVisualMarginProperty 속성을 설정합니다.
          focusVisual.Margin = GetFocusVisualMargin(focusedElement);
        }
        // FocusedElementProperty 속성을 설정합니다.
        SetFocusedElement(focusVisual, focusedElement);
      }
    }
    else
    {
      // ShowFocusVisual 속성이 false로 설정되면 포커스 시각 요소를 숨깁니다.
      FrameworkElement focusedElement = GetFocusedElement(focusVisual);

      // ShowFocusVisual 속성이 false인 경우, 포커스 시각 요소를 숨깁니다.
      if (focusedElement != null)
      {
        // ShowFocusVisual 속성이 false인 경우, 포커스 시각 요소를 숨깁니다.
        focusedElement.ClearValue(ShowFocusVisualPropertyKey);

        // 포커스 시각 요소의 스타일을 초기화합니다.
        focusVisual.ClearValue(FocusVisualPrimaryBrushProperty);

        // FocusVisualPrimaryThicknessProperty, FocusVisualSecondaryBrushProperty, FocusVisualSecondaryThicknessProperty 속성을 초기화합니다.
        focusVisual.ClearValue(FocusVisualPrimaryThicknessProperty);

        // FocusVisualSecondaryBrushProperty, FocusVisualSecondaryThicknessProperty 속성을 초기화합니다.
        focusVisual.ClearValue(FocusVisualSecondaryBrushProperty);

        // FocusVisualSecondaryThicknessProperty 속성을 초기화합니다.
        focusVisual.ClearValue(FocusVisualSecondaryThicknessProperty);

        // FocusVisualMarginProperty 속성을 초기화합니다.
        focusVisual.ClearValue(ControlHelper.CornerRadiusProperty);

        // ControlHelper.CornerRadiusProperty 속성을 초기화합니다.
        focusVisual.ClearValue(FrameworkElement.MarginProperty);

        // FocusedElementProperty 속성을 초기화합니다.
        focusVisual.ClearValue(Control.TemplateProperty);

        focusVisual.ClearValue(TemplateFocusTargetProperty);
        focusVisual.ClearValue(FocusedElementProperty);
      }
    }
  }
  /// <summary>
  /// TransferValue 메서드는 종속성 속성의 값을 소스에서 대상 객체로 전송합니다.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="target"></param>
  /// <param name="dp"></param>
  private static void TransferValue(DependencyObject source, DependencyObject target, DependencyProperty dp)
  {
    // source가 null이거나 target이 null인 경우, 예외를 발생시킵니다.
    if (!Helper.HasDefaultValue(source, dp))
    {
      Debug.Assert(source != null, "Source 객체가 null입니다.");

      Debug.Assert(target != null, "Target 객체가 null입니다.");

      // source가 null이 아니고 target이 null이 아닌 경우, 종속성 속성의 값을 전송합니다.
      target.SetValue(dp, source.GetValue(dp));
    }
  }

  /// <summary>
  /// FocusVisualAdorner 클래스는 Control에 포커스 시각 요소를 추가하는 어도너입니다.
  /// </summary>
  private sealed class FocusVisualAdorner : Adorner
  {
    /// <summary>
    /// FocusVisualAdorner 생성자입니다.
    /// </summary>
    /// <param name="focusedElement"></param>
    /// <param name="adornedElement"></param>
    /// <param name="focusVisualStyle"></param>
    public FocusVisualAdorner(Control focusedElement, UIElement adornedElement, Style focusVisualStyle) : base(adornedElement)
    {
      // 생성자에서 focusedElement, adornedElement, focusVisualStyle가 null이 아닌지 확인합니다.
      Debug.Assert(focusedElement != null, "Focused Element null 되어서는 안됩니다");

      // adornedElement가 null이 아닌지 확인합니다.
      Debug.Assert(adornedElement != null, "Adorned Element null 되어서는 안됩니다");

      // focusVisualStyle가 null이 아닌지 확인합니다.
      Debug.Assert(focusVisualStyle != null, "FocusVisual null 되어서는 안됩니다");

      // 생성자에서 FocusedElement, IsClipEnabled, IsHitTestVisible, IsEnabled 속성을 설정합니다.
      FocusedElement = focusedElement;

      // FocusedElement 속성을 설정합니다.
      Control control = new Control();

      // ControlHelper.SetTemplateFocusTarget(control, focusedElement);
      SetFocusedElement(control, focusedElement);

      // UseSystemFocusVisualsProperty를 설정합니다.
      SetIsSystemFocusVisual(control, false);

      // ControlHelper.SetUseSystemFocusVisuals(control, false);
      control.Style = focusVisualStyle;

      // ControlHelper.SetUseSystemFocusVisuals(control, false);
      control.Margin = GetFocusVisualMargin(focusedElement);

      // ControlHelper.SetFocusVisualPrimaryThickness(control, GetFocusVisualPrimaryThickness(focusedElement));
      TransferValue(focusedElement, control, FocusVisualPrimaryBrushProperty);

      // FocusVisualPrimaryThicknessProperty, FocusVisualSecondaryBrushProperty, FocusVisualSecondaryThicknessProperty 속성을 설정합니다.
      TransferValue(focusedElement, control, FocusVisualPrimaryThicknessProperty);

      // FocusVisualSecondaryBrushProperty, FocusVisualSecondaryThicknessProperty 속성을 설정합니다.
      TransferValue(focusedElement, control, FocusVisualSecondaryBrushProperty);

      // ocusVisualSecondaryThicknessProperty 속성을 설정합니다.
      TransferValue(focusedElement, control, FocusVisualSecondaryThicknessProperty);

      // ControlHelper.SetCornerRadius(control, ControlHelper.GetCornerRadius(focusedElement));
      TransferValue(focusedElement, control, ControlHelper.CornerRadiusProperty);

      _adorderChild = control;

      IsClipEnabled = true;

      IsHitTestVisible = false;

      IsEnabled = false;
      // AdornedElement에 VisualChild를 추가합니다.
      AddVisualChild(_adorderChild);
    }

    /// <summary>
    /// FocusedElement 속성은 포커스가 있는 Control을 나타냅니다.
    /// </summary>
    public Control FocusedElement { get; }

    /// <summary>
    /// MeasureOverride 메서드는 어도너의 크기를 측정합니다.
    /// </summary>
    /// <param name="constraint"></param>
    /// <returns></returns>
    protected override Size MeasureOverride(Size constraint)
    {
      // AdornedElement의 RenderSize를 가져옵니다.
      Size desiredSize = AdornedElement.RenderSize;

      // AdornedElement의 크기를 측정합니다.
      ((UIElement)GetVisualChild(0)).Measure(desiredSize);

      return desiredSize;
    }

    /// <summary>
    /// ArrangeOverride 메서드는 어도너의 크기를 배치합니다.
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    protected override Size ArrangeOverride(Size size)
    {
      Size finalSize = base.ArrangeOverride(size);

      ((UIElement)GetVisualChild(0)).Arrange(new Rect(new Point(), finalSize));

      return finalSize;
    }

    /// <summary>
    /// VisualChildrenCount 속성은 어도너의 시각적 자식 요소의 수를 반환합니다.
    /// </summary>
    protected override int VisualChildrenCount
    {
      get
      {
        // 어도너의 시각적 자식 요소의 수를 반환합니다.
        return 1;
      }
    }

    /// <summary>
    /// GetVisualChild 메서드는 어도너의 시각적 자식 요소를 반환합니다.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    protected override Visual GetVisualChild(int index)
    {
      // 어도너의 시각적 자식 요소를 반환합니다.
      if (index == 0)
      {
        return _adorderChild;
      }
      else
      {
        // index가 0이 아닌 경우, ArgumentOutOfRangeException 예외를 발생시킵니다.
        throw new ArgumentOutOfRangeException("index");
      }
    }

    /// <summary>
    /// _adorderChild 필드는 어도너의 시각적 자식 요소를 나타냅니다.
    /// </summary>
    private UIElement _adorderChild;
  }

  /// <summary>
  /// FocusVisualAdornerCache 필드는 FocusVisualAdorner의 캐시를 나타냅니다.
  /// </summary>
  private static FocusVisualAdorner _focusVisualAdornerCache = null!;
}
