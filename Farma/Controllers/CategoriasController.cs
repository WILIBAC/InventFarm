using Farma.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Farma.Data;

namespace Farma.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly FarmaciaDbContext _context;

        public CategoriasController(FarmaciaDbContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "El id en nulo, no se puede continuar";
                return RedirectToAction(nameof(Index));
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                TempData["ErrorMessage"] = "Categoría no encontrada en la base de datos";
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        [HttpPost]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            ModelState.Remove(nameof(Categoria.Medicamentos));
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Categoría creada con éxito";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Error al crear la categoría";
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Categoria categoria)
        {
            ModelState.Remove(nameof(Categoria.Medicamentos));
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Categoría editada con éxito";
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    if (!CategoriaExists(categoria.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Categoría eliminada con éxito";
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}

