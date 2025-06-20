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

  // 단위 선택지: Key = ProductUnit enum, Value = 표시 이름
  public ObservableCollection<KeyValuePair<ProductUnit, string>> ProductUnitOptions { get; }

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

    #region ComboBox, NumericUpDown Demo
    // 단위 옵션 초기화 (enum 전체를 순회하여 KeyValuePair 컬렉션 생성)
    ProductUnitOptions = new ObservableCollection<KeyValuePair<ProductUnit, string>>(
        Enum.GetValues(typeof(ProductUnit))
        .Cast<ProductUnit>()
        .Select(u => new KeyValuePair<ProductUnit, string>(u, u.GetDescription()))
    );

    SelectedUnit = ProductUnit.Dozen; // 초기 선택 단위 (Step=12) 
    #endregion
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


  #region NumbericUpDown

  public string SelectedUnitDisplayName => SelectedUnit.GetDescription();
  /// <summary>
  private ProductUnit _selectedUnit = ProductUnit.Dozen;  // 초기값 설정

  public ProductUnit SelectedUnit
  {
    get => _selectedUnit;
    set
    {
      if (_selectedUnit != value)
      {
        _selectedUnit = value;
        OnPropertyChanged(nameof(SelectedUnit));
        UpdateStep();  // SelectedUnit 변경 시 Step 자동 업데이트
        OnPropertyChanged(nameof(SelectedUnitDisplayName)); // 표시명도 같이 갱신 가능
      }
    }
  }

  private double _step = 1;
  public double Step
  {
    get => _step;
    private set
    {
      if (_step != value)
      {
        _step = value;
        OnPropertyChanged(nameof(Step));
      }
    }
  }

  private void UpdateStep()
  {
    Step = SelectedUnit.GetQuantity();
  }
  #endregion

  #region PropressBar Test
  private DelegateCommand? _progressBarCommand;
  public DelegateCommand? ProgressBarCommand => _progressBarCommand ??= new DelegateCommand(async () => await StartProgressAsync(), () => true);

  private double _progressValue;
  public double ProgressValue
  {
    get => _progressValue;
    set
    {
      if (_progressValue != value)
      {
        _progressValue = value;
        OnPropertyChanged();
      }
    }
  }

  private bool _isRunning = false;

  /// <summary>
  /// 20초 동안 0 → 100 진행시키는 비동기 메서드
  /// </summary>
  public async Task StartProgressAsync()
  {
    if (_isRunning) return; // 중복 실행 방지
    _isRunning = true;

    int durationMs = 2_000;  // 총 20초
    int steps = 1;          // 0.1초 단위 (200번)
    double increment = 100.0 / steps;
    int delayMs = durationMs / steps;

    ProgressValue = 0;

    var stopwatch = System.Diagnostics.Stopwatch.StartNew();

    for (int i = 0; i <= steps; i++)
    {
      if (!_isRunning) break; // 외부에서 중단 요청 시

      var elapsed = stopwatch.ElapsedMilliseconds;

      if (elapsed >= durationMs)
      {
        ProgressValue = 100;
        break;
      }

      ProgressValue = increment * i;

      // 남은 시간과 delay 보정
      int remainingMs = (int)(durationMs - elapsed);
      await Task.Delay(Math.Min(delayMs, remainingMs));
    }

    stopwatch.Stop();

    ProgressValue = 100;  // 루프 종료 후 100% 고정
    _isRunning = false;

    OnProgressCompleted();
  }

  /// <summary>
  /// 진행 완료 이벤트
  /// </summary>
  public event Action? ProgressCompleted;

  /// <summary>
  /// 완료 시 이벤트 호출
  /// </summary>
  private void OnProgressCompleted()
  {
    ProgressCompleted?.Invoke();
  }

  /// <summary>
  /// 외부에서 강제 중단 요청
  /// </summary>
  public void StopProgress()
  {
    _isRunning = false;
  }
  #endregion

}



