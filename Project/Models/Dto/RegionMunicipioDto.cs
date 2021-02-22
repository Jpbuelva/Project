using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Project.Models.Dto
{
    public class RegionMunicipioDto
    {
        public List<MunicipioDto> MunicipiosList { get; set; }
        public SelectList RegionListCombox { get; set; }
        public int RegionId { get; set; }
        public int MunicipioId { get; set; }

        public bool Active { get; set; }

    }
}
