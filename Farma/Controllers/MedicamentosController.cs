using Farma.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using Farma.Data;
using Microsoft.AspNetCore.SignalR;

namespace Farma.Controllers
{
    public class MedicamentosController : Controller
    {
        private readonly FarmaciaDbContext _context;
        private readonly IHubContext<MedicamentosHub> _hubContext;

        public MedicamentosController(FarmaciaDbContext context, IHubContext<MedicamentosHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: Medicamentos
        public async Task<IActionResult> Index()
        {
            var medicamentos = await _context.Medicamentos.Include(m => m.Categoria).ToListAsync();
            //return View(await _context.Medicamentos.ToListAsync());
            return View(medicamentos);
        }

        // GET: Medicamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = "El id en nulo, no se puede continuar";
                return RedirectToAction(nameof(Index), new { });
            }

            var medicamento = await _context.Medicamentos
                .Include(m => m.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicamento == null)
            {
                TempData["ErrorMessage"] = "Medicamento no encontrado en la base de datos";
                return RedirectToAction(nameof(Index), new { });
            }

            return View(medicamento);
        }

        // GET: Medicamentos/Create
        public IActionResult Create()

        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Medicamento medicamento)
        {
            ModelState.Remove(nameof(Medicamento.Categoria));
            if (ModelState.IsValid)
            {
                if(medicamento.FechaVencimiento <= DateTime.Now)
                {
                    TempData["WarningMessage"] = "la fecha de vencimiento tiene que ser superior a la fecha de hoy!";
                    ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre");
                    return View();
                }
                _context.Add(medicamento);
                await _context.SaveChangesAsync();
                // Notificar a los clientes conectados sobre el cambio
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "update", "Medicamento creado");

                TempData["SuccesMessage"] = $"Se ha agregado el {medicamento.Nombre} con éxito";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", medicamento.CategoriaId);
            TempData["ErrorMessage"] = $"No se ha agregado el {medicamento.Nombre} con éxito";
            return View(medicamento);
        }


        // GET: Medicamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();

            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", medicamento.CategoriaId);
            return View(medicamento);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Medicamento medicamento)
        {
            ModelState.Remove(nameof(Medicamento.Categoria));
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicamento);
                    await _context.SaveChangesAsync();
                    // Notificar a los clientes conectados sobre el cambio
                    await _hubContext.Clients.All.SendAsync("ReceiveMessage", "update", "Medicamento editado");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToAction(nameof(Edit), new { id = medicamento.Id });
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", medicamento.CategoriaId);
            return View(medicamento);
        }

        // GET: Medicamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicamento = await _context.Medicamentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medicamento == null)
            {
                return NotFound();
            }

            return View(medicamento);
        }

        // POST: Medicamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            _context.Medicamentos.Remove(medicamento);
            await _context.SaveChangesAsync();
            // Notificar a los clientes conectados sobre el cambio
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "update", "Medicamento eliminado");

            TempData["SuccesMessage"] = $"El elemento {medicamento.Nombre} ha sido eliminado";
            return RedirectToAction(nameof(Index));
        }

        //private bool MedicamentoExists(int id)
        //{
        //    return _context.Medicamentos.Any(e
        //    => e.Id == id);
        //}

        // Método para obtener los medicamentos próximos a vencer
        public async Task<IActionResult> ProximosAVencer(int meses)
        {
            // Obtener la fecha límite en función de los meses proporcionados
            DateTime fechaLimite = DateTime.Now.AddMonths(meses);

            // Filtrar los medicamentos que vencen antes de la fecha límite
            var proximosAVencer = await _context.Medicamentos
                                                .Include(m => m.Categoria)
                                                .Where(m => m.FechaVencimiento <= fechaLimite)
                                                .ToListAsync();

            return View(proximosAVencer);
        }
    }
}
