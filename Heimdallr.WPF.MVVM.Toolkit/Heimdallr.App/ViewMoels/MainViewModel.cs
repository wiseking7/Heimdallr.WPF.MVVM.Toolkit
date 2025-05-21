using CommunityToolkit.Mvvm.Input;
using Heimdallr.ToolKit.Commons;
using Heimdallr.ToolKit.Models.TreeViewTest;
using System.Collections.ObjectModel;
using System.IO;

namespace Heimdallr.App.ViewMoels;

public partial class MainViewModel : ObservableBase
{
  public ObservableCollection<Person> People { get; set; }

  public MainViewModel()
  {
    People = new ObservableCollection<Person>
            {
                new Person { Name = "John", Age = 30 },
                new Person { Name = "Jane", Age = 25 },
                new Person { Name = "Mark", Age = 35 },
                new Person { Name = "Sara", Age = 28 }
            };

    // TreeView Test
    int depth = 0;
    FileCreator fileCreator = new();
    fileCreator.Crate();

    string root = fileCreator.BasePath + "/Vicky";
    List<FileItem> source = new();

    GetFiles(root, source, depth);

    Files = source;
  }

  // TreeView Test
  private void GetFiles(string root, List<FileItem> source, int depth)
  {
    string[] dirs = Directory.GetDirectories(root);
    string[] files = Directory.GetFiles(root);

    foreach (string dir in dirs)
    {
      FileItem item = new();
      item.Name = Path.GetFileNameWithoutExtension(dir);
      item.Path = dir;
      item.Type = "Folder";
      item.Size = null;
      item.Depth = depth;
      item.Children = new();

      source.Add(item);

      GetFiles(dir, item.Children, depth + 1);
    }

    foreach (string file in files)
    {
      FileItem item = new();
      item.Name = Path.GetFileNameWithoutExtension(file);
      item.Path = file;
      item.Type = "Folder";
      item.Size = new FileInfo(file).Length;
      item.Type = "File";
      item.Extension = new FileInfo(file).Extension;
      item.Depth = depth;

      source.Add(item);
    }
  }

  #region TreeViewText
  public List<FileItem>? Files { get; set; }

  #endregion

  [RelayCommand]
  private void Selection(FileItem item)
  {
    string? name = item.Name;
  }
}

public class Person
{
  public string? Name { get; set; }
  public int Age { get; set; }
}
