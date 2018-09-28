using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trails4Health.Models;

namespace Trails4Health.Controllers
{
    public class ReservasGuiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservasGuiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReservasGuia
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReservasGuia.Include(r => r.Guia).Include(r => r.Trilho2).Include(r => r.Turista);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ReservasGuia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaGuia = await _context.ReservasGuia
                .Include(r => r.Guia)
                .Include(r => r.Trilho2)
                .Include(r => r.Turista)
                .SingleOrDefaultAsync(m => m.ReservaID == id);
            if (reservaGuia == null)
            {
                return NotFound();
            }

            return View(reservaGuia);
        }

        // GET: ReservasGuia/Create
        [Authorize(Roles = "Turista")]
        public IActionResult Create()
        {
            ViewData["GuiaID"] = new SelectList(_context.Guias, "GuiaID", "Nome");
            ViewData["TrilhoID"] = new SelectList(_context.Trilhos2, "TrilhoID", "Nome");
            ViewData["TuristaID"] = new SelectList(_context.Turistas, "TuristaID", "Nome");
            return View();
        }

        // POST: ReservasGuia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Turista")]
        public async Task<IActionResult> Create([Bind("ReservaID,ReservaParaDia,GuiaID,TuristaID,TrilhoID")] ReservaGuia reservaGuia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservaGuia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuiaID"] = new SelectList(_context.Guias, "GuiaID", "Nome", reservaGuia.GuiaID);
            ViewData["TrilhoID"] = new SelectList(_context.Trilhos2, "TrilhoID", "Nome", reservaGuia.TrilhoID);
            ViewData["TuristaID"] = new SelectList(_context.Turistas, "TuristaID", "Nome", reservaGuia.TuristaID);
            return View(reservaGuia);
        }

        // GET: ReservasGuia/Edit/5
        [Authorize(Roles = "Turista")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaGuia = await _context.ReservasGuia.SingleOrDefaultAsync(m => m.ReservaID == id);
            if (reservaGuia == null)
            {
                return NotFound();
            }
            ViewData["GuiaID"] = new SelectList(_context.Guias, "GuiaID", "Nome", reservaGuia.GuiaID);
            ViewData["TrilhoID"] = new SelectList(_context.Trilhos2, "TrilhoID", "Nome", reservaGuia.TrilhoID);
            ViewData["TuristaID"] = new SelectList(_context.Turistas, "TuristaID", "Nome", reservaGuia.TuristaID);
            return View(reservaGuia);
        }

        // POST: ReservasGuia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Turista")]
        public async Task<IActionResult> Edit(int id, [Bind("ReservaID,ReservaParaDia,GuiaID,TuristaID,TrilhoID")] ReservaGuia reservaGuia)
        {
            if (id != reservaGuia.ReservaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservaGuia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaGuiaExists(reservaGuia.ReservaID))
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
            ViewData["GuiaID"] = new SelectList(_context.Guias, "GuiaID", "Nome", reservaGuia.GuiaID);
            ViewData["TrilhoID"] = new SelectList(_context.Trilhos2, "TrilhoID", "Nome", reservaGuia.TrilhoID);
            ViewData["TuristaID"] = new SelectList(_context.Turistas, "TuristaID", "Nome", reservaGuia.TuristaID);
            return View(reservaGuia);
        }

        // GET: ReservasGuia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservaGuia = await _context.ReservasGuia
                .Include(r => r.Guia)
                .Include(r => r.Trilho2)
                .Include(r => r.Turista)
                .SingleOrDefaultAsync(m => m.ReservaID == id);
            if (reservaGuia == null)
            {
                return NotFound();
            }

            return View(reservaGuia);
        }

        // POST: ReservasGuia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservaGuia = await _context.ReservasGuia.SingleOrDefaultAsync(m => m.ReservaID == id);
            _context.ReservasGuia.Remove(reservaGuia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaGuiaExists(int id)
        {
            return _context.ReservasGuia.Any(e => e.ReservaID == id);
        }
    }
}
