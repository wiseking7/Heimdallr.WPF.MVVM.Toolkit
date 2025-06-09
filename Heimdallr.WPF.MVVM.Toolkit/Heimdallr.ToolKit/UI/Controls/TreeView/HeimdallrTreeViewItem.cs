using Heimdallr.ToolKit.Enums;
using Heimdallr.ToolKit.Models.TreeViewTest;
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
  #region CornerRadius

  public static readonly DependencyProperty CornerRadiusProperty =
    DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius),
      typeof(HeimdallrTreeViewItem),
 new FrameworkPropertyMetadata());

  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }

  public Geometry ExpandIcon
  {
    get => (Geometry)GetValue(ExpandIconProperty);
    set => SetValue(ExpandIconProperty, value);
  }
  public static readonly DependencyProperty ExpandIconProperty =
      DependencyProperty.Register(nameof(ExpandIcon), typeof(Geometry), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(null));

  public Geometry CollapseIcon
  {
    get => (Geometry)GetValue(CollapseIconProperty);
    set => SetValue(CollapseIconProperty, value);
  }
  public static readonly DependencyProperty CollapseIconProperty =
      DependencyProperty.Register(nameof(CollapseIcon), typeof(Geometry), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(null));


  public Geometry Data
  {
    get => (Geometry)GetValue(DataProperty);
    set => SetValue(DataProperty, value);
  }
  public static readonly DependencyProperty DataProperty =
      DependencyProperty.Register(nameof(Data), typeof(Geometry), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(null));

  public Brush Fill
  {
    get => (Brush)GetValue(FillProperty);
    set => SetValue(FillProperty, value);
  }
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(Brushes.Silver));

  public ImageSource Source
  {
    get => (ImageSource)GetValue(SourceProperty);
    set => SetValue(SourceProperty, value);
  }

  public static readonly DependencyProperty SourceProperty =
      DependencyProperty.Register(nameof(Source), typeof(ImageSource), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(null));

  public IconMode Mode
  {
    get { return (IconMode)GetValue(ModeProperty); }
    set { SetValue(ModeProperty, value); }
  }
  public static readonly DependencyProperty ModeProperty =
      DependencyProperty.Register(nameof(Mode), typeof(IconMode), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(IconMode.None));


  public IconType Icon
  {
    get { return (IconType)GetValue(IconProperty); }
    set { SetValue(IconProperty, value); }
  }
  public static readonly DependencyProperty IconProperty =
      DependencyProperty.Register(nameof(Icon), typeof(IconType), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(IconType.None, IconPropertyChanged));

  private static void IconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    HeimdallrTreeViewItem heimdallrTreeViewItemIcon = (HeimdallrTreeViewItem)d;

    string geometryData = ToolKit.Resources.Geometies.GeometryConverter.GetData(heimdallrTreeViewItemIcon.Icon.ToString());

    heimdallrTreeViewItemIcon.Data = Geometry.Parse(geometryData);
    heimdallrTreeViewItemIcon.Mode = IconMode.Icon;
  }

  public ImageType Image
  {
    get => (ImageType)GetValue(ImageProperty);
    set => SetValue(ImageProperty, value);
  }

  public static readonly DependencyProperty ImageProperty =
      DependencyProperty.Register(nameof(Image), typeof(ImageType), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(ImageType.None, ImagePropertyChanged));

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

  public ICommand SelectionCommand
  {
    get { return (ICommand)GetValue(SelectionCommandProperty); }
    set { SetValue(SelectionCommandProperty, value); }
  }

  public static readonly DependencyProperty SelectionCommandProperty =
      DependencyProperty.Register("SelectionCommand",
        typeof(ICommand), typeof(HeimdallrTreeViewItem),
        new PropertyMetadata(null));

  static HeimdallrTreeViewItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrTreeViewItem),
      new FrameworkPropertyMetadata(typeof(HeimdallrTreeViewItem)));
  }

  protected override DependencyObject GetContainerForItemOverride()
  {
    return new HeimdallrTreeViewItem();
  }

  public HeimdallrTreeViewItem()
  {
    MouseLeftButtonUp += HeimdallrTreeViewItem_MouseLeftButtonUp;
  }

  private void HeimdallrTreeViewItem_MouseLeftButtonUp(object sender,
    MouseButtonEventArgs e)
  {
    e.Handled = true;

    if (DataContext is FileItem items)
    {
      SelectionCommand?.Execute(items);
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
