using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.UI.Controls;
/// <summary>
/// HeimdallrToggleButtonSwitch는 ToggleButton을 상속받아 스위치 스타일의 토글 버튼을 구현합니다.
/// </summary>
public class HeimdallrToggleButtonSwitch : ToggleButton
{
  private const double ThumbPadding = 4;
  private Border? _thumb;
  private TranslateTransform? _thumbTranslate;

  static HeimdallrToggleButtonSwitch()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrToggleButtonSwitch),
        new FrameworkPropertyMetadata(typeof(HeimdallrToggleButtonSwitch)));
  }

  /// <summary>
  /// OnApplyTemplate 메서드는 템플릿이 적용된 후에 호출됩니다.
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    _thumb = GetTemplateChild("PART_Thumb") as Border;

    if (_thumb != null)
    {
      _thumbTranslate = new TranslateTransform();
      var group = new TransformGroup();
      group.Children.Add(_thumbTranslate);
      _thumb.RenderTransform = group;
    }

    this.Checked += (_, _) => UpdateThumbPosition();
    this.Unchecked += (_, _) => UpdateThumbPosition();
    this.SizeChanged += (_, _) => UpdateThumbPosition();

    UpdateThumbPosition();
  }

  /// <summary>
  /// UpdateThumbPosition 메서드는 토글 버튼의 상태에 따라 썸네일의 위치를 업데이트합니다.
  /// </summary>
  private void UpdateThumbPosition()
  {
    if (_thumb == null || _thumbTranslate == null)
      return;

    double trackWidth = ActualWidth;
    double thumbWidth = _thumb.ActualWidth;

    double toX = IsChecked == true
        // 오른쪽이 깔금하게 맞춰지지 않음
        //? Math.Max(0, trackWidth - thumbWidth - ThumbPadding)
        ? Math.Max(0, trackWidth - thumbWidth)
        : 0;

    AnimateThumb(toX);
  }

  /// <summary>
  /// AnimateThumb 메서드는 썸네일의 위치를 애니메이션으로 변경합니다.
  /// </summary>
  /// <param name="toValue"></param>
  private void AnimateThumb(double toValue)
  {
    if (_thumbTranslate == null)
      return;

    var animation = new DoubleAnimation
    {
      To = toValue,
      Duration = TimeSpan.FromMilliseconds(200),
      EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
    };

    _thumbTranslate.BeginAnimation(TranslateTransform.XProperty, animation);
  }
}
