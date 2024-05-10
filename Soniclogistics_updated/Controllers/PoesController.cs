using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Soniclogistics_updated.Models;

namespace Soniclogistics_updated.Controllers
{
    public class PoesController : Controller
    {
        private readonly SoniclogisticsDbContext _context;

        public PoesController(SoniclogisticsDbContext context)
        {
            _context = context;
        }

        // GET: Poes
      public async Task<IActionResult> Index()
{
    var POS_data = await _context.Pos.ToListAsync();

    var query = from pos in _context.Pos
                join rfq in _context.Rfqs on pos.RfqId equals rfq.RfqId
                join product in _context.Products on rfq.ProductName equals product.ProdId
                select new { pos, rfq, product };

    Dictionary<int, string> rfqProductsMap = new Dictionary<int, string>();

    foreach (var result in query)
    {
        if (!rfqProductsMap.ContainsKey(result.rfq.RfqId))
        {
            rfqProductsMap[result.rfq.RfqId] = $"{result.product.ProductName} - {result.rfq.Quantity}";
        }
        else
        {
            rfqProductsMap[result.rfq.RfqId] += $"\n{result.product.ProductName} - {result.rfq.Quantity}";
        }
    }

    ViewData["POS_data"] = POS_data;
    ViewData["rfqProductsMap"] = rfqProductsMap;

    return View(POS_data);
}



        // GET: Poes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var po = await _context.Pos
                .Include(p => p.Prod)
                .Include(p => p.Rfq)
                .Include(p => p.Sup)
          
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (po == null)
            {
                return NotFound();
            }

            return View(po);
        }

        // GET: Poes/Create
        public IActionResult Create()
        {

            ViewData["ProdId"] = new SelectList(_context.Products, "ProdId", "ProdId");
            ViewData["RfqId"] = new SelectList(_context.Rfqs, "RfqId", "RfqId");
            ViewData["SupId"] = new SelectList(_context.Suppliers, "SupId", "SupId");
          
            return View();
        }

        // POST: Poes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,SupId,ProdId,OrderDateTime,Expected_date,Address,City,Country,Status,UserId,RfqId,Price")] Po po)
        {
            
            
                _context.Add(po);
                await _context.SaveChangesAsync();
               
            
            ViewData["ProdId"] = new SelectList(_context.Products, "ProdId", "ProdId", po.ProdId);
            ViewData["RfqId"] = new SelectList(_context.Rfqs, "RfqId", "RfqId", po.RfqId);
            ViewData["SupId"] = new SelectList(_context.Suppliers, "SupId", "SupId", po.SupId);
   
            return View(po);
        }

        // GET: Poes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var po = await _context.Pos.FindAsync(id);
            if (po == null)
            {
                return NotFound();
            }
            ViewData["ProdId"] = new SelectList(_context.Products, "ProdId", "ProdId", po.ProdId);
            ViewData["RfqId"] = new SelectList(_context.Rfqs, "RfqId", "RfqId", po.RfqId);
            ViewData["SupId"] = new SelectList(_context.Suppliers, "SupId", "SupId", po.SupId);
        
            return View(po);
        }

        // POST: Poes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,SupId,ProdId,OrderDateTime,,Address,City,Country,Status,UserId,RfqId,Price")] Po po)
        {
            if (id != po.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(po);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoExists(po.OrderId))
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
            ViewData["ProdId"] = new SelectList(_context.Products, "ProdId", "ProdId", po.ProdId);
            ViewData["RfqId"] = new SelectList(_context.Rfqs, "RfqId", "RfqId", po.RfqId);
            ViewData["SupId"] = new SelectList(_context.Suppliers, "SupId", "SupId", po.SupId);
        
            return View(po);
        }

        // GET: Poes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var po = await _context.Pos
                .Include(p => p.Prod)
                .Include(p => p.Rfq)
                .Include(p => p.Sup)
             
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (po == null)
            {
                return NotFound();
            }

            return View(po);
        }

        // POST: Poes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var po = await _context.Pos.FindAsync(id);
            if (po != null)
            {
                _context.Pos.Remove(po);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoExists(int id)
        {
            return _context.Pos.Any(e => e.OrderId == id);
        }
    }
}
