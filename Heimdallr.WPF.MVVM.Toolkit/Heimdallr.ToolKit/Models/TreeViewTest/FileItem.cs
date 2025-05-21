namespace Heimdallr.ToolKit.Models.TreeViewTest;

public class FileItem
{
  public string? Name { get; set; }
  public string? Path { get; set; }
  public string? Type { get; set; }
  public string? Extension { get; set; }
  public long? Size { get; set; }
  public int Depth { get; set; }

  // TreeView 의 하위 자식
  public List<FileItem>? Children { get; set; }
}
