using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrSwitchCheckBox는 토글 스위치 형태의 사용자 정의 CheckBox입니다.
/// 체크 여부(IsChecked)에 따라 Thumb(스위치 버튼)가 트랙 위에서 좌우로 부드럽게 이동합니다.
/// </summary>
public class HeimdallrSwitchCheckBox : CheckBox
{
  /// <summary>
  /// Thumb(버튼)의 좌우 여백 (트랙 끝까지 이동하지 않도록 조정용)
  /// </summary>
  private const double ThumbPadding = 4;

  /// <summary>
  /// XAML 템플릿에서 연결될 Thumb 요소 (스위치 버튼)
  /// </summary>
  private Border? _thumb;

  /// <summary>
  /// Thumb의 이동을 위한 TranslateTransform
  /// </summary>
  private TranslateTransform? _thumbTranslate;

  /// <summary>
  /// 정적 생성자: 기본 스타일을 Generic.xaml에 있는 스타일로 지정
  /// </summary>
  static HeimdallrSwitchCheckBox()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrSwitchCheckBox),
        new FrameworkPropertyMetadata(typeof(HeimdallrSwitchCheckBox)));
  }

  /// <summary>
  /// 템플릿이 적용된 후 호출되는 메서드
  /// Thumb 요소를 찾아 TranslateTransform을 설정하고, 이벤트를 연결합니다.
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    // 템플릿에서 Thumb 이름을 가진 Border 컨트롤을 가져옴
    _thumb = GetTemplateChild("Thumb") as Border;

    if (_thumb != null)
    {
      // 이동을 위한 TransformGroup 구성
      _thumbTranslate = new TranslateTransform();
      var group = new TransformGroup();
      group.Children.Add(_thumbTranslate);

      // Transform을 Thumb에 적용
      _thumb.RenderTransform = group;
      _thumb.RenderTransformOrigin = new Point(0.5, 0.5); // 중앙 기준
    }

    // 체크 상태 변경 이벤트 연결
    Checked += OnCheckedChanged;
    Unchecked += OnCheckedChanged;

    // 사이즈 변경 시 Thumb 위치 재계산
    SizeChanged += (_, _) => UpdateThumbPosition();

    // 초기 상태에서도 Thumb 위치 반영
    UpdateThumbPosition();
  }

  /// <summary>
  /// 체크/언체크 될 때마다 Thumb 위치를 업데이트
  /// </summary>
  private void OnCheckedChanged(object sender, RoutedEventArgs e)
  {
    UpdateThumbPosition();
  }

  /// <summary>
  /// 현재 체크 상태에 따라 Thumb 위치 계산 및 애니메이션 적용
  /// </summary>
  private void UpdateThumbPosition()
  {
    // 요소가 아직 연결되지 않은 경우 무시
    if (_thumb == null || _thumbTranslate == null)
      return;

    double trackWidth = ActualWidth;        // 전체 스위치 트랙 길이
    double thumbWidth = _thumb.ActualWidth; // Thumb 너비

    // Thumb가 이동해야 할 목표 X 위치 계산
    double toX = IsChecked == true
        ? Math.Max(0, trackWidth - thumbWidth - ThumbPadding) // 오른쪽으로 이동
        : 0;                                                  // 왼쪽으로 초기화

    AnimateThumb(toX);
  }

  /// <summary>
  /// Thumb를 주어진 위치로 부드럽게 애니메이션 이동
  /// </summary>
  private void AnimateThumb(double toValue)
  {
    if (_thumbTranslate == null)
      return;

    var animation = new DoubleAnimation
    {
      To = toValue,                                      // 이동할 위치
      Duration = TimeSpan.FromMilliseconds(300),         // 이동 시간
      EasingFunction = new QuadraticEase
      {
        EasingMode = EasingMode.EaseInOut                // 부드러운 애니메이션
      }
    };

    // Thumb의 X 위치 애니메이션 시작
    _thumbTranslate.BeginAnimation(TranslateTransform.XProperty, animation);
  }

}



