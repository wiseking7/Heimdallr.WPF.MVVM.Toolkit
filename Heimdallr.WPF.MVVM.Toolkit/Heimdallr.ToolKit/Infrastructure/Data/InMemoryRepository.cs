namespace Heimdallr.ToolKit.Infrastructure;

/// <summary>
/// 제네릭 인터페이스 <c>IRepository&lt;T&gt;</c> 를 구현한 메모리 기반 저장소 클래스입니다.
/// <typeparam name="T">클래스 타입이며, 기본 생성자(<c>new()</c>)가 필요합니다.</typeparam>
/// </summary>
public class InMemoryRepository<T> : IRepository<T> where T : class, new()
{
  // 메모리에 데이터를 저장할 내부 리스트
  private readonly List<T> _items = new();

  /// <summary>
  /// 데이터를 저장소에 추가합니다.
  /// </summary>
  /// <param name="entity">추가할 엔티티</param>
  /// <returns></returns>
  public Task CreateAsync(T entity)
  {
    // 리스트에 엔터티 추가
    _items.Add(entity);

    // 비동기 작업 완료를 나타냄 (실제 작업 없음)
    return Task.CompletedTask;
  }

  /// <summary>
  /// 저장소에 저장된 모든 엔터티를 반환합니다.
  /// </summary>
  /// <returns>저장된 엔터티 목록</returns>
  public Task<IEnumerable<T>> GetAllAsync() => Task.FromResult(_items.AsEnumerable());

  /// <summary>
  /// ID 값을 기반으로 특정 엔터티를 검색합니다.
  /// </summary>
  /// <param name="id">검색할 ID 값</param>
  /// <returns>ID에 해당하는 엔터티 (없으면 null)</returns>
  public Task<T?> GetByIdAsync(int id)
  {
    // 리플렉션을 사용하여 T 타입의 'id' 프로퍼티를 가져옴
    var prop = typeof(T).GetProperty("Id");

    if (prop == null)
      throw new InvalidOperationException("T Type 은 Type int의 'Id' 속성을 가져야 합니다.");

    // 'id' 프로퍼티가 존재하고, 해당 값을 비교하여 일치하는 첫 번째 항목을 찾음
    return Task.FromResult(_items.FirstOrDefault(x => (int)prop?.GetValue(x)! == id));
  }

  /// <summary>
  /// 저장소에서 특정 엔터티를 제거합니다.
  /// </summary>
  /// <param name="entity">제거할 엔티티</param>
  /// <returns></returns>
  public Task RemoveAsync(T entity)
  {
    // 리스트에서 해당 엔터티 제거
    _items.Remove(entity);
    return Task.CompletedTask;
  }

  /// <summary>
  /// 기존 엔터티를 ID를 기준으로 찾아 업데이트합니다.
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  public Task UpdateAsync(T entity)
  {
    // 'Id' 프로퍼티를 가져옴
    var prop = typeof(T).GetProperty("Id");
    if (prop == null)
      throw new InvalidOperationException("T Type 은 Type int의 'Id' 속성을 가져야 합니다.");

    // 전달된 entity의 ID 값을 가져옴
    int id = (int)prop.GetValue(entity)!;

    // 기존 엔터티를 리스트에서 찾아 인덱스를 얻음
    var index = _items.FindIndex(x => (int)prop.GetValue(x)! == id);
    if (index >= 0)
    {
      _items[index] = entity; // 해당 위치의 항목을 새 엔터티로 교체
    }

    // 실제 비동기 작업은 없지만 Task 반환이 필요한 경우
    // 진짜 비동기 처리는 없지만 async 시그니처가 필요한 경우
    return Task.CompletedTask;
  }
}
