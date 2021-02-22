using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entity
{
    [Table("RegionToMunicipio")]
    public class RegionToMunicipioEntity
    {
        [Key]
        public int RegionToMunicipioId { get; set; }

        [ForeignKey("RegionEntity")]
        public int RegionId { get; set; }
         

        [ForeignKey("MunicipioEntity")]
        public int MunicipioId { get; set; }

        public MunicipioEntity MunicipioEntity { get; set; }
        public RegionEntity RegionEntity { get; set; }
    


    }
}
