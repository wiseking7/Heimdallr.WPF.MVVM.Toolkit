using CommunityToolkit.Mvvm.Input;
using Heimdallr.ToolKit.Commons;
using Heimdallr.ToolKit.Models.TreeViewTest;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Heimdallr.App.ViewMoels;

public partial class MainViewModel : ObservableBase
{
  private readonly IEventAggregator? _eventAggregator;
  public string? TextMessage { get; set; }
  public ObservableCollection<Person> People { get; set; }

  public DelegateCommand HoldSubmitButtonCommamd => new DelegateCommand(ExecuteHoldSubmit);

  private void ExecuteHoldSubmit()
  {
    MessageBox.Show("Hold Button");
  }

  public MainViewModel(IEventAggregator eventAggregator)
  {

    _eventAggregator = eventAggregator;

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

    // HeimdallrTerrView
    TextMessage = "MainViewModel!!";

    ButtonClick = new DelegateCommand(OnButton);
  }

  private string? _myTextBox;
  public string? MyTextbox
  {
    get { return _myTextBox; }
    set { SetProperty(ref _myTextBox, value); }
  }

  private bool _isLoading;
  public bool IsLoading
  {
    get => _isLoading;
    set => SetProperty(ref _isLoading, value);
  }

  private async void OnButton()
  {
    if (!double.TryParse(MyTextbox, out double duration))
    {
      duration = 5;
    }

    IsLoading = true;

    await Task.Delay(TimeSpan.FromSeconds(duration));
    IsLoading = false;
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

  // 데이터 로딩 예시
  public async Task LoadDataAsync()
  {
    // 로딩 시작
    IsLoading = true;

    // 데이터 로딩 시뮬레이션
    await Task.Delay(3000); // 예시로 3초 지연

    // 로딩 완료
    IsLoading = false;
  }

  public DelegateCommand? ButtonClick { get; }
}

public class Person
{
  public string? Name { get; set; }
  public int Age { get; set; }
}
