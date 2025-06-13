using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrTreeViewItem은 사용자 정의 속성(아이콘, 색상, CornerRadius 등)을 지원하는 TreeViewItem입니다.
/// MVVM 패턴에서 명령 바인딩, 아이콘 및 스타일 확장을 지원합니다.
/// </summary>
public class HeimdallrTreeViewItem : TreeViewItem
{
  #region 확장 속성 정의 (CornerRadius, ExpandIcon, CollapseIcon, IconWidth, IconHeight, Fill, PathIcon, Text, SelectedCommand)

  /// <summary>
  /// 트리뷰 아이템의 테두리 둥글기 정도를 지정하는 속성입니다.
  /// 예를 들어 "4,4,4,4"와 같이 지정하면 테두리가 둥글게 렌더링됩니다.
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
    DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius),
      typeof(HeimdallrTreeViewItem), new FrameworkPropertyMetadata());

  /// <summary>
  /// 아이템이 확장될 때 표시할 아이콘을 지정합니다.
  /// 예: 화살표 아래쪽(ChevronDown), + 기호(Plus) 등
  /// </summary>
  public PathIconType ExpandIcon
  {
    get => (PathIconType)GetValue(ExpandIconProperty);
    set => SetValue(ExpandIconProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty ExpandIconProperty =
      DependencyProperty.Register(nameof(ExpandIcon), typeof(PathIconType), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(PathIconType.None));

  /// <summary>
  /// 아이템이 축소될 때 표시할 아이콘을 지정합니다.
  /// 예: 화살표 위쪽(ChevronUp), - 기호(Minus) 등
  /// </summary>
  public PathIconType CollapseIcon
  {
    get => (PathIconType)GetValue(CollapseIconProperty);
    set => SetValue(CollapseIconProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty CollapseIconProperty =
      DependencyProperty.Register(nameof(CollapseIcon), typeof(PathIconType), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(PathIconType.None));

  /// <summary>
  /// 주 아이콘(PathIcon)의 너비 (픽셀 단위) 지정
  /// 기본값은 16픽셀
  /// </summary>
  public double IconWidth
  {
    get => (double)GetValue(IconWidthProperty);
    set => SetValue(IconWidthProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty IconWidthProperty =
    DependencyProperty.Register(nameof(IconWidth), typeof(double), typeof(HeimdallrTreeViewItem),
      new PropertyMetadata(16.0)); // 기본값 16

  /// <summary>
  /// 주 아이콘(PathIcon)의 높이 (픽셀 단위) 지정
  /// 기본값은 16픽셀
  /// </summary>
  public double IconHeight
  {
    get => (double)GetValue(IconHeightProperty);
    set => SetValue(IconHeightProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty IconHeightProperty =
    DependencyProperty.Register(nameof(IconHeight), typeof(double), typeof(HeimdallrTreeViewItem),
      new PropertyMetadata(16.0)); // 기본값 16

  /// <summary>
  /// PathIcon (주 아이콘) 의 색상 지정용 Brush
  /// 기본값은 회색(Gray)
  /// </summary>
  public Brush PathIconFill
  {
    get { return (Brush)GetValue(PathIconFillProperty); }
    set { SetValue(PathIconFillProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty PathIconFillProperty =
      DependencyProperty.Register(nameof(PathIconFill), typeof(Brush), typeof(HeimdallrTreeViewItem), new PropertyMetadata(Brushes.Gray));

  /// <summary>
  /// 확장 아이콘(ExpandIcon)의 색상 지정용 Brush
  /// 기본값은 밝은 파랑색(AliceBlue)
  /// </summary>
  public Brush ExpandIconFill
  {
    get { return (Brush)GetValue(ExpandIconFillProperty); }
    set { SetValue(ExpandIconFillProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty ExpandIconFillProperty =
      DependencyProperty.Register(nameof(ExpandIconFill), typeof(Brush), typeof(HeimdallrTreeViewItem), new PropertyMetadata(Brushes.AliceBlue));

  /// <summary>
  /// 축소 아이콘(CollapseIcon)의 색상 지정용 Brush
  /// 기본값은 검정색(Black)
  /// </summary>
  public Brush CollapseIconFill
  {
    get { return (Brush)GetValue(CollapseIconFillProperty); }
    set { SetValue(CollapseIconFillProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty CollapseIconFillProperty =
      DependencyProperty.Register(nameof(CollapseIconFill), typeof(Brush), typeof(HeimdallrTreeViewItem), new PropertyMetadata(Brushes.Black));

  /// <summary>
  /// 타이틀 아이콘으로 표시될 PathIcon 타입 지정 (예: Barcode, Setting 등)
  /// </summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(PathIconType.None));

  /// <summary>
  /// TreeViewItem에 표시할 텍스트 (타이틀 문자열)
  /// </summary>
  public string Text
  {
    get => (string)GetValue(TextProperty);
    set => SetValue(TextProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty TextProperty =
    DependencyProperty.Register(nameof(Text), typeof(string), typeof(HeimdallrTreeViewItem),
      new PropertyMetadata(string.Empty));

  /// <summary>
  /// TreeViewItem이 선택되었을 때 실행할 ICommand 속성
  /// MVVM 패턴에서 ViewModel의 명령과 바인딩하여 동작하도록 함
  /// </summary>
  public ICommand SelectedCommand
  {
    get => (ICommand)GetValue(SelectedCommandProperty);
    set => SetValue(SelectedCommandProperty, value);
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty SelectedCommandProperty =
      DependencyProperty.Register(nameof(SelectedCommand), typeof(ICommand), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(null));

  #endregion

  /// <summary>
  /// 기본 스타일 연결 (Themes/Generic.xaml에 정의된 스타일 적용)
  /// </summary>
  static HeimdallrTreeViewItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrTreeViewItem),
      new FrameworkPropertyMetadata(typeof(HeimdallrTreeViewItem)));
  }

  /// <summary>
  /// 하위 아이템 컨테이너도 HeimdallrTreeViewItem으로 생성하여
  /// 트리뷰 전체가 일관된 사용자 정의 아이템 타입을 사용하게 함
  /// </summary>
  /// <returns>새로운 HeimdallrTreeViewItem 인스턴스</returns>
  protected override DependencyObject GetContainerForItemOverride()
  {
    return new HeimdallrTreeViewItem();
  }

  /// <summary>
  /// TreeViewItem 선택 시 호출되는 이벤트 핸들러 오버라이드
  /// 기본 선택 동작 수행 후, SelectedCommand가 바인딩 되어 있으면 실행함
  /// </summary>
  /// <param name="e">이벤트 인자</param>
  protected override void OnSelected(RoutedEventArgs e)
  {
    // 기본 TreeViewItem의 선택 처리 수행
    base.OnSelected(e);

    // SelectedCommand가 null이 아니고, 실행 가능하면 DataContext를 인자로 실행
    if (SelectedCommand?.CanExecute(DataContext) == true)
    {
      SelectedCommand.Execute(DataContext);
    }
  }
}

