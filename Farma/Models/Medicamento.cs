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
        public string Producto { get; set; }
        [Display(Name = "Forma Farmaceutica")]
        public int FormaFarmaceuticaId { get; set; }
        public FormaFarmaceutica FormaFarmaceutica { get; set; }
        [Display(Name = "Fecha de Vencimiento")]
        public DateTime FechaVencimiento { get; set; }
        public string Lote { get; set; }
        public int Cantidad { get; set; }
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

    }

}
