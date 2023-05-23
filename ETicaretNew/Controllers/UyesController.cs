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
    public class UyesController : Controller
    {
        private readonly EticaretContext _context;

        public UyesController()
        {
            _context = new EticaretContext();
        }

        // GET: Uyes
        public async Task<IActionResult> Index()
        {
              return _context.Uyes != null ? 
                          View(await _context.Uyes.ToListAsync()) :
                          Problem("Entity set 'EticaretContext.Uyes'  is null.");
        }

        // GET: Uyes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Uyes == null)
            {
                return NotFound();
            }

            var uye = await _context.Uyes
                .FirstOrDefaultAsync(m => m.UyeId == id);
            if (uye == null)
            {
                return NotFound();
            }

            return View(uye);
        }

        // GET: Uyes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uyes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UyeId,Adi,Soyadi,Email,TelefonNo,Adres,Ilce,Il,PostaKodu,Sifre,SifreTekrar")] Uye uye)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uye);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uye);
        }

        // GET: Uyes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Uyes == null)
            {
                return NotFound();
            }

            var uye = await _context.Uyes.FindAsync(id);
            if (uye == null)
            {
                return NotFound();
            }
            return View(uye);
        }

        // POST: Uyes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UyeId,Adi,Soyadi,Email,TelefonNo,Adres,Ilce,Il,PostaKodu,Sifre,SifreTekrar")] Uye uye)
        {
            if (id != uye.UyeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uye);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UyeExists(uye.UyeId))
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
            return View(uye);
        }

        // GET: Uyes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Uyes == null)
            {
                return NotFound();
            }

            var uye = await _context.Uyes
                .FirstOrDefaultAsync(m => m.UyeId == id);
            if (uye == null)
            {
                return NotFound();
            }

            return View(uye);
        }

        // POST: Uyes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Uyes == null)
            {
                return Problem("Entity set 'EticaretContext.Uyes'  is null.");
            }
            var uye = await _context.Uyes.FindAsync(id);
            if (uye != null)
            {
                _context.Uyes.Remove(uye);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UyeExists(int id)
        {
          return (_context.Uyes?.Any(e => e.UyeId == id)).GetValueOrDefault();
        }
    }
}
