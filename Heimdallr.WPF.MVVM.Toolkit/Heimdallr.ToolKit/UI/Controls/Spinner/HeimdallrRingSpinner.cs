using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Heimdallr.ToolKit.UI.Controls;
/// <summary>
/// HeimdallrRingSpinner는 원형으로 여러 점(Ellipse)이 배치되어 회전하는 로딩 스피너를 표현하는 커스텀 컨트롤입니다.
/// SpinnerColor, Message, Duration, IsLoading 같은 의존성 속성들을 제공하며,
/// 템플릿의 PART_SpinnerCanvas에 점들을 원형으로 배치합니다.
/// </summary>
public class HeimdallrRingSpinner : Control
{
  /// <summary>
  /// 정적 생성자: 이 컨트롤에 맞는 기본 스타일을 자동으로 적용하기 위해 DefaultStyleKey를 오버라이드
  /// </summary>
  static HeimdallrRingSpinner()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrRingSpinner),
        new FrameworkPropertyMetadata(typeof(HeimdallrRingSpinner)));
  }

  /// <summary>
  /// 스피너 점들의 색상을 설정하는 의존성 속성 (기본값: 빨간색)
  /// </summary>
  public static readonly DependencyProperty SpinnerColorProperty =
      DependencyProperty.Register("SpinnerColor", typeof(Brush), typeof(HeimdallrRingSpinner),
        new PropertyMetadata(Brushes.Red));

  /// <summary>
  /// 스피너 옆에 표시할 안내 메시지 (기본값: "Please wait...")
  /// </summary>
  public static readonly DependencyProperty MessageProperty =
      DependencyProperty.Register("Message", typeof(string), typeof(HeimdallrRingSpinner),
        new PropertyMetadata("Please wait..."));

  /// <summary>
  /// 한 바퀴 도는 애니메이션의 지속 시간(초)
  /// </summary>
  public static readonly DependencyProperty DurationProperty =
      DependencyProperty.Register("Duration", typeof(double), typeof(HeimdallrRingSpinner),
        new PropertyMetadata(1.5));

  /// <summary>
  /// 스피너 활성화 여부 (로딩 중인지 여부)
  /// </summary>
  public static readonly DependencyProperty IsLoadingProperty =
      DependencyProperty.Register("IsLoading", typeof(bool), typeof(HeimdallrRingSpinner),
        new PropertyMetadata(false));


  /// IsLoading 프로퍼티 래퍼
  public bool IsLoading
  {
    get => (bool)GetValue(IsLoadingProperty);
    set => SetValue(IsLoadingProperty, value);
  }

  /// <summary>
  /// SpinnerColor 프로퍼티 래퍼
  /// </summary>
  public Brush SpinnerColor
  {
    get => (Brush)GetValue(SpinnerColorProperty);
    set => SetValue(SpinnerColorProperty, value);
  }

  /// Message 프로퍼티 래퍼
  public string Message
  {
    get => (string)GetValue(MessageProperty);
    set => SetValue(MessageProperty, value);
  }

  /// Duration 프로퍼티 래퍼
  public double Duration
  {
    get => (double)GetValue(DurationProperty);
    set => SetValue(DurationProperty, value);
  }

  /// <summary>
  /// 템플릿이 적용될 때 호출되는 메서드
  /// PART_SpinnerCanvas라는 이름의 Canvas를 찾아서 그 안에 점들(Ellipse)을 원형으로 배치함.
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    // PART_SpinnerCanvas 이름의 캔버스를 가져옴
    if (GetTemplateChild("PART_SpinnerCanvas") is Canvas dots)
    {
      dots.Children.Clear(); // 기존 자식들 초기화

      int dotCount = 8;      // 원을 구성할 점 개수
      double radius = 40;    // 원의 반지름

      // 각 점을 원형으로 배치
      for (int i = 0; i < dotCount; i++)
      {
        // 점 위치 각도 계산 (360도 나누기 점 개수 * i)
        double angle = 360.0 / dotCount * i;
        // 라디안 단위로 변환
        double rad = angle * Math.PI / 180;
        // 원 중심으로부터 점의 좌표(x, y) 계산
        double x = radius * Math.Cos(rad);
        double y = radius * Math.Sin(rad);

        // 점 모양 생성 (타원)
        Ellipse ellipse = new Ellipse
        {
          Width = 10,
          Height = 10,
          Fill = SpinnerColor  // SpinnerColor로 채움
        };

        // 점의 중심을 캔버스 중심(50,50)으로 맞춤 (점 크기 절반만큼 보정)
        Canvas.SetLeft(ellipse, 50 - 5);
        Canvas.SetTop(ellipse, 50 - 5);

        // 위치 이동을 위한 TranslateTransform 생성, x,y 좌표만큼 이동
        TranslateTransform translate = new TranslateTransform(x, y);
        ellipse.RenderTransform = translate;

        // 캔버스에 점 추가
        dots.Children.Add(ellipse);
      }
    }
  }
}





