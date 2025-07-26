using System.Windows;
using System.Windows.Media;

namespace Heimdallr.ToolKit.Helpers;

/// <summary>
/// VisaulTreeHelper 메서드
/// </summary>
public static class VisualTreeHelperExtensions
{
  /// <summary>
  /// 지정한 요소의 시각적 트리에서 가장 가까운 부모 요소를 찾습니다.
  /// </summary>
  /// <typeparam name="T">찾고자 하는 부모 요소의 타입</typeparam>
  /// <param name="parent">시작 요소</param>
  /// <returns>찾은 부모 요소 또는 null</returns>
  /// <example>
  /// 예시 1: MyButton 컨트롤의 부모 중 StackPanel을 찾기
  /// <code>
  /// StackPanel? parentPanel = MyButton.FindParent&lt;StackPanel&gt;();
  /// </code>
  /// </example>
  public static T? FindChild<T>(this DependencyObject parent) where T : DependencyObject
  {
    if (parent == null) return null;

    int count = VisualTreeHelper.GetChildrenCount(parent);
    for (int i = 0; i < count; i++)
    {
      var child = VisualTreeHelper.GetChild(parent, i);

      if (child is T found)
        return found;

      var descendant = FindChild<T>(child);
      if (descendant != null)
        return descendant;
    }

    return null;
  }

  /// <summary>
  /// 지정된 요소의 시각적 트리에서 하위 요소를 검색합니다.
  /// </summary>
  /// <typeparam name="T">찾고자 하는 자식 요소의 타입</typeparam>
  /// <param name="parent">부모 요소</param>
  /// <param name="predicate">조건을 만족하는 요소 필터 (선택사항)</param>
  /// <returns>조건을 만족하는 자식 요소 또는 null</returns>
  /// <example>
  /// 예시 2: RootGrid 내에 존재하는 첫 번째 Button을 찾기
  /// <code>
  /// Button? foundButton = RootGrid.FindChild&lt;Button&gt;();
  /// </code>
  ///
  /// 예시 3: 특정 조건을 만족하는 Button 찾기 (예: Content가 "Click Me"인 경우)
  /// <code>
  /// Button? filteredButton = RootGrid.FindChild&lt;Button&gt;(btn =&gt; btn.Content?.ToString() == "Click Me");
  /// </code>
  /// </example>
  public static T? FindChild<T>(this DependencyObject parent, Func<T, bool> predicate) where T : DependencyObject
  {
    if (parent == null) return null;

    int count = VisualTreeHelper.GetChildrenCount(parent);
    for (int i = 0; i < count; i++)
    {
      var child = VisualTreeHelper.GetChild(parent, i);

      if (child is T t && predicate(t))
        return t;

      var result = FindChild<T>(child, predicate);
      if (result != null)
        return result;
    }

    return null;
  }

  /// <summary>
  /// 시각적 트리에서 지정한 타입의 부모 요소를 찾습니다.
  /// </summary>
  /// <typeparam name="T">찾고자 하는 부모 요소의 타입</typeparam>
  /// <param name="child">자식 요소</param>
  /// <returns>첫 번째로 발견된 부모 요소 또는 null</returns>
  public static T? FindParent<T>(this DependencyObject child) where T : DependencyObject
  {
    if (child == null) return null;

    DependencyObject? parent = VisualTreeHelper.GetParent(child);

    while (parent != null)
    {
      if (parent is T found)
        return found;

      parent = VisualTreeHelper.GetParent(parent);
    }

    return null;
  }
}

