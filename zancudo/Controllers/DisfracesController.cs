using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zancudo;
using zancudo.Entidades;

namespace zancudo.Controllers
{
    public class DisfracesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisfracesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IQueryable<Disfraz> disfracesQuery = _context.Disfraces.Where(c => !c.EstaBorrado);

            var disfracesData = disfracesQuery
                .Select(d => new
                {
                    Id = d.Id,
                    Nombre = d.Nombre,
                    TipoDisfraz = d.TipoDisfraz.Nombre,
                })
                .ToList();

            ViewBag.DataSource = disfracesData;
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.TipoDisfrazId = new SelectList(_context.TiposDisfraces.ToList(), "Id", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,TipoDisfrazId,EstaBorrado")] Disfraz disfraz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disfraz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disfraz);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disfraz = await _context.Disfraces.FindAsync(id);

            if (disfraz == null)
            {
                return NotFound();
            }

            ViewBag.TipoDisfrazId = new SelectList(_context.TiposDisfraces.ToList(), "Id", "Nombre");

            return View(disfraz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,TipoDisfrazId,EstaBorrado")] Disfraz disfraz)
        {
            if (id != disfraz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disfraz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisfrazExists(disfraz.Id))
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
            return View(disfraz);
        }

        public async Task<ActionResult> SoftDelete(int id)
        {
            var disfraz = await _context.Disfraces.AsTracking().FirstOrDefaultAsync(d => d.Id == id);

            if (disfraz is null)
            {
                return NotFound();
            }

            disfraz.EstaBorrado = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private bool DisfrazExists(int id)
        {
            return _context.Disfraces.Any(e => e.Id == id);
        }
    }
}
