using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Soniclogistics_updated.Models;

namespace Soniclogistics_updated.Controllers
{
    public class RfqsController : Controller
    {
        private readonly SoniclogisticsDbContext _context;

        public RfqsController(SoniclogisticsDbContext context)
        {
            _context = context;
        }

        // GET: Rfqs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rfqs.ToListAsync());
        }

        // GET: Rfqs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rfq = await _context.Rfqs
                .FirstOrDefaultAsync(m => m.RfqId == id);
            if (rfq == null)
            {
                return NotFound();
            }

            return View(rfq);
        }

        // GET: Rfqs/Create
        public IActionResult Create()
        {
            var productNames = _context.Products.Select(p => p.ProductName).ToList();
            ViewData["ProductNames"] = productNames;
            return View();
        }

        // POST: Rfqs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //  [HttpPost]
        // [ValidateAntiForgeryToken]
        //   public async Task<IActionResult> Create([Bind("RfqId,OperationalUnit,ShippingAddress,CreateDate,ProdId,Quantity,ItemDiscription,Currency")] Rfq rfq)
        //   {
        //     if (ModelState.IsValid)
        //     {
        //     _context.Add(rfq);
        //      await _context.SaveChangesAsync();
        //      return RedirectToAction(nameof(Index));
        //  }
        //  return View(rfq);
        // }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RfqId,OperationalUnit,ShippingAddress,CreateDate,Currency")] Rfq rfq, string productDetailsJson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rfq);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(productDetailsJson))
                {
                    var productDetails = JsonConvert.DeserializeObject<List<Rfq>>(productDetailsJson);
                    foreach (var productDetail in productDetails)
                    {
                        productDetail.RfqId = rfq.RfqId; // Set the RFQ ID for each product detail
                        _context.Add(productDetail);
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(rfq);
        }
        // GET: Rfqs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rfq = await _context.Rfqs.FindAsync(id);
            if (rfq == null)
            {
                return NotFound();
            }
            return View(rfq);
        }

        // POST: Rfqs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RfqId,OperationalUnit,ShippingAddress,CreateDate,ProdId,Quantity,ItemDiscription,Currency")] Rfq rfq)
        {
            if (id != rfq.RfqId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rfq);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RfqExists(rfq.RfqId))
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
            return View(rfq);
        }

        // GET: Rfqs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rfq = await _context.Rfqs
                .FirstOrDefaultAsync(m => m.RfqId == id);
            if (rfq == null)
            {
                return NotFound();
            }

            return View(rfq);
        }

        // POST: Rfqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rfq = await _context.Rfqs.FindAsync(id);
            if (rfq != null)
            {
                _context.Rfqs.Remove(rfq);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RfqExists(int id)
        {
            return _context.Rfqs.Any(e => e.RfqId == id);
        }
    }
}
