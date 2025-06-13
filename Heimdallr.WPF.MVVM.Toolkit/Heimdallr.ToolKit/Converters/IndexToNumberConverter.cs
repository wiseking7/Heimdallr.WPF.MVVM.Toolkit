using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// WPF ItemsControl (ListView, ListBox, ComboBox, WrapPanel 등)에서
/// 특정 아이템의 1-based 인덱스 번호를 반환하는 IValueConverter 구현입니다.
/// 즉, 리스트의 각 항목 옆에 "1, 2, 3, ..." 같은 번호를 자동으로 붙이기 위한 컨버터입니다.
/// </summary>
public class IndexToNumberConverter : BaseValueConverter<IndexToNumberConverter>
{
  /// <summary>
  /// Convert 메서드:
  /// value로 받은 요소(보통 ListViewItem 등)의 부모 ItemsControl에서
  /// 해당 요소가 몇 번째 아이템인지 0-based 인덱스를 찾아 1-based로 변환하여 반환합니다.
  /// </summary>
  /// <param name="value">ListViewItem 또는 다른 ItemsControl 아이템에 해당하는 DependencyObject</param>
  /// <param name="targetType">바인딩 타겟의 타입 (보통 int 혹은 string)</param>
  /// <param name="parameter">추가 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>아이템의 1부터 시작하는 순번 (int)</returns>
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value가 DependencyObject 타입인지 체크 (UI 요소)
    if (value is DependencyObject dependencyObject)
    {
      // 해당 아이템이 속한 부모 ItemsControl 찾기
      var itemsControl = FindParent<ItemsControl>(dependencyObject);
      if (itemsControl != null)
      {
        // 부모 ItemsControl에서 value 아이템의 인덱스 가져오기 (0부터 시작)
        var index = itemsControl.ItemContainerGenerator.IndexFromContainer(dependencyObject);
        // 1-based 번호로 변환하여 반환
        return index + 1;
      }
    }

    // 부모 ItemsControl을 못 찾거나, value가 예상 타입이 아니면 value를 그대로 반환
    return value;
  }

  /// <summary>
  /// ConvertBack 메서드는 사용하지 않음.
  /// 데이터 바인딩이 단방향이므로 예외 발생 처리.
  /// </summary>
  /// <exception cref="NotImplementedException"></exception>
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// 특정 UI 요소부터 시작하여 부모 트리를 거슬러 올라가면서
  /// 지정한 타입 T의 부모를 찾아 반환하는 재귀 함수입니다.
  /// </summary>
  /// <typeparam name="T">찾고자 하는 부모 타입 (ItemsControl 등)</typeparam>
  /// <param name="child">시작 요소</param>
  /// <returns>부모가 있으면 해당 객체, 없으면 null</returns>
  private static T? FindParent<T>(DependencyObject child) where T : DependencyObject
  {
    // 현재 요소의 부모 가져오기
    var parentObject = VisualTreeHelper.GetParent(child);

    // 부모가 없으면 null 반환 (트리 최상단 도달)
    if (parentObject == null)
      return null;

    // 부모가 찾고자 하는 타입이면 해당 부모 반환
    if (parentObject is T parent)
      return parent;

    // 아니면 부모의 부모를 재귀적으로 계속 탐색
    return FindParent<T>(parentObject);
  }
}
/* 사용예제
  목록 항목에 번호를 붙여야 하는 경우: 예를 들어, 사용자에게 순서대로 번호를 붙여서 표시하고 싶을 때 사용합니다.
  동적으로 변하는 항목 번호를 표시해야 하는 경우: 데이터가 추가되거나 삭제되면 번호가 자동으로 갱신되어야 할 때 유용합니다.
  페이징 처리된 목록에서 번호를 다시 계산해야 할 때: 여러 페이지에 걸쳐 번호를 표시하려면 페이지마다 번호가 올바르게 계산되어야 
  하기 때문에 이 컨버터를 사용할 수 있습니다. 
 
  <!-- 리소스로 컨버터 등록 -->
    <Window.Resources>
        <local:IndexToNumberConverter x:Key="IndexToNumberConverter" />
    </Window.Resources>

    <Grid>
        <!-- ListView 사용 예제 -->
        <ListView Name="MyListView" ItemsSource="{Binding MyItems}">
            <ListView.ItemTemplate>
                <DataTemplate>
                    <!-- 각 항목 옆에 번호를 표시하는 TextBlock -->
                    <StackPanel Orientation="Horizontal">
                        <!-- ListViewItem의 1-based 인덱스를 표시 -->
                        <TextBlock Text="{Binding RelativeSource={RelativeSource Mode=Self}, 
                                      Converter={StaticResource IndexToNumberConverter}" />
                        <TextBlock Text=": " />
                        <TextBlock Text="{Binding Name}" />
                    </StackPanel>
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
    </Grid>
 
  public class MyItem
  {
    public string Name { get; set; }
  }

  public class MainViewModel
  {
      // ListView에 바인딩할 아이템 컬렉션
      public ObservableCollection<MyItem> MyItems { get; set; }

      public MainViewModel()
      {
          MyItems = new ObservableCollection<MyItem>
          {
              new MyItem { Name = "Item 1" },
              new MyItem { Name = "Item 2" },
              new MyItem { Name = "Item 3" }
          };
      }
  }

ListView 예제
<ListBox Name="MyListBox" ItemsSource="{Binding MyItems}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <StackPanel>
                <!-- 인덱스를 붙여주는 TextBlock -->
                <TextBlock Text="{Binding RelativeSource={RelativeSource Mode=Self}, 
                           Converter={StaticResource IndexToNumberConverter}" />
                <TextBlock Text=" - " />
                <TextBlock Text="{Binding Name}" />
            </StackPanel>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>


 DataGrid 예제
 <DataGrid Name="MyDataGrid" ItemsSource="{Binding MyItems}">
    <DataGrid.Columns>
        <DataGridTextColumn Header="Index">
            <DataGridTextColumn.CellStyle>
                <Style TargetType="DataGridCell">
                    <Setter Property="TextBlock.Text" 
                            Value="{Binding RelativeSource={RelativeSource Mode=Self}, 
                            Converter={StaticResource IndexToNumberConverter}}" />
                </Style>
            </DataGridTextColumn.CellStyle>
        </DataGridTextColumn>
        <DataGridTextColumn Header="Name" Binding="{Binding Name}" />
    </DataGrid.Columns>
</DataGrid>

 
 */