using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnBanDienThoai.Data;
using DoAnBanDienThoai.Models;
using System.Drawing.Drawing2D;

namespace DoAnBanDienThoai.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DoAnBanDienThoaiContext _context;

        public ProductsController(DoAnBanDienThoaiContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var DoAnBanDienThoaiContext = _context.Product.Include(p => p.Brand).Include(p => p.Category);
            return View(await DoAnBanDienThoaiContext.ToListAsync());
        }
        // Search
        [HttpPost]
        public async Task<IActionResult> ProductByCategory(int catid, string keywords)
        {
            var DoAnBanDienThoaiContext = _context.Product.Include(p => p.Brand)
                .Include(p => p.Category).Where(p => p.ProductName.Contains(keywords) && p.CategoryId == catid);
            return View(await DoAnBanDienThoaiContext.ToListAsync());
        }
        // List Category Group
        public async Task<IActionResult> ProductByCategory(int catID)
        {
            var DoAnBanDienThoaiContext = _context.Product.Include(p => p.Brand).Include(p => p.Category).Where(p => p.CategoryId == catID);
            return View(await DoAnBanDienThoaiContext.ToListAsync());
        }

        public async Task<IActionResult> ProductByBrand(int brID)
        {
            var DoAnBanDienThoaiContext = _context.Product.Include(p => p.Brand).Include(p => p.Category).Where(p => p.BrandId == brID);
            return View(await DoAnBanDienThoaiContext.ToListAsync());
        }

        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "BrandName");
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ProductDescription,ProductQuantity,ProductImage,CategoryId,BrandId")] Product product)
        {
            if (ModelState.IsValid)
            {
                // Check if the product name already exists
                var exist = await _context.Product.FirstOrDefaultAsync(n => n.ProductName == product.ProductName);

                if (exist == null)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Product name already exists, add a custom error message
                    ModelState.AddModelError("ProductName", "Product name already exists.");
                }
            }

            // Populate ViewData before returning to the view
            ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "BrandName", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", product.CategoryId);

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "BrandName", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductPrice,ProductDescription,ProductQuantity,ProductImage,CategoryId,BrandId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if the edited product name already exists for another product
                    var nameExists = await _context.Product
                        .AnyAsync(p => p.ProductName == product.ProductName && p.ProductId != product.ProductId);

                    if (nameExists)
                    {
                        ModelState.AddModelError("ProductName", "Product name already exists.");
                        ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "BrandName", product.BrandId);
                        ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", product.CategoryId);
                        return View(product);
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Populate ViewData before returning to the view
            ViewData["BrandId"] = new SelectList(_context.Brand, "BrandId", "BrandName", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", product.CategoryId);

            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'DoAnBanDienThoaiContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
