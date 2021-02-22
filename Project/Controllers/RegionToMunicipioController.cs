using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models.Context;
using Project.Models.Dto;
using Project.Models.Entity;

namespace Project.Controllers
{
    public class RegionToMunicipioController : Controller
    {
        private readonly ProjectContext _context;

        public RegionToMunicipioController(ProjectContext context)
        {
            _context = context;
        }

        // GET: RegionToMunicipio
        public ActionResult Index()
        {
            RegionMunicipioDto model = new RegionMunicipioDto();
            var listRegion = _context.Region.ToList();
            model.RegionListCombox = new SelectList(listRegion, "RegionId", "Nombre", null);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(RegionMunicipioDto region)
        {
            RegionMunicipioDto model = new RegionMunicipioDto();
            try
            {
                if (region.RegionId != 0)
                {
                    //Get list Region
                    var regionList = _context.Region.ToList();
                    model.RegionListCombox = new SelectList(regionList, "RegionId", "Nombre", null);

                    //Get Region to Municipio
                    var list = _context.RegionToMunicipio.ToList();

                    //Get list municipio
                    model.MunicipiosList = new List<MunicipioDto>();
                    var listMunicipio = _context.Municipio.ToList();

                    model.MunicipiosList = (from item in listMunicipio
                                            select new MunicipioDto()
                                            {
                                                MunicipioId = item.MunicipioId,
                                                Nombre = item.Nombre,
                                                Active = ((list.Where(x => x.MunicipioId == item.MunicipioId && x.RegionId == region.RegionId).Count() > 0) ? true : false)

                                            }).ToList();
                }
                else
                {
                    //Get list Region
                    var regionList = _context.Region.ToList();
                    model.RegionListCombox = new SelectList(regionList, "RegionId", "Nombre", null);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return View(model);


        }

        [HttpPost]
        public ActionResult Save(RegionMunicipioDto Rs)
        {
            try
            {
                int cont = 0;
                foreach (var item in Rs.MunicipiosList)
                {
                    if (item.Active)
                    {
                        if (!_context.RegionToMunicipio.Where(x => x.RegionId == Rs.RegionId && x.MunicipioId == item.MunicipioId).Any())
                        {
                            var regionMunicipio = new RegionToMunicipioEntity
                            {
                                MunicipioId = item.MunicipioId,
                                RegionId = Rs.RegionId
                            };
                            _context.RegionToMunicipio.AddRange(regionMunicipio);
                            cont++;

                        }

                    }
                    else if (_context.RegionToMunicipio.Where(x => x.RegionId == Rs.RegionId && x.MunicipioId == item.MunicipioId).Any())
                    {
                        _context.RegionToMunicipio.RemoveRange(_context.RegionToMunicipio.Where(x => x.RegionId == Rs.RegionId && x.MunicipioId == item.MunicipioId));
                        cont++;

                    }
                }
                if (cont == 0)
                {
                    _context.RegionToMunicipio.RemoveRange(_context.RegionToMunicipio.Where(x => x.RegionId == Rs.RegionId && x.MunicipioId == Rs.MunicipioId));

                }
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Index");

        }
    }
}
