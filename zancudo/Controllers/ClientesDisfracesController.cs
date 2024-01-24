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
    public class ClientesDisfracesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesDisfracesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var clientesDisfraces = await _context.ClientesDisfraces
                .Include(cd => cd.Cliente)
                .Include(cd => cd.Disfraz)
                .Include(cd => cd.TipoPago)
                .Where(cd => !cd.EstaBorrado)
                .Select(cd => new
                {
                    Id = cd.Id,
                    Cliente = $"{cd.Cliente.Nombres} {cd.Cliente.Apellidos} -{cd.Cliente.Rut}",
                    Disfraz = $"{cd.Disfraz.Nombre} - {cd.Disfraz.TipoDisfraz.Nombre}",
                    FechaInicio = cd.FechaInicio,
                    FechaFin = cd.FechaFin,
                    TipoPagoId = cd.TipoPago.Nombre,
                    DiasArriendo = cd.DiasArriendo,
                })
                .ToListAsync();

            ViewBag.DataSource = clientesDisfraces;
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteDisfraz = await _context.ClientesDisfraces
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteDisfraz == null)
            {
                return NotFound();
            }

            return View(clienteDisfraz);
        }

        public IActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(_context.Clientes.Where(c => !c.EstaBorrado)
                .Select(c => new { Id = c.Id, NombreCompleto = $"{c.Nombres} {c.Apellidos} #{c.Rut}" })
                .ToList(), "Id", "NombreCompleto");

            ViewBag.DisfrazId = new SelectList(_context.Disfraces
                .Where(c => !c.EstaBorrado)
                .Select(d => new { Id = d.Id, NombreCompleto = $"{d.Nombre} - {d.TipoDisfraz.Nombre}" })
                .ToList(), "Id", "NombreCompleto");

            ViewBag.TipoPagoId = new SelectList(_context.TiposPagos.ToList(), "Id", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,DisfrazId,FechaInicio,FechaFin,TipoPagoId,EstaBorrado")] ClienteDisfraz clienteDisfraz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteDisfraz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clienteDisfraz);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteDisfraz = await _context.ClientesDisfraces.FindAsync(id);
            if (clienteDisfraz == null)
            {
                return NotFound();
            }

            ViewBag.ClienteId = new SelectList(_context.Clientes.Where(c => !c.EstaBorrado)
                .Select(c => new { Id = c.Id, NombreCompleto = $"{c.Nombres} {c.Apellidos} #{c.Rut}" })
                .ToList(), "Id", "NombreCompleto");

            ViewBag.DisfrazId = new SelectList(_context.Disfraces
                .Where(c => !c.EstaBorrado)
                .Select(d => new { Id = d.Id, NombreCompleto = $"{d.Nombre} - {d.TipoDisfraz.Nombre}" })
                .ToList(), "Id", "NombreCompleto");

            ViewBag.TipoPagoId = new SelectList(_context.TiposPagos.ToList(), "Id", "Nombre");

            return View(clienteDisfraz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,DisfrazId,FechaInicio,FechaFin,TipoPagoId,EstaBorrado")] ClienteDisfraz clienteDisfraz)
        {
            if (id != clienteDisfraz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteDisfraz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteDisfrazExists(clienteDisfraz.Id))
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
            return View(clienteDisfraz);
        }

        public async Task<ActionResult> SoftDelete(int id)
        {
            var clienteDisfraz = await _context.ClientesDisfraces.AsTracking().FirstOrDefaultAsync(cd => cd.Id == id);

            if (clienteDisfraz is null)
            {
                return NotFound();
            }

            clienteDisfraz.EstaBorrado = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteDisfrazExists(int id)
        {
            return _context.ClientesDisfraces.Any(e => e.Id == id);
        }
    }
}
