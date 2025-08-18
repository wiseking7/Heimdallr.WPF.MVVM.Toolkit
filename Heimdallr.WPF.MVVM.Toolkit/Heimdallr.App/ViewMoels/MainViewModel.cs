using Heimdallr.App.Model;
using Heimdallr.ToolKit.Commons;
using Heimdallr.ToolKit.UI.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace Heimdallr.App.ViewMoels;

public class MainViewModel : ViewModelBase
{
  #region 필수 속성 패턴
  private bool _isMenuOpen;
  public bool IsMenuOpen
  {
    get => _isMenuOpen;
    set => SetProperty(ref _isMenuOpen, value);
  }
  private string? _username;
  public string? Username
  {
    get => _username;
    set => SetProperty(ref _username, value);
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


  public MainViewModel(IContainerProvider container) : base(container)
  {
    Title = "사용자 목록";
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
        new Person { Id = 2, Name = "김철수", Age = 45, BirthDay = DateTime.Now, Address ="한국" }
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
}




