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
    public class Trilho2Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Trilho2Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trilho2
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trilhos2.ToListAsync());
        }

        // GET: Trilho2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trilho2 = await _context.Trilhos2
                .SingleOrDefaultAsync(m => m.TrilhoID == id);
            if (trilho2 == null)
            {
                return NotFound();
            }

            return View(trilho2);
        }

        // GET: Trilho2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trilho2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrilhoID,Nome,Distancia")] Trilho2 trilho2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trilho2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trilho2);
        }

        // GET: Trilho2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trilho2 = await _context.Trilhos2.SingleOrDefaultAsync(m => m.TrilhoID == id);
            if (trilho2 == null)
            {
                return NotFound();
            }
            return View(trilho2);
        }

        // POST: Trilho2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrilhoID,Nome,Distancia")] Trilho2 trilho2)
        {
            if (id != trilho2.TrilhoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trilho2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Trilho2Exists(trilho2.TrilhoID))
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
            return View(trilho2);
        }

        // GET: Trilho2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trilho2 = await _context.Trilhos2
                .SingleOrDefaultAsync(m => m.TrilhoID == id);
            if (trilho2 == null)
            {
                return NotFound();
            }

            return View(trilho2);
        }

        // POST: Trilho2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trilho2 = await _context.Trilhos2.SingleOrDefaultAsync(m => m.TrilhoID == id);
            _context.Trilhos2.Remove(trilho2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Trilho2Exists(int id)
        {
            return _context.Trilhos2.Any(e => e.TrilhoID == id);
        }
    }
}
