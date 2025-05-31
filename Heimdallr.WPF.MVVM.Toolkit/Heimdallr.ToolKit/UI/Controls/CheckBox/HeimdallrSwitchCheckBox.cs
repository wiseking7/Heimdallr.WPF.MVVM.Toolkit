using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrSwitchCheckBox는 토글 형태의 사용자 정의 CheckBox입니다.
/// Thumb(스위치 버튼)이 트랙 위를 애니메이션으로 이동합니다.
/// </summary>
public class HeimdallrSwitchCheckBox : CheckBox
{
  private const double ThumbPadding = 4;

  private Border? _thumb;
  private TranslateTransform? _thumbTranslate;

  static HeimdallrSwitchCheckBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrSwitchCheckBox),
        new FrameworkPropertyMetadata(typeof(HeimdallrSwitchCheckBox)));
  }

  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    _thumb = GetTemplateChild("Thumb") as Border;

    if (_thumb != null)
    {
      _thumbTranslate = new TranslateTransform();
      var group = new TransformGroup();
      group.Children.Add(_thumbTranslate);
      _thumb.RenderTransform = group;
      _thumb.RenderTransformOrigin = new Point(0.5, 0.5);
    }

    Checked += OnCheckedChanged;
    Unchecked += OnCheckedChanged;
    SizeChanged += (_, _) => UpdateThumbPosition();

    UpdateThumbPosition();
  }

  private void OnCheckedChanged(object sender, RoutedEventArgs e)
  {
    UpdateThumbPosition();
  }

  private void UpdateThumbPosition()
  {
    if (_thumb == null || _thumbTranslate == null)
      return;

    double trackWidth = ActualWidth;
    double thumbWidth = _thumb.ActualWidth;

    double toX = IsChecked == true
        ? Math.Max(0, trackWidth - thumbWidth - ThumbPadding)
        : 0;

    AnimateThumb(toX);
  }

  private void AnimateThumb(double toValue)
  {
    if (_thumbTranslate == null)
      return;

    var animation = new DoubleAnimation
    {
      To = toValue,
      Duration = TimeSpan.FromMilliseconds(300),
      EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
    };

    _thumbTranslate.BeginAnimation(TranslateTransform.XProperty, animation);
  }
}


