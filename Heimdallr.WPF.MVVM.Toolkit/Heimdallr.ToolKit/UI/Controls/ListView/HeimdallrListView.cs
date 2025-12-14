using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 완전 커스터마이징 가능한 Heimdallr 스타일의 ListView 컨트롤입니다.
/// 외형, 선택/마우스오버 색상, 컬럼 숨김/복원, 컬럼 너비 저장/복원, 정렬 기능 포함
/// </summary>
public class HeimdallrListView : ListView
{
  static HeimdallrListView()
  {
    // 기본 스타일 키를 HeimdallrListView 타입으로 설정
    // Themes/Generic.xaml에서 이 스타일을 찾습니다.
    DefaultStyleKeyProperty.OverrideMetadata(
        typeof(HeimdallrListView),
        new FrameworkPropertyMetadata(typeof(HeimdallrListView))
    );
  }

  #region 컬럼 숨김 기능 (ColumnVisibility)

  /// <summary>
  /// GridViewColumn에는 Visibility 속성이 없으므로 Width를 0으로 설정하여 숨깁니다. Width가 0이면 최소 기본값(100)으로 복원 가능
  /// true = 표시, false = 숨김
  /// </summary>
  public Dictionary<string, bool> ColumnVisibility
  {
    get => (Dictionary<string, bool>)GetValue(ColumnVisibilityProperty);
    set => SetValue(ColumnVisibilityProperty, value);
  }

  /// <summary>
  /// 종속성 등록: 컬럼 가시성 딕셔너리
  /// </summary>
  public static readonly DependencyProperty ColumnVisibilityProperty =
      DependencyProperty.Register(
          nameof(ColumnVisibility),
          typeof(Dictionary<string, bool>),
          typeof(HeimdallrListView),
          new PropertyMetadata(new Dictionary<string, bool>(), OnColumnVisibilityChanged)
      );

  /// <summary>
  /// 컬럼 가시성 변경 시 호출되는 콜백
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnColumnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is HeimdallrListView lv && lv.View is GridView gv)
    {
      if (e.NewValue is not Dictionary<string, bool> dict) return;

      foreach (var col in gv.Columns)
      {
        if (col.Header is string header && dict.TryGetValue(header, out var isVisible))
        {
          // GridViewColumn에는 Visibility 속성이 없으므로 Width 0으로 숨김
          col.Width = isVisible ? (col.Width == 0 ? 100 : col.Width) : 0;
        }
      }
    }
  }

  #endregion

  #region 컬럼 너비 저장/복원 (ColumnWidths)

  /// <summary>
  /// 컬럼별 저장된 너비 정보를 담는 딕셔너리
  /// </summary>
  public Dictionary<string, double> ColumnWidths
  {
    get => (Dictionary<string, double>)GetValue(ColumnWidthsProperty);
    set => SetValue(ColumnWidthsProperty, value);
  }

  /// <summary>
  /// 종속성 등록: 컬럼 너비 딕셔너리
  /// </summary>
  public static readonly DependencyProperty ColumnWidthsProperty =
      DependencyProperty.Register(
          nameof(ColumnWidths),
          typeof(Dictionary<string, double>),
          typeof(HeimdallrListView),
          new PropertyMetadata(new Dictionary<string, double>())
      );

  /// <summary>
  /// 현재 GridView의 컬럼 너비를 ColumnWidths에 저장
  /// </summary>
  public void SaveColumnWidths()
  {
    if (View is not GridView gv) return;

    var dict = new Dictionary<string, double>();
    foreach (var col in gv.Columns)
    {
      if (col.Header is string header)
        dict[header] = col.Width;
    }
    ColumnWidths = dict;
  }

  /// <summary>
  /// ColumnWidths에 저장된 컬럼 너비를 GridView에 적용
  /// </summary>
  public void RestoreColumnWidths()
  {
    if (View is not GridView gv) return;

    foreach (var col in gv.Columns)
    {
      if (col.Header is string header && ColumnWidths.TryGetValue(header, out var width))
      {
        col.Width = width;
      }
    }
  }

  #endregion

  #region 컬럼 클릭 정렬

  private readonly Dictionary<string, ListSortDirection> _columnSortDirections = new();

  /// <summary>
  /// GridViewColumnHeader 클릭 시 정렬 수행
  /// </summary>
  protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
  {
    base.OnPreviewMouseLeftButtonUp(e);

    if (e.OriginalSource is not DependencyObject source) return;

    var header = VisualUpwardSearch<GridViewColumnHeader>(source);
    if (header?.Column == null) return;

    // DisplayMemberBinding에서 실제 속성명 추출
    if (header.Column.DisplayMemberBinding is not Binding binding || string.IsNullOrEmpty(binding.Path?.Path))
      return;

    string sortBy = binding.Path.Path;
    var collectionView = CollectionViewSource.GetDefaultView(ItemsSource);
    if (collectionView == null) return;

    // 이전 정렬 상태 확인, 기본 Ascending
    _columnSortDirections.TryGetValue(sortBy, out var currentDirection);
    var newDirection = currentDirection == ListSortDirection.Ascending
        ? ListSortDirection.Descending
        : ListSortDirection.Ascending;

    _columnSortDirections[sortBy] = newDirection;

    // 정렬 적용
    collectionView.SortDescriptions.Clear();
    collectionView.SortDescriptions.Add(new SortDescription(sortBy, newDirection));
    collectionView.Refresh();
  }

  /// <summary>
  /// VisualTree를 따라 특정 타입의 부모를 찾는 헬퍼
  /// </summary>
  private static T? VisualUpwardSearch<T>(DependencyObject source) where T : DependencyObject
  {
    while (source != null && source is not T)
      source = VisualTreeHelper.GetParent(source);
    return source as T;
  }

  #endregion

  #region RowDetailsTemplate 지원
  /// <summary>
  /// 행 세부 정보 템플릿
  /// </summary>
  public DataTemplate? RowDetailsTemplate
  {
    get => (DataTemplate?)GetValue(RowDetailsTemplateProperty);
    set => SetValue(RowDetailsTemplateProperty, value);
  }

  /// <summary>
  /// 행 세부 정보 템플릿 종속성 속성
  /// </summary>
  public static readonly DependencyProperty RowDetailsTemplateProperty =
      DependencyProperty.Register(
          nameof(RowDetailsTemplate),
          typeof(DataTemplate),
          typeof(HeimdallrListView)
      );

  #endregion

  #region ListViewItem 연결 (HeimdallrListViewItem)
  /// <summary>
  /// ListViewItem 컨테이너 생성  
  /// </summary>
  /// <returns></returns>
  protected override DependencyObject GetContainerForItemOverride()
      => new HeimdallrListViewItem();

  /// <summary>
  /// ListViewItem 컨테이너 확인
  /// </summary>
  /// <param name="item"></param>
  /// <returns></returns>
  protected override bool IsItemItsOwnContainerOverride(object item)
      => item is HeimdallrListViewItem;

  /// <summary>
  /// ListViewItem이 생성될 때 호출됩니다. 여기서 선택/마우스오버 색상, Background/Foreground를 전달합니다.
  /// </summary>
  /// <param name="element"></param>
  /// <param name="item"></param>
  protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
  {
    base.PrepareContainerForItemOverride(element, item);

    if (element is HeimdallrListViewItem lvi)
    {
      lvi.SelectedBackground = this.SelectedBackground;
      lvi.MouseOverBackground = this.MouseOverBackground;
      lvi.Background ??= this.Background ?? Brushes.Transparent;
      lvi.Foreground ??= this.Foreground ?? Brushes.White;
    }
  }

  #endregion

  #region 선택/마우스오버 색상
  /// <summary>
  /// 마우스오버 및 선택 시 배경색을 지정하는 의존성 속성입니다.
  /// </summary>
  public Brush SelectedBackground
  {
    get => (Brush)GetValue(SelectedBackgroundProperty);
    set => SetValue(SelectedBackgroundProperty, value);
  }

  /// <summary>
  /// 종속성 속성: 선택 시 배경색 기본값은 회색입니다.
  /// </summary>
  public static readonly DependencyProperty SelectedBackgroundProperty =
      DependencyProperty.Register(
          nameof(SelectedBackground),
          typeof(Brush),
          typeof(HeimdallrListView),
          new PropertyMetadata(Brushes.Gray)
      );

  /// <summary>
  /// 마우스 오버 시 배경색을 지정하는 의존성 속성입니다.
  /// </summary>
  public Brush MouseOverBackground
  {
    get => (Brush)GetValue(MouseOverBackgroundProperty);
    set => SetValue(MouseOverBackgroundProperty, value);
  }

  /// <summary>
  /// 종속성 속성: 마우스 오버 시 배경색 기본값은 #334155 입니다.
  /// </summary>
  public static readonly DependencyProperty MouseOverBackgroundProperty =
      DependencyProperty.Register(
          nameof(MouseOverBackground),
          typeof(Brush),
          typeof(HeimdallrListView),
          new PropertyMetadata(new SolidColorBrush(Color.FromRgb(51, 65, 85))) // #334155
      );

  #endregion

  #region CornerRadius

  /// <summary>
  /// 코너라디우스
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }

  /// <summary>
  /// 종속성 주입(코너라디우스): 기본값 0
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(
          nameof(CornerRadius),
          typeof(CornerRadius),
          typeof(HeimdallrListView),
          new FrameworkPropertyMetadata(new CornerRadius(0))
      );

  #endregion

  /// <summary>
  /// 열 컬렉션에 대한 편리한 액세스
  /// </summary>
  public GridViewColumnCollection Columns
  {
    get
    {
      if (this.View is GridView gridView)
        return gridView.Columns;
      return null!;
    }
  }

  /// <summary>
  /// 생성자
  /// </summary>
  public HeimdallrListView()
  {
    this.Loaded += (s, e) =>
    {
      var headers = FindVisualChildren<GridViewColumnHeader>(this);
      foreach (var header in headers)
      {
        header.Loaded -= OnHeaderLoaded;
        header.Loaded += OnHeaderLoaded;
      }
    };
  }

  private void OnHeaderLoaded(object sender, RoutedEventArgs e)
  {
    if (sender is GridViewColumnHeader header)
    {
      if (header.Content is TextBlock tb)
      {
        Debug.WriteLine($"[{nameof(HeimdallrListView)}.{MethodBase.GetCurrentMethod()?.Name}] Header 로드 -> {tb.Text} | Foreground={tb.Foreground} | Background={tb.Background}");
      }
      else
      {
        Debug.WriteLine($"[{nameof(HeimdallrListView)}.{MethodBase.GetCurrentMethod()?.Name}] Header 로드 -> {header.Content} | Foreground={header.Foreground} | Background={header.Background}");
      }
    }
  }

  private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
  {
    if (depObj != null)
    {
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
}



