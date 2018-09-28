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
    public class QuestoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Questoes
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questoes.ToListAsync());
        }

        // GET: Questoes/Details/5
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questao = await _context.Questoes
                .SingleOrDefaultAsync(m => m.QuestaoID == id);
            if (questao == null)
            {
                return NotFound();
            }

            return View(questao);
        }

        // GET: Questoes/Create
        [Authorize(Roles = "Administrador, Professor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Create([Bind("QuestaoID,NomeQuestao,Desactivada,Descricao")] Questao questao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questao);
        }

        // GET: Questoes/Edit/5
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questao = await _context.Questoes.SingleOrDefaultAsync(m => m.QuestaoID == id);
            if (questao == null)
            {
                return NotFound();
            }
            return View(questao);
        }

        // POST: Questoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Edit(int id, [Bind("QuestaoID,NomeQuestao,Desactivada,Descricao")] Questao questao)
        {
            if (id != questao.QuestaoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestaoExists(questao.QuestaoID))
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
            return View(questao);
        }

        // GET: Questoes/Delete/5
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questao = await _context.Questoes
                .SingleOrDefaultAsync(m => m.QuestaoID == id);
            if (questao == null)
            {
                return NotFound();
            }

            return View(questao);
        }

        // POST: Questoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questao = await _context.Questoes.SingleOrDefaultAsync(m => m.QuestaoID == id);
            _context.Questoes.Remove(questao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestaoExists(int id)
        {
            return _context.Questoes.Any(e => e.QuestaoID == id);
        }
    }
}
