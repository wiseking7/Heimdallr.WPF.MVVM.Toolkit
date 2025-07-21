using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;

namespace Heimdallr.ToolKit;

/// <summary>
/// DataGridRowNumberBehavior
/// DataGrid의 RowHeader에 행 번호를 표시하는 Behavior입니다.
/// </summary>
public class DataGridRowNumberBehavior : Behavior<DataGrid>
{
  /// <summary>
  /// ShowRowNumber
  /// 행 번호를 표시할지 여부를 설정하는 속성입니다.
  /// </summary>
  public bool ShowRowNumber { get; set; }

  /// <summary>
  /// OnAttached
  /// Behavior가 DataGrid에 부착되었을 때 호출됩니다.
  /// ShowRowNumber가 true일 경우, 행 번호를 표시하도록 설정합니다.
  /// </summary>
  protected override void OnAttached()
  {
    if (ShowRowNumber)
    {
      // AssociatedObject가 null이 아닌지 확인 후 작업 진행
      if (AssociatedObject != null)
      {
        AssociatedObject.RowHeaderWidth = 40;

        // LoadingRow와 UnloadingRow 이벤트에 대한 핸들러를 등록
        AssociatedObject.LoadingRow += AssociatedObject_LoadingRow;
        AssociatedObject.UnloadingRow += AssociatedObject_UnloadingRow;
      }
    }
  }

  /// <summary>
  /// AssociatedObject_UnloadingRow
  /// 로우가 언로딩(삭제)될 때 호출되는 이벤트 핸들러입니다.
  /// 로우가 삭제될 때 번호를 새로 갱신하여 UI를 업데이트합니다.
  /// </summary>
  private void AssociatedObject_UnloadingRow(object? sender, DataGridRowEventArgs e)
  {
    RefreshRowNumber();
  }

  /// <summary>
  /// RefreshRowNumber
  /// 데이터그리드의 모든 아이템에 대해 행 번호를 다시 설정하는 메서드입니다.
  /// </summary>
  private void RefreshRowNumber()
  {
    // AssociatedObject.Items는 DataGrid의 모든 아이템을 포함하고 있음
    if (AssociatedObject != null)
    {
      foreach (var item in AssociatedObject.Items)
      {
        // ContainerFromItem을 사용하여 각 아이템에 해당하는 DataGridRow를 가져옵니다.
        var row = AssociatedObject.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;

        // row가 null이 아니면 해당 row에 대해 행 번호를 설정
        if (row != null)
        {
          // row의 Header를 인덱스 + 1로 설정 (1부터 시작하는 행 번호)
          row.Header = row.GetIndex() + 1;
        }
      }
    }
  }

  /// <summary>
  /// AssociatedObject_LoadingRow
  /// 새로운 로우가 로딩될 때 호출되는 이벤트 핸들러입니다.
  /// 로딩되는 로우에 대해 행 번호를 설정합니다.
  /// </summary>
  private void AssociatedObject_LoadingRow(object? sender, DataGridRowEventArgs e)
  {
    // 새로운 로우에 대해서 인덱스 + 1을 Header로 설정
    if (e.Row != null)
    {
      e.Row.Header = e.Row.GetIndex() + 1;
    }
  }

  /// <summary>
  /// OnDetaching
  /// Behavior가 DataGrid에서 분리될 때 호출됩니다.
  /// 이벤트 핸들러를 해제하여 메모리 누수를 방지합니다.
  /// </summary>
  protected override void OnDetaching()
  {
    if (ShowRowNumber)
    {
      // 이벤트 핸들러를 해제하여 메모리 누수를 방지
      // null 체크 후 이벤트를 해제하는 방식으로 안전하게 처리
      if (AssociatedObject != null)
      {
        AssociatedObject.LoadingRow -= AssociatedObject_LoadingRow;
        AssociatedObject.UnloadingRow -= AssociatedObject_UnloadingRow;
      }
    }
  }
}
/* XAML 에서 사용방법
<!-- DataGrid -->
<DataGrid Grid.Row="1"
          Style="{StaticResource ReusableDataGridStyle}"
          ItemsSource="{Binding Brands}"
          SelectedItem="{Binding SelectedBrand, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
          SelectionMode="Single"
          IsReadOnly="True">

  <!-- 행 번호 출력 -->
  <b:Interaction.Behaviors>
    <units:DataGridRowNumberBehavior ShowRowNumber="True"/>
  </b:Interaction.Behaviors>
 
  <DataGrid.Columns>
    <!--<DataGridTextColumn Header="#" Binding="{Binding RelativeSource={RelativeSource AncestorType=DataGridRow},
                Converter={units:DataGridRowIndexConverter}}" />-->
    <!--<DataGridTextColumn Header="ID" Binding="{Binding Id}" Width="Auto"/>-->
    <DataGridTextColumn Header="이름" Binding="{Binding Name}" Width="Auto"/>
    <DataGridTextColumn Header="회사" Binding="{Binding Company}" Width="Auto"/>
    <DataGridTextColumn Header="생성일" Binding="{Binding CreatedDateFormatted}" Width="Auto"/>
    <DataGridCheckBoxColumn Header="활성" Binding="{Binding IsActive}" Width="Auto"/>
    <DataGridTextColumn Header="생성자" Binding="{Binding CreatedByDisplay}" Width="*" />
    <DataGridTextColumn Header="수정자" Binding="{Binding ModifiedByDisplay}" Width="*" />
  </DataGrid.Columns>
</DataGrid>
 
 
 */
