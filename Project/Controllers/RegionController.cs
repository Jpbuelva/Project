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
    public class RegionController : Controller
    {
        private readonly ProjectContext _context;

        public RegionController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Region
        public async Task<IActionResult> Index()
        {
            TempData["Success"] = "Success";
            return View(await _context.Region.ToListAsync());
        }

        // GET: Region/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var regionEntity = await _context.Region
                .FirstOrDefaultAsync(m => m.RegionId == id);
            if (regionEntity == null)
            {
                return NotFound();
            }

            var detalleMunicipio = await (from item in _context.RegionToMunicipio
                                          join reg in _context.Municipio on item.MunicipioId equals reg.MunicipioId
                                          where item.RegionId == regionEntity.RegionId
                                          select new MunicipioDto()
                                          {
                                              MunicipioId = reg.MunicipioId,
                                              Active = true,
                                              Codigo=reg.Codigo,
                                              Nombre = reg.Nombre
                                          }).ToListAsync();

            var detalleRegion = new RegionDto()
            {
                RegionId = regionEntity.RegionId,
                Codigo = regionEntity.Codigo,
                Nombre = regionEntity.Nombre,
                DetallesMunicipio = detalleMunicipio
            };



            return View(detalleRegion);
        }

        // GET: Region/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("RegionId,Codigo,Nombre")] RegionEntity regionEntity)
        {
            if (ModelState.IsValid)
            {
                if (!_context.Region.Where(x => x.Codigo == regionEntity.Codigo).Any())
                {
                    _context.Add(regionEntity);
                    await _context.SaveChangesAsync();
                    ViewBag.mensaje = null;
                }
                else
                {
                    ViewBag.mensaje = $"El codigo {regionEntity.Codigo} ya exite";
                    return View(regionEntity);

                }

                return RedirectToAction(nameof(Index));
            }
            return View(regionEntity);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regionEntity = await _context.Region.FindAsync(id);
            if (regionEntity == null)
            {
                return NotFound();
            }
            return View(regionEntity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("RegionId,Codigo,Nombre")] RegionEntity regionEntity)
        {
            if (id != regionEntity.RegionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_context.Region.Where(x => x.Codigo == regionEntity.Codigo && x.RegionId == regionEntity.RegionId).Any())
                    {
                        _context.Update(regionEntity);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ViewBag.mensaje = $"El codigo {regionEntity.Codigo} ya exite";
                        return View(regionEntity);

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegionEntityExists(regionEntity.RegionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(regionEntity);
        }

        // GET: Region/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regionEntity = await _context.Region
                .FirstOrDefaultAsync(m => m.RegionId == id);
            if (regionEntity == null)
            {
                return NotFound();
            }

            return View(regionEntity);
        }

        // POST: Region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var regionEntity = await _context.Region.FindAsync(id);
            _context.Region.Remove(regionEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegionEntityExists(int id)
        {
            return _context.Region.Any(e => e.RegionId == id);
        }
    }
}
