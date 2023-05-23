using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ETicaretNew.Models;

namespace ETicaretNew.Controllers
{
    public class UrunsController : Controller
    {
        private EticaretContext _context;

        public UrunsController()
        {
            _context = new EticaretContext();
        }

        // GET: Uruns
        public IActionResult Index()
        {
            _context=new EticaretContext();
              return _context.Uruns != null ? 
                          View( _context.Uruns.ToList()) :
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
                .FirstOrDefaultAsync(m => m.UrunId == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // GET: Uruns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uruns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UrunId,Adi,Aciklama,Fiyat,Anasayfa,Stok,KategoriId")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(urun);
        }

        // GET: Uruns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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
        public async Task<IActionResult> Edit(int id, [Bind("UrunId,Adi,Aciklama,Fiyat,Anasayfa,Stok,KategoriId")] Urun urun)
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

            var urun = await _context.Uruns
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
