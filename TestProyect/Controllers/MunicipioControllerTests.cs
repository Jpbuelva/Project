using Microsoft.AspNetCore.Mvc;
 
using NUnit.Framework;
using Project.Controllers;
using Project.Models.Entity;
using System;
using System.Threading.Tasks;

namespace TestProyect.Controllers
{
    [TestFixture]
    public class MunicipioControllerTests : BasePruebas
    {
        [Test]
        public async Task ObtenerMunicipio()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            contexto.Municipio.Add(new MunicipioEntity() { Codigo=1234, Estado=true, Nombre="Prueba 1"});
            contexto.Municipio.Add(new MunicipioEntity() { Codigo=5678, Estado=true, Nombre="Prueba 2"});
             await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);

            //Prueba
            var controller = new MunicipioController(contexto2);
            var respuesta = await  controller.Index() as ViewResult;

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
            contexto.Municipio.Add(new MunicipioEntity() { Codigo = 1234, Estado = true, Nombre = "Prueba 1" });
            contexto.Municipio.Add(new MunicipioEntity() { Codigo = 5678, Estado = true, Nombre = "Prueba 2" });
            await contexto.SaveChangesAsync();
            var municipio = new MunicipioEntity() { Codigo = 1234, Estado = true, Nombre = "Prueba 1" };

            //Prueba
            var controller = new MunicipioController(contexto);
            var respuesta = await controller.Create(municipio) as ViewResult;

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
            await contexto.SaveChangesAsync();
            var municipio = new MunicipioEntity() { Codigo = 1234, Estado = true, Nombre = "Prueba 1" };

            //Prueba
            var controller = new MunicipioController(contexto);
            var respuesta = await controller.Create(municipio) as ViewResult;

            //Resultado
             
            Assert.IsNotNull(respuesta);
            Assert.IsNotNull(respuesta.Model);
        }

        [Test]
        public async Task EditarMunicipio_OK()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            contexto.Municipio.Add(new MunicipioEntity() { Codigo = 1234, Estado = true, MunicipioId=1 ,Nombre = "Prueba 1" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);


            //Prueba
            var controller = new MunicipioController(contexto2);

            var municipio = new MunicipioEntity() { Codigo = 1234, Estado = true, MunicipioId = 1 , Nombre = "Prueba 1" };

            var respuesta = await controller.Edit(1, municipio) as ViewResult;

            //Resultado

            Assert.IsNull(respuesta); 
        }

        [Test]
        public async Task EditarMunicipio_OK_Estado_False()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            contexto.Municipio.Add(new MunicipioEntity() { Codigo = 1234, Estado = true, MunicipioId = 1, Nombre = "Prueba 1" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);


            //Prueba
            var controller = new MunicipioController(contexto2);

            var municipio = new MunicipioEntity() { Codigo = 1234, Estado = false, MunicipioId = 1, Nombre = "Prueba 1" };

            var respuesta = await controller.Edit(1, municipio) as ViewResult;

            //Resultado

            Assert.IsNull(respuesta);
        }

        [Test]
        public async Task EditarMunicipio_OK_Codigo_Existe()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            contexto.Municipio.Add(new MunicipioEntity() { Codigo = 1234, Estado = true, MunicipioId = 1, Nombre = "Prueba 1" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);


            //Prueba
            var controller = new MunicipioController(contexto2);

            var municipio = new MunicipioEntity() { Codigo = 1234, Estado = true, MunicipioId = 2, Nombre = "Prueba 1" };

            var respuesta = await controller.Edit(2, municipio) as ViewResult;

            //Resultado


            Assert.IsNotNull(respuesta);
            Assert.IsNotNull(respuesta.Model);
        }

        [Test]
        public async Task DeleteMunicipio_Existe()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            contexto.Municipio.Add(new MunicipioEntity() { Codigo = 1234, Estado = true, MunicipioId = 1, Nombre = "Prueba 1" });
            await contexto.SaveChangesAsync();

            //Prueba
            var controller = new MunicipioController(contexto); 
            int id = 1;
            var respuesta = await controller.Delete(id) as ViewResult;

            //Resultado
            Assert.IsNotNull(respuesta);
            Assert.IsNotNull(respuesta.Model);
        }

        [Test]
        public async Task DeleteMunicipio_No_Existe()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            //Prueba
            var controller = new MunicipioController(contexto);
            int id = 2;
            var respuesta = await controller.Delete(id) as ViewResult;

            //Resultado

            Assert.IsNull(respuesta);
        }

    

        [Test]
        public async Task DeleteMunicipio_DeleteConfirmed()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            contexto.Municipio.Add(new MunicipioEntity() { Codigo = 1234, Estado = true, MunicipioId = 1, Nombre = "Prueba 1" });
            await contexto.SaveChangesAsync();

            //Prueba
            var controller = new MunicipioController(contexto);
            int id = 1;
            var respuesta = await controller.DeleteConfirmed(id) as ViewResult;

            //Resultado
            Assert.IsNull(respuesta);

        }
    }
}
