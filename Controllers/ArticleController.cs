using MakaleYazariMvc.Models.Entities;
using MakaleYazariMvc.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MakaleYazariMvc.Controllers;

[Authorize]
public class ArticleController : Controller
{
    private readonly IArticleService _articleService;
    private readonly ICategoryService _categoryService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ArticleController(IArticleService articleService, ICategoryService categoryService, UserManager<ApplicationUser> userManager)
    {
        _articleService = articleService;
        _categoryService = categoryService;
        _userManager = userManager;
    }

    // Kullanıcının kendi makalelerini gösterir
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User); // Giriş yapan kullanıcının ID'si
        var articles = await _articleService.GetUserArticlesAsync(userId); // Kullanıcının makaleleri
        return View(articles);
    }

    // Makale oluşturma sayfasını render et
    public IActionResult Create()
    {
        // Kategorileri ViewBag'e gönderdim
        var categories = _categoryService.GetAllCategoriesAsync().Result; // Kategorileri aldım
        ViewBag.Categories = categories;

        return View();
    }

    // Makale oluşturma işlemi
    [HttpPost]
    public async Task<IActionResult> Create(Article article, List<int> categoryIds)
    {
        if (!ModelState.IsValid)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories;
            return View(article);
        }

        // Makale ve kategorileri ekle
        await _articleService.CreateArticleAsync(article, categoryIds);
        return RedirectToAction("Index"); // Başarıyla ekledikten sonra Index sayfasına yönlendirdim.
    }

    // Makale düzenleme sayfasını gösterir
    public async Task<IActionResult> Edit(int id)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        if (article == null || article.ApplicationUserId != _userManager.GetUserId(User)) // Kullanıcı yalnızca kendi makalesini düzenleyebilir.
        {
            return Unauthorized();
        }

        var categories = await _categoryService.GetAllCategoriesAsync();
        ViewBag.Categories = categories;
        return View(article);
    }

    // Makale güncelleme işlemi
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Article article, List<int> categoryIds)
    {
        if (id != article.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var existingArticle = await _articleService.GetArticleByIdAsync(id);
            if (existingArticle == null || existingArticle.ApplicationUserId != _userManager.GetUserId(User)) // Kullanıcı yalnızca kendi makalesini düzenleyebilir.
            {
                return Unauthorized();
            }

            await _articleService.UpdateArticleAsync(article, categoryIds); // Makale güncelleme işlemi
            return RedirectToAction(nameof(Index)); // Makale listesine yönlendirdim.
        }

        var categories = await _categoryService.GetAllCategoriesAsync();
        ViewBag.Categories = categories;
        return View(article);
    }

    // Makale silme işlemi
    public async Task<IActionResult> Delete(int id)
    {
        var article = await _articleService.GetArticleByIdAsync(id); // Makaleyi al
        if (article == null || article.ApplicationUserId != _userManager.GetUserId(User)) // Kullanıcı yalnızca kendi makalesini silebilir
        {
            return Unauthorized();
        }

        return View(article); // Silme onay sayfasına yönlendir
    }

    // Silme işlemini gerçekleştirir
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        if (article == null || article.ApplicationUserId != _userManager.GetUserId(User)) // Kullanıcı yalnızca kendi makalesini silebilir.
        {
            return Unauthorized();
        }

        await _articleService.DeleteArticleAsync(id);
        return RedirectToAction(nameof(Index)); // Makale listesine yönlendirdim.
    }

    // Makale detay sayfası
    public async Task<IActionResult> Details(int id)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        if (article == null)
        {
            return NotFound();
        }

        return View(article);
    }
}