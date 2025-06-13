using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// 커스텀 TreeView 컨트롤입니다.
/// 둥근 모서리(CornerRadius) 속성을 지원하고,
/// 선택된 항목이 변경될 때 커맨드를 실행할 수 있도록 설계되었습니다.
/// 또한, 각 TreeViewItem은 HeimdallrTreeViewItem으로 대체하여 사용자 정의 스타일이나 동작을 적용할 수 있습니다.
/// </summary>
public class HeimdallrTreeView : TreeView
{
  #region CornerRadius

  /// <summary>
  /// 둥근 모서리(CornerRadius)를 나타내는 DependencyProperty 정의
  /// 사용자가 트리뷰 테두리의 모서리 둥근 정도를 지정 가능
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
    DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius),
      typeof(HeimdallrTreeView), new FrameworkPropertyMetadata(
        new CornerRadius(0), // 기본값은 모서리 반경 0 (둥글지 않음)
        FrameworkPropertyMetadataOptions.AffectsRender));
  // AffectsRender: 값이 바뀌면 다시 렌더링 해야 함을 알림

  /// <summary>
  /// CLR 래퍼 프로퍼티
  /// 이 속성을 통해 코드에서 CornerRadius 값을 읽고 쓸 수 있음
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }

  #endregion

  #region SelectedCommand

  /// <summary>
  /// 사용자가 트리뷰에서 항목을 선택했을 때 실행할 ICommand 속성
  /// MVVM 패턴에서 선택 항목에 대한 처리를 커맨드 바인딩으로 구현 가능
  /// </summary>
  public ICommand SelectedCommand
  {
    get => (ICommand)GetValue(SelectedCommandProperty);
    set => SetValue(SelectedCommandProperty, value);
  }

  /// <summary>
  /// SelectedCommand 의존성 속성 등록
  /// 초기값은 null이며, 필요시 변경 콜백을 추가 가능
  /// </summary>
  public static readonly DependencyProperty SelectedCommandProperty =
      DependencyProperty.Register(nameof(SelectedCommand), typeof(ICommand), typeof(HeimdallrTreeView),
        new PropertyMetadata(null));

  #endregion

  /// <summary>
  /// 정적 생성자
  /// 기본 스타일 키를 이 컨트롤 타입으로 지정
  /// Themes/Generic.xaml 등에 스타일 정의가 있어야 함
  /// </summary>
  static HeimdallrTreeView()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrTreeView),
      new FrameworkPropertyMetadata(typeof(HeimdallrTreeView)));
  }

  /// <summary>
  /// TreeView의 각 아이템 컨테이너를 생성할 때 호출
  /// 기본 TreeViewItem 대신 HeimdallrTreeViewItem 인스턴스를 반환하도록 오버라이드
  /// 이를 통해 아이템별 커스텀 스타일 및 동작 구현 가능
  /// </summary>
  protected override DependencyObject GetContainerForItemOverride()
    => new HeimdallrTreeViewItem();

  /// <summary>
  /// 생성자
  /// SelectedItemChanged 이벤트에 핸들러 등록
  /// </summary>
  public HeimdallrTreeView()
  {
    SelectedItemChanged += HeimdallrTreeView_SelectedItemChanged;
  }

  /// <summary>
  /// 선택된 아이템이 변경될 때 호출되는 이벤트 핸들러
  /// 새로 선택된 아이템이 null이 아니면, SelectedCommand 실행
  /// </summary>
  private void HeimdallrTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
  {
    if (e.NewValue != null)
    {
      SelectedCommand?.Execute(e.NewValue);
    }

    // 또는 이렇게도 작성 가능
    // if (SelectedItem != null)
    // {
    //   SelectedCommand?.Execute(SelectedItem);
    // }
  }
}


/*
 FrameworkPropertyMetadataOptions는 여러 플래그를 지원합니다. 예:

옵션	설명
AffectsRender	값이 바뀌면 UI를 다시 그림
AffectsMeasure	레이아웃 측정(Measure 단계) 다시 수행
AffectsArrange	레이아웃 정렬(Arrange 단계) 다시 수행
BindsTwoWayByDefault	바인딩 시 기본이 양방향(BindingMode.TwoWay)
Inherits	상위 요소로부터 속성값을 상속받음
Journal	내비게이션 히스토리에 포함됨 (프레임 내 탐색 시)

 */