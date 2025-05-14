namespace Heimdallr.ToolKit.Enums;

/// <summary>
/// UI 요소의 정렬 방식을 정의하기 위한 열거형(enum)
/// 수평 레이아웃 배치에서 자식 요소들 사이의 간격을 어떻게 분배할지를 설정할 때 사용됩니다. 
/// 이 값들은 Flexbox, DockPanel, WrapPanel, StackPanel 같은 레이아웃 시스템에서 
/// 자주 사용되는 정렬 개념을 반영하고 있습니다.
/// </summary>
public enum JustifyEnum
{
  None,
  SpaceAround,  // 자식 요소들 간의 간격을 고르게 분배
  SpaceBetween, // 자식 요소들 간의 간격만 균등하게 배치
  SpaceEvenly   // 자식 요소들 간의 간격을 고르게 분배
}

/* 사용예제
<Window x:Class="JustifyEnumExample.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="JustifyEnum Example" Height="200" Width="400">
  <Window.Resources>
    <ObjectDataProvider x:Key="JustifyEnumProvider" MethodName="GetValues" ObjectType="{x:Type sys:Enum}">
      <ObjectDataProvider.MethodParameters>
          <x:Type TypeName="local:JustifyEnum" />
      </ObjectDataProvider.MethodParameters>
    </ObjectDataProvider>
  </Window.Resources>
  <Grid>
    <StackPanel Orientation="Horizontal" HorizontalAlignment="Stretch">
        <!-- JustifyEnum.SpaceAround -->
        <Button Content="SpaceAround" Margin="10" HorizontalAlignment="Center" />
        <Button Content="SpaceAround" Margin="10" HorizontalAlignment="Center" />
        <Button Content="SpaceAround" Margin="10" HorizontalAlignment="Center" />
    </StackPanel>

    <!-- JustifyEnum.SpaceBetween -->
    <StackPanel Orientation="Horizontal" HorizontalAlignment="Stretch" VerticalAlignment="Top" Margin="10">
        <Button Content="SpaceBetween" HorizontalAlignment="Stretch" />
        <Button Content="SpaceBetween" HorizontalAlignment="Stretch" />
        <Button Content="SpaceBetween" HorizontalAlignment="Stretch" />
    </StackPanel>
        
    <!-- JustifyEnum.SpaceEvenly -->
    <StackPanel Orientation="Horizontal" HorizontalAlignment="Stretch" VerticalAlignment="Top" Margin="20">
        <Button Content="SpaceEvenly" HorizontalAlignment="Stretch" />
        <Button Content="SpaceEvenly" HorizontalAlignment="Stretch" />
        <Button Content="SpaceEvenly" HorizontalAlignment="Stretch" />
    </StackPanel>
  </Grid>
</Window>
*/