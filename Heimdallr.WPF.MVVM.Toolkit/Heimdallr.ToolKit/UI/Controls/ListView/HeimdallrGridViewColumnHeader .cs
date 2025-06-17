using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrGridViewColumnHeader 클래스는 GridViewColumnHeader를 확장
/// </summary>
public class HeimdallrGridViewColumnHeader : GridViewColumnHeader
{
  static HeimdallrGridViewColumnHeader()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrGridViewColumnHeader),
        new FrameworkPropertyMetadata(typeof(HeimdallrGridViewColumnHeader)));
  }
  /// <summary>
  /// 
  /// </summary>
  public ICommand ClickCommand
  {
    get => (ICommand)GetValue(ClickCommandProperty);
    set => SetValue(ClickCommandProperty, value);
  }
  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty ClickCommandProperty =
      DependencyProperty.Register(nameof(ClickCommand), typeof(ICommand),
          typeof(HeimdallrGridViewColumnHeader), new PropertyMetadata(null));


  /// <summary>
  /// 정렬 방향을 저장할 AttachedProperty 선언
  /// </summary>
  public static readonly DependencyProperty SortDirectionProperty =
      DependencyProperty.RegisterAttached(
          "SortDirection",
          typeof(ListSortDirection?),
          typeof(HeimdallrGridViewColumnHeader),
          new PropertyMetadata(null));

  /// <summary>
  /// 
  /// </summary>
  /// <param name="element"></param>
  /// <param name="value"></param>
  public static void SetSortDirection(DependencyObject element, ListSortDirection? value)
      => element.SetValue(SortDirectionProperty, value);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public static ListSortDirection? GetSortDirection(DependencyObject element)
      => (ListSortDirection?)element.GetValue(SortDirectionProperty);

  /// <summary>
  /// 
  /// </summary>
  protected override void OnClick()
  {
    base.OnClick();

    // 정렬 속성명 구하기 (DisplayMemberBinding or Header 텍스트)
    string? sortBy = null;
    if (Column != null)
    {
      if (Column.DisplayMemberBinding is System.Windows.Data.Binding binding)
      {
        sortBy = binding.Path.Path;
      }
      else
      {
        sortBy = Column.Header?.ToString();
      }
    }

    if (string.IsNullOrEmpty(sortBy)) return;

    // ListView의 ItemsSource 컬렉션뷰 가져오기
    if (FindAncestor<ListView>(this) is ListView listView)
    {
      var dataView = CollectionViewSource.GetDefaultView(listView.ItemsSource);
      if (dataView == null) return;

      // 정렬 방향 토글
      var currentSort = GetSortDirection(this) ?? ListSortDirection.Descending;
      var newDirection = currentSort == ListSortDirection.Ascending
          ? ListSortDirection.Descending
          : ListSortDirection.Ascending;

      // 기존 헤더들의 SortDirection 초기화
      if (FindVisualChildren<HeimdallrGridViewColumnHeader>(listView) is IEnumerable<HeimdallrGridViewColumnHeader> headers)
      {
        foreach (var h in headers)
        {
          SetSortDirection(h, null);
        }
      }

      // 정렬 설정
      dataView.SortDescriptions.Clear();
      dataView.SortDescriptions.Add(new SortDescription(sortBy, newDirection));
      dataView.Refresh();

      // 클릭한 헤더에 정렬 방향 설정
      SetSortDirection(this, newDirection);
    }

    // MVVM 커맨드 실행
    if (ClickCommand?.CanExecute(DataContext) == true)
    {
      ClickCommand.Execute(DataContext);
    }
  }

  // Helper: 상위 ListView 찾기
  private static T? FindAncestor<T>(DependencyObject current) where T : DependencyObject
  {
    while (current != null)
    {
      if (current is T typed)
        return typed;

      current = VisualTreeHelper.GetParent(current);
    }
    return null;
  }

  // Helper: 자식 중 특정 타입 찾기 (Visual Tree)
  private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
  {
    if (depObj == null) yield break;

    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
    {
      var child = VisualTreeHelper.GetChild(depObj, i);
      if (child is T t)
        yield return t;

      foreach (var childOfChild in FindVisualChildren<T>(child))
        yield return childOfChild;
    }
  }
}


