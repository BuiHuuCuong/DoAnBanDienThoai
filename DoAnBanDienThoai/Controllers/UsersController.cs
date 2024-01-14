using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnBanDienThoai.Data;
using DoAnBanDienThoai.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Text;
using System.Security.Cryptography;

namespace DoAnBanDienThoai.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly DoAnBanDienThoaiContext _context;

        public UsersController(DoAnBanDienThoaiContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.User != null ? 
                          View(await _context.User.ToListAsync()) :
                          Problem("Entity set 'DoAnBanDienThoaiContext.User'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,UserName,UserEmail,UserPassword,UserRole")] User user)
        {
            if (ModelState.IsValid)
            {
                var userExist = await _context.User.FirstOrDefaultAsync(n => n.UserEmail == user.UserEmail);

                if (userExist == null)
                {
                    // Hash mật khẩu bằng MD5
                    using (MD5 md5 = MD5.Create())
                    {
                        byte[] hashedPassword = md5.ComputeHash(Encoding.UTF8.GetBytes(user.UserPassword));
                        StringBuilder sb = new StringBuilder();

                        for (int i = 0; i < hashedPassword.Length; i++)
                        {
                            sb.Append(hashedPassword[i].ToString("x2"));
                        }

                        user.UserPassword = sb.ToString();
                    }

                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("UserEmail", "The user already exists, please give it a different name");
                }
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,UserName,UserEmail,UserPassword,UserRole")] User user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing user from the database
                    var existingUser = await _context.User.FindAsync(user.UserID);

                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    // Check if the edited email already exists for another user
                    var emailExists = await _context.User
                        .AnyAsync(u => u.UserEmail == user.UserEmail && u.UserID != user.UserID);

                    if (emailExists)
                    {
                        ModelState.AddModelError("UserEmail", "The changed email is already in use by another user.");
                        return View(user);
                    }

                    // If a new password is provided, hash and update the password
                    if (!string.IsNullOrEmpty(user.UserPassword))
                    {
                        using (MD5 md5 = MD5.Create())
                        {
                            byte[] hashedPassword = md5.ComputeHash(Encoding.UTF8.GetBytes(user.UserPassword));
                            StringBuilder sb = new StringBuilder();

                            for (int i = 0; i < hashedPassword.Length; i++)
                            {
                                sb.Append(hashedPassword[i].ToString("x2"));
                            }

                            existingUser.UserPassword = sb.ToString();
                        }
                    }

                    // Update other properties of the user
                    existingUser.UserName = user.UserName;
                    existingUser.UserEmail = user.UserEmail;
                    existingUser.UserRole = user.UserRole;

                    _context.Update(existingUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
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

            return View(user);
        }


        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'DoAnBanDienThoaiContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
