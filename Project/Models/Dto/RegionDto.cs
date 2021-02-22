using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Dto
{
    public class RegionDto
    {
        public int RegionId { get; set; }

        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public List<MunicipioDto> DetallesMunicipio { get; set; }

    }
}
