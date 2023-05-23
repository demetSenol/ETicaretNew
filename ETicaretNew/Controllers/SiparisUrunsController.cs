﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ETicaretNew.Models;

namespace ETicaretNew.Controllers
{
    public class SiparisUrunsController : Controller
    {
        private readonly EticaretContext _context;

        public SiparisUrunsController()
        {
            _context = new EticaretContext();
        }

        // GET: SiparisUruns
        public async Task<IActionResult> Index()
        {
            var eticaretContext = _context.SiparisUruns.Include(s => s.Siparis).Include(s => s.Urun);
            return View(await eticaretContext.ToListAsync());
        }

        // GET: SiparisUruns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SiparisUruns == null)
            {
                return NotFound();
            }

            var siparisUrun = await _context.SiparisUruns
                .Include(s => s.Siparis)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.KayitId == id);
            if (siparisUrun == null)
            {
                return NotFound();
            }

            return View(siparisUrun);
        }

        // GET: SiparisUruns/Create
        public IActionResult Create()
        {
            ViewData["SiparisId"] = new SelectList(_context.Siparis, "SiparisId", "SiparisId");
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId");
            return View();
        }

        // POST: SiparisUruns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KayitId,SiparisId,UrunId,SiparisTarihi,SiparisDurumu,Adet,BririmFiyat")] SiparisUrun siparisUrun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siparisUrun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SiparisId"] = new SelectList(_context.Siparis, "SiparisId", "SiparisId", siparisUrun.SiparisId);
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", siparisUrun.UrunId);
            return View(siparisUrun);
        }

        // GET: SiparisUruns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SiparisUruns == null)
            {
                return NotFound();
            }

            var siparisUrun = await _context.SiparisUruns.FindAsync(id);
            if (siparisUrun == null)
            {
                return NotFound();
            }
            ViewData["SiparisId"] = new SelectList(_context.Siparis, "SiparisId", "SiparisId", siparisUrun.SiparisId);
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", siparisUrun.UrunId);
            return View(siparisUrun);
        }

        // POST: SiparisUruns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KayitId,SiparisId,UrunId,SiparisTarihi,SiparisDurumu,Adet,BririmFiyat")] SiparisUrun siparisUrun)
        {
            if (id != siparisUrun.KayitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siparisUrun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiparisUrunExists(siparisUrun.KayitId))
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
            ViewData["SiparisId"] = new SelectList(_context.Siparis, "SiparisId", "SiparisId", siparisUrun.SiparisId);
            ViewData["UrunId"] = new SelectList(_context.Uruns, "UrunId", "UrunId", siparisUrun.UrunId);
            return View(siparisUrun);
        }

        // GET: SiparisUruns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SiparisUruns == null)
            {
                return NotFound();
            }

            var siparisUrun = await _context.SiparisUruns
                .Include(s => s.Siparis)
                .Include(s => s.Urun)
                .FirstOrDefaultAsync(m => m.KayitId == id);
            if (siparisUrun == null)
            {
                return NotFound();
            }

            return View(siparisUrun);
        }

        // POST: SiparisUruns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SiparisUruns == null)
            {
                return Problem("Entity set 'EticaretContext.SiparisUruns'  is null.");
            }
            var siparisUrun = await _context.SiparisUruns.FindAsync(id);
            if (siparisUrun != null)
            {
                _context.SiparisUruns.Remove(siparisUrun);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiparisUrunExists(int id)
        {
          return (_context.SiparisUruns?.Any(e => e.KayitId == id)).GetValueOrDefault();
        }
    }
}