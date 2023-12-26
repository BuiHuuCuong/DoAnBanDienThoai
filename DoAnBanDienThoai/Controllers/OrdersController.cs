using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnBanDienThoai.Data;
using DoAnBanDienThoai.Models;
using System.Security.Claims;

namespace DoAnBanDienThoai.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DoAnBanDienThoaiContext _context;

        public OrdersController(DoAnBanDienThoaiContext context)
        {
            _context = context;
        }

        // GET: Orders
        //public async Task<IActionResult> Index()
        //{
        //    var doAnBanDienThoaiContext = _context.Order.Include(o => o.CartItems).Include(o => o.User);
        //    return View(await doAnBanDienThoaiContext.ToListAsync());
        //}

        //// GET: Orders/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Order == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order
        //        .Include(o => o.CartItems)
        //        .Include(o => o.User)
        //        .FirstOrDefaultAsync(m => m.OrderID == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        // GET: Orders/Create
        public IActionResult CartCheckout()
        {
            ViewBag.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["Date"] = DateTime.Now;
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CartCheckout([Bind("OrderID,UserID,CartItemId,Email,Address,Phone,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                
            }          
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserEmail", order.UserID);
            return View(order);
        }

        //public IActionResult ThankYou()
        //{
        //    Order order = new()
        //    {
        //        OrderDate = DateTime.Now,
        //        OrderID = _context.Order.

        //    };
        //    return View();
        //}

        //// GET: Orders/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Order == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CartItemId"] = new SelectList(_context.CartItem, "CartItemId", "CartItemId", order.CartItemId);
        //    ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserEmail", order.UserID);
        //    return View(order);
        //}

        //// POST: Orders/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("OrderID,UserID,CartItemId,Email,Address,Phone,Total,OrderDate")] Order order)
        //{
        //    if (id != order.OrderID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(order);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderExists(order.OrderID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CartItemId"] = new SelectList(_context.CartItem, "CartItemId", "CartItemId", order.CartItemId);
        //    ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserEmail", order.UserID);
        //    return View(order);
        //}

        //// GET: Orders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Order == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order
        //        .Include(o => o.CartItem)
        //        .Include(o => o.User)
        //        .FirstOrDefaultAsync(m => m.OrderID == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Order == null)
        //    {
        //        return Problem("Entity set 'DoAnBanDienThoaiContext.Order'  is null.");
        //    }
        //    var order = await _context.Order.FindAsync(id);
        //    if (order != null)
        //    {
        //        _context.Order.Remove(order);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OrderExists(int id)
        //{
        //  return (_context.Order?.Any(e => e.OrderID == id)).GetValueOrDefault();
        //}
    }
}
