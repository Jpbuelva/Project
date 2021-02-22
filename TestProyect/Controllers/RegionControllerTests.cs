using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Project.Controllers;
using Project.Models.Context;
using Project.Models.Entity;
using System;
using System.Threading.Tasks;

namespace TestProyect.Controllers
{
    [TestFixture]
    public class RegionControllerTests : BasePruebas
    {
        [Test]
        public async Task GetRegiones()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            contexto.Region.Add(new RegionEntity() { Codigo = 1234,   Nombre = "Prueba 1" });
            contexto.Region.Add(new RegionEntity() { Codigo = 5678,  Nombre = "Prueba 2" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);

            //Prueba
            var controller = new RegionController(contexto2);
            var respuesta = await controller.Index() as ViewResult;

            //Resultado


            Assert.IsNotNull(respuesta);
            Assert.IsNotNull(respuesta.Model);
        }
        [Test]
        public async Task GetDetailsRegiones()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            contexto.Region.Add(new RegionEntity() { RegionId=1,Codigo = 1234, Nombre = "Prueba 1" });
            contexto.Municipio.Add(new MunicipioEntity() { MunicipioId = 2, Codigo = 5678, Nombre = "Prueba 2" });
            contexto.RegionToMunicipio.Add(new RegionToMunicipioEntity() { RegionToMunicipioId=1,RegionId=1,MunicipioId = 2 });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);

            //Prueba
            int id = 1;
            var controller = new RegionController(contexto2);
            var respuesta = await controller.Details(id) as ViewResult;

            //Resultado


            Assert.IsNotNull(respuesta);
            Assert.IsNotNull(respuesta.Model);
        }

        [Test]
        public async Task CrearMunicipio_CodigoExiste()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            contexto.Region.Add(new RegionEntity() { RegionId = 1, Codigo = 1234, Nombre = "Prueba 1" });

            await contexto.SaveChangesAsync();
            RegionEntity region = new RegionEntity() { Codigo = 1234 , Nombre = "Prueba 1" };         

            //Prueba
            var controller = new RegionController(contexto);

            var respuesta = await controller.Create(region) as ViewResult;

            //Resultado
            Assert.IsNotNull(respuesta);
            Assert.IsNotNull(respuesta.Model);
        }

        [Test]
        public async Task CrearMunicipio()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
        
            RegionEntity region = new RegionEntity() { Codigo = 1234, Nombre = "Prueba 1" };

            //Prueba
            var controller = new RegionController(contexto);

            var respuesta = await controller.Create(region) as ViewResult;

            //Resultado
            Assert.IsNull(respuesta);
        }

        [Test]
        public async Task EditarRegion_OK()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            RegionEntity region = new RegionEntity() { RegionId=1, Codigo = 1234, Nombre = "Prueba 1" };

            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);


            //Prueba
            var controller = new RegionController(contexto2);

            var regioan = new RegionEntity() { RegionId = 1, Codigo = 1234, Nombre = "Prueba 1" };

            var respuesta = await controller.Edit(1, regioan) as ViewResult;

            //Resultado

            Assert.IsNotNull(respuesta);
            Assert.IsNotNull(respuesta.Model);
        }

        [Test]
        public async Task DeleteRegion_Existe()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
             contexto.Region.Add( new RegionEntity() { RegionId = 1, Codigo = 1234, Nombre = "Prueba 1" });
            await contexto.SaveChangesAsync();

            //Prueba
            var controller = new RegionController(contexto);
            int id = 1;
            var respuesta = await controller.Delete(id) as ViewResult;

            //Resultado
            Assert.IsNotNull(respuesta);
            Assert.IsNotNull(respuesta.Model);
        }
         

        [Test]
        public async Task DeleteRegion_DeleteConfirmed()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            contexto.Region.Add(new RegionEntity() { RegionId = 1, Codigo = 1234, Nombre = "Prueba 1" });
            await contexto.SaveChangesAsync();

            //Prueba
            var controller = new RegionController(contexto);
            int id = 1;
            var respuesta = await controller.DeleteConfirmed(id) as ViewResult;

            //Resultado
            Assert.IsNull(respuesta);

        }
    }
}
