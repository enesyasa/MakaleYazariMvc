using MakaleYazariMvc.Models.Entities;

namespace MakaleYazariMvc.Services.Abstract;

/// <summary>
/// Kategori servisi arayüzü
/// </summary>
public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();// Tüm kategorileri alır
    Task<Category> GetCategoryByIdAsync(int id);// Kategori ID'sine göre kategori alır
    Task CreateCategoryAsync(Category category);// Yeni kategori oluşturur
    Task UpdateCategoryAsync(Category category);// Kategori günceller
    Task DeleteCategoryAsync(int id);// Kategori siler
}
