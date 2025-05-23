using MakaleYazariMvc.Data;
using MakaleYazariMvc.Models.Entities;
using MakaleYazariMvc.Repositories.Abstract;

namespace MakaleYazariMvc.Repositories.Concrete;

/// <summary>
/// Unit of Work sınıfı
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IGenericRepository<Article> Articles { get; }// GenericRepository sınıfından türetilen Articles özelliği
    public IGenericRepository<Category> Categories { get; }// GenericRepository sınıfından türetilen Categories özelliği
    public IGenericRepository<ArticleCategory> ArticleCategories { get; }// GenericRepository sınıfından türetilen ArticleCategories özelliği

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Articles = new GenericRepository<Article>(context);
        Categories = new GenericRepository<Category>(context);
        ArticleCategories = new GenericRepository<ArticleCategory>(context);
    }

    /// <summary>
    /// Veritabanına yapılan değişiklikleri kaydeder
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // logging vs. eklenebilir.
            throw new InvalidOperationException("Veri tabanına kayıt edilirken hata oluştu", ex);
        }
    }
}
