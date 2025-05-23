using MakaleYazariMvc.Models.Entities;
using MakaleYazariMvc.Repositories.Abstract;
using MakaleYazariMvc.Services.Abstract;

namespace MakaleYazariMvc.Services.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Kullanıcının tüm makalelerini alır
        public async Task<IEnumerable<Article>> GetUserArticlesAsync(string userId)
        {
            return await _unitOfWork.Articles.WhereAsync(a => a.ApplicationUserId == userId);
        }

        // Makale ID'sine göre makale alır
        public async Task<Article> GetArticleByIdAsync(int id)
        {
            var article = await _unitOfWork.Articles.GetByIdAsync(id);

            if (article == null)
                throw new KeyNotFoundException($"Makale {id} bulunamadı.");

            return article;
        }

        // Yeni makale oluşturur ve kategorilere atama yapar
        public async Task CreateArticleAsync(Article article, List<int> categoryIds)
        {
            await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.CommitAsync();

            foreach (var categoryId in categoryIds)
            {
                var articleCategory = new ArticleCategory
                {
                    ArticleId = article.Id,
                    CategoryId = categoryId
                };

                await _unitOfWork.ArticleCategories.AddAsync(articleCategory);
            }

            await _unitOfWork.CommitAsync();
        }

        // Makale güncellenir ve yeni kategoriler atanır
        public async Task UpdateArticleAsync(Article article, List<int> categoryIds)
        {
            _unitOfWork.Articles.Update(article);

            // Eski kategorileri temizledim (ArticleCategory ilişkilerini temizledim)
            var existingCategories = await _unitOfWork.ArticleCategories.WhereAsync(ac => ac.ArticleId == article.Id);
            foreach (var articleCategory in existingCategories)
            {
                _unitOfWork.ArticleCategories.Remove(articleCategory);
            }

            // Yeni kategorileri ekledim.
            foreach (var categoryId in categoryIds)
            {
                var articleCategory = new ArticleCategory
                {
                    ArticleId = article.Id,
                    CategoryId = categoryId
                };
                await _unitOfWork.ArticleCategories.AddAsync(articleCategory);
            }

            await _unitOfWork.CommitAsync();
        }

        // Makale silme işlemi
        public async Task DeleteArticleAsync(int id)
        {
            var article = await _unitOfWork.Articles.GetByIdAsync(id);
            if (article != null)
            {
                _unitOfWork.Articles.Remove(article);
                await _unitOfWork.CommitAsync();
            }
        }

        // Makalelerin tümünü alma işlemi
        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _unitOfWork.Articles.GetAllAsync();
        }
    }
}
