namespace Heimdallr.ToolKit.Commons;

/// <summary>
/// Result 관련 확장 메서드 모음입니다.
/// </summary>
public static class HeimResultExtensions
{
  /// <summary>
  /// 동기 변환 함수 적용 (성공 시) + 예외 발생 시 errorFactory 호출하여 실패 결과 반환
  /// </summary>
  /// <typeparam name="TIn">입력 Result의 성공 값 타입</typeparam>
  /// <typeparam name="TOut">출력 Result의 성공 값 타입</typeparam>
  /// <typeparam name="TError">에러 타입</typeparam>
  /// <param name="result">변환 대상 Result 객체</param>
  /// <param name="map">성공 시 변환할 동기 함수</param>
  /// <param name="errorFactory">예외 발생 시 변환할 에러 생성 함수</param>
  /// <returns>성공 시 변환된 Result, 실패 시 기존 에러 또는 errorFactory로 생성된 실패 Result</returns>
  /// <remarks>
  /// 동기 메서드  
  /// 예외 발생 시 errorFactory 호출하여 실패 처리  
  /// 반환 타입: Result&lt;TOut, TError&gt;  
  /// </remarks>
  public static HeimResult<TOut, TError> Map<TIn, TOut, TError>(
      this HeimResult<TIn, TError> result,
      Func<TIn, TOut> map,
      Func<Exception, TError> errorFactory)
  {
    if (result.IsFailure)
    {
      // 실패 상태면 기존 에러를 그대로 전달
      return HeimResult<TOut, TError>.Fail(result.ErrorObject!);
    }

    try
    {
      // 성공일 때 map 함수 적용
      var value = map(result.Value!);
      return HeimResult<TOut, TError>.Ok(value);
    }
    catch (Exception ex)
    {
      // 예외 발생 시 errorFactory로 에러 변환하여 실패 반환
      return HeimResult<TOut, TError>.Fail(errorFactory(ex));
    }
  }

  /// <summary>
  /// 비동기 Result를 기다리고, 성공 시 동기 변환 함수 실행
  /// </summary>
  /// <typeparam name="TIn">입력 Result의 성공 값 타입</typeparam>
  /// <typeparam name="TOut">출력 Result의 성공 값 타입</typeparam>
  /// <typeparam name="TError">에러 타입</typeparam>
  /// <param name="resultTask">비동기 결과 Task</param>
  /// <param name="map">성공 시 적용할 동기 변환 함수</param>
  /// <returns>변환된 Result를 감싼 Task</returns>
  /// <remarks>
  ///  - 비동기 메서드 (Task&lt;Result&gt; 반환)
  ///  - 내부에서 Map(2 params) 호출
  ///  - 예외 발생 시 기본적으로 Exception.Message를 TError로 변환
  /// </remarks>
  public static async Task<HeimResult<TOut, TError>> MapAsync<TIn, TOut, TError>(
      this Task<HeimResult<TIn, TError>> resultTask,
      Func<TIn, TOut> map)
  {
    var result = await resultTask.ConfigureAwait(false);
    return result.Map(map, ex => (TError)(object)ex.Message);
  }

  /// <summary>
  /// 비동기 Result를 기다리고, 성공 시 다음 비동기 작업 연결 (비동기 바인딩)
  /// </summary>
  /// <typeparam name="TIn">입력 Result의 성공 값 타입</typeparam>
  /// <typeparam name="TOut">출력 Result의 성공 값 타입</typeparam>
  /// <typeparam name="TError">에러 타입</typeparam>
  /// <param name="resultTask">비동기 결과 Task</param>
  /// <param name="bind">성공 시 실행할 다음 비동기 작업 함수</param>
  /// <returns>다음 작업 결과 Task</returns>
  /// <remarks>
  /// - 비동기 메서드  
  /// - 실패 시 즉시 실패 결과 반환하여 작업 중단  
  /// - 성공 시 bind 함수 실행하여 결과 반환  
  /// </remarks>
  public static async Task<HeimResult<TOut, TError>> BindAsync<TIn, TOut, TError>(
      this Task<HeimResult<TIn, TError>> resultTask,
      Func<TIn, Task<HeimResult<TOut, TError>>> bind)
  {
    var result = await resultTask;
    if (result.IsFailure)
    {
      if (result.ErrorObject is null)
        throw new InvalidOperationException("실패 결과에 ErrorObject가 없습니다.");
      return HeimResult<TOut, TError>.Fail(result.ErrorObject);
    }

    return await bind(result.Value!);
  }

  /// <summary>
  /// 비동기 Result를 기다리고, 성공 시 비동기 후처리 작업 실행 (사이드 이펙트)
  /// </summary>
  /// <typeparam name="TIn">입력 Result의 성공 값 타입</typeparam>
  /// <typeparam name="TError">에러 타입</typeparam>
  /// <param name="resultTask">비동기 결과 Task</param>
  /// <param name="action">성공 시 실행할 비동기 작업</param>
  /// <returns>원본 Result Task</returns>
  /// <remarks>
  /// - 비동기 메서드  
  /// - 실패 시 후처리 무시  
  /// - 성공 시에만 action 실행  
  /// </remarks>
  public static async Task<HeimResult<TIn, TError>> OnSuccessAsync<TIn, TError>(
      this Task<HeimResult<TIn, TError>> resultTask,
      Func<TIn, Task> action)
  {
    var result = await resultTask.ConfigureAwait(false);

    if (result.IsSuccess)
    {
      await action(result.Value!);
    }

    return result;
  }

  /// <summary>
  /// 동기 변환 함수 적용 (성공 시) + 기본 예외 변환 (Exception.Message -> TError)
  /// </summary>
  /// <typeparam name="TIn">입력 Result의 성공 값 타입</typeparam>
  /// <typeparam name="TOut">출력 Result의 성공 값 타입</typeparam>
  /// <typeparam name="TError">에러 타입</typeparam>
  /// <param name="result">변환 대상 Result 객체</param>
  /// <param name="map">성공 시 변환할 동기 함수</param>
  /// <returns>성공 시 변환된 Result, 실패 시 기존 에러</returns>
  /// <remarks>
  /// - 동기 메서드  
  /// - 예외 발생 시 Exception.Message를 기본 TError로 변환  
  /// - 내부적으로 Map(3 params) 호출  
  /// </remarks>
  public static HeimResult<TOut, TError> Map<TIn, TOut, TError>(
      this HeimResult<TIn, TError> result,
      Func<TIn, TOut> map)
  {
    return result.Map(map, ex => (TError)(object)ex.Message);
  }
}

/* ======= 메서드 요약 =======
   메서드명       | 동기/비동기 | 예외 처리 여부         | 반환 타입
----------------|------------|----------------------|------------------------
Map (3 params)  | 동기       | 예외 발생 시 errorFactory 호출  | Result<TOut, TError>
Map (2 params)  | 동기       | 기본 예외 처리 (ex.Message -> TError) | Result<TOut, TError>
MapAsync        | 비동기     | 내부에서 Map 호출 (기본 예외 처리) | Task<Result<TOut, TError>>
BindAsync       | 비동기     | 실패 시 즉시 중단, 성공 시 다음 비동기 작업 | Task<Result<TOut, TError>>
OnSuccessAsync  | 비동기     | 실패 시 후처리 무시, 성공 시 비동기 후처리 실행 | Task<Result<TIn, TError>>
*/