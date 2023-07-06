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
			return _context.Uruns != null ?
						View(_context.Uruns.ToList()) :
						Problem("Entity set 'EticaretContext.Uruns'  is null.");
		}
		// GET: Siparis/Details/5
		public async Task<IActionResult> Sepet(int? id)
		{
			if (id == null || _context.Siparis == null)
			{
				return NotFound();
			}

			var sipari = await _context.Siparis
				.Include(s => s.Adres)
				.Include(s => s.SiparisUruns)
				 .ThenInclude(u => u.Urun)
				.Include(s => s.Uye)
				.FirstOrDefaultAsync(m => m.SiparisId == id);
			if (sipari == null)
			{
				return NotFound();
			}

			return View(sipari);
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

		public IActionResult Store(int? id)
		{
            return _context.Uruns != null ?
                        View(_context.Uruns //.Include(k=>k.Kategori)
						.Where(x=>x.KategoriId==id).ToList()) :
                        Problem("Entity set 'EticaretContext.Uruns'  is null.");
        }

		public IActionResult SepetCheckout() 
		{
			return View();
		}
    }
}
