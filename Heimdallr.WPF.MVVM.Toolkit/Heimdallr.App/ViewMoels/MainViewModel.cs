using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Heimdallr.App.ViewMoels;

public class MainViewModel : INotifyPropertyChanged
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
  }

  public event PropertyChangedEventHandler? PropertyChanged;
  protected void OnPropertyChanged(string propertyName) =>
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}


public class Person
{
  public string? Name { get; set; }
  public int Age { get; set; }
}
