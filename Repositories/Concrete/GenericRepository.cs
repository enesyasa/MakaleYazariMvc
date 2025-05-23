using MakaleYazariMvc.Data;
using MakaleYazariMvc.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MakaleYazariMvc.Repositories.Concrete;
/// <summary>
/// Generic repository sınıfı
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{

    protected readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;// DbSet<T> ile veritabanı tablosuna erişim sağladım

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    /// <summary>
    /// Veritabanına yeni bir kayıt ekler
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    /// <summary>
    /// Veritabanındaki tüm kayıtları getirir
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync(); // AsNoTracking ile performansın artmasını amaçladım.
    }

    /// <summary>
    /// Veritabanındaki bir kaydı id ile getirir
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Veritabanındaki bir kaydı siler
    /// </summary>
    /// <param name="entity"></param>
    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    /// <summary>
    /// Veritabanındaki bir kaydı günceller
    /// </summary>
    /// <param name="entity"></param>
    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    /// <summary>
    /// Veritabanındaki kayıtları filtreler
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync(); // AsNoTracking ile performansın artmasını amaçladım.
    }
}
