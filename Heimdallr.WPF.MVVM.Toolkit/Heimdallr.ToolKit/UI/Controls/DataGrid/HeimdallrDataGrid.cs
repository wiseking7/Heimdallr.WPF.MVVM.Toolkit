using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Custom DataGrid 클래스로, 기본적인 스타일과 데이터그리드의 외관을 커스터마이즈할 수 있도록 여러 속성을 제공합니다.
/// 기본 스타일을 설정하고, 헤더와 셀의 색상, 글자 크기, 선택 상태 등의 다양한 UI 요소를 설정할 수 있습니다.
/// </summary>
public class HeimdallrDataGrid : DataGrid
{
  /// <summary>
  /// 정적 생성자: DataGrid의 기본 스타일을 설정합니다.
  /// </summary>
  static HeimdallrDataGrid()
  {
    // HeimdallrDataGrid의 기본 스타일 키를 이 타입으로 설정
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrDataGrid),
      new FrameworkPropertyMetadata(typeof(HeimdallrDataGrid)));
  }

  // 컬럼 헤더 배경색
  /// <summary>
  /// 컬럼 헤더의 배경색을 지정하는 DependencyProperty입니다.
  /// 기본값은 투명 (Transparent)입니다.
  /// </summary>
  public static readonly DependencyProperty HeadColumnBackgroundProperty =
      DependencyProperty.Register(nameof(HeadColumnBackground), typeof(Brush), typeof(HeimdallrDataGrid),
          new PropertyMetadata(Brushes.Transparent));

  /// <summary>
  /// 컬럼 헤더의 배경색을 가져오거나 설정합니다.
  /// </summary>
  public Brush HeadColumnBackground
  {
    get => (Brush)GetValue(HeadColumnBackgroundProperty);
    set => SetValue(HeadColumnBackgroundProperty, value);
  }

  // 데이터그리드 배경색
  /// <summary>
  /// 데이터그리드의 배경색을 지정하는 DependencyProperty입니다.
  /// 기본값은 어두운 색상 (#FF1E293B)입니다.
  /// </summary>
  public static readonly DependencyProperty GridBackgroundProperty =
      DependencyProperty.Register(nameof(GridBackground), typeof(Brush), typeof(HeimdallrDataGrid),
          new PropertyMetadata(new SolidColorBrush(Color.FromRgb(30, 41, 59)))); // #FF1E293B

  /// <summary>
  /// 데이터그리드의 배경색을 가져오거나 설정합니다.
  /// </summary>
  public Brush GridBackground
  {
    get => (Brush)GetValue(GridBackgroundProperty);
    set => SetValue(GridBackgroundProperty, value);
  }

  // 가로 그리드라인 색상
  /// <summary>
  /// 데이터그리드의 가로 그리드 라인 색상을 지정하는 DependencyProperty입니다.
  /// 기본값은 #FF475569입니다.
  /// </summary>
  public static readonly DependencyProperty HorizontalGridLinesBrushCustomProperty =
      DependencyProperty.Register(nameof(HorizontalGridLinesBrushCustom), typeof(Brush), typeof(HeimdallrDataGrid),
          new PropertyMetadata(new SolidColorBrush(Color.FromRgb(71, 85, 105)))); // #FF475569

  /// <summary>
  /// 가로 그리드라인 색상을 가져오거나 설정합니다.
  /// </summary>
  public Brush HorizontalGridLinesBrushCustom
  {
    get => (Brush)GetValue(HorizontalGridLinesBrushCustomProperty);
    set => SetValue(HorizontalGridLinesBrushCustomProperty, value);
  }

  // 기본 글자 크기
  /// <summary>
  /// 데이터그리드의 기본 글자 크기를 지정하는 DependencyProperty입니다.
  /// 기본값은 14.0입니다.
  /// </summary>
  public static readonly DependencyProperty DataGridFontSizeProperty =
      DependencyProperty.Register(nameof(DataGridFontSize), typeof(double), typeof(HeimdallrDataGrid),
          new PropertyMetadata(14.0));

  /// <summary>
  /// 데이터그리드의 기본 글자 크기를 가져오거나 설정합니다.
  /// </summary>
  public double DataGridFontSize
  {
    get => (double)GetValue(DataGridFontSizeProperty);
    set => SetValue(DataGridFontSizeProperty, value);
  }

  // 컬럼 헤더 글자 크기
  /// <summary>
  /// 데이터그리드 컬럼 헤더의 글자 크기를 지정하는 DependencyProperty입니다.
  /// 기본값은 16.0입니다.
  /// </summary>
  public static readonly DependencyProperty HeaderFontSizeProperty =
      DependencyProperty.Register(nameof(HeaderFontSize), typeof(double), typeof(HeimdallrDataGrid),
          new PropertyMetadata(16.0));

  /// <summary>
  /// 데이터그리드 컬럼 헤더의 글자 크기를 가져오거나 설정합니다.
  /// </summary>
  public double HeaderFontSize
  {
    get => (double)GetValue(HeaderFontSizeProperty);
    set => SetValue(HeaderFontSizeProperty, value);
  }

  // 행 마우스 오버 배경색
  /// <summary>
  /// 행이 마우스 오버될 때의 배경색을 지정하는 DependencyProperty입니다.
  /// 기본값은 #FF2563EB입니다.
  /// </summary>
  public static readonly DependencyProperty RowMouseOverBackgroundProperty =
      DependencyProperty.Register(nameof(RowMouseOverBackground), typeof(Brush), typeof(HeimdallrDataGrid),
          new PropertyMetadata(new SolidColorBrush(Color.FromRgb(37, 99, 235)))); // #FF2563EB

  /// <summary>
  /// 행이 마우스 오버될 때의 배경색을 가져오거나 설정합니다.
  /// </summary>
  public Brush RowMouseOverBackground
  {
    get => (Brush)GetValue(RowMouseOverBackgroundProperty);
    set => SetValue(RowMouseOverBackgroundProperty, value);
  }

  // 행 선택 배경색
  /// <summary>
  /// 행이 선택되었을 때의 배경색을 지정하는 DependencyProperty입니다.
  /// 기본값은 #FF1D4ED8입니다.
  /// </summary>
  public static readonly DependencyProperty RowSelectedBackgroundProperty =
      DependencyProperty.Register(nameof(RowSelectedBackground), typeof(Brush), typeof(HeimdallrDataGrid),
          new PropertyMetadata(new SolidColorBrush(Color.FromRgb(29, 78, 216)))); // #FF1D4ED8

  /// <summary>
  /// 행이 선택되었을 때의 배경색을 가져오거나 설정합니다.
  /// </summary>
  public Brush RowSelectedBackground
  {
    get => (Brush)GetValue(RowSelectedBackgroundProperty);
    set => SetValue(RowSelectedBackgroundProperty, value);
  }

  // 선택된 셀 배경색
  /// <summary>
  /// 선택된 셀의 배경색을 지정하는 DependencyProperty입니다.
  /// 기본값은 #FF1D4ED8입니다.
  /// </summary>
  public static readonly DependencyProperty SelectedCellBackgroundProperty =
      DependencyProperty.Register(nameof(SelectedCellBackground), typeof(Brush), typeof(HeimdallrDataGrid),
          new PropertyMetadata(new SolidColorBrush(Color.FromRgb(29, 78, 216)))); // #FF1D4ED8

  /// <summary>
  /// 선택된 셀의 배경색을 가져오거나 설정합니다.
  /// </summary>
  public Brush SelectedCellBackground
  {
    get => (Brush)GetValue(SelectedCellBackgroundProperty);
    set => SetValue(SelectedCellBackgroundProperty, value);
  }


  /// <summary>
  /// 데이터그리드의 각 모서리의 반지름을 설정하는 DependencyProperty입니다.
  /// 기본값은 0으로 설정되어 있습니다.
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
      DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(HeimdallrDataGrid),
          new PropertyMetadata(new CornerRadius(0)));

  /// <summary>
  /// 데이터그리드의 각 모서리의 반지름을 가져오거나 설정합니다.
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }

  /// <summary>
  /// 생성자: 데이터그리드 초기화 및 이벤트 핸들러 연결
  /// </summary>
  public HeimdallrDataGrid()
  {
    // 행 로딩 시 헤더 번호 설정
    this.LoadingRow += HeimdallrDataGrid_LoadingRow;

    // 자동 생성된 컬럼에 스타일 적용
    this.AutoGeneratingColumn += HeimdallrDataGrid_AutoGeneratingColumn;

    // 데이터그리드가 로드될 때 인덱스 컬럼을 추가
    this.Loaded += HeimdallrDataGrid_Loaded;
  }

  /// <summary>
  /// 행이 로딩될 때, 각 행의 헤더를 행 번호로 설정하는 이벤트 핸들러
  /// </summary>
  private void HeimdallrDataGrid_LoadingRow(object? sender, DataGridRowEventArgs e)
  {
    e.Row.Header = (e.Row.GetIndex() + 1).ToString();
  }

  /// <summary>
  /// 데이터그리드가 로드되었을 때, 인덱스 컬럼을 강제로 추가하는 이벤트 핸들러
  /// </summary>
  private void HeimdallrDataGrid_Loaded(object sender, RoutedEventArgs e)
  {
    // 인덱스 컬럼이 없으면 강제로 추가
    if (!Columns.Any(c => c.Header?.ToString() == "#"))
    {
      var indexColumn = new DataGridTemplateColumn()
      {
        Header = "#",
        Width = new DataGridLength(1, DataGridLengthUnitType.Auto)
      };

      var template = new DataTemplate();

      // 텍스트블록 바인딩: 행 헤더 (즉, LoadingRow에서 설정한 번호)
      var factory = new FrameworkElementFactory(typeof(TextBlock));
      factory.SetBinding(TextBlock.TextProperty, new Binding
      {
        RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(DataGridRow), 1),
        Path = new PropertyPath("Header"),
        Mode = BindingMode.OneWay
      });

      factory.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);

      // 좌,우 여백 추가
      factory.SetValue(TextBlock.MarginProperty, new Thickness(5, 0, 20, 0));

      template.VisualTree = factory;

      indexColumn.CellTemplate = template;

      // 컬럼에 인덱스 컬럼 추가
      Columns.Insert(0, indexColumn);
    }
  }

  /// <summary>
  /// 데이터그리드의 컬럼이 자동으로 생성될 때 컬럼을 커스터마이즈하는 이벤트 핸들러
  /// </summary>
  private void HeimdallrDataGrid_AutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
  {
    // 필요하면 여기서 컬럼 자동생성 제어
    //if (e.PropertyName == "Id")  // Id 컬럼을 자동으로 생성하지 않음
    //{
    //  e.Cancel = true;
    //}

    // 텍스트 컬럼에 스타일 적용: 텍스트 중앙 정렬
    if (e.Column is DataGridTextColumn textColumn)
    {
      textColumn.ElementStyle = new Style(typeof(TextBlock))
      {
        Setters = { new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center) }
      };

      textColumn.CellStyle = (Style)FindResource("DataGridCellStyle"); // CellStyle 강제 적용
    }
  }
}
