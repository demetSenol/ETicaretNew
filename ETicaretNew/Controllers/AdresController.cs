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
    public class AdresController : Controller
    {
        private readonly EticaretContext _context;

        public AdresController()
        {
            _context = new EticaretContext();
        }

        // GET: Adres
        public async Task<IActionResult> Index()
        {
            var eticaretContext = _context.Adres.Include(a => a.Uye);
            return View(await eticaretContext.ToListAsync());
        }

        // GET: Adres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Adres == null)
            {
                return NotFound();
            }

            var adre = await _context.Adres
                .Include(a => a.Uye)
                .FirstOrDefaultAsync(m => m.AdresId == id);
            if (adre == null)
            {
                return NotFound();
            }

            return View(adre);
        }

        // GET: Adres/Create
        public IActionResult Create()
        {
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId");
            return View();
        }

        // POST: Adres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdresId,Adres,UyeId")] Adre adre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", adre.UyeId);
            return View(adre);
        }

        // GET: Adres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Adres == null)
            {
                return NotFound();
            }

            var adre = await _context.Adres.FindAsync(id);
            if (adre == null)
            {
                return NotFound();
            }
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", adre.UyeId);
            return View(adre);
        }

        // POST: Adres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdresId,Adres,UyeId")] Adre adre)
        {
            if (id != adre.AdresId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdreExists(adre.AdresId))
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
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", adre.UyeId);
            return View(adre);
        }

        // GET: Adres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Adres == null)
            {
                return NotFound();
            }

            var adre = await _context.Adres
                .Include(a => a.Uye)
                .FirstOrDefaultAsync(m => m.AdresId == id);
            if (adre == null)
            {
                return NotFound();
            }

            return View(adre);
        }

        // POST: Adres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Adres == null)
            {
                return Problem("Entity set 'EticaretContext.Adres'  is null.");
            }
            var adre = await _context.Adres.FindAsync(id);
            if (adre != null)
            {
                _context.Adres.Remove(adre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdreExists(int id)
        {
          return (_context.Adres?.Any(e => e.AdresId == id)).GetValueOrDefault();
        }
    }
}
