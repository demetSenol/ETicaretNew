using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ETicaretNew.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles ="Admin")]
        //denetleyicinin veya eylemin sadece belirli bir roldeki kullanıcılara erişim izni vermesini sağlayan yetkilendirme işlemi için kullanılır
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            //kullanıcının oturumunu sonlandırılır
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //kullanıcının kimlik bilgilerini temizler ve oturumu sonlandırır
            return RedirectToAction("Index", "Login");
        }
    }
}
