using Project.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entity
{
    [Table("Municipio")]
    public class MunicipioEntity
    {
        [Key]
        public int MunicipioId { get; set; }


        [Column(TypeName = "varchar(5)")]
       
        [Required(ErrorMessage = "El campo es requerido")]
        public int Codigo { get; set; }
       
        [Required(ErrorMessage = "El campo es requerido")]
        public string Nombre { get; set; }
       
        
        public bool Estado { get; set; }
 


    }
}
