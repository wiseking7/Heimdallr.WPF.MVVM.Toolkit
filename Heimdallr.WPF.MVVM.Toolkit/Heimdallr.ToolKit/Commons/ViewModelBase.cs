using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Heimdallr.ToolKit.Commons;

/// <summary>
/// INotifyDataErrorInfo를 확장한 ViewModel 기본 클래스
/// - Prism ViewModelBase 상속 (INotifyPropertyChanged, DI, EventAggregator 등 지원)
/// - 속성별 동기/비동기 검증 지원
/// - UI 스레드 안전하게 오류 이벤트 전달
/// - 여러 ViewModel에서 재사용 가능
/// </summary>
public class ViewModelBase : BaseViewModel, INotifyDataErrorInfo
{
  #region 필드
  // 실제 오류 메시지를 속성별로 저장
  // key: 속성명, value: 오류 메시지 리스트
  private readonly Dictionary<string, List<string>> _errors = new();

  // 동기 검증 규칙 저장
  // key: 속성명, value: 검증 함수 리스트 (각 함수는 IEnumerable<string> 반환)
  private readonly Dictionary<string, List<Func<IEnumerable<string>>>> _syncRules = new();

  // 비동기 검증 규칙 저장
  // key: 속성명, value: 검증 함수 리스트 (각 함수는 Task<IEnumerable<string>> 반환)
  private readonly Dictionary<string, List<Func<Task<IEnumerable<string>>>>> _asyncRules = new();


  // UI 바인딩 가능한 HasErrors 속성
  private bool _hasErrorsBindable;
  #endregion

  #region 생성자
  /// <summary>
  /// 생성자
  /// - DI 컨테이너(IContainerProvider)를 상속받아 ViewModelBase 초기화
  /// </summary>
  /// <param name="container">Prism DI 컨테이너</param>
  protected ViewModelBase(IContainerProvider container) : base(container)
  {
  }
  #endregion

  #region 속성
  /// <summary>
  /// 전체 오류 존재 여부 확인
  /// - _errors 딕셔너리가 비어있지 않으면 true 반환
  /// </summary>
  public bool HasErrors => _errors.Any();

  /// <summary>
  /// UI 바인딩용 전체 오류 존재 여부
  /// - 예: 버튼 Enable/Disable 바인딩
  /// </summary>
  public bool HasErrorsBindable
  {
    get => _hasErrorsBindable;
    private set => SetProperty(ref _hasErrorsBindable, value);
  }

  /// <summary>
  /// 전체 오류 문자열
  /// - 예: 모든 속성 오류를 TextBlock이나 메시지 창에 표시
  /// </summary>
  public string AllErrors
  {
    get
    {
      var all = GetErrors(null)?.Cast<string>().ToList() ?? new List<string>();
      return string.Join(Environment.NewLine, all);
    }
  }

  /// <summary>
  /// 현재 ViewModel의 전체 오류 메시지 개수 반환
  /// - UI에서 오류 개수 표시 또는 로깅용
  /// - 예: "입력 오류 3건 발생" 표시
  /// </summary>
  public int HasErrorsCount => _errors.Sum(kv => kv.Value.Count);
  #endregion

  #region INotifyDataErrorInfo 이벤트
  /// <summary>
  /// INotifyDataErrorInfo 이벤트
  /// - 속성의 오류가 변경될 때 UI에 알리기 위해 사용
  /// </summary>
  public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

  /// <summary>
  /// 특정 속성 또는 모든 속성의 오류 반환
  /// - propertyName이 null이면 전체 속성의 오류를 반환
  /// - propertyName이 지정되면 해당 속성 오류 반환
  /// </summary>
  /// <param name="propertyName">속성 이름</param>
  /// <returns>오류 문자열 IEnumerable</returns>
  public IEnumerable GetErrors(string? propertyName)
  {
    // 1️⃣ 오류 딕셔너리가 비었으면 빈 컬렉션 반환
    if (_errors.Count == 0)
      return Enumerable.Empty<string>();

    // 2️⃣ 특정 속성 지정되지 않은 경우 → 전체 반환
    if (string.IsNullOrEmpty(propertyName))
    {
      return _errors
        .SelectMany(kv => kv.Value ?? Enumerable.Empty<string>())
        .ToList(); // 안전하게 materialize
    }

    // 3️⃣ 특정 속성의 오류 반환
    if (_errors.TryGetValue(propertyName, out var value))
      return value ?? Enumerable.Empty<string>();

    return Enumerable.Empty<string>();
  }
  #endregion

  #region 검증 규칙 등록
  /// <summary>
  /// 동기 검증 규칙 등록
  /// - 속성별로 여러 검증 함수를 등록 가능
  /// </summary>
  /// <param name="propertyName">속성 이름</param>
  /// <param name="rule">검증 함수: IEnumerable<string></string> 반환 (오류 메시지)</param>
  protected void AddRule(string propertyName, Func<IEnumerable<string>> rule)
  {
    if (!_syncRules.TryGetValue(propertyName, out var list))
    {
      // 속성에 대한 규칙 리스트가 없으면 새로 생성
      list = new List<Func<IEnumerable<string>>>();

      _syncRules[propertyName] = list;
    }

    // 검증 함수 추가
    list.Add(rule);
  }

  /// <summary>
  /// 비동기 검증 규칙 등록
  /// - 예: 서버 API 체크, DB 중복 확인 등 비동기 검증에 사용
  /// </summary>
  /// <param name="propertyName">속성 이름</param>
  /// <param name="asyncRule">검증 함수 반환</param>
  protected void AddRuleAsync(string propertyName, Func<Task<IEnumerable<string>>> asyncRule)
  {
    if (!_asyncRules.TryGetValue(propertyName, out var list))
    {
      // 속성에 대한 비동기 규칙 리스트가 없으면 새로 생성
      list = new List<Func<Task<IEnumerable<string>>>>();

      _asyncRules[propertyName] = list;
    }

    // 비동기 검증 함수 추가
    list.Add(asyncRule);
  }
  #endregion

  #region 검증 수행
  /// <summary>
  /// 특정 속성 검증 수행
  /// - 동기/비동기 규칙을 모두 실행
  /// - 오류가 있으면 SetErrors 호출하여 UI 이벤트 발생
  /// 오류 발생시 SerErrors 호출하여 UI 갱신
  /// 예외 발생시 DEBUB 모드에서 로그 기록
  /// </summary>
  /// <param name="propertyName">속성 이름</param>
  /// <returns>Task</returns>
  protected async Task ValidatePropertyAsync(string propertyName)
  {
    var errors = new List<string>();

    // 1. 동기 검증 실행
    if (_syncRules.TryGetValue(propertyName, out var syncList))
    {
      foreach (var rule in syncList)
      {
        try
        {
          // 검증함수 실행
          var res = rule();
          if (res != null)
            errors.AddRange(res.Where(s => !string.IsNullOrWhiteSpace(s)));
        }
        catch (Exception ex)
        {
          Debug.WriteLine($"[{nameof(ViewModelBase)}.{MethodBase.GetCurrentMethod()?.Name}] 실패: {propertyName} 에서 검증 규칙 오류: {ex.Message}");
          // 운영 환경에서는 로그 기록 가능
        }
      }
    }

    // 2. 비동기 검증 실행
    if (_asyncRules.TryGetValue(propertyName, out var asyncList))
    {
      foreach (var asyncRule in asyncList)
      {
        try
        {
          // 비동기 실행
          var res = await asyncRule().ConfigureAwait(false);
          if (res != null)
            errors.AddRange(res.Where(s => !string.IsNullOrWhiteSpace(s)));
        }
        catch (Exception ex)
        {
          Debug.WriteLine($"[{nameof(ViewModelBase)}.{MethodBase.GetCurrentMethod()?.Name}] 실패: {propertyName} 에서 검증 규칙 오류: {ex.Message}");
          // 운영 환경에서는 로그 기록 가능
        }
      }
    }

    // 오류 저장 및 UI 알림 이벤트 호출
    SetErrors(propertyName, errors);
  }

  /// <summary>
  /// 모든 속성 검증 수행
  /// - 등록된 모든 속성에 대해 ValidatePropertyAsync 실행
  /// </summary>
  /// <returns>Task</returns>
  protected Task ValidateAllAsync()
  {
    // 등록된 모든 속성 이름
    var propertyNames = _syncRules.Keys.Union(_asyncRules.Keys).Distinct().ToArray();

    return Task.WhenAll(propertyNames.Select(ValidatePropertyAsync));
  }

  /// <summary>
  /// 모든 속성 검증 수행 후 오류 여부 반환
  /// - 예: 저장 버튼 클릭 전 검증 체크
  /// </summary>
  /// <returns>오류가 없음 true </returns>
  public async Task<bool> ValidateAllAndReturnAsync()
  {
    await ValidateAllAsync();
    return !HasErrors;
  }
  #endregion

  #region 오류 처리

  /// <summary>
  /// 속성별 오류 설정
  /// - UI에 알리기 위해 ErrorsChanged 이벤트 호출
  /// - 이전 오류와 비교하여 변경이 있을 때만 이벤트 발생
  /// </summary>
  /// <param name="propertyName">속성 이름</param>
  /// <param name="errors">오류 문자열 컬렉션</param>
  private void SetErrors(string propertyName, IEnumerable<string> errors)
  {
    bool changed = false;

    if (errors != null && errors.Any())
    {
      var list = errors.ToList();

      // 기존 오류와 비교 (순서 무시)
      if (!_errors.TryGetValue(propertyName, out var exists) || !new HashSet<string>(exists).SetEquals(list))
      {
        _errors[propertyName] = list;
        changed = true; // 오류 내용이 변경된 경우에만 true
      }
    }
    else
    {
      // 오류 제거 (기존에 존재하면 true 반환)
      changed = _errors.Remove(propertyName);
    }

    // 바인딩용 속성 갱신
    HasErrorsBindable = _errors.Any();

    // 오류 상태가 실제로 변경된 경우에만 이벤트 발생
    if (changed)
      OnErrorsChanged(propertyName);
  }


  /// <summary>
  /// 특정 속성 오류 제거
  /// </summary>
  /// <param name="propertyName">속성 이름</param>
  protected void ClearErrors(string propertyName) => SetErrors(propertyName, Enumerable.Empty<string>());

  /// <summary>
  /// ErrorsChanged 이벤트 호출
  /// - UI 스레드에서 안전하게 호출
  /// </summary>
  /// <param name="propertyName">속성 이름</param>
  protected virtual void OnErrorsChanged(string propertyName)
  {
    // WPF UI 스레드에서 비동기 안전하게 호출
    if (Application.Current?.Dispatcher != null)
    {
      _ = Application.Current?.Dispatcher.InvokeAsync(() =>
          ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName)));
    }
    else
    {
      // UI 스레드가 없으면 직접 호출
      ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
  }
  #endregion

  #region 편의 메서드
  /// <summary>
  /// 속성 값 변경 + 자동 검증
  /// - 예: SetPropertyAndValidate(ref _name, value);
  /// </summary>
  protected bool SetPropertyAndValidate<T>(ref T storage, T value, [CallerMemberName] string propertyName = null!)
  {
    if (SetProperty(ref storage, value, propertyName))
    {
      _ = ValidatePropertyAsync(propertyName);
      return true;
    }
    return false;
  }

  /// <summary>
  /// 속성 초기화(기본값 설정) + 검증 + PropertyChanged 이벤트 발생
  /// - 사용 예: ResetProperty(ref _name);
  /// - UI 바인딩에서 값 변경과 오류 상태 모두 갱신됨
  /// </summary>
  protected void ResetProperty<T>(ref T storage, T defaultValue = default!, [CallerMemberName] string propertyName = null!)
  {
    storage = defaultValue!;
    _ = ValidatePropertyAsync(propertyName);
    RaisePropertyChanged(propertyName);
  }

  /// <summary>
  /// ViewModel의 모든 속성 초기화 후 검증
  /// - 등록된 모든 동기/비동기 검증 규칙을 기반으로 오류 상태 갱신
  /// - 사용 예: ViewModel 초기화 시 전체 속성 초기화
  /// </summary>
  protected async Task ResetAllPropertiesAndValidateAsync()
  {
    foreach (var prop in _syncRules.Keys.Union(_asyncRules.Keys))
    {
      ClearErrors(prop);
    }
    await ValidateAllAsync();
  }

  /// <summary>
  /// 모든 속성의 오류 초기화
  /// - 값은 변경하지 않고 오류 상태만 제거
  /// </summary>
  protected void ClearAllErrors()
  {
    foreach (var prop in _errors.Keys.ToArray())
      ClearErrors(prop);
  }
  #endregion
}
