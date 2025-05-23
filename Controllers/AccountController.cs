using MakaleYazariMvc.Models.Entities;
using MakaleYazariMvc.Models.VMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MakaleYazariMvc.Controllers
{
    public class AccountController : Controller
    {
        // Uygulama kullanıcılarıyla işlem yapmak için UserManager ve SignInManager servisleri enjekte ettim.
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // Constructor
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Register sayfasına yönlendirme işlemi yaptım
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }




        // Kullanıcı kaydı yapılırken, önce model doğrulamasını yaptım. 
        // Kullanıcı başarıyla oluşturulursa, giriş işlemi yapılır ve anasayfaya yönlendirilir. 
        // Hata durumunda hata mesajları model durumuna eklenir.
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        // Kullanıcı giriş sayfasını görüntüledim.
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        // Kullanıcı giriş işlemi yaptım ve doğrulama gerçekleştirdim.
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Geçersiz giriş. Lütfen tekrar deneyin.");
            return View(model);
        }

        // Kullanıcı çıkış işlemi yaptım ve anasayfaya yönlendirdim.
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
