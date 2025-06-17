using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// ListView 상속받아 컬럼, 헤더 클릭시 자동으로 컬렉션을 정렬하는 기능
/// </summary>
public class HeimdallrListView : HeimdallrListViewBase
{
  /// <summary>
  /// 그리드 항목의 모서리 반경을 설정하는 종속성 속성입니다.
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }
  /// <summary>
  /// 그리드 항목의 모서리 반경을 설정하는 종속성 속성입니다.
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
    DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(HeimdallrGridView),
      new PropertyMetadata(new CornerRadius(0)));

  static HeimdallrListView()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrListView),
      new FrameworkPropertyMetadata(typeof(HeimdallrListView)));
  }

  /// <summary>
  /// 오버라이드해서 아이템 컨테이너로 HeimdallrListViewItem을 반환하도록 구현
  /// </summary>
  /// <returns></returns>
  protected override DependencyObject GetContainerForItemOverride()
    => new HeimdallrListViewItem();

  /// <summary>
  /// 컨트롤이 초기화 될 때 호출됩니다.
  /// </summary>
  /// <param name="e"></param>
  protected override void OnInitialized(EventArgs e)
  {
    base.OnInitialized(e);

    // GridViewColumnHeader의 클릭 이벤트를 핸들링하도록 이벤트 핸들러를 등록합니다.
    // 이를 통해 컬럼 헤더 클릭 시 정렬 로직을 수행할 수 있습니다.
    AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(OnHeaderClick));
  }

  /// <summary>
  /// 컬럼 헤더 클릭 시 호출되는 이벤트 핸들러
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnHeaderClick(object sender, RoutedEventArgs e)
  {
    // 클릭한 원본 소스가 GridViewColumnHeader이고, 해당 컬럼이 존재하는지 확인
    if (e.OriginalSource is GridViewColumnHeader header && header.Column != null)
    {
      // 컬럼의 DisplayMemberBinding을 Binding 타입으로 가져옵니다.
      // 예: <GridViewColumn DisplayMemberBinding="{Binding Name}" ... />
      var binding = header.Column.DisplayMemberBinding as System.Windows.Data.Binding;

      // 정렬할 속성명 추출:
      // 바인딩이 있으면 바인딩된 경로(Path), 없으면 컬럼 헤더 텍스트 사용
      string? sortBy = binding?.Path.Path ?? header.Column.Header?.ToString();

      // 현재 ListView의 ItemsSource의 컬렉션 뷰를 가져옵니다.
      ICollectionView dataView = CollectionViewSource.GetDefaultView(ItemsSource);

      // 정렬 속성명이나 데이터 뷰가 없으면 정렬 처리하지 않고 종료
      if (dataView == null || string.IsNullOrEmpty(sortBy))
      {
        return;
      }

      // 정렬 방향 기본값: 오름차순(Ascending)
      ListSortDirection direction = ListSortDirection.Ascending;

      // 현재 정렬 상태 확인:
      // 기존에 정렬 상태가 있고, 정렬 대상이 동일한 속성이면 정렬 방향 토글 (Ascending <-> Descending)
      if (dataView.SortDescriptions.Count > 0 && dataView.SortDescriptions[0].PropertyName == sortBy)
      {
        direction = dataView.SortDescriptions[0].Direction == ListSortDirection.Ascending
          ? ListSortDirection.Descending
          : ListSortDirection.Ascending;

        // 기존 정렬 조건 초기화
        dataView.SortDescriptions.Clear();
      }
      else
      {
        // 이전 정렬 조건이 다르면 초기화만 해줍니다.
        dataView.SortDescriptions.Clear();
      }

      // 새 정렬 조건을 추가해서 데이터 뷰를 정렬합니다.
      dataView.SortDescriptions.Add(new SortDescription(sortBy, direction));
    }
  }
}
