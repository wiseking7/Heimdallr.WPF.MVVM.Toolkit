using Heimdallr.App.Model;
using Heimdallr.ToolKit.Commons;
using Heimdallr.ToolKit.UI.Controls;
using System.Collections.ObjectModel;
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

  private DelegateCommand? _informationCommand;
  public DelegateCommand? InformationCommand => _informationCommand ??= new DelegateCommand(ExecuteOepnInformationView);

  private void ExecuteLogin()
  {
    Username = "로그인시 사용자이름표기"; // 로그인 후 사용자 이름 설정  
  }

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


  #endregion
  public ObservableCollection<Person> People { get; } = new();

  private DelegateCommand? _loadUsersCommand;
  public DelegateCommand LoadUsersCommand =>
      _loadUsersCommand ??= new DelegateCommand(async () => await LoadUsersAsync(), () => !IsBusy)
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
      var dummyUsers = new List<Person>
      {
        new Person { Id = 1, Name = "홍길동", Age = 25, BirthDay = DateTime.Now, Address ="조선" },
        new Person { Id = 2, Name = "김철수", Age = 45, BirthDay = DateTime.Now, Address ="한국" }
      };

      await RunOnUiThread(async () =>
      {
        // UI 스레드에서 ObservableCollection에 데이터 추가
        People.Clear();
        foreach (var user in dummyUsers)
        {
          People.Add(user);
        }

        // UI 스레드에서 비동기 작업을 실행하기 위해 await 사용
        await Task.CompletedTask;
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
}




