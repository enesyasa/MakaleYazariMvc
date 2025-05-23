using MakaleYazariMvc.Models.Entities;

namespace MakaleYazariMvc.Services.Abstract;

/// <summary>
/// Makale servisi arayüzü
/// </summary>
public interface IArticleService
{
    Task<IEnumerable<Article>> GetUserArticlesAsync(string userId);// Kullanıcının tüm makaleleri alır
    Task<Article> GetArticleByIdAsync(int id);// Makale ID'sine göre makale alır
    Task CreateArticleAsync(Article article, List<int> categoryIds);// Yeni makale oluşturur ve kategorilere atama yapar
    Task UpdateArticleAsync(Article article, List<int> categoryIds);// Makale güncellenir ve yeni kategoriler atanır
    Task DeleteArticleAsync(int id);// Makale silinir
    Task<IEnumerable<Article>> GetAllArticlesAsync();// Tüm makaleleri alır
}
