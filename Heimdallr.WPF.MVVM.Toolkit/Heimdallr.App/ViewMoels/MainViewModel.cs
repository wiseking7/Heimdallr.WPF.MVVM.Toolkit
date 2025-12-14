using Heimdallr.App.Model;
using Heimdallr.ToolKit.Commons;
using Heimdallr.ToolKit.UI.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace Heimdallr.App.ViewMoels;

public class MainViewModel : ViewModelBase
{
  #region 필수 속성 패턴
  private bool _isMenuOpen;
  public bool IsMenuOpen
  {
    get => _isMenuOpen;
    set => SetPropertyAndValidate(ref _isMenuOpen, value);
  }
  private string? _username;
  public string? Username
  {
    get => _username;
    set => SetPropertyAndValidate(ref _username, value); // 검증 자동실행
  }

  public string? _number;
  public string? Number
  {
    get => _number;
    set => SetPropertyAndValidate(ref _number, value);
  }

  public string? _numberString;
  public string? NumberString
  {
    get => _numberString;
    set => SetPropertyAndValidate(ref _numberString, value);
  }

  #endregion

  #region 필수 Commands
  private DelegateCommand? _loginCommand;
  public DelegateCommand? LoginCommand => _loginCommand ??= new DelegateCommand(ExecuteLogin);

  private AsyncDelegateCommand? _informationCommand;
  public AsyncDelegateCommand? InformationCommand => _informationCommand ??=
    new AsyncDelegateCommand(ExecuteOpenInformationViewAsync);

  private AsyncDelegateCommand? _testTreeViewCommand;
  public AsyncDelegateCommand? TestTreeViewCommand => _testTreeViewCommand
    ??= new AsyncDelegateCommand(OnTreeViewChedk);

  private DelegateCommand? _editPersonCommand;
  public DelegateCommand? EditPersonCommand => _editPersonCommand ??= new DelegateCommand(async () =>
  {
    await Task.CompletedTask;
  });

  private async Task OnTreeViewChedk()
  {
    MessageBox.Show("확인됨");
    await Task.CompletedTask;
  }

  private void ExecuteLogin()
  {
    Username = "로그인시 사용자이름표기"; // 로그인 후 사용자 이름 설정  
  }

  private async Task ExecuteOpenInformationViewAsync()
  {
    // 팝업을 새로 띄움
    InformationWindow window = new InformationWindow();

    // 현재 활성 창이 있다면 Owner 설정
    window.Owner = Application.Current.Windows
        .OfType<InformationWindow>()
        .FirstOrDefault(w => w.IsActive);

    // ShowDialog는 UI 스레드 차단. 비동기로 래핑하려면 별도 처리가 필요
    // 하지만 일반적으로 팝업은 동기 실행
    // 따라서 그냥 호출하고 Task.CompletedTask 반환해도 무방

    // window.Show(); // 사용하면 두 창을 동시에 조작할 수 있습니다.
    // window.ShowDialog(); // 모달 창으로 띄우면 현재 창을 차단하고 팝업이 닫힐 때까지 대기합니다.
    window.Show();

    await Task.CompletedTask;  // async 메서드라 await 필요
  }
  #endregion
  public ObservableCollection<Person> People { get; } = new();

  private AsyncDelegateCommand? _loadUsersCommand;
  public AsyncDelegateCommand LoadUsersCommand =>
      _loadUsersCommand ??= new AsyncDelegateCommand(async () => await LoadUsersAsync(), () => !IsBusy)
          .ObservesProperty(() => IsBusy);

  public DelegateCommand? _saveCommand;
  public DelegateCommand? SaveCommand => _saveCommand ??= new DelegateCommand(async () =>
  {
    bool isvalid = await SaveAsync();
    if (isvalid)
    {
      MessageBox.Show("저장 성공!");
    }
    else
    {
      // 저장 실패 시, HeimdallrMessageBox를 사용하여 오류 메시지 표시
      MessageBoxResult result = HeimdallrMessageBox.Show(
          "입력값을 확인하세요.",    // 메시지 내용
          "오류",
          MessageBoxButton.OK,     // 버튼 설정 (OK 버튼만 표시)
          MessageBoxImage.Error   // 아이콘 설정 (Error 아이콘 표시)
      );

      // 만약 버튼 클릭 후 추가 로직이 필요하다면, 'result' 값을 사용하여 추가 처리를 할 수 있습니다.
      if (result == MessageBoxResult.OK)
      {
        // OK 버튼 클릭 후 처리할 코드
        Debug.WriteLine($"[{nameof(MainViewModel)}.{MethodBase.GetCurrentMethod()?.Name}] OK 버튼 클릭됨");
      }
    }
  });

  private DelegateCommand? _messageCommand;
  public DelegateCommand? MessageCommand => _messageCommand ??= new DelegateCommand(() =>
  {
    MessageBox.Show("버튼을 클릭 성공");
  });

  public MainViewModel(IContainerProvider container) : base(container)
  {
    Title = "사용자 목록";

    ComboBoxItems = new ObservableCollection<Item>
    {
        new Item { Key = "1", Value = "Item 1" },
        new Item { Key = "2", Value = "Item 2" },
        new Item { Key = "3", Value = "Item 3" }
    };

    _ = LoadUsersAsync();

    AddValidationRules();

    // ErrorsChanged 이벤트를 구독해서 자동 삭제 (ViewModelBase 에서 상속)
    ErrorsChanged += OnErrorsChangedAutoClear;
  }

  private async Task LoadUsersAsync()
  {
    // 기본작업 취소
    CancelCurrentTask();

    IsBusy = true;

    BusyMessage = "사용자 목록을 불러오는 중...";

    try
    {
      // 실제 API 호출이나 데이터베이스 쿼리 등 비동기 작업을 시뮬레이션합니다.
      await Task.Delay(2000, CancellationToken);

      // 예시 데이터 생성
      var dummyPeoples = new List<Person>
      {
        new Person { Id = 1, Name = "홍길동", Age = 25, BirthDay = DateTime.Now, Address ="조선" },
        new Person { Id = 2, Name = "김철수", Age = 45, BirthDay = DateTime.Now, Address ="한국" },
        new Person { Id = 3, Name = "이영희", Age = 30, BirthDay = DateTime.Now, Address ="대한민국" },
        new Person { Id = 4, Name = "박민수", Age = 28, BirthDay = DateTime.Now, Address ="서울" },
        new Person { Id = 5, Name = "최지은", Age = 35, BirthDay = DateTime.Now, Address ="부산" },
        new Person { Id = 6, Name = "장보고", Age = 40, BirthDay = DateTime.Now, Address ="완도" },
        new Person { Id = 7, Name = "신사임당", Age = 50, BirthDay = DateTime.Now, Address ="강릉" },
        new Person { Id = 8, Name = "세종대왕", Age = 60, BirthDay = DateTime.Now, Address ="조선" },
        new Person { Id = 9, Name = "이순신", Age = 55, BirthDay = DateTime.Now, Address ="한산도" }
      };

      await RunOnUiThread(() =>
      {
        // UI 스레드에서 ObservableCollection에 데이터 추가
        People.Clear();
        foreach (var person in dummyPeoples)
        {
          People.Add(person);
        }

        // UI 스레드에서 비동기 작업을 실행하기 위해 await 사용
        return Task.CompletedTask;
      });

      // BusyMessage 완료메세지 설정
      BusyMessage = "사용자 목록 불러오기가 완료되었습니다.";
    }
    catch (OperationCanceledException)
    {
      BusyMessage = "사용자 목록 불러오기가 취소되었습니다.";
    }
    finally
    {
      IsBusy = false;
    }
  }

  #region INavigationAware 구현
  /// <summary>
  /// 사용자가 MainViewModel에 진입할 때 자동으로 LoadUsersCommand가 실행됩니다.
  /// 이는 페이지가 네비게이션 될 때 사용자를 자동으로 불러오는 작업을 시뮬레이션합니다.
  /// </summary>
  /// <param name="navigationContext"></param>
  public override void OnNavigatedTo(NavigationContext navigationContext)
  {
    base.OnNavigatedTo(navigationContext);

    // 진입시 사용자 자동 로딩
    if (People.Count == 0)
    {
      LoadUsersCommand.Execute();
    }
  }

  /// <summary>
  /// 페이지를 떠날 때 호출됩니다. 
  /// 여기서는 현재 진행 중인 비동기 작업을 취소하고, 상태를 정리하는 코드가 포함되어 있습니다
  /// </summary>
  /// <param name="navigationContext"></param>
  public override void OnNavigatedFrom(NavigationContext navigationContext)
  {
    base.OnNavigatedFrom(navigationContext);

    // 필요시 상태 저장 또는 리소스 정리
    CancelCurrentTask();
  }

  /// <summary>
  /// 현재 페이지가 네비게이션 대상인지 확인합니다. 
  /// 여기서는 true를 반환하여 현재 뷰모델이 새로 생성되지 않고 재사용된다는 의미입니다.
  /// </summary>
  /// <param name="navigationContext"></param>
  /// <returns></returns>
  public override bool IsNavigationTarget(NavigationContext navigationContext)
  {
    // true: 동일 뷰Model 재사용
    // false: 항상 새로 생성
    return true;
  }
  #endregion

  #region
  private Lazy<AsyncDelegateCommand>? _loadDataCommand;

  public AsyncDelegateCommand LoadDataCommand =>
      (_loadDataCommand ??= new Lazy<AsyncDelegateCommand>(() =>
          new AsyncDelegateCommand(LoadDataAsync, () => !IsBusy)
              .ObservesProperty(() => IsBusy)))
      .Value;

  private bool _isBusy;

  /// <summary>
  /// ViewModel의 IsBusy 속성은 현재 작업이 진행 중인지 여부를 나타냅니다(new 표기).
  /// </summary>
  public new bool IsBusy
  {
    get => _isBusy;
    set
    {
      SetProperty(ref _isBusy, value);
      // 명시적 호출 필요 없지만 안전하게 한 번 더 호출 가능
      LoadDataCommand.RaiseCanExecuteChanged();
    }
  }

  private async Task LoadDataAsync()
  {
    IsBusy = true;
    try
    {
      await Task.Delay(2000);
      Debug.WriteLine("데이터 로딩 완료");
    }
    finally
    {
      IsBusy = false;
    }
  }
  #endregion

  public class Item
  {
    public string? Key { get; set; }
    public string? Value { get; set; }
  }
  public ObservableCollection<Item> ComboBoxItems { get; set; }

  #region ViewModelBase text
  private string _name = string.Empty;

  public string Name
  {
    get => _name;
    set => SetPropertyAndValidate(ref _name, value);
  }

  //테스트메서드
  private void AddValidationRules()
  {
    // 동기 검증
    AddRule(nameof(Name), () =>
    {
      var errors = new List<string>();
      if (string.IsNullOrWhiteSpace(Name))
        errors.Add("이름은 필수 입력 항목입니다.");
      else if (Name.Length < 3)
        errors.Add("이름은 3글자 이상이어야 합니다.");
      return errors;
    });

    // 비동기 검증
    AddRuleAsync(nameof(Name), async () =>
    {
      await Task.Delay(200); // 서버 호출 시뮬레이션
      if (Name == "admin")
        return new List<string> { "이 이름은 이미 사용 중입니다." };
      return new List<string>();
    });
  }

  private void OnErrorsChangedAutoClear(object? sender, DataErrorsChangedEventArgs e)
  {
    // 오류가 있으면 3초 후 자동 제거
    if (GetErrors(e.PropertyName).Cast<string>().Any())
      _ = AutoClearErrorAsync(e.PropertyName!, 3000);
  }

  private async Task AutoClearErrorAsync(string propertyName, int delayMilliseconds)
  {
    await Task.Delay(delayMilliseconds);
    ClearErrors(propertyName);
  }

  public async Task<bool> SaveAsync()
  {
    bool isValid = await ValidateAllAndReturnAsync();
    if (isValid)
    {
      // 실제 저장 로직 수행
      // 예: DB 저장, API 호출 등
    }
    return isValid;
  }

  protected override void OnErrorsChanged(string propertyName)
  {
    base.OnErrorsChanged(propertyName);

    // 전체 오류 메시지 속성 갱신 알림
    RaisePropertyChanged(nameof(ErrorMessage));
  }

  public string ErrorMessage => AllErrors;
  #endregion
}




