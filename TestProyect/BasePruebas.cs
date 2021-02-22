using Microsoft.EntityFrameworkCore;
using Project.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProyect
{
    public class BasePruebas
    {
        protected ProjectContext ConstruirContext(string nombreDB)
        {
            var opciones = new DbContextOptionsBuilder<ProjectContext>()
                .UseInMemoryDatabase(nombreDB).Options;


            var dbContext = new ProjectContext(opciones);
            return dbContext;
        }

    }
}
