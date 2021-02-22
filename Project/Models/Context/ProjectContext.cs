using Microsoft.EntityFrameworkCore;
using Project.Models.Entity;

namespace Project.Models.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }

        public DbSet<MunicipioEntity> Municipio { get; set; }
        public DbSet<RegionEntity> Region { get; set; }
        public DbSet<RegionToMunicipioEntity> RegionToMunicipio { get; set; }
    }
}
