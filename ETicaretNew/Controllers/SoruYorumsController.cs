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
    public class SoruYorumsController : Controller
    {
        private readonly EticaretContext _context;

        public SoruYorumsController()
        {
            _context = new EticaretContext();
        }

        // GET: SoruYorums
        public async Task<IActionResult> Index()
        {
            var eticaretContext = _context.SoruYorums.Include(s => s.Urun).Include(s => s.Uye);
            return View(await eticaretContext.ToListAsync());
        }

        // GET: SoruYorums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SoruYorums == null)
            {
                return NotFound();
            }

            var soruYorum = await _context.SoruYorums
                .Include(s => s.Urun)
                .Include(s => s.Uye)
                .FirstOrDefaultAsync(m => m.YorumId == id);
            if (soruYorum == null)
            {
                return NotFound();
            }

            return View(soruYorum);
        }

        // GET: SoruYorums/Create
        public IActionResult Create()
        {
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId");
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId");
            return View();
        }

        // POST: SoruYorums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("YorumId,UyeId,UrunId,Email,YorumTarihSaati,Yorum,KontrolEdildiMi")] SoruYorum soruYorum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soruYorum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", soruYorum.UrunId);
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", soruYorum.UyeId);
            return View(soruYorum);
        }

        // GET: SoruYorums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SoruYorums == null)
            {
                return NotFound();
            }

            var soruYorum = await _context.SoruYorums.FindAsync(id);
            if (soruYorum == null)
            {
                return NotFound();
            }
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", soruYorum.UrunId);
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", soruYorum.UyeId);
            return View(soruYorum);
        }

        // POST: SoruYorums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YorumId,UyeId,UrunId,Email,YorumTarihSaati,Yorum,KontrolEdildiMi")] SoruYorum soruYorum)
        {
            if (id != soruYorum.YorumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soruYorum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoruYorumExists(soruYorum.YorumId))
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
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", soruYorum.UrunId);
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", soruYorum.UyeId);
            return View(soruYorum);
        }

        // GET: SoruYorums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SoruYorums == null)
            {
                return NotFound();
            }

            var soruYorum = await _context.SoruYorums
                .Include(s => s.Urun)
                .Include(s => s.Uye)
                .FirstOrDefaultAsync(m => m.YorumId == id);
            if (soruYorum == null)
            {
                return NotFound();
            }

            return View(soruYorum);
        }

        // POST: SoruYorums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SoruYorums == null)
            {
                return Problem("Entity set 'EticaretContext.SoruYorums'  is null.");
            }
            var soruYorum = await _context.SoruYorums.FindAsync(id);
            if (soruYorum != null)
            {
                _context.SoruYorums.Remove(soruYorum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoruYorumExists(int id)
        {
          return (_context.SoruYorums?.Any(e => e.YorumId == id)).GetValueOrDefault();
        }
    }
}
