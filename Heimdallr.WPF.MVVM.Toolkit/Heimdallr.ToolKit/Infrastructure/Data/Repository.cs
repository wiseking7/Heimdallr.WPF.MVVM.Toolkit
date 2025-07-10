using Microsoft.EntityFrameworkCore;

namespace Heimdallr.ToolKit.Infrastructure;

/// <summary>
/// 제네릭 타입 매개변수 <c>T</c>에 대한 제약 조건을 설명합니다.
/// 
/// <para><c>where T : class, new()</c> 구문은 <c>T</c>가 다음 조건을 만족하도록 제한합니다:</para>
/// 
/// <list type="bullet">
///   <item><description><c>class</c>: <c>T</c>는 참조 형식이어야 하며, 값 형식(int, struct 등)은 허용되지 않습니다.</description></item>
///   <item><description><c>new()</c>: <c>T</c>는 매개변수가 없는 기본 생성자를 가져야 하며, 이를 통해 인스턴스화할 수 있어야 합니다.</description></item>
/// </list>
/// 
/// <para>예: <c>T</c>는 <c>string</c>, 사용자 정의 클래스(<c>MyClass</c>), <c>List&lt;T&gt;</c> 등 참조형 클래스여야 하며, 기본 생성자가 필요합니다.</para>
/// </summary>
/// <typeparam name="T">기본 생성자가 있는 참조형 타입</typeparam>

public class Repository<T> : IRepository<T> where T : class, new()
{
  // 데이터베이스 컨텍스트 객체 (DbContext) dbcontext 클래스명 예: AppDbContext.cs 파일
  private readonly DbContext _context;

  // 해당 엔티티 타입에 대한 DbSet 개체 (EF Core에서 테이블을 나타냄)
  private readonly DbSet<T> _dbSet;

  /// <summary>
  /// Repository 생성자. DbContext를 주입받아 내부 DbSet을 초기화합니다.
  /// </summary>
  /// <param name="context">EF Core DbContext 객체</param>
  public Repository(DbContext context)
  {
    _context = context ?? throw new ArgumentNullException(nameof(context));  // DbContext 초기화
    _dbSet = _context.Set<T>();      // 해당 엔티티 타입의 DbSet 초기화
  }

  /// <summary>
  /// 데이터베이스에 존재하는 모든 엔티티를 비동기적으로 가져옵니다.
  /// </summary>
  /// <returns>
  /// 모든 엔티티를 담은 <see cref="IEnumerable{T}"/> 컬렉션입니다.  
  /// 결과가 없을 경우 빈 컬렉션이 반환됩니다.
  /// </returns>
  public async Task<IEnumerable<T>> GetAllAsync()
  {
    // DbSet에서 모든 엔티티를 비동기적으로 가져옴
    return await _dbSet.ToListAsync();
  }

  /// <summary>
  /// 특정 ID에 해당하는 엔티티를 비동기적으로 가져옵니다.
  /// </summary>
  /// <param name="id">검색하려는 엔티티의 고유 ID</param>
  /// <returns>찾은 엔티티 또는 null</returns>
  public async Task<T?> GetByIdAsync(int id)
  {
    // 주어진 ID에 해당하는 엔티티를 비동기적으로 찾음
    return await _dbSet.FindAsync(id);
  }


  /// <summary>
  /// 새로운 엔티티를 데이터베이스에 비동기적으로 추가합니다.
  /// </summary>
  /// <param name="entity">추가할 엔티티 객체</param>
  public async Task CreateAsync(T entity)
  {
    // 엔티티가 null이 아닐 경우에만 추가
    if (entity == null)
    {
      throw new ArgumentNullException(nameof(entity));
    }

    // 비동기적으로 엔티티를 DbSet에 추가
    await _dbSet.AddAsync(entity);

    // 데이터베이스에 변경 사항을 저장
    await _context.SaveChangesAsync();
  }

  /// <summary>
  /// 기존 엔티티를 데이터베이스에서 비동기적으로 업데이트합니다.
  /// </summary>
  /// <param name="entity">수정할 엔티티 객체</param>
  public async Task UpdateAsync(T entity)
  {
    // 엔티티가 null일 경우 예외를 발생시킴
    if (entity == null)
    {
      throw new ArgumentNullException(nameof(entity));
    }

    // 엔티티를 DbSet에서 업데이트
    _dbSet.Update(entity);

    // 데이터베이스에 변경 사항을 저장
    await _context.SaveChangesAsync();
  }


  /// <summary>
  /// 기존 엔티티를 데이터베이스에서 비동기적으로 삭제합니다.
  /// </summary>
  /// <param name="entity">삭제할 엔티티 객체</param>
  public async Task RemoveAsync(T entity)
  {
    // 엔티티가 null일 경우 예외를 발생시킴
    if (entity == null)
    {
      throw new ArgumentNullException(nameof(entity));
    }

    // 엔티티를 DbSet에서 제거
    _dbSet.Remove(entity);

    // 데이터베이스에 변경 사항을 저장
    await _context.SaveChangesAsync();
  }
}
