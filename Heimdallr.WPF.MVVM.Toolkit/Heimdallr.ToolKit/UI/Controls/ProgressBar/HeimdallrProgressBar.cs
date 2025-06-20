using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrProgressBar는 막대 단위로 진행률을 표시하는 커스텀 ProgressBar입니다.
/// </summary>
public class HeimdallrProgressBar : ProgressBar
{
  static HeimdallrProgressBar()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrProgressBar),
        new FrameworkPropertyMetadata(typeof(HeimdallrProgressBar)));
  }

  /// <summary>
  /// 진행 표시 색상을 나타냅니다.
  /// </summary>
  public Brush IndicatorBrush
  {
    get => (Brush)GetValue(IndicatorBrushProperty);
    set => SetValue(IndicatorBrushProperty, value);
  }

  /// <summary>
  /// IndicatorBrush 의존성 속성
  /// </summary>
  public static readonly DependencyProperty IndicatorBrushProperty =
      DependencyProperty.Register(nameof(IndicatorBrush), typeof(Brush), typeof(HeimdallrProgressBar),
          new PropertyMetadata(Brushes.DeepSkyBlue));

  /// <summary>
  /// 배경 색상을 나타냅니다.
  /// </summary>
  public Brush TrackBrush
  {
    get => (Brush)GetValue(TrackBrushProperty);
    set => SetValue(TrackBrushProperty, value);
  }

  /// <summary>
  /// TrackBrush 의존성 속성
  /// </summary>
  public static readonly DependencyProperty TrackBrushProperty =
      DependencyProperty.Register(nameof(TrackBrush), typeof(Brush), typeof(HeimdallrProgressBar),
          new PropertyMetadata(Brushes.LightGray));

  // <summary>
  /// 애니메이션 상태 변경 (예: "IndicatorNormal", "IndicatorPaused")
  /// <param name="stateName"></param>
  public void ChangeIndicatorAnimationState(string stateName)
  {
    VisualStateManager.GoToState(this, stateName, true);
  }

  /// <summary>
  /// 기본 탬플릿이 적용된 이후 호출되는 메서드 
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    var indicator = GetTemplateChild("PART_Indicator") as Border;
    var movingBar = indicator?.FindName("MovingBarTransform") as TranslateTransform;

    if (indicator != null && movingBar != null)
    {
      indicator.SizeChanged += (s, e) => UpdateAnimation(indicator, movingBar);
    }

    ChangeIndicatorAnimationState("IndicatorNormal");
  }

  /// <summary>
  /// 빨간 막대 애니메이션을 시작하거나 갱신
  /// </summary>
  private void UpdateAnimation(Border indicator, TranslateTransform movingBar)
  {
    double barWidth = 20;
    double maxTranslate = Math.Max(0, indicator.ActualWidth - barWidth);

    var animation = new DoubleAnimation
    {
      From = 0,
      To = maxTranslate,
      Duration = TimeSpan.FromSeconds(1),
      AutoReverse = true,
      RepeatBehavior = RepeatBehavior.Forever
    };

    movingBar.BeginAnimation(TranslateTransform.XProperty, animation);
  }

  /// <summary>
  /// 애니메이션을 종료 (필요 시 호출)
  /// </summary>
  public void StopMovingBarAnimation()
  {
    var indicator = GetTemplateChild("PART_Indicator") as Border;
    var movingBar = indicator?.FindName("MovingBarTransform") as TranslateTransform;

    if (movingBar != null)
    {
      movingBar.BeginAnimation(TranslateTransform.XProperty, null); // 애니메이션 제거
    }
  }
}

