using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Heimdallr.ToolKit.UI.Controls;

/// <summary>
/// ListViewItem 을 상속하여 확장한 HeimdallrListViewItem 클래스. 커스텀 스타일 및 MVVM 방식 더블 클릭 명령 처리를 지원합니다.
/// </summary>
public class HeimdallrListViewItem : HeimdallrListViewBaseItem
{
  /// <summary>
  /// ViewModel에서 실행할 커맨드 이름 (기본값: "ItemDoubleClickCommand")
  /// </summary>
  public string ItemDoubleClickCommandName
  {
    get => (string)GetValue(ItemDoubleClickCommandNameProperty);
    set => SetValue(ItemDoubleClickCommandNameProperty, value);
  }

  /// <summary>
  /// 종속성 속성 기본값 설정(ItemDoubleClickCommand)
  /// </summary>
  public static readonly DependencyProperty ItemDoubleClickCommandNameProperty =
    DependencyProperty.Register(nameof(ItemDoubleClickCommandName), typeof(string),
      typeof(HeimdallrListViewItem), new PropertyMetadata("ItemDoubleClickCommand"));


  /// <summary>
  /// 정적 생성자: 이 클래스의 스타일을 연결합니다.
  /// Themes/Generic.xaml 또는 ResourceDictionary에 정의된 스타일을 연결합니다.
  /// </summary>
  static HeimdallrListViewItem()
  {
    // WPF 테마 스타일을 이 타입에 연결합니다.
    DefaultStyleKeyProperty.OverrideMetadata(typeof(HeimdallrListViewItem),
      new FrameworkPropertyMetadata(typeof(HeimdallrListViewItem)));
  }

  /// <summary>
  /// 인스턴스 생성자: 더블 클릭 이벤트 핸들러를 등록합니다.
  /// </summary>
  public HeimdallrListViewItem()
  {
    MouseDoubleClick += HeimdallrListViewItem_MouseDoubleClick;
  }

  /// <summary>
  /// 마우스 더블 클릭 시 ViewModel의 커맨드를 찾아 실행합니다.
  /// 커맨드는 ListView의 DataContext에서 CommandPropertyName에 해당하는 속성명으로 검색됩니다.
  /// </summary>
  private void HeimdallrListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
  {
    if (DataContext == null) return;

    // ListView 찾기 (시각적 트리 기준)
    var listView = FindAncestor<ListView>(this);
    if (listView?.DataContext == null) return;

    var commandName = ItemDoubleClickCommandName ?? "ItemDoubleClickCommand";

    // ViewModel에서 해당 명령 속성 찾기
    var commandProperty = listView.DataContext.GetType().GetProperty(commandName);
    if (commandProperty?.GetValue(listView.DataContext) is ICommand command &&
        command.CanExecute(DataContext))
    {
      command.Execute(DataContext);
    }
  }

  /// <summary>
  /// 시각적 트리에서 특정 타입의 상위 요소를 재귀적으로 찾는 유틸리티 메서드.
  /// 예: 현재 항목이 포함된 ListView를 찾을 때 사용
  /// </summary>
  private T? FindAncestor<T>(DependencyObject current) where T : DependencyObject
  {
    while (current != null)
    {
      if (current is T target)
        return target;

      // 부모로 한 단계 위로 올라감
      current = VisualTreeHelper.GetParent(current);
    }

    // 찾지 못한 경우 기본값 반환 (C# 8.0 이상 null-forgiving 사용 시 ! 가능)
    return null!;
  }
}
