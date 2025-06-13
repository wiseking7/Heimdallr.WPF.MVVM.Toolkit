using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;


/// <summary>
/// 아이콘과 워터마크(Placeholder)를 지원하는 커스텀 TextBox 컨트롤입니다.
/// 기본 TextBox를 상속하며, PathIcon 타입 아이콘과 워터마크 텍스트, 색상 등의 속성을 제공합니다.
/// </summary>
public class HeimdallrIconWaterMarkTextBox : TextBox
{
  /// <summary>
  /// 정적 생성자: 이 컨트롤의 기본 스타일 키를 이 타입으로 지정합니다.
  /// 이를 통해 Themes/Generic.xaml 등에 정의된 기본 스타일이 적용됩니다.
  /// </summary>
  static HeimdallrIconWaterMarkTextBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrIconWaterMarkTextBox),
      new FrameworkPropertyMetadata(typeof(HeimdallrIconWaterMarkTextBox)));
  }

  #region PathIcon
  /// <summary>
  /// PathIconType 아이콘을 지정하는 의존성 속성.
  /// 이 속성에 따라 XAML 스타일에서 PathIcon을 그려서 표시 가능.
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
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType),
          typeof(HeimdallrIconWaterMarkTextBox), new PropertyMetadata(PathIconType.None));
  #endregion

  #region WaterMark 문자열
  /// <summary>
  /// 워터마크(Placeholder) 텍스트를 지정하는 의존성 속성.
  /// 텍스트박스가 비어있을 때 화면에 흐릿하게 표시하는 안내 텍스트용.
  /// </summary>
  public string WaterMark
  {
    get { return (string)GetValue(WaterMarkProperty); }
    set { SetValue(WaterMarkProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty WaterMarkProperty =
      DependencyProperty.Register(nameof(WaterMark), typeof(string), typeof(HeimdallrIconWaterMarkTextBox),
        new PropertyMetadata(string.Empty));
  #endregion

  #region WaterMarkForeground 색상 
  /// <summary>
  /// 워터마크 텍스트의 전경색(색상)을 지정하는 의존성 속성.
  /// 기본값은 회색(Gray)이며, 워터마크 텍스트 색상을 조절할 수 있음.
  /// </summary>
  public Brush WaterMarkForeground
  {
    get { return (Brush)GetValue(WaterMarkForegroundProperty); }
    set { SetValue(WaterMarkForegroundProperty, value); }
  }
  /// <summary>
  /// 종속성주입
  /// </summary>
  public static readonly DependencyProperty WaterMarkForegroundProperty =
      DependencyProperty.Register(nameof(WaterMarkForeground), typeof(Brush), typeof(HeimdallrIconWaterMarkTextBox),
        new PropertyMetadata(Brushes.Gray));
  #endregion

  #region Fill
  /// <summary>
  /// 아이콘 등의 채우기 색상을 지정하는 의존성 속성.
  /// 기본적으로 회색(Gray)이 설정되어 있으며, 아이콘의 색상을 바꾸는 데 사용.
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
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrIconWaterMarkTextBox),
        new PropertyMetadata(Brushes.Gray));
  #endregion
}

