using ETicaretNew.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ETicaretNew.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly EticaretContext _context;

        public UserLoginController()
        {
            _context = new EticaretContext();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] User p)
        {

            var bilgiler = _context.Users.FirstOrDefault(x => x.Email == p.Email && x.Sifre == p.Sifre);
            if (bilgiler != null)
            {

                List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier,p.Email),
				new Claim("OtherProperties","Demet"),
				new Claim(ClaimTypes.Role,"User")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    //IsPersistent = Convert.ToBoolean(p.Durum)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);


                return RedirectToAction("Index", "Default");
            }
            else
            {
                TempData["hata"] = "Giriş bilgileriniz yanlış";
                return RedirectToAction("UserLogin", "Login");
            }
        }
      
    }
}
