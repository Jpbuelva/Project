using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Project.Controllers;
using Project.Models.Context;
using Project.Models.Dto;
using Project.Models.Entity;
using System;
using System.Collections.Generic;

namespace TestProyect.Controllers
{
    [TestFixture]
    public class RegionToMunicipioControllerTests:BasePruebas
    {
        [Test]
        public void GetRelacion()
        {

            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            contexto.RegionToMunicipio.Add(new RegionToMunicipioEntity() { RegionId = 1, MunicipioId = 1, RegionToMunicipioId = 1 });
            contexto.Region.Add(new RegionEntity() { RegionId = 1, Codigo = 1234, Nombre = "Prueba 1" });
            contexto.Municipio.Add(new MunicipioEntity() { MunicipioId = 1, Codigo = 5678, Estado = true, Nombre = "Prueba 2" });
            contexto.SaveChanges();
            var contexto2 = ConstruirContext(nombreBD);


            //Prueba
            var regionToMunicipioController = new RegionToMunicipioController(contexto2);
            var result = regionToMunicipioController.Index() as ActionResult;


            //Resultado 
            Assert.IsNotNull(result); 
        }

        [Test]
        public void GetCompanies()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            contexto.RegionToMunicipio.Add(new RegionToMunicipioEntity() { RegionId = 1, MunicipioId = 1, RegionToMunicipioId = 1 });
            contexto.Region.Add(new RegionEntity() { RegionId = 1, Codigo = 1234, Nombre = "Prueba 1" });
            contexto.Municipio.Add(new MunicipioEntity() { MunicipioId = 1, Codigo = 5678, Estado = true, Nombre = "Prueba 2" });
            contexto.SaveChanges();
            var contexto2 = ConstruirContext(nombreBD);


            //Prueba
            var regionToMunicipioController = new RegionToMunicipioController(contexto2);
            RegionMunicipioDto region = new RegionMunicipioDto()
            {
                Active = true,
                MunicipioId = 1,
                RegionId = 1,
                MunicipiosList = new List<MunicipioDto>()
                {
                    new MunicipioDto
                    {
                        MunicipioId=1,
                        Active=true,
                        Codigo=1234,
                        Nombre="Prueba 1"
                    }
                }
            };
     
            var result = regionToMunicipioController.Index(region);

            //Resultado 
            Assert.IsNotNull(result);
        }

        [Test]
        public void Save_StateUnderTest_ExpectedBehavior()
        {
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
          
            // Arrange
            var regionToMunicipioController = new RegionToMunicipioController(contexto);
            RegionMunicipioDto Rs = new RegionMunicipioDto()
            {
                MunicipioId=1,
                RegionId=1,
                Active=true,
                MunicipiosList= new List<MunicipioDto>()
                {
                     new MunicipioDto
                    {
                        MunicipioId=1,
                        Active=true,
                        Codigo=1234,
                        Nombre="Prueba 1"
                    }
                }

            };

            // Act
            var result = regionToMunicipioController.Save(Rs);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
