using MakaleYazariMvc.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace MakaleYazariMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public HomeController(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }

        // Anasayfa, tüm makaleleri ve kategorileri gösterir
        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetAllArticlesAsync(); // Tüm makaleler
            var categories = await _categoryService.GetAllCategoriesAsync(); // Tüm kategoriler
            ViewBag.Categories = categories; // Viewbag ile kategorileri view'e gönderdim.
            return View(articles);
        }
    }
}
