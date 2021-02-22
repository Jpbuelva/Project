using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entity
{
    [Table("Region")]
    public class RegionEntity
    {
        [Key]
        public int RegionId { get; set; }

        [Column(TypeName = "varchar(5)")]
        [Required(ErrorMessage = "El campo es requerido")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Nombre { get; set; }

    }
}
