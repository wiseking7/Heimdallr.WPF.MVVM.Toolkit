using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;
/// <summary>
/// ControlHelper 클래스는 WPF 컨트롤에 대한 다양한 유틸리티 메서드와 속성을 제공합니다.
/// </summary>
public static class ControlHelper
{
  #region CornerRadius
  /// <summary>
  /// Control의 모서리 반경을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <returns></returns>
  public static CornerRadius GetCornerRadius(Control control)
  {
    return (CornerRadius)control.GetValue(CornerRadiusProperty);
  }
  /// <summary>
  /// Control의 모서리 반경을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <param name="value"></param>
  public static void SetCornerRadius(Control control, CornerRadius value)
  {
    control.SetValue(CornerRadiusProperty, value);
  }
  /// <summary>
  /// Control의 모서리 반경을 설정하는 첨부 속성입니다.
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
    DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius),
      typeof(ControlHelper), null);
  #endregion

  #region Header
  /// <summary>
  /// Control의 헤더를 설정하는 첨부 속성입니다.
  /// </summary>
  public static readonly DependencyProperty HeaderProperty =
    DependencyProperty.RegisterAttached("Header", typeof(object), typeof(ControlHelper),
      new FrameworkPropertyMetadata(OnHeaderChanged));
  /// <summary>
  /// Control의 헤더를 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <returns></returns>
  public static object GetHeader(Control control)
  {
    // GetHeader 메서드는 Control의 헤더 속성 값을 반환합니다.
    return control.GetValue(HeaderProperty);
  }
  /// <summary>
  /// Control의 헤더를 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <param name="value"></param>
  public static void SetHeader(Control control, object value)
  {
    // SetHeader 메서드는 Control의 헤더 속성 값을 설정합니다.
    control.SetValue(HeaderProperty, value);
  }

  /// <summary>
  /// 헤더 속성이 변경될 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // 헤더 속성이 변경되면 UpdateHeaderVisibility 메서드를 호출하여 헤더의 가시성을 업데이트합니다.
    UpdateHeaderVisibility((Control)d);
  }
  #endregion

  #region HeaderTemplate
  /// <summary>
  /// Control의 헤더 템플릿을 설정하는 첨부 속성입니다.
  /// </summary>
  public static readonly DependencyProperty HeaderTemplateProperty =
    DependencyProperty.RegisterAttached("HeaderTemplate", typeof(DataTemplate),
      typeof(ControlHelper), new FrameworkPropertyMetadata(OnHeaderTemplateChanged));
  /// <summary>
  /// Control의 헤더 템플릿을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <returns></returns>
  public static DataTemplate GetHeaderTemplate(Control control)
  {
    return (DataTemplate)control.GetValue(HeaderTemplateProperty);
  }
  /// <summary>
  /// Control의 헤더 템플릿을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <param name="value"></param>
  public static void SetHeaderTemplate(Control control, DataTemplate value)
  {
    control.SetValue(HeaderTemplateProperty, value);
  }

  /// <summary>
  /// 헤더 템플릿 속성이 변경될 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnHeaderTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    UpdateHeaderVisibility((Control)d);
  }
  #endregion

  #region HeaderVisibility
  /// <summary>
  /// Control의 헤더 가시성을 설정하는 첨부 속성입니다.
  /// </summary>
  private static readonly DependencyPropertyKey HeaderVisibilityPropertyKey =
    DependencyProperty.RegisterAttachedReadOnly("HeaderVisibility", typeof(Visibility),
      typeof(ControlHelper), new FrameworkPropertyMetadata(Visibility.Collapsed));

  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty HeaderVisibilityProperty =
    HeaderVisibilityPropertyKey.DependencyProperty;

  /// <summary>
  /// Control의 헤더 가시성을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <returns></returns>
  public static Visibility GetHeaderVisibility(Control control)
  {
    return (Visibility)control.GetValue(HeaderVisibilityProperty);
  }

  /// <summary>
  /// Control의 헤더 가시성을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <param name="value"></param>
  private static void SetHeaderVisibility(Control control, Visibility value)
  {
    control.SetValue(HeaderVisibilityPropertyKey, value);
  }

  /// <summary>
  /// 헤더 가시성을 업데이트하는 메서드입니다.
  /// </summary>
  /// <param name="control"></param>
  private static void UpdateHeaderVisibility(Control control)
  {
    // 헤더 템플릿이 설정되어 있으면 가시성을 Visible로 설정하고,
    Visibility visibility;

    // 그렇지 않으면 헤더 텍스트가 비어 있지 않으면 Visible로, 비어 있으면 Collapsed로 설정합니다.
    if (GetHeaderTemplate(control) != null)
    {
      // 헤더 템플릿이 설정되어 있으면 가시성을 Visible로 설정합니다.
      visibility = Visibility.Visible;
    }
    else
    {
      // 헤더 템플릿이 설정되어 있지 않으면 헤더 텍스트가 비어 있지 않으면 Visible로, 비어 있으면 Collapsed로 설정합니다.
      visibility = IsNullOrEmptyString(GetHeader(control)) ? Visibility.Collapsed : Visibility.Visible;
    }

    // Control의 헤더 가시성을 업데이트합니다.
    SetHeaderVisibility(control, visibility);
  }
  #endregion

  #region PlaceholderText
  /// <summary>
  /// Control의 플레이스홀더 텍스트를 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <returns></returns>
  public static string GetPlaceholderText(Control control)
  {
    // GetPlaceholderText 메서드는 Control의 플레이스홀더 텍스트 속성 값을 반환합니다.
    return (string)control.GetValue(PlaceholderTextProperty);
  }

  /// <summary>
  /// Control의 플레이스홀더 텍스트를 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <param name="value"></param>
  public static void SetPlaceholderText(Control control, string value)
  {
    control.SetValue(PlaceholderTextProperty, value);
  }

  /// <summary>
  /// Control의 플레이스홀더 텍스트를 설정하는 첨부 속성입니다.
  /// </summary>
  public static readonly DependencyProperty PlaceholderTextProperty =
    DependencyProperty.RegisterAttached("PlaceholderText", typeof(string),
      typeof(ControlHelper), new FrameworkPropertyMetadata(string.Empty, OnPlaceholderTextChanged));

  /// <summary>
  /// 플레이스홀더 텍스트 속성이 변경될 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnPlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    // 플레이스홀더 텍스트가 변경되면 UpdatePlaceholderTextVisibility 메서드를 호출하여 가시성을 업데이트합니다.
    UpdatePlaceholderTextVisibility((Control)d);
  }
  #endregion

  #region PlaceholderTextVisibility

  /// <summary>
  /// Control의 플레이스홀더 텍스트 가시성을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <returns></returns>
  public static Visibility GetPlaceholderTextVisibility(Control control)
  {
    return (Visibility)control.GetValue(PlaceholderTextVisibilityProperty);
  }

  /// <summary>
  /// Control의 플레이스홀더 텍스트 가시성을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <param name="value"></param>
  private static void SetPlaceholderTextVisibility(Control control, Visibility value)
  {
    control.SetValue(PlaceholderTextVisibilityPropertyKey, value);
  }

  /// <summary>
  /// Control의 플레이스홀더 텍스트 가시성을 설정하는 첨부 속성입니다.
  /// </summary>
  private static readonly DependencyPropertyKey PlaceholderTextVisibilityPropertyKey =
    DependencyProperty.RegisterAttachedReadOnly("PlaceholderTextVisibility", typeof(Visibility),
      typeof(ControlHelper), new FrameworkPropertyMetadata(Visibility.Collapsed));

  /// <summary>
  /// Control의 플레이스홀더 텍스트 가시성을 설정하는 첨부 속성입니다.
  /// </summary>
  public static readonly DependencyProperty PlaceholderTextVisibilityProperty =
      PlaceholderTextVisibilityPropertyKey.DependencyProperty;

  /// <summary>
  /// Control의 플레이스홀더 텍스트 가시성을 업데이트하는 메서드입니다.
  /// </summary>
  /// <param name="control"></param>
  private static void UpdatePlaceholderTextVisibility(Control control)
  {
    SetPlaceholderTextVisibility(control, string.IsNullOrEmpty(GetPlaceholderText(control)) ? Visibility.Collapsed : Visibility.Visible);
  }

  #endregion

  #region PlaceholderForeground
  /// <summary>
  /// Control의 플레이스홀더 전경색을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <returns></returns>
  public static Brush GetPlaceholderForeground(Control control)
  {
    return (Brush)control.GetValue(PlaceholderForegroundProperty);
  }

  /// <summary>
  /// Control의 플레이스홀더 전경색을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <param name="value"></param>
  public static void SetPlaceholderForeground(Control control, Brush value)
  {
    control.SetValue(PlaceholderForegroundProperty, value);
  }

  /// <summary>
  /// Control의 플레이스홀더 전경색을 설정하는 첨부 속성입니다.
  /// </summary>
  public static readonly DependencyProperty PlaceholderForegroundProperty =
    DependencyProperty.RegisterAttached("PlaceholderForeground", typeof(Brush),
      typeof(ControlHelper), null);

  #endregion

  #region Description
  /// <summary>
  /// Control의 설명을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <returns></returns>
  public static object GetDescription(Control control)
  {
    return control.GetValue(DescriptionProperty);
  }

  /// <summary>
  /// Control의 설명을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <param name="value"></param>
  public static void SetDescription(Control control, object value)
  {
    control.SetValue(DescriptionProperty, value);
  }

  /// <summary>
  /// Control의 설명을 설정하는 첨부 속성입니다.
  /// </summary>
  public static readonly DependencyProperty DescriptionProperty =
    DependencyProperty.RegisterAttached("Description", typeof(object), typeof(ControlHelper),
      new FrameworkPropertyMetadata(OnDescriptionChanged));

  /// <summary>
  /// 설명 속성이 변경될 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnDescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    UpdateDescriptionVisibility((Control)d);
  }

  #endregion

  #region DescriptionVisibility
  /// <summary>
  /// Control의 설명 가시성을 설정하는 첨부 속성입니다.
  /// </summary>
  private static readonly DependencyPropertyKey DescriptionVisibilityPropertyKey =
    DependencyProperty.RegisterAttachedReadOnly("DescriptionVisibility", typeof(Visibility),
      typeof(ControlHelper), new FrameworkPropertyMetadata(Visibility.Collapsed));

  /// <summary>
  /// Control의 설명 가시성을 설정하는 첨부 속성입니다.
  /// </summary>
  public static readonly DependencyProperty DescriptionVisibilityProperty =
      DescriptionVisibilityPropertyKey.DependencyProperty;

  /// <summary>
  /// Control의 설명 가시성을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <returns></returns>
  public static Visibility GetDescriptionVisibility(Control control)
  {
    return (Visibility)control.GetValue(DescriptionVisibilityProperty);
  }

  /// <summary>
  /// Control의 설명 가시성을 설정하는 첨부 속성입니다.
  /// </summary>
  /// <param name="control"></param>
  /// <param name="value"></param>
  private static void SetDescriptionVisibility(Control control, Visibility value)
  {
    control.SetValue(DescriptionVisibilityPropertyKey, value);
  }

  /// <summary>
  /// Control의 설명 가시성을 업데이트하는 메서드입니다.
  /// </summary>
  /// <param name="control"></param>
  private static void UpdateDescriptionVisibility(Control control)
  {
    SetDescriptionVisibility(control, IsNullOrEmptyString(GetDescription(control)) ? Visibility.Collapsed : Visibility.Visible);
  }

  #endregion
  /// <summary>
  /// 객체가 null이거나 빈 문자열인지 확인하는 메서드입니다.
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public static bool IsNullOrEmptyString(object obj)
  {
    return obj == null || obj is string s && string.IsNullOrEmpty(s);
  }
}