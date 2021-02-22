using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models.Context;
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

            return View(regionEntity);
        }

        // GET: Region/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Region/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegionId,Codigo,Nombre")] RegionEntity regionEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regionEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regionEntity);
        }

        // GET: Region/Edit/5
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

        // POST: Region/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    _context.Update(regionEntity);
                    await _context.SaveChangesAsync();
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
