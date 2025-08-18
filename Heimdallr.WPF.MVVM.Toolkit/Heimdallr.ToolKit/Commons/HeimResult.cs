namespace Heimdallr.ToolKit.Commons;

/// <summary>
/// 성공/실패 상태와 Error 메세지를 표현하는 기본 결과(Result)클래스입니다.
/// </summary>
public class HeimResult
{
  /// <summary>
  /// 작업 성공 여부를 나타냅니다. (읽기 전용)
  /// </summary>
  public bool IsSuccess { get; }

  /// <summary>
  /// 작업 실패 여부를 나타냅니다. IsSuccess의 반대 값입니다.
  /// </summary>
  public bool IsFailure => !IsSuccess;

  /// <summary>
  /// 실패시 Error 메세지를 담는 속성입니다.
  /// 성공일 경우에는 null이 됩니다.
  /// </summary>
  public string? Error { get; }

  /// <summary>
  /// 생성자: 외부에서 직접 인스턴스를 만들지 못하도록 protected로 선언했습니다.
  /// 이는 상속받은 클래스에서 재사용하거나 내부 정적 팩토리 메서드를 통해서만 
  /// 인스턴스를 생성하도록 의도한 설계입니다.
  /// </summary>
  /// <param name="isSuccess">성공 여부</param>
  /// <param name="error">에러 메시지 (성공 시 null이어야 함)</param>
  /// <exception cref="InvalidOperationException">성공인데 에러 메시지가 있거나 
  /// 실패인데 에러 메시지가 없으면 예외 발생</exception>
  protected HeimResult(bool isSuccess, string? error)
  {
    // 성공이면서 에러메세지가 있으면 논리 오류
    if (isSuccess && error != null)
    {
      throw new InvalidOperationException("성공 결과에 Error 메시지가 있을 수 없습니다");
    }

    // 실패인데 에러 메세지가 없으면 논리 오류
    if (!isSuccess && error == null)
    {
      throw new InvalidOperationException("실패 결과에는 반드시 Error 메시지가 있어야 합니다");
    }

    IsSuccess = isSuccess;

    Error = error;
  }

  /// <summary>
  /// 성공 결과 인스턴스를 반환하는 정적 팩토리 메서드입니다.
  /// </summary>
  /// <returns>성공을 나타내는 Result 객체</returns>
  public static HeimResult Ok() => new HeimResult(true, null);

  /// <summary>
  /// 실패 결과 인스턴스를 반환하는 정적 팩토리 메서드입니다.
  /// </summary>
  /// <param name="error">실패 이유를 담은 에러 메시지</param>
  /// <returns>실패를 나타내는 Result 객체</returns>
  public static HeimResult Fail(string error) => new HeimResult(false, error);
}

/// <summary>
/// 제네릭 버전 Result 클래스: 
/// 성공 시 반환할 값(Value)과 실패 시 에러 정보(TError)를 포함할 수 있습니다.
/// </summary>
/// <typeparam name="T">성공 시 반환할 값의 타입</typeparam>
/// <typeparam name="TError">실패 시 에러 정보 타입</typeparam>
public class HeimResult<T, TError> : HeimResult
{
  // 내부에 성공 시 저장할 값 (nullable 허용)
  private readonly T? _value;
  private readonly TError? _errorObject;

  /// <summary>
  /// 성공 결과일 때만 접근 가능한 값입니다.
  /// 실패인 경우 접근 시 예외를 던집니다.
  /// </summary>
  /// <exception cref="InvalidOperationException">
  /// 실패 결과에서 값을 요청할 경우 예외 발생</exception>
  public T? Value
  {
    get
    {
      if (!IsSuccess)
      {
        throw new InvalidOperationException("성공한 결과에서만 Value에 접근할 수 있습니다");
      }
      return _value;
    }
  }

  /// <summary>
  /// 실패 시 원본 에러 객체 (성공 시 null)
  /// </summary>
  public TError? ErrorObject => _errorObject;

  /// <summary>
  /// 내부 생성자: 외부에서 직접 호출하지 않고 정적 메서드를 통해 생성하도록 설계.
  /// base 생성자에 문자열 형태의 에러 메시지를 넘기기 위해 error?.ToString() 호출.
  /// </summary>
  /// <param name="value">성공 시 반환할 값</param>
  /// <param name="isSuccess">성공 여부</param>
  /// <param name="error">실패 시 에러 정보</param>
  protected internal HeimResult(T? value, bool isSuccess, TError? error)
    : base(isSuccess, ConvertErrorToString(error))
  {
    _value = value;
    _errorObject = error;
  }

  /// <summary>
  /// 성공 결과를 생성하는 팩토리 메서드.
  /// </summary>
  /// <param name="value">성공 시 반환할 값</param>
  /// <returns>성공 상태의 Result 객체</returns>
  public static HeimResult<T, TError> Ok(T? value) =>
    new HeimResult<T, TError>(value, true, default);

  /// <summary>
  /// 실패 결과를 생성하는 팩토리 메서드.
  /// </summary>
  /// <param name="error">실패 이유를 담은 에러 정보</param>
  /// <returns>실패 상태의 Result 객체</returns>
  public static HeimResult<T, TError> Fail(TError error) =>
    new HeimResult<T, TError>(default, false, error);

  /// <summary>
  /// 에러 객체를 문자열로 변환하는 내부 헬퍼 메서드
  /// </summary>
  private static string? ConvertErrorToString(TError? error) =>
      error?.ToString();
}
