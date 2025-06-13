using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Heimdallr.ToolKit.Converters;

/// <summary>
/// 다중 값(2개의 bool 값)을 입력받아 Visibility 상태를 결정하는 WPF 컨버터
/// </summary>
public class MultiBooleanToVisibilityConverter : MarkupExtension, IMultiValueConverter
{
  /// <summary>
  /// 두 개의 bool 값을 받아 Visibility 상태를 반환하는 메서드
  /// </summary>
  /// <param name="values">Binding 으로 전달된 다중 값 배열</param>
  /// <param name="targetType">타겟 타입 (보통 Visibility)</param>
  /// <param name="parameter">추가 파라미터 (사용하지 않음)</param>
  /// <param name="culture">문화권 정보</param>
  /// <returns>Visibility.Visible 또는 Visibility.Collapsed</returns>
  public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
  {
    // 입력 값 검증: 값 개수가 2개가 아니거나, 각 값이 bool 타입이 아니면
    if (values.Length != 2 || !(values[0] is bool isLoggedIn) || !(values[1] is bool isLoginButton))
    {
      // 조건 중 하나라도 만족하지 않으면 보이지 않도록 설정
      return Visibility.Collapsed;
    }

    // 사용자가 로그인하지 않았고, 로그인 버튼이 표시 상태일 경우
    if (!isLoggedIn && isLoginButton)
    {
      // 로그인 버튼을 화면에 표시
      return Visibility.Visible;
    }

    // 그 외의 모든 경우에는 화면에 표시하지 않음
    return Visibility.Collapsed;
  }

  /// <summary>
  /// ConvertBack은 구현되지 않음 (단방향 바인딩용)
  /// </summary>
  /// <param name="value"></param>
  /// <param name="targetTypes"></param>
  /// <param name="parameter"></param>
  /// <param name="culture"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// MarkupExtension 기능을 통해 XAML에서 직접 사용 가능하게 함
  /// </summary>
  /// <param name="serviceProvider"></param>
  /// <returns></returns>
  public override object ProvideValue(IServiceProvider serviceProvider) => this;

}

/* 사용예제
public class MainViewModel : INotifyPropertyChanged
{
  private bool _isLoggedIn;
  private bool _isLoginButton;

  public bool IsLoggedIn
  {
      get => _isLoggedIn;
      set
      {
          _isLoggedIn = value;
          OnPropertyChanged(nameof(IsLoggedIn));
      }
  }

  public bool IsLoginButton
  {
      get => _isLoginButton;
      set
      {
          _isLoginButton = value;
          OnPropertyChanged(nameof(IsLoginButton));
      }
  }

  public MainViewModel()
  {
      IsLoggedIn = false;
      IsLoginButton = true;
  }

  public event PropertyChangedEventHandler? PropertyChanged;
  protected void OnPropertyChanged(string propertyName)
      => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

<Button Content="로그인"
        Width="100"
        Height="30"
        HorizontalAlignment="Center"
        Margin="10">
    <Button.Visibility>
        <MultiBinding Converter="{StaticResource MultiBoolToVisibility}">
            <Binding Path="IsLoggedIn" />
            <Binding Path="IsLoginButton" />
        </MultiBinding>
    </Button.Visibility>
</Button>

IsLoggedIn == false AND IsLoginButton == true ⟶ Visible

그 외 경우 ⟶ Collapsed (숨김)

예제 동작 테스트
// ViewModel 상태 변경 테스트
public partial class MainWindow : Window
{
  private MainViewModel viewModel = new MainViewModel();

  public MainWindow()
  {
    InitializeComponent();
    DataContext = viewModel;

    // 예시: 3초 후 로그인 처리됨 → 버튼 숨겨짐
    Task.Delay(3000).ContinueWith(_ =>
    {
        Dispatcher.Invoke(() =>
        {
            viewModel.IsLoggedIn = true; // 이제 로그인 버튼이 사라짐
        });
    });
  }
}

*/

