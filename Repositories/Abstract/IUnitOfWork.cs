using MakaleYazariMvc.Models.Entities;

namespace MakaleYazariMvc.Repositories.Abstract;

/// <summary>
/// Unit of Work arayüzü
/// </summary>
public interface IUnitOfWork
{
    IGenericRepository<Article> Articles { get; }
    IGenericRepository<Category> Categories { get; }
    IGenericRepository<ArticleCategory> ArticleCategories { get; }
    Task CommitAsync();
}
