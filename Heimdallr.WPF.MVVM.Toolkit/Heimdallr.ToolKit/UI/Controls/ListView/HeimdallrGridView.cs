using System.Windows;
using System.Windows.Controls;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// HeimdallrGridView 클래스는 ListView를 확장하여 그리드 형식의 항목 표시를 지원하는 사용자 지정 ListView 컨트롤입니다.
/// </summary>
public class HeimdallrGridView : HeimdallrListViewBase
{
  /// <summary>
  /// 그리드 항목의 모서리 반경을 설정하는 종속성 속성입니다.
  /// </summary>
  public CornerRadius CornerRadius
  {
    get => (CornerRadius)GetValue(CornerRadiusProperty);
    set => SetValue(CornerRadiusProperty, value);
  }
  /// <summary>
  /// 그리드 항목의 모서리 반경을 설정하는 종속성 속성입니다.
  /// </summary>
  public static readonly DependencyProperty CornerRadiusProperty =
    DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(HeimdallrGridView),
      new PropertyMetadata(new CornerRadius(0)));

  /// <summary>
  /// HeimdallrGridView 클래스의 정적 생성자입니다.
  /// </summary>
  static HeimdallrGridView()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(GridView), new FrameworkPropertyMetadata(typeof(HeimdallrGridView)));
  }

  /// <summary>
  /// HeimdallrGridView 클래스의 새 인스턴스를 초기화합니다.
  /// </summary>
  public HeimdallrGridView()
  {
  }

  /// <summary>
  /// 항목이 자체 컨테이너인지 여부를 확인하는 메서드입니다.
  /// 이 메서드는 WPF가 아이템 컨테이너를 생성할 때 해당 아이템이 이미 컨테이너 역할을 할 수 있는지를 판단하는 데 사용됩니다.
  /// 예: ListViewItem이 이미 있다면 새로 만들 필요 없음.
  /// 여기서는 아이템이 HeimdallrGridViewItem인 경우 컨테이너로 사용할 수 있다고 판단합니다.
  /// </summary>
  /// <param name="item"></param>
  /// <returns></returns>
  protected override bool IsItemItsOwnContainerOverride(object item)
  {
    // 항목이 HeimdallrGridViewItem 타입인지 확인합니다.
    return item is HeimdallrGridViewItem;
  }

  /// <summary>
  /// 항목에 대한 컨테이너를 가져오는 메서드입니다.
  /// 이 메서드는 각 항목을 표시할 컨테이너(즉, HeimdallrGridViewItem)를 새로 생성하는 역할을 합니다.
  /// WPF는 이 메서드를 통해 각 아이템에 대해 적절한 시각적 컨테이너를 제공합니다.
  /// </summary>
  /// <returns></returns>
  protected override DependencyObject GetContainerForItemOverride()
  {
    // 새로운 HeimdallrGridViewItem 인스턴스를 반환합니다.
    return new HeimdallrGridViewItem();
  }
}
