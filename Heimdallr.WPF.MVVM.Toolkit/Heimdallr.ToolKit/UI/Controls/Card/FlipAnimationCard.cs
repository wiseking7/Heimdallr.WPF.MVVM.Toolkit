using Heimdallr.ToolKit.Enums;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// CardView: 카드 형태의 사용자 정의 컨트롤
/// 이 컨트롤은 제목, 설명, 이미지 속성을 갖고 있으며,
/// 클릭 이벤트를 처리할 수 있습니다.
/// </summary>
public class FlipAnimationCard : ContentControl
{
  // 정적 생성자
  static FlipAnimationCard()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(FlipAnimationCard), new FrameworkPropertyMetadata(typeof(FlipAnimationCard)));
  }

  #region Title, Description
  /// <summary>
  /// 카드제목 Depnendency Property
  /// </summary>
  public static readonly DependencyProperty CardTitleProperty =
      DependencyProperty.Register("CardTitle", typeof(string), typeof(FlipAnimationCard), new PropertyMetadata("Card 1"));

  /// <summary>
  /// 카드제목 프로퍼티
  /// </summary>
  public string CardTitle
  {
    get => (string)GetValue(CardTitleProperty);
    set => SetValue(CardTitleProperty, value);
  }

  /// <summary>
  /// 카드설명 Dependency Property
  /// </summary>
  public static readonly DependencyProperty CardDescriptionProperty =
      DependencyProperty.Register("CardDescription", typeof(string), typeof(FlipAnimationCard), new PropertyMetadata("Default description."));

  /// <summary>
  /// 카드설명 프로퍼티 
  /// </summary>
  public string CardDescription
  {
    get => (string)GetValue(CardDescriptionProperty);
    set => SetValue(CardDescriptionProperty, value);
  }
  #endregion

  /// <summary>
  /// 생성자
  /// </summary>
  public FlipAnimationCard()
  {
  }

  #region Card width, height
  /// <summary>
  /// 카드 너비 설정
  /// </summary>
  public double CardWidth
  {
    get => (double)GetValue(CardWidthProperty);
    set => SetValue(CardWidthProperty, value);
  }
  /// <summary>
  /// 카드너비 속성
  /// </summary>
  public static readonly DependencyProperty CardWidthProperty =
      DependencyProperty.Register(nameof(CardWidth), typeof(double), typeof(FlipAnimationCard),
          new PropertyMetadata(200.0));

  /// <summary>
  /// 카드의 높이 설정
  /// </summary>
  public double CardHeight
  {
    get => (double)GetValue(CardHeightProperty);
    set => SetValue(CardHeightProperty, value);
  }
  /// <summary>
  /// 카드의 높이 속성
  /// </summary>
  public static readonly DependencyProperty CardHeightProperty =
      DependencyProperty.Register(nameof(CardHeight), typeof(double), typeof(FlipAnimationCard),
          new PropertyMetadata(200.0));
  #endregion

  #region Card BorderThickness, BorderBrush
  /// <summary>
  /// BorderThickness 설정
  /// </summary>
  public Thickness CardBorderThickness
  {
    get => (Thickness)GetValue(CardBorderThicknessProperty);
    set => SetValue(CardBorderThicknessProperty, value);
  }
  /// <summary>
  /// BorderThickness 두께 속성
  /// </summary>
  public static readonly DependencyProperty CardBorderThicknessProperty =
      DependencyProperty.Register(nameof(CardBorderThickness), typeof(Thickness), typeof(FlipAnimationCard),
          new PropertyMetadata(new Thickness(1)));

  /// <summary>
  /// BorderBrush 설정
  /// </summary>
  public Brush CardBorderBrush
  {
    get => (Brush)GetValue(CardBorderBrushProperty);
    set => SetValue(CardBorderBrushProperty, value);
  }
  /// <summary>
  /// BorderBrush 색상 속성
  /// </summary>
  public static readonly DependencyProperty CardBorderBrushProperty =
      DependencyProperty.Register(nameof(CardBorderBrush), typeof(Brush), typeof(FlipAnimationCard),
          new PropertyMetadata(Brushes.Gray));
  #endregion

  #region Card : front/back 개별 설정
  /// <summary>
  /// 앞면 모서리 둥글기 설정
  /// </summary>
  public CornerRadius FrontCornerRadius
  {
    get => (CornerRadius)GetValue(FrontCornerRadiusProperty);
    set => SetValue(FrontCornerRadiusProperty, value);
  }
  /// <summary>
  /// 앞면 모서리 둥글기 속성
  /// </summary>
  public static readonly DependencyProperty FrontCornerRadiusProperty =
      DependencyProperty.Register(nameof(FrontCornerRadius), typeof(CornerRadius), typeof(FlipAnimationCard),
          new PropertyMetadata(new CornerRadius(10)));

  /// <summary>
  /// 뒷면 모서리 둥글기 설정
  /// </summary>
  public CornerRadius BackCornerRadius
  {
    get => (CornerRadius)GetValue(BackCornerRadiusProperty);
    set => SetValue(BackCornerRadiusProperty, value);
  }

  /// <summary>
  /// 뒷면 모서리 둥글기 속성
  /// </summary>
  public static readonly DependencyProperty BackCornerRadiusProperty =
      DependencyProperty.Register(nameof(BackCornerRadius), typeof(CornerRadius), typeof(FlipAnimationCard),
          new PropertyMetadata(new CornerRadius(10)));
  #endregion

  #region Card : Front/Back 배경색 설정
  /// <summary>
  /// 앞면 배경색 설정
  /// </summary>
  public Brush FrontBackground
  {
    get => (Brush)GetValue(FrontBackgroundProperty);
    set => SetValue(FrontBackgroundProperty, value);
  }
  /// <summary>
  /// 앞면 배경색 속성
  /// </summary>
  public static readonly DependencyProperty FrontBackgroundProperty =
      DependencyProperty.Register(nameof(FrontBackground), typeof(Brush), typeof(FlipAnimationCard),
          new PropertyMetadata(Brushes.White));

  /// <summary>
  /// 뒷면 배경색 설정
  /// </summary>
  public Brush BackBackground
  {
    get => (Brush)GetValue(BackBackgroundProperty);
    set => SetValue(BackBackgroundProperty, value);
  }
  /// <summary>
  /// 뒷면 배경색 속성
  /// </summary>
  public static readonly DependencyProperty BackBackgroundProperty =
      DependencyProperty.Register(nameof(BackBackground), typeof(Brush), typeof(FlipAnimationCard),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#21201E"))));
  #endregion

  #region BackContent
  /// <summary>
  /// 뒷면 컨텐츠
  /// </summary>
  public object BackContent
  {
    get => GetValue(BackContentProperty);
    set => SetValue(BackContentProperty, value);
  }

  /// <summary>
  /// 뒷면 컨텐츠 DependencyProperty
  /// </summary>
  public static readonly DependencyProperty BackContentProperty =
      DependencyProperty.Register(nameof(BackContent), typeof(object), typeof(FlipAnimationCard), new PropertyMetadata(null));
  #endregion

  #region IconMode
  /// <summary>
  /// 아이콘 모드 (Icon, Image, PathIcon 중 현재 활성화된 모드를 나타냄)
  /// 주로 내부에서 설정되며, UI 템플릿에서 어떤 모드로 렌더링할지 결정할 때 사용
  /// </summary>
  public IconMode FrontMode
  {
    get
    {
      // 우선순위 PathIcon > Image > Icon
      if (PathIcon != PathIconType.None) return IconMode.PathIcon;
      if (Image != ImageType.None) return IconMode.Image;
      if (Icon != IconType.None) return IconMode.Icon;
      return IconMode.None;
    }
  }
  #endregion

  #region IconSize
  /// <summary>
  /// 이이콘 사이즈 너비,높이
  /// </summary>
  public double IconSize
  {
    get => (double)GetValue(IconSizeProperty);
    set => SetValue(IconSizeProperty, value);
  }

  /// <summary>
  /// 아이콘사이즈 기본값
  /// </summary>
  public static readonly DependencyProperty IconSizeProperty =
      DependencyProperty.Register(nameof(IconSize), typeof(double),
          typeof(FlipAnimationCard), new PropertyMetadata(25.0));
  #endregion

  #region PathIcon
  /// <summary>
  /// 아이콘 지정
  /// </summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set
    {
      SetValue(PathIconProperty, value);
      if (value != PathIconType.None)
      {
        Image = ImageType.None;
        Icon = IconType.None;
      }
    }
  }

  /// <summary>
  /// 아이콘 속성
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(FlipAnimationCard),
          new PropertyMetadata(PathIconType.None));

  /// <summary>
  /// 아이콘 색상지정
  /// </summary>
  public Brush Fill
  {
    get => (Brush)GetValue(FillProperty);
    set => SetValue(FillProperty, value);
  }

  /// <summary>
  /// 아이콘 색상지정 속성
  /// </summary>
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(FlipAnimationCard),
          new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AAAAAA"))));
  #endregion

  #region Image
  /// <summary>
  /// ImageType Enum 값으로 지정된 아이콘 이미지를 표시 (Base64 인코딩된 이미지 사용)
  /// </summary>
  public ImageType Image
  {
    get { return (ImageType)GetValue(ImageProperty); }
    set
    {
      SetValue(ImageProperty, value);
      if (value != ImageType.None && PathIcon == PathIconType.None)
      {
        Icon = IconType.None;
      }
    }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(nameof(Image), typeof(ImageType),
        typeof(FlipAnimationCard), new PropertyMetadata(ImageType.None, ImagePropertyChanged));

  /// <summary>
  /// Image 속성이 변경될 때 호출되는 콜백
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void ImagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    FlipAnimationCard FlipAnimationCardImage = (FlipAnimationCard)d;

    try
    {
      // 이미지 이름에 해당하는 Base64 문자열 가져오기
      string base64 = ToolKit.Resources.ImageConverter.GetData(FlipAnimationCardImage.Image.ToString());

      // Base64 → byte[]
      byte[] binaryData = Convert.FromBase64String(base64);

      // byte[] → BitmapImage 생성
      BitmapImage bitmapImage = new();
      bitmapImage.BeginInit();
      bitmapImage.StreamSource = new MemoryStream(binaryData);
      bitmapImage.EndInit();

      // Source 속성에 할당
      FlipAnimationCardImage.Source = bitmapImage;
    }
    catch (KeyNotFoundException ex)
    {
      Debug.WriteLine($"이미지 항목을 찾을 수 없습니다: {ex.Message}");
      FlipAnimationCardImage.Source = null!;
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"이미지 로딩 중 오류 발생: {ex.Message}");
    }
  }
  #endregion

  #region ImageData, Source
  /// <summary>
  /// 현재 표시되는 Geometry 데이터
  /// </summary>
  public Geometry Data
  {
    get { return (Geometry)GetValue(DataProperty); }
    set { SetValue(DataProperty, value); }
  }
  /// <summary>
  /// 종속성 주입
  /// </summary>
  public static readonly DependencyProperty DataProperty =
      DependencyProperty.Register(nameof(Data), typeof(Geometry),
        typeof(FlipAnimationCard),
        new PropertyMetadata(null));

  /// <summary>
  /// 현재 표시되는 이미지 데이터 (BitmapImage 등)
  /// </summary>
  public ImageSource Source
  {
    get { return (ImageSource)GetValue(SourceProperty); }
    set { SetValue(SourceProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty SourceProperty =
      DependencyProperty.Register(nameof(Source), typeof(ImageSource),
        typeof(FlipAnimationCard),
        new PropertyMetadata(null));
  #endregion

  #region Icon
  /// <summary>
  /// IconType 속성 추가
  /// </summary>
  public IconType Icon
  {
    get => (IconType)GetValue(IconProperty);
    set
    {
      SetValue(IconProperty, value);
      if (value != IconType.None && PathIcon == PathIconType.None && Image == ImageType.None)
      {
        // 아무것도 클리어 필요 없음
      }
    }
  }
  /// <summary>
  /// 아이콘 속성
  /// </summary>
  public static readonly DependencyProperty IconProperty =
      DependencyProperty.Register(nameof(Icon), typeof(IconType), typeof(FlipAnimationCard),
          new PropertyMetadata(IconType.None));
  #endregion
}

