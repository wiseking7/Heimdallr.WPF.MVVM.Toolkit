using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Heimdallr.ToolKit.Converters;
/// <summary>
/// WPF ItmsContrl 상속(ListView, ListBox, ComboBox, WrapPanel 등에서 특정 아이템의 1-based 인덱스 번호를 반환하는 
/// IValueConverter 구현입니다. 
/// 즉, 리스트의 각 항목 옆에 "1, 2, 3, ..." 같은 번호를 자동으로 붙이기 위한 컨버터입니다.
/// </summary>
public class IndexToNumberConverter : BaseValueConverter<IndexToNumberConverter>
{
  // Convert 메서드: value가 ListViewItem 또는 다른 ItemsControl 항목일 때 인덱스를 계산
  public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    // value가 ListViewItem 또는 다른 ItemsControl 항목일 때
    if (value is DependencyObject dependencyObject)
    {
      // 해당 항목이 속한 부모 ItemsControl을 찾음
      var itemsControl = FindParent<ItemsControl>(dependencyObject);
      if (itemsControl != null)
      {
        // ItemsControl에서 인덱스 계산
        var index = itemsControl.ItemContainerGenerator.IndexFromContainer(dependencyObject);
        return index + 1; // 1부터 시작하는 번호
      }
    }
    return value;
  }

  // ConvertBack 메서드는 필요하지 않음 (데이터 바인딩에서 되돌리지는 않음)
  public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }

  // 부모 요소 찾기 (재귀적으로 부모를 찾음)
  private static T? FindParent<T>(DependencyObject child) where T : DependencyObject
  {
    var parentObject = VisualTreeHelper.GetParent(child);
    if (parentObject == null) return null;
    if (parentObject is T parent) return parent;
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