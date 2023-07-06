using ETicaretNew.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ETicaretNew.Controllers
{
    public class UrunsController : Controller
    {
        private EticaretContext _context;

        public UrunsController()
        {
            _context = new EticaretContext();
        }
        PublicClass publicClass = new PublicClass();
        // GET: Uruns
        public IActionResult Index()
        {
             _context=new EticaretContext();
              return _context.Uruns != null ? 
                          View( _context.Uruns.Include(x => x.Kategori).ToList()) :
                          Problem("Entity set 'EticaretContext.Uruns'  is null.");
        }

        // GET: Uruns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Uruns == null)
            {
                return NotFound();
            }

                   var urun = await _context.Uruns
    .Include(x => x.Kategori) // İlişkili Kategori varlığını yükle
    .FirstOrDefaultAsync(m => m.UrunId == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // GET: Uruns/Create
        public async Task<IActionResult> Create()
        {
            
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "KategoriId", "Adi");
            return View();

        }
        // POST: Uruns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Urun urun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urun);
                foreach (var file in urun.ImageFile)
                {
                    var galeri = new Galeri
                    {
                        Resim = publicClass.ImgToBase64(file) //viewden aldığımız resmi gönderdik karşılığında base64 olarak döndürdü
                    };

                    urun.Galeris.Add(galeri); // Her bir resmi urunun galerisine ekler 
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(urun);
        }

        // GET: Uruns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategoris, "KategoriId", "Adi");
            if (id == null || _context.Uruns == null)
            {
                return NotFound();
            }

            var urun = await _context.Uruns.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }
            return View(urun);
        }

        // POST: Uruns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Urun urun)
        {
            if (id != urun.UrunId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urun);
                    foreach (var file in urun.ImageFile)
                    {
                        var galeri = new Galeri
                        {
                            Resim = publicClass.ImgToBase64(file)
                        };

                        urun.Galeris.Add(galeri); // Her bir resmi urun.Galeris'e ekleyin
                    }
                    await _context.SaveChangesAsync();
                }
                   
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunExists(urun.UrunId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(urun);
        }

        // GET: Uruns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Uruns == null)
            {
                return NotFound();
            }

            //var urun = await _context.Include<Urun>(x=> x.KategoriId).Uruns
            //    .FirstOrDefaultAsync(m => m.UrunId == id);
            var urun = await _context.Uruns
    .Include(x => x.Kategori) // İlişkili Kategori varlığını yükle
    .FirstOrDefaultAsync(m => m.UrunId == id);
            if (urun == null)
            {
                return NotFound();
            }
            return View(urun);
        }

        // POST: Uruns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Uruns == null)
            {
                return Problem("Entity set 'EticaretContext.Uruns'  is null.");
            }
            var urun = await _context.Uruns.FindAsync(id);
            if (urun != null)
            {
                var resims = _context.Galeris.Where(m => m.UrunId == id).ToList();
                if (resims.Count > 0)
                {
                    foreach (var resim in resims)
                    {
                        _context.Galeris.Remove(resim);
                    }
                }
                _context.Uruns.Remove(urun);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunExists(int id)
        {
          return (_context.Uruns?.Any(e => e.UrunId == id)).GetValueOrDefault();
        }
    }
}
