using Microsoft.AspNetCore.Mvc;

namespace ETicaretNew.Controllers
{
    public class DefaultLoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
