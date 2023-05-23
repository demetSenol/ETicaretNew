using ETicaretNew.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ETicaretNew.Controllers
{
    public class HomeController : Controller
    {
     

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            // _context.Uyes.ToList(); //kendi uye tablomdaki her şeyi alabilirim
           // _context.Uyes.Add(new Uye { adi = "Demet", soyadi = "Şenol", email = "senolldemet@gmail.com",adres="2000 evler",telefonNo=45459774,il="Nevşehir",ilce="merkez",sifre="1234",sifreTekrar="1234"});
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}