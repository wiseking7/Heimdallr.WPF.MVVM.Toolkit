using Heimdallr.ToolKit.Animations;
using Heimdallr.ToolKit.Animations.EasingFunctions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// MagicBar는 ListView를 상속한 사용자 정의 컨트롤입니다.
/// 선택된 항목의 위치에 따라 원형(또는 강조 표시)을 좌우로 애니메이션 이동시킵니다
/// </summary>
public class MagicBar : ListView
{
  // 정적 생성자에서 기본 스타일 키를 MagicBar로 오버라이드
  // 이 컨트롤을 사용할 때 XAML에서 해당 타입의 기본 스타일을 적용할 수 있게 합니다.
  static MagicBar()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(MagicBar),
      new FrameworkPropertyMetadata(typeof(MagicBar)));
  }

  // Storyboard에서 사용할 DoubleAnimationItem (좌우 이동 애니메이션)
  private DoubleAnimationItem? _vi;

  // 실제 애니메이션이 포함된 Storyboard
  private Storyboard? _sb;

  /// <summary>
  /// 템플릿이 적용될 때 호출됨, 여기서 PART_Circle 이름의 Grid를 찾고 애니메이션 초기화 수행
  /// </summary>
  public override void OnApplyTemplate()
  {
    base.OnApplyTemplate();

    // 템플릿에서 "PART_Circle"이라는 이름의 Grid를 찾음
    Grid grid = (Grid)GetTemplateChild("PART_Circle");

    // 애니메이션 초기화
    InitStoryboard(grid);
  }

  /// <summary>
  /// 애니메이션 구성 초기화 함수
  /// </summary>
  /// <param name="circle"></param>
  private void InitStoryboard(Grid circle)
  {
    _vi = new();       // DoubleAnimationItem 인스턴스 생성
    _sb = new();       // Storyboard 인스턴스 생성

    // 커스텀 EasingFunction (QuinticEaseInOut) 적용
    _vi.Mode = EasingFunctionBaseMode.QuinticEaseInOut;

    // 애니메이션 대상 속성: Canvas.Left (좌우 이동)
    _vi.Property = new PropertyPath(Canvas.LeftProperty);

    // 애니메이션 지속 시간: 500ms
    _vi.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 500));

    // 타겟 설정: 애니메이션 대상은 circle(Grid)
    Storyboard.SetTarget(_vi, circle);
    Storyboard.SetTargetProperty(_vi, _vi.Property);

    // Storyboard에 애니메이션 추가
    _sb.Children.Add(_vi);
  }

  /// <summary>
  /// 선택 항목이 변경되었을 때 호출되는 이벤트 핸들러, 선택된 인덱스를 기준으로 원형(circle)의 이동 좌표를 계산하여 애니메이션 실행
  /// </summary>
  /// <param name="e"></param>
  protected override void OnSelectionChanged(SelectionChangedEventArgs e)
  {
    base.OnSelectionChanged(e);

    // 애니메이션 목표 좌표 설정 (예: 각 아이템의 너비가 80이라고 가정)
    _vi!.To = SelectedIndex * 80;

    // 애니메이션 실행
    _sb!.Begin();
  }
}

