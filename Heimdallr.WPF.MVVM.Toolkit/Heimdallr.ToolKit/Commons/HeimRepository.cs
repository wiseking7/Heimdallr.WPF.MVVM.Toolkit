using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Heimdallr.ToolKit.Commons;

/// <summary>
/// 저장소(Repository) 구현체입니다.
/// Entity Framework Core의 IDbContextFactory를 사용하여
/// DbContext 인스턴스를 생성하고 CRUD 작업을 수행합니다.
/// </summary>
/// <typeparam name="T">엔티티 타입 (class, 기본 생성자 필요)</typeparam>
/// <typeparam name="TContext">EF Core DbContext 타입</typeparam>
public class HeimRepository<T, TContext> : IHeimRepository<T> where T : class, new()
    where TContext : DbContext
{
  private readonly IDbContextFactory<TContext> _contextFactory;

  /// <summary>
  /// 생성자: DbContextFactory를 주입받아 저장소 인스턴스를 초기화합니다.
  /// </summary>
  /// <param name="contextFactory">DbContext 인스턴스 생성 팩토리</param>
  /// <exception cref="ArgumentNullException">contextFactory가 null일 경우 발생</exception>
  public HeimRepository(IDbContextFactory<TContext> contextFactory)
  {
    _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
  }

  /// <summary>
  /// 새로운 엔티티를 비동기 생성합니다.
  /// </summary>
  /// <param name="entity">추가할 엔티티 객체. null 허용하지 않음.</param>
  /// <returns>
  /// 성공/실패 상태를 담은 <see cref="HeimResult"/> 객체.
  /// - 성공 시: 성공 상태 반환  
  /// - 실패 시: 에러 메시지를 포함한 실패 상태 반환
  /// </returns>
  public async Task<HeimResult> CreateAsync(T? entity)
  {
    if (entity == null)
      return HeimResult.Fail("추가할 Entity null 일 수 없습니다");

    try
    {
      await using var context = await _contextFactory.CreateDbContextAsync();
      await context.Set<T>().AddAsync(entity);
      await context.SaveChangesAsync();
      return HeimResult.Ok();
    }
    catch (Exception ex)
    {
      return HeimResult.Fail($"Data 추가 실패: {ex.Message}");
    }
  }

  /// <summary>
  /// 특정 조건에 맞는 엔티티 존재 여부를 비동기 검사합니다.
  /// </summary>
  /// <param name="predicate">검사할 조건식 (null 허용하지 않음)</param>
  /// <returns>
  /// <see /> cref="HeimResult{bool, string}" 형태의 결과.  
  /// - 성공 시: 조건에 맞는 엔티티 존재 여부(true/false) 반환  
  /// - 실패 시: 에러 메시지를 포함한 실패 결과 반환
  /// </returns>
  /// <exception cref="ArgumentNullException">predicate가 null일 경우 발생</exception>
  public async Task<HeimResult<bool, string>> ExistsAsync(Expression<Func<T, bool>> predicate)
  {
    try
    {
      await using var context = await _contextFactory.CreateDbContextAsync();
      var exists = await context.Set<T>().AnyAsync(predicate);
      return HeimResult<bool, string>.Ok(exists);
    }
    catch (Exception ex)
    {
      return HeimResult<bool, string>.Fail($"존재 여부 조회 실패: {ex.Message}");
    }
  }

  /// <summary>
  /// 조건에 맞는 단일 엔티티 조회 (첫 번째 엔티티 조회)
  /// 예시 -> var result = await userRepo.FindAsync(u => u.Email == "test@example.com");
  /// </summary>
  /// <param name="predicate"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public async Task<HeimResult<T?, string>> FindAsync(Expression<Func<T, bool>> predicate)
  {
    try
    {
      await using var context = await _contextFactory.CreateDbContextAsync();
      var entity = await context.Set<T>().FirstOrDefaultAsync(predicate);
      if (entity == null)
        return HeimResult<T?, string>.Fail("조건에 맞는 데이터가 없습니다.");

      return HeimResult<T?, string>.Ok(entity);
    }
    catch (Exception ex)
    {
      return HeimResult<T?, string>.Fail($"조회 실패: {ex.Message}");
    }
  }

  /// <summary>
  /// 모든 엔티티 목록을 비동기 조회합니다.
  /// </summary>
  /// <returns>
  /// <see /> cref="HeimResult{IEnumerable{T}, string}" 형태의 결과.  
  /// - 성공 시: 조회된 엔티티 목록 (빈 컬렉션 가능) 반환  
  /// - 실패 시: 에러 메시지를 포함한 실패 결과 반환
  /// </returns>
  public async Task<HeimResult<IEnumerable<T>, string>> GetAllAsync()
  {
    try
    {
      await using var context = await _contextFactory.CreateDbContextAsync();
      var list = await context.Set<T>().ToListAsync();
      return HeimResult<IEnumerable<T>, string>.Ok(list);
    }
    catch (Exception ex)
    {
      return HeimResult<IEnumerable<T>, string>.Fail($"Data 조회 실패: {ex.Message}");
    }
  }

  /// <summary>
  /// ID로 단일 엔티티를 비동기 조회합니다.
  /// </summary>
  /// <param name="id">조회할 엔티티의 식별자</param>
  /// <returns>
  /// <see /> cref="HeimResult{T?, string}" 형태의 결과.  
  /// - 성공 시: 조회된 엔티티 객체 또는 null 반환 (해당 ID 없음)  
  /// - 실패 시: 에러 메시지를 포함한 실패 결과 반환
  /// </returns>
  public async Task<HeimResult<T?, string>> GetByIdAsync(int id)
  {
    try
    {
      await using var context = await _contextFactory.CreateDbContextAsync();
      var entity = await context.Set<T>().FindAsync(id);

      if (entity == null)
        return HeimResult<T?, string>.Fail($"ID {id} 에 해당하는 Data 찾을 수 없습니다.");

      return HeimResult<T?, string>.Ok(entity);
    }
    catch (Exception ex)
    {
      return HeimResult<T?, string>.Fail($"Data 조회 중 예외 발생: {ex.Message}");
    }
  }

  /// <summary>
  /// 필터, 정렬, 페이징을 적용한 엔티티 목록을 비동기 조회합니다.
  /// </summary>
  /// <param name="filter">조회 조건식 (null 가능)</param>
  /// <param name="orderBy">정렬 함수 (null 가능)</param>
  /// <param name="pageIndex">페이지 번호, 0부터 시작 (예: 0은 첫 페이지, 1은 두 번째 페이지)</param>
  /// <param name="pageSize">한페이지에 보여줄 데이터 개수(예: 10개씩 보여주기)</param>
  /// 예를 들어, pageSize = 10이고 pageIndex = 0이면 0~9번째 데이터를 가져오고,
  /// pageIndex = 1이면 10~19번째 데이터를 가져오는 식입니다
  /// <returns>
  /// <see /> cref="HeimResult{IEnumerable{T}, string}" 형태의 결과.  
  /// - 성공 시: 조회된 페이지 데이터 반환 (빈 컬렉션 가능)  
  /// - 실패 시: 에러 메시지를 포함한 실패 결과 반환
  /// </returns>
  public async Task<HeimResult<IEnumerable<T>, string>> GetPagedAsync(Expression<Func<T, bool>>? filter,
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
      int pageIndex, int pageSize)
  {
    try
    {
      await using var context = await _contextFactory.CreateDbContextAsync();
      IQueryable<T> query = context.Set<T>();

      if (filter != null)
        query = query.Where(filter);

      if (orderBy != null)
        query = orderBy(query);

      var list = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

      return HeimResult<IEnumerable<T>, string>.Ok(list);
    }
    catch (Exception ex)
    {
      return HeimResult<IEnumerable<T>, string>.Fail($"페이지 Data 조회 실패: {ex.Message}");
    }
  }

  /// <summary>
  /// Include를 사용하여 관련 Data를 포함한 쿼리 실행 (관련 Entity 포함한 조회 수행)
  /// 예시-> var result = await orderRepo.IncludeAsync(q => q.Include(o => o.OrderItems));
  /// </summary>
  /// <param name="include"></param>
  /// <param name="predicate"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public async Task<HeimResult<IEnumerable<T>, string>> IncludeAsync(Func<IQueryable<T>,
    IQueryable<T>> include, Expression<Func<T, bool>>? predicate = null)
  {
    try
    {
      await using var context = await _contextFactory.CreateDbContextAsync();
      IQueryable<T> query = context.Set<T>();

      if (predicate != null)
        query = query.Where(predicate);

      query = include(query);

      var result = await query.ToListAsync();
      return HeimResult<IEnumerable<T>, string>.Ok(result);
    }
    catch (Exception ex)
    {
      return HeimResult<IEnumerable<T>, string>.Fail($"Include 조회 실패: {ex.Message}");
    }
  }

  /// <summary>
  /// 쿼리 가능한 <see cref="IQueryable{T}"/> 형태로 데이터 소스를 제공합니다.
  /// <para>반환된 IQueryable 사용 시, 호출자가 DbContext 수명 관리에 주의해야 합니다.</para>
  /// </summary>
  /// <returns>데이터 소스의 <see cref="IQueryable{T}"/> 컬렉션</returns>
  public IQueryable<T> Query()
  {
    var context = _contextFactory.CreateDbContext();
    return context.Set<T>();
  }

  /// <summary>
  /// 기존 엔티티를 비동기 삭제합니다.
  /// </summary>
  /// <param name="entity">삭제할 엔티티 객체 (null 허용하지 않음)</param>
  /// <returns>
  /// 성공/실패 상태를 담은 <see cref="HeimResult"/> 객체.  
  /// - 성공 시: 성공 상태 반환  
  /// - 실패 시: 에러 메시지를 포함한 실패 상태 반환
  /// </returns>
  public async Task<HeimResult> RemoveAsync(T? entity)
  {
    if (entity == null)
      return HeimResult.Fail("삭제할 Entity null일 수 없습니다.");

    try
    {
      await using var context = await _contextFactory.CreateDbContextAsync();
      context.Set<T>().Remove(entity);
      await context.SaveChangesAsync();
      return HeimResult.Ok();
    }
    catch (Exception ex)
    {
      return HeimResult.Fail($"Data 삭제 실패: {ex.Message}");
    }
  }

  /// <summary>
  /// 명시적 트랜젝션 처리 지원 (트랜젝션 단위 작업 실행)
  /// 여러 Entity(Table)을 동시에 조작할 때
  /// 예시-> 
  /// await productRepo.TransactionAsync(async tx =>
  /// {
  ///   var newProduct = new Product { Name = "Sample", Price = 100 };
  ///   await productRepo.CreateAsync(newProduct);
  ///    다른 작업들...
  /// });
  /// </summary>
  /// <param name="action"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public async Task<HeimResult> TransactionAsync(Func<IDbContextTransaction, Task> action)
  {
    try
    {
      await using var context = await _contextFactory.CreateDbContextAsync();
      await using var transaction = await context.Database.BeginTransactionAsync();

      await action(transaction);

      await context.SaveChangesAsync();
      await transaction.CommitAsync();

      return HeimResult.Ok();
    }
    catch (Exception ex)
    {
      return HeimResult.Fail($"트랜잭션 실패: {ex.Message}");
    }
  }

  /// <summary>
  /// 기존 엔티티를 비동기 갱신합니다.
  /// </summary>
  /// <param name="entity">수정할 엔티티 객체 (null 허용하지 않음)</param>
  /// <returns>
  /// 성공/실패 상태를 담은 <see cref="HeimResult"/> 객체.  
  /// - 성공 시: 성공 상태 반환  
  /// - 실패 시: 에러 메시지를 포함한 실패 상태 반환
  /// </returns>
  public async Task<HeimResult> UpdateAsync(T? entity)
  {
    if (entity == null)
      return HeimResult.Fail("수정할 Entity null일 수 없습니다.");

    try
    {
      await using var context = await _contextFactory.CreateDbContextAsync();
      context.Set<T>().Update(entity);
      await context.SaveChangesAsync();
      return HeimResult.Ok();
    }
    catch (Exception ex)
    {
      return HeimResult.Fail($"Data 수정 실패: {ex.Message}");
    }
  }
}
