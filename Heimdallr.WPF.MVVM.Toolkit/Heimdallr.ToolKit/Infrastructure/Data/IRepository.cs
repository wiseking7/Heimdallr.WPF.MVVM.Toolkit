namespace Heimdallr.ToolKit.Infrastructure;

/// <summary>
/// 레포지토리 인터페이스
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : class, new()
{
  /// <summary>
  /// 특정 ID에 해당하는 엔티티를 비동기적으로 가져옵니다.
  /// </summary>
  /// <param name="id">검색하려는 엔티티의 고유 식별자(ID)</param>
  /// <returns>
  /// 요청된 ID에 해당하는 엔티티 객체를 반환합니다. 
  /// 만약 해당 ID의 엔티티가 없으면 null을 반환합니다.
  /// </returns>
  Task<T?> GetByIdAsync(int id);

  /// <summary>
  /// 모든 엔티티를 비동기적으로 가져옵니다.
  /// </summary>
  /// <returns>
  /// 데이터베이스에 존재하는 모든 엔티티를 컬렉션 형태로 반환합니다. 
  /// 엔티티의 목록을 IEnumerab 반환하며, 비어 있을 수 있습니다.
  /// </returns>
  Task<IEnumerable<T>> GetAllAsync();

  /// <summary>
  /// 새로운 엔티티를 비동기적으로 추가합니다.
  /// </summary>
  /// <param name="entity">추가할 엔티티 객체</param>
  /// <remarks>
  /// 새로 추가하려는 엔티티는 유효한 값이어야 하며, 
  /// 해당 엔티티의 기본 키(ID)는 자동으로 생성되거나 지정되어야 합니다.
  /// </remarks>
  Task CreateAsync(T entity);

  /// <summary>
  /// 기존 엔티티를 비동기적으로 업데이트합니다.
  /// </summary>
  /// <param name="entity">수정할 엔티티 객체</param>
  /// <remarks>
  /// 주어진 엔티티 객체는 반드시 이미 데이터베이스에 존재하는 엔티티이어야 하며, 
  /// 해당 엔티티의 식별자는 변경되지 않아야 합니다.
  /// 만약 엔티티가 존재하지 않으면 예외가 발생할 수 있습니다.
  /// </remarks>
  Task UpdateAsync(T entity);

  /// <summary>
  /// 엔티티를 비동기적으로 삭제합니다.
  /// </summary>
  /// <param name="entity">삭제할 엔티티 객체</param>
  /// <remarks>
  /// 삭제하려는 엔티티는 반드시 데이터베이스에 존재하는 엔티티여야 합니다. 
  /// 만약 해당 엔티티가 데이터베이스에 존재하지 않으면 예외가 발생할 수 있습니다.
  /// 삭제 후, 엔티티는 더 이상 데이터베이스에 존재하지 않게 됩니다.
  /// </remarks>
  Task RemoveAsync(T entity);
}



