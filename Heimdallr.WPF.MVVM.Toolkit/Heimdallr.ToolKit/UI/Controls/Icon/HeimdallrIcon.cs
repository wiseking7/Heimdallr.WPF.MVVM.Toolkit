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

/// <summary>
/// Icon, Image, PathData 를 사용한 커스터마이징
/// </summary>
public class HeimdallrIcon : ContentControl
{
  #region IconMode
  /// <summary>
  /// 아이콘 모드 (Icon, Image, PathIcon 중 현재 활성화된 모드를 나타냄)
  /// 주로 내부에서 설정되며, UI 템플릿에서 어떤 모드로 렌더링할지 결정할 때 사용
  /// </summary>
  public IconMode Mode
  {
    get { return (IconMode)GetValue(ModeProperty); }
    set { SetValue(ModeProperty, value); }
  }

  /// <summary>
  /// 종속성 주입
  /// </summary>
  public static readonly DependencyProperty ModeProperty =
      DependencyProperty.Register(nameof(Mode), typeof(IconMode),
        typeof(HeimdallrIcon), new PropertyMetadata(IconMode.None));
  #endregion

  #region IconType
  /// <summary>
  /// 정해진 Enum 값으로 아이콘을 지정하는 방식 (예: Save, Delete, Edit 등)
  /// 내부적으로 Toolkit에 정의된 Path Geometry 문자열로 변환하여 표시
  /// </summary>
  public IconType Icon
  {
    get { return (IconType)GetValue(IconProperty); }
    set { SetValue(IconProperty, value); }
  }

  /// <summary>
  /// 종속성 주입
  /// </summary>
  public static readonly DependencyProperty IconProperty =
      DependencyProperty.Register(nameof(Icon), typeof(IconType),
        typeof(HeimdallrIcon),
        new PropertyMetadata(IconType.None, IconPropertyChanged));

  /// <summary>
  /// IconType 속성이 변경될 때 호출되는 콜백
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void IconPropertyChanged(DependencyObject d,
    DependencyPropertyChangedEventArgs e)
  {
    HeimdallrIcon heimdallrIcon = (HeimdallrIcon)d;

    // ToolKit에서 아이콘 이름에 해당하는 Path 데이터 문자열 가져오기
    string geometryData = ToolKit.Resources.Geometies.GeometryConverter
      .GetData(heimdallrIcon.Icon.ToString());

    // 문자열을 Geometry로 파싱하여 Data 속성에 할당
    heimdallrIcon.Data = Geometry.Parse(geometryData);

    // 현재 모드를 Icon으로 설정
    heimdallrIcon.Mode = IconMode.Icon;
  }
  #endregion

  #region Image
  /// <summary>
  /// ImageType Enum 값으로 지정된 아이콘 이미지를 표시 (Base64 인코딩된 이미지 사용)
  /// </summary>
  public ImageType Image
  {
    get { return (ImageType)GetValue(ImageProperty); }
    set { SetValue(ImageProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty ImageProperty =
      DependencyProperty.Register(nameof(Image), typeof(ImageType),
        typeof(HeimdallrIcon),
        new PropertyMetadata(ImageType.None, ImagePropertyChanged));

  /// <summary>
  /// Image 속성이 변경될 때 호출되는 콜백
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void ImagePropertyChanged(DependencyObject d,
    DependencyPropertyChangedEventArgs e)
  {
    HeimdallrIcon heimdallrIcon = (HeimdallrIcon)d;

    try
    {
      // 이미지 이름에 해당하는 Base64 문자열 가져오기
      string base64 = ToolKit.Resources.Images.ImageConverter
        .GetData(heimdallrIcon.Image.ToString());

      // Base64 → byte[]
      byte[] binaryData = Convert.FromBase64String(base64);

      // byte[] → BitmapImage 생성
      BitmapImage bitmapImage = new();
      bitmapImage.BeginInit();
      bitmapImage.StreamSource = new MemoryStream(binaryData);
      bitmapImage.EndInit();

      // Source 속성에 할당
      heimdallrIcon.Source = bitmapImage;
      heimdallrIcon.Mode = IconMode.Image;
    }
    catch (KeyNotFoundException ex)
    {
      Debug.WriteLine($"이미지 항목을 찾을 수 없습니다: {ex.Message}");
      heimdallrIcon.Source = null!;
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"이미지 로딩 중 오류 발생: {ex.Message}");
    }
  }
  #endregion

  #region PathIcon
  /// <summary>
  /// XAML에서 직접 등록된 PathGeometry 리소스를 사용하는 방식
  /// </summary>
  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }
  /// <summary>
  /// 종속성 주입
  /// </summary>
  public static readonly DependencyProperty PathIconProperty =
    DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType),
      typeof(HeimdallrIcon),
      new PropertyMetadata(PathIconType.None, PathIconPropertyChanged));

  /// <summary>
  /// PathIcon 속성이 변경될 때 호출되는 콜백
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void PathIconPropertyChanged(DependencyObject d,
    DependencyPropertyChangedEventArgs e)
  {
    if (d is not HeimdallrIcon pathIcon)
      return;

    // PathGeometryStore에서 PathIcon에 대응하는 Geometry를 가져옴
    var geometry = PathGeometryStore.GetGeometry(pathIcon.PathIcon);
    if (geometry != null)
    {
      pathIcon.Data = geometry;
      pathIcon.Mode = IconMode.PathIcon;
      Debug.WriteLine($"[HeimdallrIcon] '{pathIcon.PathIcon}' → Geometry 적용");
    }
    else
    {
      Debug.WriteLine($"[HeimdallrIcon] '{pathIcon.PathIcon}' → Geometry 없음");
    }
  }
  #endregion

  #region Fill
  /// <summary>
  /// Path 아이콘에 색상을 적용할 때 사용되는 속성 (브러시)
  /// </summary>
  public Brush Fill
  {
    get { return (Brush)GetValue(FillProperty); }
    set { SetValue(FillProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush),
        typeof(HeimdallrIcon),
        new PropertyMetadata(Brushes.Silver));
  #endregion

  #region Data
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
        typeof(HeimdallrIcon),
        new PropertyMetadata(null));
  #endregion

  #region Source
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
        typeof(HeimdallrIcon),
        new PropertyMetadata(null));
  #endregion

  /// <summary>
  /// OnApplyTemplate: 컨트롤 템플릿이 적용될 때 호출되는 메서드
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    if (Data != null)
    {
      Debug.WriteLine("Data 는 속성(Property) 설정 입니다");
    }
    else
    {
      Debug.WriteLine("Data 가 null 입니다");
    }
  }

  #region 생성자
  /// <summary>
  /// 정적 생성자: HeimdallrIcon의 기본 스타일 키를 설정
  /// </summary>
  static HeimdallrIcon()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrIcon),
      new FrameworkPropertyMetadata(typeof(HeimdallrIcon)));
  }
  /// <summary>
  /// HeimdallrIcon 생성자: 기본 생성자
  /// </summary>
  public HeimdallrIcon()
  {
    MouseLeftButtonUp += (sender, e) =>
    {
      // 마우스 왼쪽 버튼 클릭 시 Command 실행
      if (Command != null && Command.CanExecute(CommandParameter))
      {
        Command.Execute(CommandParameter);
        e.Handled = true; // 이벤트 처리 완료 표시
      }
    };

    Cursor = Cursors.Hand; // 마우스 커서를 손 모양으로 변경
  }

  private void HeimdallrIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
  {
    throw new NotImplementedException();
  }
  #endregion

  #region Command
  /// <summary>
  /// 
  /// </summary>
  public ICommand? Command
  {
    get { return (ICommand?)GetValue(CommandProperty); }
    set { SetValue(CommandProperty, value); }
  }

  /// <summary>
  /// Command 속성은 ICommand 인터페이스를 구현하는 명령을 나타냅니다.
  /// </summary>
  public static readonly DependencyProperty CommandProperty =
      DependencyProperty.Register(nameof(Command), typeof(ICommand),
          typeof(HeimdallrIcon), new PropertyMetadata(null));

  /// <summary>
  /// CommandParameter 속성은 명령에 전달할 추가 매개변수를 나타냅니다.
  /// </summary>
  public object? CommandParameter
  {
    get { return GetValue(CommandParameterProperty); }
    set { SetValue(CommandParameterProperty, value); }
  }

  /// <summary>
  /// CommandParameter 속성은 ICommand에 전달할 매개변수를 나타냅니다.
  /// </summary>
  public static readonly DependencyProperty CommandParameterProperty =
      DependencyProperty.Register(nameof(CommandParameter), typeof(object),
          typeof(HeimdallrIcon), new PropertyMetadata(null));
  #endregion
}
