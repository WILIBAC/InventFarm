using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Farma.Data;
using Microsoft.EntityFrameworkCore;

namespace Farma.Services
{
    public class MedicamentoMonitorService : BackgroundService
    {
        //private readonly ILogger<MedicamentoMonitorService> _logger;
        //private readonly IServiceProvider _serviceProvider;
        //private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHubContext<MedicamentosHub> _hubContext;

        private HashSet<int> _notifiedExpiredMedicamentos = new HashSet<int>();
        private HashSet<int> _notifiedZeroSaldoMedicamentos = new HashSet<int>();

        public MedicamentoMonitorService(/*ILogger<MedicamentoMonitorService> logger, IServiceProvider serviceProvider*/ IServiceScopeFactory serviceScopeFactory, IHubContext<MedicamentosHub> hubContext)
        {
            //_logger = logger;
            //_serviceProvider = serviceProvider;
            _serviceScopeFactory = serviceScopeFactory;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    await CheckMedicamentosAsync();
            //    await Task.Delay(_checkInterval, stoppingToken);
            //}

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<FarmaciaDbContext>();

                    // Obtener medicamentos vencidos que aún no han sido notificados
                    var medicamentosVencidos = await context.Medicamentos
                        .Where(m => m.FechaVencimiento <= DateTime.Today && !_notifiedExpiredMedicamentos.Contains(m.Id))
                        .ToListAsync(stoppingToken);

                    // Obtener medicamentos con saldo cero que aún no han sido notificados
                    var medicamentosSaldoCero = await context.Medicamentos
                        .Where(m => m.Cantidad == 0 && !_notifiedZeroSaldoMedicamentos.Contains(m.Id))
                        .ToListAsync(stoppingToken);

                    // Notificar sobre medicamentos vencidos
                    if (medicamentosVencidos.Any())
                    {
                        await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Medicamentos vencidos", "Hay medicamentos que han vencido.", stoppingToken);

                        // Agregar los IDs de medicamentos vencidos a la lista de notificados
                        foreach (var medicamento in medicamentosVencidos)
                        {
                            _notifiedExpiredMedicamentos.Add(medicamento.Id);
                        }
                    }

                    // Notificar sobre medicamentos con saldo cero
                    if (medicamentosSaldoCero.Any())
                    {
                        await _hubContext.Clients.All.SendAsync("ReceiveNotification", "Medicamentos con saldo bajo", "Hay medicamentos con saldo en cero.", stoppingToken);

                        // Agregar los IDs de medicamentos con saldo cero a la lista de notificados
                        foreach (var medicamento in medicamentosSaldoCero)
                        {
                            _notifiedZeroSaldoMedicamentos.Add(medicamento.Id);
                        }
                    }
                }

                // Espera un tiempo antes de verificar nuevamente
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        //private async Task CheckMedicamentosAsync()
        //{
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var context = scope.ServiceProvider.GetRequiredService<FarmaciaDbContext>();
        //        var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<MedicamentosHub>>();

        //        var now = DateTime.UtcNow;
        //        var vencidos = context.Medicamentos.Where(m => m.FechaVencimiento < now).ToList();
        //        var saldoCero = context.Medicamentos.Where(m => m.Saldo <= 0).ToList();

        //        if (vencidos.Any())
        //        {
        //            await hubContext.Clients.All.SendAsync("ReceiveNotification", "Medicamentos vencidos", "Hay medicamentos que han vencido.");
        //        }

        //        if (saldoCero.Any())
        //        {
        //            await hubContext.Clients.All.SendAsync("ReceiveNotification", "Medicamentos con saldo bajo", "Hay medicamentos con saldo en cero.");
        //        }
        //    }
        //}
    }
}

