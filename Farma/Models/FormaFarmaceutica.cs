using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Farma.Models
{
    [Table("FormasFarmaceuticas")]
    public class FormaFarmaceutica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Display(Name = "Forma Farmacéutica")]
        public string Nombre { get; set; }

        public ICollection<Medicamento> Medicamentos { get; set; }
    }
}

