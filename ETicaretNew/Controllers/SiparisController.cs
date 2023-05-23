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
    public class SiparisController : Controller
    {
        private readonly EticaretContext _context;

        public SiparisController()
        {
            _context = new EticaretContext();
        }

        // GET: Siparis
        public async Task<IActionResult> Index()
        {
            var eticaretContext = _context.Siparis.Include(s => s.Adres).Include(s => s.Uye);
            return View(await eticaretContext.ToListAsync());
        }

        // GET: Siparis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Siparis == null)
            {
                return NotFound();
            }

            var sipari = await _context.Siparis
                .Include(s => s.Adres)
                .Include(s => s.Uye)
                .FirstOrDefaultAsync(m => m.SiparisId == id);
            if (sipari == null)
            {
                return NotFound();
            }

            return View(sipari);
        }

        // GET: Siparis/Create
        public IActionResult Create()
        {
            ViewData["AdresId"] = new SelectList(_context.Adres, "AdresId", "AdresId");
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId");
            return View();
        }

        // POST: Siparis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiparisId,UyeId,AdresId,Tutar")] Sipari sipari)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sipari);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresId"] = new SelectList(_context.Adres, "AdresId", "AdresId", sipari.AdresId);
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", sipari.UyeId);
            return View(sipari);
        }

        // GET: Siparis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Siparis == null)
            {
                return NotFound();
            }

            var sipari = await _context.Siparis.FindAsync(id);
            if (sipari == null)
            {
                return NotFound();
            }
            ViewData["AdresId"] = new SelectList(_context.Adres, "AdresId", "AdresId", sipari.AdresId);
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", sipari.UyeId);
            return View(sipari);
        }

        // POST: Siparis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SiparisId,UyeId,AdresId,Tutar")] Sipari sipari)
        {
            if (id != sipari.SiparisId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sipari);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SipariExists(sipari.SiparisId))
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
            ViewData["AdresId"] = new SelectList(_context.Adres, "AdresId", "AdresId", sipari.AdresId);
            ViewData["UyeId"] = new SelectList(_context.Uyes, "UyeId", "UyeId", sipari.UyeId);
            return View(sipari);
        }

        // GET: Siparis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Siparis == null)
            {
                return NotFound();
            }

            var sipari = await _context.Siparis
                .Include(s => s.Adres)
                .Include(s => s.Uye)
                .FirstOrDefaultAsync(m => m.SiparisId == id);
            if (sipari == null)
            {
                return NotFound();
            }

            return View(sipari);
        }

        // POST: Siparis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Siparis == null)
            {
                return Problem("Entity set 'EticaretContext.Siparis'  is null.");
            }
            var sipari = await _context.Siparis.FindAsync(id);
            if (sipari != null)
            {
                _context.Siparis.Remove(sipari);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SipariExists(int id)
        {
          return (_context.Siparis?.Any(e => e.SiparisId == id)).GetValueOrDefault();
        }
    }
}
