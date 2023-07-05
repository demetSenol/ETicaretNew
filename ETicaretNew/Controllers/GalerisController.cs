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
    public class GalerisController : Controller
    {
        private readonly EticaretContext _context;

        public GalerisController()
        {
            _context = new EticaretContext();
        }

        // GET: Galeris
        public async Task<IActionResult> Index()
        {
            var eticaretContext = _context.Galeris.Include(g => g.Urun);
            return View(await eticaretContext.ToListAsync());
        }

        // GET: Galeris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Galeris == null)
            {
                return NotFound();
            }

            var galeri = await _context.Galeris
                .Include(g => g.Urun)
                .FirstOrDefaultAsync(m => m.ResimId == id);
            if (galeri == null)
            {
                return NotFound();
            }

            return View(galeri);
        }

        // GET: Galeris/Create
        public IActionResult Create()
        {
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "Adi");
           
            return View();
        }

        // POST: Galeris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Galeri galeri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galeri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", galeri.UrunId);
            return View(galeri);
        }

        // GET: Galeris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Galeris == null)
            {
                return NotFound();
            }

            var galeri = await _context.Galeris.FindAsync(id);
            if (galeri == null)
            {
                return NotFound();
            }
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", galeri.UrunId);
            return View(galeri);
        }

        // POST: Galeris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResimId,Resim,UrunId")] Galeri galeri)
        {
            if (id != galeri.ResimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galeri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GaleriExists(galeri.ResimId))
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
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", galeri.UrunId);
            return View(galeri);
        }

        // GET: Galeris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Galeris == null)
            {
                return NotFound();
            }

            var galeri = await _context.Galeris
                .Include(g => g.Urun)
                .FirstOrDefaultAsync(m => m.ResimId == id);
            if (galeri == null)
            {
                return NotFound();
            }

            return View(galeri);
        }

        // POST: Galeris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Galeris == null)
            {
                return Problem("Entity set 'EticaretContext.Galeris'  is null.");
            }
            var galeri = await _context.Galeris.FindAsync(id);
            if (galeri != null)
            {
                _context.Galeris.Remove(galeri);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GaleriExists(int id)
        {
          return (_context.Galeris?.Any(e => e.ResimId == id)).GetValueOrDefault();
        }
    }
}
