using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 완전 커스터마이징 가능한 Heimdallr 스타일의 ListView 컨트롤입니다.
/// </summary>
public class HeimdallrListView : ListView
{
  static HeimdallrListView()
  {
    // 기본 스타일 키를 HeimdallrListView 타입으로 설정하여 Themes/Generic.xaml에서 스타일을 찾도록 함
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrListView),
        new FrameworkPropertyMetadata(typeof(HeimdallrListView)));
  }

  #region 2. 컬럼 숨김 제어용 딕셔너리 (컬럼 이름 - Visible bool)

  /// <summary>
  /// 컬럼 이름별 가시성 상태를 저장하는 딕셔너리입니다.
  /// true이면 컬럼을 보여주고, false이면 숨깁니다.
  /// </summary>
  public Dictionary<string, bool> ColumnVisibility
  {
    get { return (Dictionary<string, bool>)GetValue(ColumnVisibilityProperty); }
    set { SetValue(ColumnVisibilityProperty, value); }
  }
  /// <summary>
  /// ColumnVisibility 의존성 속성입니다.
  /// </summary>
  public static readonly DependencyProperty ColumnVisibilityProperty =
      DependencyProperty.Register(nameof(ColumnVisibility), typeof(Dictionary<string, bool>),
          typeof(HeimdallrListView), new PropertyMetadata(new Dictionary<string, bool>(), OnColumnVisibilityChanged));

  /// <summary>
  /// ColumnVisibility가 변경되었을 때 호출됩니다.
  /// 각 컬럼 헤더 이름에 대응하는 Visibility를 설정합니다.
  /// </summary>
  private static void OnColumnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is HeimdallrListView lv && lv.View is GridView gv)
    {
      var dict = e.NewValue as Dictionary<string, bool>;
      if (dict == null) return;

      foreach (var col in gv.Columns)
      {
        if (col.Header is string header && dict.TryGetValue(header, out var isVisible))
        {
          if (isVisible)
          {
            // 숨김 해제 시, 기존 너비가 0이었으면 기본 너비 100으로 설정
            if (col.Width == 0)
              col.Width = 100;

            // Visibility 속성은 GridViewColumn에 없으므로 대신 Width 조절로 숨김 구현
          }
          else
          {
            // 컬럼 숨김을 Width 0으로 구현 (Visibility 속성 없음)
            col.Width = 0;
          }
        }
      }
    }
  }
  #endregion

  #region 3. 열 너비 저장/복원 지원용 컬렉션
  /// <summary>
  /// 컬럼별로 저장된 너비 정보를 저장하는 딕셔너리입니다.
  /// </summary>
  public Dictionary<string, double> ColumnWidths
  {
    get { return (Dictionary<string, double>)GetValue(ColumnWidthsProperty); }
    set { SetValue(ColumnWidthsProperty, value); }
  }
  /// <summary>
  /// ColumnWidths 의존성 속성입니다.
  /// </summary>
  public static readonly DependencyProperty ColumnWidthsProperty =
      DependencyProperty.Register(nameof(ColumnWidths), typeof(Dictionary<string, double>),
          typeof(HeimdallrListView), new PropertyMetadata(new Dictionary<string, double>()));

  /// <summary>
  /// 열 너비 변경을 감지하여 저장할 수 있도록 초기화 시 호출할 수 있는 메서드입니다.
  /// (실제 너비 변경 감지는 별도 이벤트 등에서 구현 필요)
  /// </summary>
  protected override void OnInitialized(EventArgs e)
  {
    base.OnInitialized(e);
    // 예: 여기서 마우스 업 이벤트 등으로 너비 저장 처리 구현 권장
  }

  /// <summary>
  /// 현재 GridView의 컬럼 너비 상태를 ColumnWidths 프로퍼티에 저장합니다.
  /// </summary>
  public void SaveColumnWidths()
  {
    if (View is GridView gv)
    {
      var dict = new Dictionary<string, double>();
      foreach (var col in gv.Columns)
      {
        if (col.Header is string header)
        {
          dict[header] = col.Width;
        }
      }
      ColumnWidths = dict;
      // 이후 저장된 딕셔너리를 파일이나 설정에 저장하는 로직 추가 가능
    }
  }

  /// <summary>
  /// ColumnWidths 프로퍼티에 저장된 너비 정보를 읽어와 컬럼 너비를 복원합니다.
  /// </summary>
  public void RestoreColumnWidths()
  {
    if (View is GridView gv)
    {
      foreach (var col in gv.Columns)
      {
        if (col.Header is string header && ColumnWidths.TryGetValue(header, out var width))
        {
          col.Width = width;
        }
      }
    }
  }
  #endregion

  #region 1. 컬럼 헤더 클릭 시 정렬 처리
  private Dictionary<string, ListSortDirection> _columnSortDirections = new();
  /// <summary>
  /// GridViewColumnHeader 클릭 시 해당 컬럼으로 정렬을 수행합니다.
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
  {
    base.OnPreviewMouseLeftButtonUp(e);

    if (e.OriginalSource is DependencyObject source)
    {
      var header = VisualUpwardSearch<GridViewColumnHeader>(source);
      if (header?.Column != null)
      {
        // DisplayMemberBinding 경로에서 실제 정렬 속성명 추출
        string? sortBy = null;
        if (header.Column.DisplayMemberBinding is Binding binding && !string.IsNullOrEmpty(binding.Path?.Path))
        {
          sortBy = binding.Path.Path;
        }

        if (string.IsNullOrEmpty(sortBy)) return;

        var collectionView = CollectionViewSource.GetDefaultView(ItemsSource);
        if (collectionView == null) return;

        // 이전에 저장된 정렬 상태 가져오기 (기본 Ascending)
        if (!_columnSortDirections.TryGetValue(sortBy, out var currentDirection))
          currentDirection = ListSortDirection.Ascending;

        // 토글 정렬 방향
        var newDirection = currentDirection == ListSortDirection.Ascending
            ? ListSortDirection.Descending
            : ListSortDirection.Ascending;

        // 현재 정렬 상태와 비교해서 변경 없으면 리턴 (불필요한 Refresh 방지)
        if (collectionView.SortDescriptions.Count > 0)
        {
          var currentSort = collectionView.SortDescriptions[0];
          if (currentSort.PropertyName == sortBy && currentSort.Direction == newDirection)
          {
            // 이미 같은 정렬 상태, 리턴
            return;
          }
        }

        // 정렬 상태 업데이트
        _columnSortDirections[sortBy] = newDirection;

        // 정렬 변경 적용
        collectionView.SortDescriptions.Clear();
        collectionView.SortDescriptions.Add(new SortDescription(sortBy, newDirection));
        collectionView.Refresh();
      }
    }
  }

  /// <summary>
  /// Visual Tree를 거슬러 올라가면서 특정 타입의 부모를 찾는 헬퍼 메서드입니다.
  /// </summary>
  private static T? VisualUpwardSearch<T>(DependencyObject source) where T : DependencyObject
  {
    while (source != null && !(source is T))
    {
      source = VisualTreeHelper.GetParent(source);
    }
    return source as T;
  }
  #endregion

  #region 6. RowDetails를 위한 의존성 속성 및 템플릿

  /// <summary>
  /// 행 상세 내용을 보여줄 DataTemplate을 바인딩할 수 있는 의존성 속성입니다.
  /// </summary>
  public DataTemplate? RowDetailsTemplate
  {
    get { return (DataTemplate?)GetValue(RowDetailsTemplateProperty); }
    set { SetValue(RowDetailsTemplateProperty, value); }
  }
  /// <summary>
  /// RowDetailsTemplate 의존성 속성입니다.
  /// </summary>
  public static readonly DependencyProperty RowDetailsTemplateProperty =
      DependencyProperty.Register(nameof(RowDetailsTemplate), typeof(DataTemplate), typeof(HeimdallrListView));
  #endregion

  #region 7. GetContainerForItemOverride 및 IsItemItsOwnContainerOverride (추가)
  /// <summary>
  /// 커스텀 ListViewItem 컨테이너로 HeimdallrListViewItem을 생성하도록 오버라이드.
  /// </summary>
  protected override DependencyObject GetContainerForItemOverride()
  {
    return new HeimdallrListViewItem();
  }

  /// <summary>
  /// 아이템이 자체 컨테이너인지 확인
  /// </summary>
  /// <param name="item"></param>
  /// <returns></returns>
  protected override bool IsItemItsOwnContainerOverride(object item)
  {
    return item is HeimdallrListViewItem;
  }
  #endregion

  #region 8. 선택 시 배경색 지정
  /// <summary>
  /// 
  /// </summary>
  /// <param name="element"></param>
  /// <param name="item"></param>
  protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
  {
    base.PrepareContainerForItemOverride(element, item);

    if (element is HeimdallrListViewItem lvi)
    {
      // ListView에 지정된 SelectedBackground를 자식 아이템에 전달
      lvi.SelectedBackground = this.SelectedBackground;
    }
  }
  /// <summary>
  /// 선택 시 배경색을 지정하는 의존성 속성입니다.
  /// </summary>
  public Brush SelectedBackground
  {
    get { return (Brush)GetValue(SelectedBackgroundProperty); }
    set { SetValue(SelectedBackgroundProperty, value); }
  }
  /// <summary>
  /// 기본색은 회색입니다.
  /// </summary>
  public static readonly DependencyProperty SelectedBackgroundProperty =
      DependencyProperty.Register(nameof(SelectedBackground), typeof(Brush), typeof(HeimdallrListView),
          new PropertyMetadata(Brushes.Gray));
  #endregion
}

