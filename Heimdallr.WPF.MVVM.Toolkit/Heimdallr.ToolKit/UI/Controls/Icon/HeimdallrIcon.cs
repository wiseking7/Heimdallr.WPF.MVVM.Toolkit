using Heimdallr.ToolKit.Enums;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Heimdallr.ToolKit.UI.Controls;

public class HeimdallrIcon : ContentControl
{
  #region IconMode
  public IconMode Mode
  {
    get { return (IconMode)GetValue(ModeProperty); }
    set { SetValue(ModeProperty, value); }
  }
  public static readonly DependencyProperty ModeProperty =
      DependencyProperty.Register(nameof(Mode), typeof(IconMode), typeof(HeimdallrIcon),
        new PropertyMetadata(IconMode.None));
  #endregion

  #region IconType
  public IconType Icon
  {
    get { return (IconType)GetValue(IconProperty); }
    set { SetValue(IconProperty, value); }
  }

  public static readonly DependencyProperty IconProperty =
      DependencyProperty.Register(nameof(Icon), typeof(IconType),
        typeof(HeimdallrIcon), new PropertyMetadata(IconType.None, IconPropertyChanged));

  private static void IconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    HeimdallrIcon heimdallrIcon = (HeimdallrIcon)d;
    string geometryData = ToolKit.Resources.Geometies.GeometryConverter.GetData(heimdallrIcon.Icon.ToString());

    heimdallrIcon.Data = Geometry.Parse(geometryData);
    heimdallrIcon.Mode = IconMode.Icon;
  }
  #endregion

  #region Image
  public ImageType Image
  {
    get { return (ImageType)GetValue(ImageProperty); }
    set { SetValue(ImageProperty, value); }
  }

  public static readonly DependencyProperty ImageProperty =
      DependencyProperty.Register(nameof(Image), typeof(ImageType), typeof(HeimdallrIcon),
        new PropertyMetadata(ImageType.None, ImagePropertyChanged));

  private static void ImagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    HeimdallrIcon heimdallrIcon = (HeimdallrIcon)d;
    try
    {
      string base64 = ToolKit.Resources.Images.ImageConverter.GetData(heimdallrIcon.Image.ToString());

      byte[] binaryData = Convert.FromBase64String(base64);

      BitmapImage bitmapImage = new();
      bitmapImage.BeginInit();
      bitmapImage.StreamSource = new MemoryStream(binaryData);
      bitmapImage.EndInit();

      heimdallrIcon.Source = bitmapImage;
      heimdallrIcon.Mode = IconMode.Image;
    }
    catch (KeyNotFoundException ex)
    {
      // KeyNotFoundException 처리: 예외 로그 출력
      Debug.WriteLine($"이미지 항목을 찾을 수 없습니다: {ex.Message}");
      heimdallrIcon.Source = null!; // 예외 발생 시 이미지를 비워둠
    }
    catch (Exception ex)
    {
      // 다른 예외 처리
      Debug.WriteLine($"이미지 로딩 중 오류 발생: {ex.Message}");
    }
  }
  #endregion

  #region Fill
  public Brush Fill
  {
    get { return (Brush)GetValue(FillProperty); }
    set { SetValue(FillProperty, value); }
  }
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrIcon),
        new PropertyMetadata(Brushes.Silver));
  #endregion

  #region Data
  public Geometry Data
  {
    get { return (Geometry)GetValue(DataProperty); }
    set { SetValue(DataProperty, value); }
  }
  public static readonly DependencyProperty DataProperty =
      DependencyProperty.Register(nameof(Data), typeof(Geometry), typeof(HeimdallrIcon),
        new PropertyMetadata(null));
  #endregion

  #region Source
  public ImageSource Source
  {
    get { return (ImageSource)GetValue(SourceProperty); }
    set { SetValue(SourceProperty, value); }
  }
  public static readonly DependencyProperty SourceProperty =
      DependencyProperty.Register(nameof(Source), typeof(ImageSource), typeof(HeimdallrIcon),
        new PropertyMetadata(null));
  #endregion

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
  }

  static HeimdallrIcon()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrIcon),
      new FrameworkPropertyMetadata(typeof(HeimdallrIcon)));
  }
}
