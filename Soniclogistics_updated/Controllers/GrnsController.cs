using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Soniclogistics_updated.Models;
using Soniclogistics_updated.Controllers;

namespace Soniclogistics_updated.Controllers
{
    public class GrnsController : Controller
    {
        private readonly SoniclogisticsDbContext _context;

        public GrnsController(SoniclogisticsDbContext context)
        {
            _context = context;
        }

        // GET: Grns
        public async Task<IActionResult> Index()
        {
            var myDataDbContext = _context.Grns.Include(g => g.Order);
            return View(await myDataDbContext.ToListAsync());
        }

        // GET: Grns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grn = await _context.Grns
                .Include(g => g.Order)
                .FirstOrDefaultAsync(m => m.GrnId == id);
            if (grn == null)
            {
                return NotFound();
            }

            return View(grn);
        }

        // GET: Grns/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Pos, "OrderId", "OrderId");
            return View();
        }

        // POST: Grns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GrnId,GrnDate,ProdId,Warehouse,BatchNo,ApprovedWarehouse,UnapprovedWarehouse,SupId,RfqId,OrderId")] Grn grn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Pos, "OrderId", "OrderId", grn.OrderId);
            return View(grn);
        }

        // GET: Grns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grn = await _context.Grns.FindAsync(id);
            if (grn == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Pos, "OrderId", "OrderId", grn.OrderId);
            return View(grn);
        }

        // POST: Grns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GrnId,GrnDate,ProdId,Warehouse,BatchNo,ApprovedWarehouse,UnapprovedWarehouse,SupId,RfqId,OrderId")] Grn grn)
        {
            if (id != grn.GrnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrnExists(grn.GrnId))
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
            ViewData["OrderId"] = new SelectList(_context.Pos, "OrderId", "OrderId", grn.OrderId);
            return View(grn);
        }

        // GET: Grns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grn = await _context.Grns
                .Include(g => g.Order)
                .FirstOrDefaultAsync(m => m.GrnId == id);
            if (grn == null)
            {
                return NotFound();
            }

            return View(grn);
        }

        // POST: Grns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grn = await _context.Grns.FindAsync(id);
            if (grn != null)
            {
                _context.Grns.Remove(grn);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrnExists(int id)
        {
            return _context.Grns.Any(e => e.GrnId == id);
        }
    }
}
