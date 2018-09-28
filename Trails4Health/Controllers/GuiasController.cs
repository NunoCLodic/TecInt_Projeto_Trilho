using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trails4Health.Models;

namespace Trails4Health.Controllers
{
    public class GuiasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuiasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Guias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Guias.ToListAsync());
        }

        // GET: Guias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guia = await _context.Guias
                .SingleOrDefaultAsync(m => m.GuiaID == id);
            if (guia == null)
            {
                return NotFound();
            }

            return View(guia);
        }

        // GET: Guias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuiaID,Nome,Telefone,Email,DataNascimento,Avaliacao")] Guia guia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guia);
        }

        // GET: Guias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guia = await _context.Guias.SingleOrDefaultAsync(m => m.GuiaID == id);
            if (guia == null)
            {
                return NotFound();
            }
            return View(guia);
        }

        // POST: Guias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GuiaID,Nome,Telefone,Email,DataNascimento,Avaliacao")] Guia guia)
        {
            if (id != guia.GuiaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuiaExists(guia.GuiaID))
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
            return View(guia);
        }

        // GET: Guias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guia = await _context.Guias
                .SingleOrDefaultAsync(m => m.GuiaID == id);
            if (guia == null)
            {
                return NotFound();
            }

            return View(guia);
        }

        // POST: Guias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guia = await _context.Guias.SingleOrDefaultAsync(m => m.GuiaID == id);
            _context.Guias.Remove(guia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuiaExists(int id)
        {
            return _context.Guias.Any(e => e.GuiaID == id);
        }
    }
}
