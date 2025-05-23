using FluentValidation;
using MakaleYazariMvc.Models.Entities;

namespace MakaleYazariMvc.Validation;

/// <summary>
/// Makale doğrulama sınıfı
/// </summary>
public class ArticleValidator : AbstractValidator<Article>
{
    public ArticleValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MinimumLength(5).WithMessage("Başlık en az 5 karakter olmalıdır.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("İçerik boş olamaz.")
            .MinimumLength(20).WithMessage("İçerik en az 20 karakter olmalıdır.");

        RuleFor(x => x.ArticleCategories)
            .Must(x => x != null && x.Any()).WithMessage("En az bir kategori seçmelisiniz.");
    }
}
