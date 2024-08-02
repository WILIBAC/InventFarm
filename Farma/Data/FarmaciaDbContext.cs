using Farma.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Farma.Data
{
    public class FarmaciaDbContext : DbContext
    {
        public FarmaciaDbContext(DbContextOptions<FarmaciaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<FormaFarmaceutica> FormasFarmaceuticas { get; set; }
    }

}
