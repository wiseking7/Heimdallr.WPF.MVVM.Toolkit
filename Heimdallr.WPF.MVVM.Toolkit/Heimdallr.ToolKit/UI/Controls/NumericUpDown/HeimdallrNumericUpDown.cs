using Heimdallr.ToolKit.Enums;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrNumericUpDown은 숫자 값을 조정할 수 있는 컨트롤입니다.
/// </summary>
public class HeimdallrNumericUpDown : Control
{
  static HeimdallrNumericUpDown()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrNumericUpDown),
        new FrameworkPropertyMetadata(typeof(HeimdallrNumericUpDown)));
  }

  #region Value
  /// <summary>
  /// 현재 값입니다. Min ~ Max 범위 내로 제한됩니다.
  /// </summary>
  public double Value
  {
    get => (double)GetValue(ValueProperty);
    set => SetValue(ValueProperty, value);
  }
  /// <summary>
  /// value 속성은 현재 컨트롤의 값을 나타냅니다. 이 값은 Min과 Max 사이에 있어야 하며, Step 단위로 증가 또는 감소할 수 있습니다.
  /// </summary>
  public static readonly DependencyProperty ValueProperty =
      DependencyProperty.Register(nameof(Value), typeof(double), typeof(HeimdallrNumericUpDown),
          new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            OnValueChanged, CoerceValue));

  /// <summary>
  /// Value 속성이 변경될 때 호출되는 메서드입니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    ((HeimdallrNumericUpDown)d).UpdateTextBox();
  }

  /// <summary>
  /// Value 속성의 값을 강제적으로 조정하는 메서드입니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="baseValue"></param>
  /// <returns></returns>
  private static object CoerceValue(DependencyObject d, object baseValue)
  {
    var control = (HeimdallrNumericUpDown)d;
    var value = (double)baseValue;
    if (value < control.Min) return control.Min;
    if (value > control.Max) return control.Max;
    return control.NormalizePrecision(value);
  }
  #endregion

  #region Min
  /// <summary>
  /// 최소값입니다.
  /// </summary>
  public double Min
  {
    get => (double)GetValue(MinProperty);
    set => SetValue(MinProperty, value);
  }
  /// <summary>
  /// Min 속성은 현재 컨트롤의 최소값을 나타냅니다. 이 값은 Value 속성이 이 범위 이하로 내려가지 않도록 제한합니다.
  /// </summary>
  public static readonly DependencyProperty MinProperty =
      DependencyProperty.Register(nameof(Min), typeof(double), typeof(HeimdallrNumericUpDown),
          new PropertyMetadata(0.0, OnMinMaxChanged));
  #endregion

  #region Max
  /// <summary>
  /// 최대값입니다.
  /// </summary>
  public double Max
  {
    get => (double)GetValue(MaxProperty);
    set => SetValue(MaxProperty, value);
  }
  /// <summary>
  /// Max 속성은 현재 컨트롤의 최대값을 나타냅니다. 이 값은 Value 속성이 이 범위 이상으로 올라가지 않도록 제한합니다.
  /// </summary>
  public static readonly DependencyProperty MaxProperty =
     DependencyProperty.Register(nameof(Max), typeof(double), typeof(HeimdallrNumericUpDown),
         new PropertyMetadata(100.0, OnMinMaxChanged));
  /// <summary>
  /// Min 또는 Max 속성이 변경될 때 호출되는 메서드입니다. 이 메서드는 Value 속성을 Min과 Max 범위 내로 강제 조정합니다.
  /// </summary>
  /// <param name="d"></param>
  /// <param name="e"></param>
  private static void OnMinMaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    var control = (HeimdallrNumericUpDown)d;
    control.CoerceValue(ValueProperty);
  }
  #endregion

  #region Step
  /// <summary>
  /// 증감 단위입니다.
  /// </summary>
  public double Step
  {
    get => (double)GetValue(StepProperty);
    set => SetValue(StepProperty, value);
  }
  /// <summary>
  /// Step 속성은 현재 컨트롤의 증감 단위를 나타냅니다. 이 값은 Value 속성이 이 단위만큼 증가 또는 감소할 수 있도록 합니다.
  /// </summary>
  public static readonly DependencyProperty StepProperty =
      DependencyProperty.Register(nameof(Step), typeof(double), typeof(HeimdallrNumericUpDown),
          new PropertyMetadata(1.0));
  #endregion

  #region UpButtonIcon
  /// <summary>
  /// 증가 버튼 아이콘입니다.
  /// </summary>
  public PathIconType UpButtonIcon
  {
    get => (PathIconType)GetValue(UpButtonIconProperty);
    set => SetValue(UpButtonIconProperty, value);
  }
  /// <summary>
  /// UpButtonIcon 속성은 증가 버튼에 표시될 아이콘을 나타냅니다. 기본값은 PathIconType.None입니다.
  /// </summary>
  public static readonly DependencyProperty UpButtonIconProperty =
     DependencyProperty.Register(nameof(UpButtonIcon), typeof(PathIconType), typeof(HeimdallrNumericUpDown),
         new PropertyMetadata(PathIconType.None));
  #endregion

  #region DownButtonIcon
  /// <summary>
  /// 감소 버튼 아이콘입니다.
  /// </summary>
  public PathIconType DownButtonIcon
  {
    get => (PathIconType)GetValue(DownButtonIconProperty);
    set => SetValue(DownButtonIconProperty, value);
  }
  /// <summary>
  /// DownButtonIcon 속성은 감소 버튼에 표시될 아이콘을 나타냅니다. 기본값은 PathIconType.None입니다.
  /// </summary>
  public static readonly DependencyProperty DownButtonIconProperty =
      DependencyProperty.Register(nameof(DownButtonIcon), typeof(PathIconType), typeof(HeimdallrNumericUpDown),
          new PropertyMetadata(PathIconType.None));
  #endregion

  #region UP 아이콘 변경색상
  /// <summary>
  /// Up 버튼 아이콘의 색상입니다. 기본값은 검정색입니다.
  /// </summary>
  public Brush UpIconFill
  {
    get => (Brush)GetValue(UpIconFillProperty);
    set => SetValue(UpIconFillProperty, value);
  }
  /// <summary>
  /// IconFill 속성은 증가 및 감소 버튼 아이콘의 색상을 나타냅니다. 기본값은 검정색입니다.
  /// </summary>
  public static readonly DependencyProperty UpIconFillProperty =
    DependencyProperty.Register(
        nameof(UpIconFill),
        typeof(Brush),
        typeof(HeimdallrNumericUpDown),
        new PropertyMetadata(new SolidColorBrush(Colors.Gray)));
  #endregion

  #region Down 아이콘 변경색상
  /// <summary>
  /// Down 버튼 아이콘의 색상입니다. 기본값은 검정색입니다.
  /// </summary>
  public Brush DownIconFill
  {
    get { return (Brush)GetValue(DownIconFillProperty); }
    set { SetValue(DownIconFillProperty, value); }
  }
  /// <summary>
  /// DowIconFill 속성은 감소 버튼 아이콘의 색상을 나타냅니다. 기본값은 검정색입니다.
  /// </summary>
  public static readonly DependencyProperty DownIconFillProperty =
      DependencyProperty.Register(nameof(DownIconFill), typeof(Brush), typeof(HeimdallrNumericUpDown),
        new PropertyMetadata(new SolidColorBrush(Colors.Gray)));


  #endregion

  /// <summary>
  /// 현재 컨트롤의 템플릿에서 TextBox를 참조하기 위한 변수입니다.
  /// </summary>
  private TextBox? _textBox;

  /// <summary>
  /// OnApplyTemplate 메서드는 컨트롤의 템플릿이 적용될 때 호출됩니다.
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    if (GetTemplateChild("PART_UpButton") is Button up)
    {
      up.Click -= Up_Click;
      up.Click += Up_Click;
    }

    if (GetTemplateChild("PART_DownButton") is Button down)
    {
      down.Click -= Down_Click;
      down.Click += Down_Click;
    }

    if (GetTemplateChild("PART_TextBox") is TextBox textBox)
    {
      if (_textBox != null)
      {
        _textBox.PreviewTextInput -= TextBox_PreviewTextInput;
        DataObject.RemovePastingHandler(_textBox, TextBox_Pasting);
        _textBox.TextChanged -= TextBox_TextChanged;
      }

      _textBox = textBox;

      _textBox.PreviewTextInput += TextBox_PreviewTextInput;
      DataObject.AddPastingHandler(_textBox, TextBox_Pasting);
      _textBox.TextChanged += TextBox_TextChanged;

      _textBox.Text = Value.ToString(CultureInfo.InvariantCulture);
    }
  }

  /// <summary>
  /// Up 버튼 클릭 이벤트 핸들러에 VisualState 변경 및 값 증가 처리 통합
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private async void Up_Click(object sender, RoutedEventArgs e)
  {
    Value = NormalizePrecision(Value + Step);

    VisualStateManager.GoToState(this, "PressedUp", true);

    await Task.Delay(200);

    VisualStateManager.GoToState(this, "Normal", true);
  }

  /// <summary>
  /// Down 버튼 클릭 이벤트 핸들러에 VisualState 변경 추가
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private async void Down_Click(object sender, RoutedEventArgs e)
  {
    Value = NormalizePrecision(Value - Step);

    VisualStateManager.GoToState(this, "PressedDown", true);

    await Task.Delay(200);

    VisualStateManager.GoToState(this, "Normal", true);
  }

  /// <summary>
  /// 현재 Value 값을 텍스트 박스에 업데이트합니다.
  /// </summary>
  private void UpdateTextBox()
  {
    if (_textBox != null)
    {
      var text = Value.ToString(CultureInfo.InvariantCulture);
      if (_textBox.Text != text)
        _textBox.Text = text;
    }
  }

  /// <summary>
  /// 텍스트 박스에 입력이 발생했을 때 호출됩니다. 입력된 텍스트가 유효한 숫자인지 확인하고, 유효하지 않으면 입력을 취소합니다.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
  {
    e.Handled = !IsTextValid(_textBox?.Text, e.Text);
  }

  /// <summary>
  /// 입력값이 기존 텍스트에 추가되었을 때 전체 텍스트가 유효한 숫자인지 검사합니다.
  /// </summary>
  private bool IsTextValid(string? existingText, string newText)
  {
    string? fullText;

    if (existingText == null)
      fullText = newText;
    else
    {
      // 현재 선택된 텍스트를 포함해서 붙여넣기 혹은 입력 후 텍스트를 시뮬레이트
      if (_textBox == null) return false;
      var selectionStart = _textBox.SelectionStart;
      var selectionLength = _textBox.SelectionLength;

      fullText = existingText.Remove(selectionStart, selectionLength)
          .Insert(selectionStart, newText);
    }

    if (string.IsNullOrEmpty(fullText))
      return true; // 빈 문자열도 허용(입력 중일 때)

    // 정규식: 음수 부호가 맨 앞에 올 수 있고, 소수점은 하나만 존재 가능
    return Regex.IsMatch(fullText, @"^-?\d*\.?\d*$");
  }

  /// <summary>
  /// 텍스트 박스에 붙여넣기 이벤트가 발생했을 때 호출됩니다. 붙여넣기된 텍스트가 유효한 숫자인지 확인하고, 유효하지 않으면 붙여넣기를 취소합니다.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
  {
    if (e.DataObject.GetDataPresent(typeof(string)))
    {
      string pasteText = (string)e.DataObject.GetData(typeof(string))!;
      if (!IsTextValid(_textBox?.Text, pasteText))
        e.CancelCommand();
    }
    else
    {
      e.CancelCommand();
    }
  }

  /// <summary>
  /// 텍스트 박스의 텍스트가 변경될 때 호출됩니다. 입력된 텍스트를 숫자로 변환하고 Value 속성을 업데이트합니다.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
  {
    if (_textBox == null) return;

    if (double.TryParse(_textBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var val))
    {
      Value = val;
    }
  }

  /// <summary>
  /// 소수 자릿수를 정규화합니다. Step이 정수면 정수로, 아니면 소수점 이하 2자리까지 처리.
  /// </summary>
  private double NormalizePrecision(double value)
  {
    if (Step % 1 == 0)
      return Math.Round(value, 0);
    else
    {
      int decimalPlaces = GetDecimalPlaces(Step);
      return Math.Round(value, decimalPlaces);
    }
  }

  /// <summary>
  /// 소수 자릿수를 계산합니다. 입력된 값의 소수점 이하 자릿수를 반환합니다.
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  private static int GetDecimalPlaces(double value)
  {
    string text = value.ToString(CultureInfo.InvariantCulture);
    int index = text.IndexOf('.');
    if (index < 0) return 0;
    return text.Length - index - 1;
  }
}



