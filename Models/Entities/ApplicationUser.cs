using Microsoft.AspNetCore.Identity;

namespace MakaleYazariMvc.Models.Entities;


public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }

    // Bir kullanıcı birden fazla makale yazabilir
    public ICollection<Article> Articles { get; set; }
}
