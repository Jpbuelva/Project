using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models.Context;
using Project.Models.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class MunicipioController : Controller
    {
        private readonly ProjectContext _context;

        public MunicipioController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Municipio
        public async Task<IActionResult> Index()
        {
            return View(await _context.Municipio.ToListAsync());
        }

      
        // GET: Municipio/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("MunicipioId,Codigo,Nombre,Estado")] MunicipioEntity municipioEntity)
        {

            if (!_context.Municipio.Where(x => x.Codigo == municipioEntity.Codigo).Any())
            {
                _context.Add(municipioEntity);
                await _context.SaveChangesAsync();
                ViewBag.mensaje = null;
            }
            else
            {
                ViewBag.mensaje = $"El codigo {municipioEntity.Codigo} ya exite";
                return View(municipioEntity);

            }
            return View(municipioEntity);
        }

        // GET: Municipio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipioEntity = await _context.Municipio.FindAsync(id);
            if (municipioEntity == null)
            {
                return NotFound();
            }
            return View(municipioEntity);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="id"></param>
        /// <param name="municipioEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MunicipioId,Codigo,Nombre,Estado")] MunicipioEntity municipioEntity)
        {
            if (id != municipioEntity.MunicipioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_context.Municipio.Where(x => x.Codigo == municipioEntity.Codigo && x.MunicipioId == municipioEntity.MunicipioId).Any())
                    {
                        if (!municipioEntity.Estado)
                        {

                            _context.RegionToMunicipio.RemoveRange(_context.RegionToMunicipio.Where(x => x.MunicipioId == municipioEntity.MunicipioId));
                            _context.Municipio.Update(municipioEntity);
                            ViewBag.mensaje = null;

                        }
                        else
                        {
                            _context.Municipio.Update(municipioEntity);
                            ViewBag.mensaje = null;

                        }
                    }
                    else
                    {
                        ViewBag.mensaje = $"El codigo {municipioEntity.Codigo} ya exite";
                        return View(municipioEntity);

                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MunicipioEntityExists(municipioEntity.MunicipioId))
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
            return View(municipioEntity);
        }

        // GET: Municipio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipioEntity = await _context.Municipio
                .FirstOrDefaultAsync(m => m.MunicipioId == id);
            if (municipioEntity == null)
            {
                return NotFound();
            }

            return View(municipioEntity);
        }

        // POST: Municipio/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var municipioEntity = await _context.Municipio.FindAsync(id);
            _context.Municipio.Remove(municipioEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MunicipioEntityExists(int id)
        {
            return _context.Municipio.Any(e => e.MunicipioId == id);
        }
    }
}
