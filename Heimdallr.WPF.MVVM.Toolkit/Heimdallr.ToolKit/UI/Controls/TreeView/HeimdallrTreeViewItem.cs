using Heimdallr.ToolKit.Enums;
using Heimdallr.ToolKit.Resources.PathDataStore;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Heimdallr.ToolKit.UI.Controls;

public class HeimdallrTreeViewItem : TreeViewItem
{
  #region 확장시 사용될 (CornerRadius, ExpandIcon, CollapseIcon, Data, Fill, Source, Mode

  public static readonly DependencyProperty CornerRadiusProperty =
    DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius),
      typeof(HeimdallrTreeViewItem),
 new FrameworkPropertyMetadata());

  // 아이콘 너비 조정
  public double IconWidth
  {
    get => (double)GetValue(IconWidthProperty);
    set => SetValue(IconWidthProperty, value);
  }

  public static readonly DependencyProperty IconWidthProperty =
      DependencyProperty.Register(nameof(IconWidth), typeof(double), typeof(HeimdallrTreeViewItem),
          new PropertyMetadata(16.0)); // 기본값 16

  // 아이콘 높이 조정
  public double IconHeight
  {
    get => (double)GetValue(IconHeightProperty);
    set => SetValue(IconHeightProperty, value);
  }

  public static readonly DependencyProperty IconHeightProperty =
      DependencyProperty.Register(nameof(IconHeight), typeof(double), typeof(HeimdallrTreeViewItem),
          new PropertyMetadata(16.0)); // 기본값 16

  // 선택시 명령
  public ICommand SelectionCommand
  {
    get { return (ICommand)GetValue(SelectionCommandProperty); }
    set { SetValue(SelectionCommandProperty, value); }
  }

  public static readonly DependencyProperty SelectionCommandProperty =
      DependencyProperty.Register("SelectionCommand",
        typeof(ICommand), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(null));


  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }

  // 확장 시 사용될 아이콘 Geometry 정의
  public Geometry ExpandIcon
  {
    get => (Geometry)GetValue(ExpandIconProperty);
    set => SetValue(ExpandIconProperty, value);
  }
  public static readonly DependencyProperty ExpandIconProperty =
      DependencyProperty.Register(nameof(ExpandIcon), typeof(Geometry), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(null));

  // 축소시 표기될 아이콘 
  public Geometry CollapseIcon
  {
    get => (Geometry)GetValue(CollapseIconProperty);
    set => SetValue(CollapseIconProperty, value);
  }
  public static readonly DependencyProperty CollapseIconProperty =
      DependencyProperty.Register(nameof(CollapseIcon), typeof(Geometry), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(null));

  // 실제 표시될 Path Data (Icon 으로 렌더링 됨)
  public Geometry Data
  {
    get => (Geometry)GetValue(DataProperty);
    set => SetValue(DataProperty, value);
  }
  public static readonly DependencyProperty DataProperty =
      DependencyProperty.Register(nameof(Data), typeof(Geometry), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(null));

  // 아이콘 색상
  public Brush Fill
  {
    get => (Brush)GetValue(FillProperty);
    set => SetValue(FillProperty, value);
  }
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(Brushes.Silver));

  // 이미지 아이콘으로 사용할 경우의 소스 경로
  public ImageSource Source
  {
    get => (ImageSource)GetValue(SourceProperty);
    set => SetValue(SourceProperty, value);
  }

  public static readonly DependencyProperty SourceProperty =
      DependencyProperty.Register(nameof(Source), typeof(ImageSource), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(null));

  // 아이콘이 어떤 모드로 동작하는지 (PathIcon, Image, Icon)
  public IconMode Mode
  {
    get { return (IconMode)GetValue(ModeProperty); }
    set { SetValue(ModeProperty, value); }
  }
  public static readonly DependencyProperty ModeProperty =
      DependencyProperty.Register(nameof(Mode), typeof(IconMode), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(IconMode.None));

  // 정해진 enum에 따라 Path 형태 아이콘을 지정
  public IconType Icon
  {
    get { return (IconType)GetValue(IconProperty); }
    set { SetValue(IconProperty, value); }
  }
  public static readonly DependencyProperty IconProperty =
      DependencyProperty.Register(nameof(Icon), typeof(IconType), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(IconType.None, IconPropertyChanged));

  //  // Icon이 변경되면 Geometry 값을 파싱하여 설정
  private static void IconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    HeimdallrTreeViewItem heimdallrTreeViewItemIcon = (HeimdallrTreeViewItem)d;

    string geometryData = ToolKit.Resources.Geometies.GeometryConverter.GetData(heimdallrTreeViewItemIcon.Icon.ToString());

    heimdallrTreeViewItemIcon.Data = Geometry.Parse(geometryData);
    heimdallrTreeViewItemIcon.Mode = IconMode.Icon;
  }

  // 이미지 아이콘 사용 시 base64 이미지를 로드하여 설정
  public ImageType Image
  {
    get => (ImageType)GetValue(ImageProperty);
    set => SetValue(ImageProperty, value);
  }

  public static readonly DependencyProperty ImageProperty =
      DependencyProperty.Register(nameof(Image), typeof(ImageType), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(ImageType.None, ImagePropertyChanged));

  // 변경되면 BitmapImage 값을 파싱하여 설정
  private static void ImagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    HeimdallrTreeViewItem heimdallrTreeViewItemImage = (HeimdallrTreeViewItem)d;

    try
    {
      string base64 = ToolKit.Resources.Images.ImageConverter.GetData(heimdallrTreeViewItemImage.Image.ToString());

      byte[] binaryData = Convert.FromBase64String(base64);

      BitmapImage bitmapImage = new BitmapImage();

      bitmapImage.BeginInit();
      bitmapImage.StreamSource = new MemoryStream(binaryData);
      bitmapImage.EndInit();

      heimdallrTreeViewItemImage.Source = bitmapImage;
      heimdallrTreeViewItemImage.Mode = IconMode.Image;
    }
    catch (KeyNotFoundException ex)
    {
      // KeyNotFoundException 처리 : 예외 로그 처리
      Debug.WriteLine($"HeimdallrTreeViewItem 의 Image 항목을 찾을 수 없습니다: {ex.Message}");

      // 예외 발생시 이미지를 비워둡니다.
      heimdallrTreeViewItemImage.Source = null!;
    }
    catch (Exception ex)
    {
      // 다른 예외처리
      Debug.WriteLine($"HeimdallrTreeViewItem 이지지 로딩중 오류 발생: {ex.Message}");
    }
  }

  // Path 아이콘을 사용하며 PathGeometry로 변환하여 설정
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }

  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(PathIconType.None, PathIconPpropertyChanged));

  private static void PathIconPpropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is not HeimdallrTreeViewItem heimdallrTreeViewItemPathIcon)
    {
      return;
    }

    var geometry = PathGeometryStore.GetGeometry(heimdallrTreeViewItemPathIcon.PathIcon);

    if (geometry != null)
    {
      heimdallrTreeViewItemPathIcon.Data = geometry;
      heimdallrTreeViewItemPathIcon.Mode = IconMode.PathIcon;
      Debug.WriteLine($"[HeimdallrTreeViewItem] '{heimdallrTreeViewItemPathIcon.PathIcon}' -> Geometry 적용");
    }
    else
    {
      Debug.WriteLine($"[HeimdallrTreeViewItem] '{heimdallrTreeViewItemPathIcon.PathIcon}' -> Geometry 없음");
    }
  }

  #endregion

  static HeimdallrTreeViewItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrTreeViewItem),
      new FrameworkPropertyMetadata(typeof(HeimdallrTreeViewItem)));
  }

  protected override DependencyObject GetContainerForItemOverride()
  {
    return new HeimdallrTreeViewItem();
  }

  private PathIconType _originalPathIcon;
  public HeimdallrTreeViewItem()
  {
    Loaded += HeimdallrTreeViewItem_Loaded;
    MouseDoubleClick += HeimdallrTreeViewItem_MouseDoubleClick;
    Expanded += HeimdallrTreeViewItem_Expanded;
    Collapsed += HeimdallrTreeViewItem_Collapsed;
  }

  // 초기 PathIcon 저장
  private void HeimdallrTreeViewItem_Loaded(object sender, RoutedEventArgs e)
  {
    _originalPathIcon = PathIcon;
  }

  // 확장 시: 원래 PathIcon이 DownEllipse일 때만 UpEllipse로 변경
  private void HeimdallrTreeViewItem_Expanded(object sender, RoutedEventArgs e)
  {
    if (_originalPathIcon == PathIconType.ChevronDownEllipse)
      this.PathIcon = PathIconType.ChevronUpEllipse;

    e.Handled = false;
  }

  // 축소 시: 원래 PathIcon이 DownEllipse일 때만 다시 Down으로 복원
  private void HeimdallrTreeViewItem_Collapsed(object sender, RoutedEventArgs e)
  {
    if (_originalPathIcon == PathIconType.ChevronDownEllipse)
      this.PathIcon = PathIconType.ChevronDownEllipse;

    e.Handled = false;
  }

  private void HeimdallrTreeViewItem_MouseDoubleClick(object sender,
    MouseButtonEventArgs e)
  {
    // FileItem 클래스가 있을 경우
    //if (DataContext is FileItem items)
    //{
    //  // FileItem이면 사용자 정의 명령 실행
    //  e.Handled = true;
    //  SelectionCommand?.Execute(items);
    //}
    //else
    //{
    //  // FileItem이 아니면 확장/축소를 수동으로 토글
    //  if (sender is HeimdallrTreeViewItem item)
    //  {
    //    item.IsExpanded = !item.IsExpanded; // 확장 상태 반전
    //    e.Handled = true; // 기본 이벤트는 처리 완료 (트리거 중복 방지)
    //  }
    //}

    // 없을 경우
    if (sender is HeimdallrTreeViewItem item)
    {
      // 현재 상태 반전 (열기/닫기)
      item.IsExpanded = !item.IsExpanded;

      // 이벤트 소비
      e.Handled = true;
    }
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    // 데이터가 제대로 바인딩되었는지 확인
    if (Data != null)
    {
      Debug.WriteLine($"HeimdallrTreeViewItem 의 Data 의 속성 설정입니다");
    }
    else
    {
      Debug.WriteLine($"HeimdallrTreeViewItem 의 Data 가 null 아닙니다");
    }
  }
}
