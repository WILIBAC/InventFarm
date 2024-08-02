using Microsoft.AspNetCore.Mvc;
using Farma.Data;
using Farma.Models;
using Microsoft.EntityFrameworkCore;

namespace Farma.Controllers
{
    public class FormasFarmaceuticasController : Controller
    {
        private readonly FarmaciaDbContext _context;

        public FormasFarmaceuticasController(FarmaciaDbContext context)
        {
            _context = context;
        }

        // GET: FormasFarmaceuticas
        public async Task<IActionResult> Index()
        {
            return View(await _context.FormasFarmaceuticas.ToListAsync());
        }

        // GET: FormasFarmaceuticas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FormasFarmaceuticas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormaFarmaceutica formaFarmaceutica)
        {
            ModelState.Remove(nameof(FormaFarmaceutica.Medicamentos));
            if (ModelState.IsValid)
            {
                _context.Add(formaFarmaceutica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formaFarmaceutica);
        }

        // GET: FormasFarmaceuticas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaFarmaceutica = await _context.FormasFarmaceuticas.FindAsync(id);
            if (formaFarmaceutica == null)
            {
                return NotFound();
            }
            return View(formaFarmaceutica);
        }

        // POST: FormasFarmaceuticas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FormaFarmaceutica formaFarmaceutica)
        {
            ModelState.Remove(nameof(FormaFarmaceutica.Medicamentos));
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formaFarmaceutica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormaFarmaceuticaExists(formaFarmaceutica.Id))
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
            return View(formaFarmaceutica);
        }

        // GET: FormasFarmaceuticas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formaFarmaceutica = await _context.FormasFarmaceuticas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formaFarmaceutica == null)
            {
                return NotFound();
            }

            return View(formaFarmaceutica);
        }

        // POST: FormasFarmaceuticas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formaFarmaceutica = await _context.FormasFarmaceuticas.FindAsync(id);
            _context.FormasFarmaceuticas.Remove(formaFarmaceutica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormaFarmaceuticaExists(int id)
        {
            return _context.FormasFarmaceuticas.Any(e => e.Id == id);
        }
    }
}

