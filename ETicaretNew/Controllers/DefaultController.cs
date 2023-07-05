using ETicaretNew.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaretNew.Controllers
{
        [AllowAnonymous]
    public class DefaultController : Controller
    {
		private EticaretContext _context;

		public DefaultController()
		{
			_context = new EticaretContext();
		}
		//public IActionResult Index()  //eski olan 
		//      {
		//          return View();
		//      }
		public IActionResult Index()
		{
			_context = new EticaretContext();
			return _context.Uruns != null ?
						View(_context.Uruns.ToList()) :
						Problem("Entity set 'EticaretContext.Uruns'  is null.");
		}
		public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
		public async Task<IActionResult> Product(int? id)
		{
			if (id == null || _context.Uruns == null)
			{
				return NotFound();
			}

			var urun = await _context.Uruns
				.FirstOrDefaultAsync(m => m.UrunId == id);
			if (urun == null)
			{
				return NotFound();
			}

			return View(urun);
		}

		public IActionResult Store()
		{
			return View();	
		}
    }
}
