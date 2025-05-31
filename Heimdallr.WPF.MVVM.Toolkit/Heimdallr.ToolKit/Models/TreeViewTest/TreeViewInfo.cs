using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Heimdallr.ToolKit.Models.TreeViewTest;

public partial class TreeViewInfo : TreeViewInfoBase
{
  [ObservableProperty]
  private bool _isTreeViewExpanded;
  [ObservableProperty]
  private bool _isTreeViewSelected;

  public ObservableCollection<TreeViewInfo>? Children { get; set; }

  // https://www.youtube.com/watch?v=KtjC8a-BA1g&list=PLcJ2AlGDlF0sVi_GqSVatXw6M4-U-UIti&index=2
  // 1시간 50분까지 
}
