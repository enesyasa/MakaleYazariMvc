namespace MakaleYazariMvc.Models.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }

    // Category ile Article arasında çoktan çoğa bir ilişkiyi temsil ettim
    public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
}
