using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Heimdallr.ToolKit.Commons;

/// <summary>
/// 저장소(Repository) 인터페이스입니다.  
/// 기본 CRUD 및 쿼리 기능을 제공합니다.
/// </summary>
/// <typeparam name="T">엔티티 타입 (class, 기본 생성자 필요)</typeparam>
public interface IHeimRepository<T> where T : class, new()
{
  /// <summary>
  /// ID로 단일 엔티티를 비동기 조회합니다.
  /// </summary>
  /// <param name="id">조회할 엔티티의 ID</param>
  /// <returns>
  /// <see /> cref="Task{Result{T?, string}}" 형태로 감싼 결과입니다.  
  /// - 성공 시: 조회된 엔티티 객체 또는 null 반환  
  /// - 실패 시: 에러 메시지를 포함한 실패 결과 반환
  /// </returns>
  Task<HeimResult<T?, string>> GetByIdAsync(int id);

  /// <summary>
  /// 모든 엔티티 목록을 비동기 조회합니다.
  /// </summary>
  /// <returns>
  /// <see/> cref="Task{Result{IEnumerable{T}, string}}" 형태로 감싼 결과입니다.  
  /// - 성공 시: 조회된 엔티티 목록 반환 (빈 컬렉션 가능)  
  /// - 실패 시: 에러 메시지를 포함한 실패 결과 반환
  /// </returns>
  Task<HeimResult<IEnumerable<T>, string>> GetAllAsync();

  /// <summary>
  /// 새로운 엔티티를 비동기 생성합니다.
  /// </summary>
  /// <param name="entity">생성할 엔티티 객체 (null 허용)</param>
  /// <returns>
  /// <see cref="Task{Result}"/> 형태로, 성공/실패 상태를 반환합니다.  
  /// - 성공 시: 성공 결과  
  /// - 실패 시: 에러 메시지를 포함한 실패 결과
  /// </returns>
  Task<HeimResult> CreateAsync(T? entity);

  /// <summary>
  /// 기존 엔티티를 비동기 갱신합니다.
  /// </summary>
  /// <param name="entity">갱신할 엔티티 객체 (null 허용)</param>
  /// <returns>
  /// <see cref="Task{Result}"/> 형태로, 성공/실패 상태를 반환합니다.  
  /// - 성공 시: 성공 결과  
  /// - 실패 시: 에러 메시지를 포함한 실패 결과
  /// </returns>
  Task<HeimResult> UpdateAsync(T? entity);

  /// <summary>
  /// 기존 엔티티를 비동기 삭제합니다.
  /// </summary>
  /// <param name="entity">삭제할 엔티티 객체 (null 허용)</param>
  /// <returns>
  /// <see cref="Task{Result}"/> 형태로, 성공/실패 상태를 반환합니다.  
  /// - 성공 시: 성공 결과  
  /// - 실패 시: 에러 메시지를 포함한 실패 결과
  /// </returns>
  Task<HeimResult> RemoveAsync(T? entity);

  /// <summary>
  /// 쿼리 가능한 <see cref="IQueryable{T}"/> 형태의 데이터 소스를 제공합니다.
  /// </summary>
  /// <returns>
  /// <see cref="IQueryable{T}"/> 형식의 쿼리 가능 데이터 컬렉션  
  /// - 주로 LINQ 쿼리에 사용됩니다.
  /// </returns>
  IQueryable<T> Query();

  /// <summary>
  /// 필터, 정렬, 페이징을 적용한 엔티티 목록을 비동기 조회합니다.
  /// </summary>
  /// <param name="filter">조회 조건 (null 허용)</param>
  /// <param name="orderBy">정렬 함수 (null 허용)</param>
  /// <param name="pageIndex">페이지 번호 (0부터 시작)</param>
  /// <param name="pageSize">페이지 크기</param>
  /// <returns>
  /// <see /> cref="Task{Result{IEnumerable{T}, string}}" 형태로 감싼 결과입니다.  
  /// - 성공 시: 조건에 맞는 엔티티 목록  
  /// - 실패 시: 에러 메시지를 포함한 실패 결과
  /// </returns>
  Task<HeimResult<IEnumerable<T>, string>> GetPagedAsync(
      Expression<Func<T, bool>>? filter,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
      int pageIndex,
      int pageSize);

  /// <summary>
  /// 특정 조건에 해당하는 엔티티 존재 여부를 비동기 검사합니다.
  /// </summary>
  /// <param name="predicate">검사 조건</param>
  /// <returns>
  /// <see /> cref="Task{Result{bool, string}}" 형태로 감싼 결과입니다.  
  /// - 성공 시: 조건에 부합하는 엔티티 존재 여부  
  /// - 실패 시: 에러 메시지를 포함한 실패 결과
  /// </returns>
  Task<HeimResult<bool, string>> ExistsAsync(Expression<Func<T, bool>> predicate);

  /// <summary>
  /// 조건에 맞는 단일 개체 직접 조회
  /// </summary>
  /// <param name="predicate"></param>
  /// <returns></returns>
  Task<HeimResult<T?, string>> FindAsync(Expression<Func<T, bool>> predicate);

  /// <summary>
  /// 자체 트랜젝션 처리 로직 내장
  /// </summary>
  /// <param name="action"></param>
  /// <returns></returns>
  Task<HeimResult> TransactionAsync(Func<IDbContextTransaction, Task> action);

  /// <summary>
  /// 관련된 엔티티를 포함한 쿼리
  /// </summary>
  /// <param name="include"></param>
  /// <param name="predicate"></param>
  /// <returns></returns>
  Task<HeimResult<IEnumerable<T>, string>> IncludeAsync(Func<IQueryable<T>,
    IQueryable<T>> include, Expression<Func<T, bool>>? predicate = null);
}


