namespace MakaleYazariMvc.Models.Entities;

public class Article : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }

    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    // Makale ile kategori arasındaki ilişkiyi temsil ettim (çoktan çoğa ilişki)
    public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
}
