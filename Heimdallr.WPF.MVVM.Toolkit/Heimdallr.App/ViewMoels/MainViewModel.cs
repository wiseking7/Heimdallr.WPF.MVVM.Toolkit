using Heimdallr.App.Model;
using Heimdallr.ToolKit.Commons;
using Heimdallr.ToolKit.Enums;
using Heimdallr.ToolKit.Extensions;
using Heimdallr.ToolKit.UI.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace Heimdallr.App.ViewMoels;

public partial class MainViewModel : ObservableBase
{
  public string? TextMessage { get; set; }

  public DelegateCommand? ButtonTextCommamd => new DelegateCommand(ExecuteHoldSubmit);

  private void ExecuteHoldSubmit()
  {
    MessageBox.Show("Button Test Command");
  }

  public MainViewModel()
  {
    _selectedCountry = string.Empty; // Initialize the non-nullable field to avoid CS8618  

    People = new ObservableCollection<Person>()
     {
         new Person
         {
             Name = "이순신", Address="조선 종로", Age=543, BirthDay=new DateTime(2005,02,28)
         },
         new Person
         {
             Name = "을지문덕", Address="고려", Age=543, BirthDay=new DateTime(2004,02,28)
         },
         new Person
         {
             Name = "왕건", Address="고려", Age=543, BirthDay=new DateTime(2003,02,28)
         }
     };
  }

  #region ListView Demo
  public ObservableCollection<Person>? People { get; set; }



  public DelegateCommand<string>? ColumnHeaderClickCommand => new DelegateCommand<string>(OnColumnHeaderClicked);
  public DelegateCommand<Person>? PersonItemDoubleClickCommand => new DelegateCommand<Person>(OnPersonItemDoubleClicked);

  private void OnPersonItemDoubleClicked(Person person)
  {
    if (person != null)
    {
      MessageBox.Show($"[MainViewModel] Double clicked: {person.Name}");
    }
  }

  private void OnColumnHeaderClicked(string header)
  {
    // 예: 헤더 이름에 따라 정렬 로직 실행
    MessageBox.Show($"Clicked Column Header: {header}");
  }

  #endregion

  #region ComboBox Demo
  public ObservableCollection<string> PeopleNames { get; } = new()
  {
      "Alice", "Bob", "Charlie"
  };


  private string _selectedCountry;
  public string SelectedCountry
  {
    get => _selectedCountry;
    set { _selectedCountry = value; OnPropertyChanged(); }
  }
  #endregion

  #region ComboBox Demo
  // ComboBox에 바인딩할 열거형 값 목록 확장(변경) 영문 -> 한글
  public IEnumerable<KeyValuePair<ProductUnit, string>> ProductUnitOptions =>
      ProductUnitExtensions.GetUnitPairs();

  private ProductUnit _selectedUnit;
  public ProductUnit SelectedUnit
  {
    get => _selectedUnit;
    set => SetProperty(ref _selectedUnit, value);
  }
  #endregion

  #region Dimming
  private bool _dimming;
  public bool Dimming
  {
    get => _dimming;
    set => SetProperty(ref _dimming, value);
  }

  private double _dimmingOpacity = 6.0;
  public double DimmingOpacity
  {
    get => _dimmingOpacity;
    set => SetProperty(ref _dimmingOpacity, value);
  }

  private Brush _dimmingColor = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)); // 반투명 블랙
  public Brush DimmingColor
  {
    get => _dimmingColor;
    set => SetProperty(ref _dimmingColor, value);
  }
  #endregion

  #region ToggleButton
  private bool _isMenuOpen;
  public bool IsMenuOpen
  {
    get => _isMenuOpen;
    set => SetProperty(ref _isMenuOpen, value);
  }

  private bool _isLoggedIn;

  public bool IsLoggedIn
  {
    get => _isLoggedIn;
    set
    {
      if (_isLoggedIn != value)
      {
        _isLoggedIn = value;
        OnPropertyChanged(nameof(IsLoggedIn));
        OnPropertyChanged(nameof(IsMenuVisibility));
      }
    }
  }

  public Visibility IsMenuVisibility => IsLoggedIn ? Visibility.Visible : Visibility.Hidden;

  private DelegateCommand? _loginCommand;
  public DelegateCommand? LoginCommand => _loginCommand ??= new DelegateCommand(ExecuteLogin);


  private DelegateCommand? _informationCommand;
  public DelegateCommand? InformationCommand => _informationCommand ??= new DelegateCommand(ExecuteOepnInformationView);

  private void ExecuteOepnInformationView()
  {
    // 팝업을 새로 띄움
    InformationWindow window = new InformationWindow();

    // 현재 활성 창이 있다면 Owner 설정
    window.Owner = Application.Current.Windows
        .OfType<InformationWindow>()
        .FirstOrDefault(w => w.IsActive);

    window.ShowDialog(); // 또는 Show()로 비모달
  }

  private void ExecuteLogin()
  {
    IsLoggedIn = true;
    Username = "로그인시 사용자이름표기"; // 로그인 후 사용자 이름 설정  
  }

  private string? _username;
  public string? Username
  {
    get => _username;
    set => SetProperty(ref _username, value);
  }
  #endregion

}

