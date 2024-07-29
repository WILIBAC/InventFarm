using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farma.Models
{
    [Table("Medicamentos")]
    public class Medicamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Display(Name = "Fecha de Vencimiento")]
        public DateTime FechaVencimiento { get; set; }
        public string Lote { get; set; }
        public int Saldo { get; set; }
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

    }

}
