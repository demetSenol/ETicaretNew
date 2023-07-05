using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using ETicaretNew.Models;

namespace ETicaretNew.Controllers
{
    public class LoginController : Controller
    {
        private readonly EticaretContext _context;

        public LoginController()
        {
            _context = new EticaretContext();
        }
        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;// mevcut kullanıcının taleplerini içeren bir "ClaimsPrincipal" nesnesi alınır.
            if (claimUser.Identity.IsAuthenticated)//kullanıcının kimlik doğrulamasının gerçekleşip gerçekleşmediği kontrol edilir
            {
                return RedirectToAction("Index", "Admin");
            }
            //Eğer kullanıcının kimlik doğrulaması gerçekleşmemişse varsayılan "Index" döndürülür.
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] Yonetici p)
        {

            var bilgiler = _context.Yoneticis.FirstOrDefault(x => x.KullaniciAdi== p.KullaniciAdi && x.Password == p.Password);
            if (bilgiler != null)
            {

                List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier,p.KullaniciAdi),
                new Claim("OtherProperties","Demet"),
                new Claim(ClaimTypes.Role,"Admin")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = Convert.ToBoolean(p.Durum)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

               
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                TempData["hata"] = "Giriş bilgileriniz yanlış";
                return RedirectToAction("Index", "Login");
            }
        }


    }
}
