using Heimdallr.ToolKit.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrSlider는 WPF Slider를 상속받아 다양한 커스터마이징이 가능한 슬라이더입니다.
/// - Thumb 아이콘 및 색상 지정
/// - 왼쪽/오른쪽 트랙 구간 색상 분리
/// </summary>
public class HeimdallrSlider : Slider
{
  static HeimdallrSlider()
  {
    // 기본 스타일 키를 오버라이드하여 커스텀 템플릿 연결
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrSlider),
      new FrameworkPropertyMetadata(typeof(HeimdallrSlider)));
  }

  /// <summary>
  /// 템플릿 적용 시 컬럼 너비 조정 이벤트 등록
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    UpdateTrackLayout();
    this.ValueChanged += (_, __) => UpdateTrackLayout();
  }

  /// <summary>
  /// 슬라이더 값에 따라 왼쪽/오른쪽 구간 비율 동적 조정
  /// </summary>
  private void UpdateTrackLayout()
  {
    var leftColumn = GetTemplateChild("LeftColumn") as ColumnDefinition;
    var rightColumn = GetTemplateChild("RightColumn") as ColumnDefinition;

    if (leftColumn != null && rightColumn != null)
    {
      double range = Maximum - Minimum;

      if (range <= 0)
      {
        // 초기 또는 이상값 처리: 왼쪽 전체, 오른쪽 없음
        leftColumn.Width = new GridLength(1, GridUnitType.Star);
        rightColumn.Width = new GridLength(0, GridUnitType.Star);
        return;
      }

      double leftRatio = (Value - Minimum) / range;
      double rightRatio = 1 - leftRatio;

      leftColumn.Width = new GridLength(leftRatio, GridUnitType.Star);
      rightColumn.Width = new GridLength(rightRatio, GridUnitType.Star);
    }
  }

  #region PathIcon
  /// <summary>
  /// Thumb 아이콘 (Path 형태)
  /// </summary>
  public PathIconType ThumbIcon
  {
    get { return (PathIconType)GetValue(ThumbIconProperty); }
    set { SetValue(ThumbIconProperty, value); }
  }
  /// <summary>
  /// 
  /// </summary>
  public static readonly DependencyProperty ThumbIconProperty =
      DependencyProperty.Register(nameof(ThumbIcon), typeof(PathIconType), typeof(HeimdallrSlider),
        new PropertyMetadata(PathIconType.None));
  #endregion

  #region Thumb 색상
  /// <summary>
  /// Thumb 기본 색상
  /// </summary>
  public Brush ThumbFill
  {
    get => (Brush)GetValue(ThumbFillProperty);
    set => SetValue(ThumbFillProperty, value);
  }
  /// <summary>
  /// 기본값은 Orange
  /// </summary>
  public static readonly DependencyProperty ThumbFillProperty =
      DependencyProperty.Register(nameof(ThumbFill), typeof(Brush), typeof(HeimdallrSlider),
          new PropertyMetadata(Brushes.Orange));
  #endregion

  #region Thumb 마우스오버 색상
  /// <summary>
  /// Thumb 마우스 오버 색상
  /// </summary>
  public Brush ThumbHoverFill
  {
    get => (Brush)GetValue(ThumbHoverFillProperty);
    set => SetValue(ThumbHoverFillProperty, value);
  }
  /// <summary>
  /// 기본값은 Gold
  /// </summary>
  public static readonly DependencyProperty ThumbHoverFillProperty =
      DependencyProperty.Register(nameof(ThumbHoverFill), typeof(Brush), typeof(HeimdallrSlider),
          new PropertyMetadata(Brushes.Gold));
  #endregion

  #region Thumb 드래그 색상
  /// <summary>
  /// Thumb 드래그 색상
  /// </summary>
  public Brush ThumbDragFill
  {
    get => (Brush)GetValue(ThumbDragFillProperty);
    set => SetValue(ThumbDragFillProperty, value);
  }
  /// <summary>
  /// 기본값은 DarkOrange
  /// </summary>
  public static readonly DependencyProperty ThumbDragFillProperty =
      DependencyProperty.Register(nameof(ThumbDragFill), typeof(Brush), typeof(HeimdallrSlider),
          new PropertyMetadata(Brushes.DarkOrange));
  #endregion

  #region Track 배경 색상
  /// <summary>
  /// 전체 슬라이더 트랙 배경색상
  /// </summary>
  public Brush TrackBackground
  {
    get => (Brush)GetValue(TrackBackgroundProperty);
    set => SetValue(TrackBackgroundProperty, value);
  }
  /// <summary>
  /// 기본값은 Gray
  /// </summary>
  public static readonly DependencyProperty TrackBackgroundProperty =
      DependencyProperty.Register(nameof(TrackBackground), typeof(Brush), typeof(HeimdallrSlider),
          new PropertyMetadata(Brushes.Gray));
  #endregion

  #region 왼쪽 구간 색상
  /// <summary>
  /// Thumb 왼쪽 영역(선택된 구간) 색상
  /// </summary>
  public Brush RangeLeftFill
  {
    get => (Brush)GetValue(RangeLeftFillProperty);
    set => SetValue(RangeLeftFillProperty, value);
  }
  /// <summary>
  /// 기본색상은 Orange
  /// </summary>
  public static readonly DependencyProperty RangeLeftFillProperty =
      DependencyProperty.Register(nameof(RangeLeftFill), typeof(Brush), typeof(HeimdallrSlider),
        new PropertyMetadata(Brushes.Orange));
  #endregion

  #region 오른쪽 구간 색상
  /// <summary>
  /// Thumb 오른쪽 영역 색상
  /// </summary>
  public Brush RangeRightFill
  {
    get => (Brush)GetValue(RangeRightFillProperty);
    set => SetValue(RangeRightFillProperty, value);
  }
  /// <summary>
  /// 기본색상은 Gray
  /// </summary>
  public static readonly DependencyProperty RangeRightFillProperty =
      DependencyProperty.Register(nameof(RangeRightFill), typeof(Brush), typeof(HeimdallrSlider),
        new PropertyMetadata(Brushes.Gray));
  #endregion
}
