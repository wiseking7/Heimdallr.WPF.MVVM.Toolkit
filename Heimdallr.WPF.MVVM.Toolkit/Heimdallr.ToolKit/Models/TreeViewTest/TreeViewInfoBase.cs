using CommunityToolkit.Mvvm.ComponentModel;
using Heimdallr.ToolKit.Enums;

namespace Heimdallr.ToolKit.Models.TreeViewTest;

public partial class TreeViewInfoBase : ObservableObject
{
  [ObservableProperty]
  private bool _isDenied;
  public string? Name { get; set; }
  public int Depth { get; set; }
  public IconType IconType { get; set; }
}
