using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// Heimdallr 스타일의 워터마크 텍스트 입력 컨트롤 (일반 TextBox 기반)
/// </summary>
public class HeimdallrIconPlaceholderTextBox : Control
{
  private TextBox? _textBox;

  static HeimdallrIconPlaceholderTextBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrIconPlaceholderTextBox),
        new FrameworkPropertyMetadata(typeof(HeimdallrIconPlaceholderTextBox)));
  }

  public HeimdallrIconPlaceholderTextBox()
  {
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    _textBox = GetTemplateChild("PART_TextBox") as TextBox;
    if (_textBox != null)
    {
      _textBox.Text = Text;
      _textBox.TextChanged += (s, e) =>
      {
        Text = _textBox.Text;
        HasText = !string.IsNullOrEmpty(_textBox.Text);
      };
    }
  }

  //====================== Text 바인딩 ======================//

  public string Text
  {
    get => (string)GetValue(TextProperty);
    set => SetValue(TextProperty, value);
  }

  public static readonly DependencyProperty TextProperty =
      DependencyProperty.Register(nameof(Text), typeof(string), typeof(HeimdallrIconPlaceholderTextBox),
          new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged));

  private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var control = (HeimdallrIconPlaceholderTextBox)d;
    if (control._textBox != null && control._textBox.Text != (string)e.NewValue)
    {
      control._textBox.Text = (string)e.NewValue;
    }

    control.HasText = !string.IsNullOrEmpty((string)e.NewValue);
  }

  //====================== Placeholder 속성 ======================//

  public string Placeholder
  {
    get => (string)GetValue(PlaceholderProperty);
    set => SetValue(PlaceholderProperty, value);
  }

  public static readonly DependencyProperty PlaceholderProperty =
      DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(string.Empty));

  public Brush PlaceholderForeground
  {
    get => (Brush)GetValue(PlaceholderForegroundProperty);
    set => SetValue(PlaceholderForegroundProperty, value);
  }

  public static readonly DependencyProperty PlaceholderForegroundProperty =
      DependencyProperty.Register(nameof(PlaceholderForeground), typeof(Brush), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(Brushes.Gray));

  //====================== 내부 상태 ======================//

  public bool HasText
  {
    get => (bool)GetValue(HasTextProperty);
    private set => SetValue(HasTextPropertyKey, value);
  }

  private static readonly DependencyPropertyKey HasTextPropertyKey =
      DependencyProperty.RegisterReadOnly(nameof(HasText), typeof(bool), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(false));

  public static readonly DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;

  //====================== 아이콘 관련 ======================//

  public PathIconType PathIcon
  {
    get => (PathIconType)GetValue(PathIconProperty);
    set => SetValue(PathIconProperty, value);
  }

  public static readonly DependencyProperty PathIconProperty =
      DependencyProperty.Register(nameof(PathIcon), typeof(PathIconType), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(PathIconType.None));

  public Brush Fill
  {
    get => (Brush)GetValue(FillProperty);
    set => SetValue(FillProperty, value);
  }

  public static readonly DependencyProperty FillProperty =
      DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(HeimdallrIconPlaceholderTextBox),
          new PropertyMetadata(Brushes.Gray));
}
