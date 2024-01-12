using DoAnBanDienThoai.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DoAnBanDienThoai.Data;
using System.Text.RegularExpressions;

namespace DoAnBanDienThoai.Controllers
{
    public class AccountController : Controller
    {
        private readonly DoAnBanDienThoaiContext _context;

        public AccountController(DoAnBanDienThoaiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, [Bind("UserID,UserName,UserEmail,UserPassword,UserRole")] User user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }
            if (user.UserPassword == null)
            {
                var _user = _context.User.Where(m => m.UserID == user.UserID).FirstOrDefault();
                user.UserPassword = _user.UserPassword;
                ModelState.Clear();
                _context.Entry(_user).State = EntityState.Detached;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
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
        private bool UserExists(int id)
        {
            return (_context.User?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User _userFromPage)
        {
            var _user = _context.User.Where(m => m.UserEmail == _userFromPage.UserEmail && m.UserPassword == _userFromPage.UserPassword).FirstOrDefault();
            if (_user == null)
            {
                ViewBag.LoginStatus = 0;
                GlobalVariables.MyGlobalVariable = false;
            }
            else
            {
                var claims = new List<Claim>
                {

                    new Claim(ClaimTypes.Name, _user.UserEmail),
                    new Claim("FullName", _user.UserName),
                    new Claim(ClaimTypes.Role, _user.UserRole),
                    new Claim(ClaimTypes.NameIdentifier, _user.UserID.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                var authProperties = new AuthenticationProperties
                {

                };
                Thread.CurrentPrincipal = claimsPrincipal;
                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal,
                    authProperties);
                GlobalVariables.MyGlobalVariable = true;
                GlobalVariables.CurrentPrinciple = claimsPrincipal;
                
                if(_user.UserRole.Equals("Administrator"))
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Index", "Home");

            }
            return View();
        }
        public IActionResult Logout()
        {
            GlobalVariables.MyGlobalVariable = false;
            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // GET: Users/Create
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("UserID,UserName,UserEmail,UserPassword,UserRole")] User user)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra tên user đã tồn tại hay chưa
                var userExists = await _context.User.AnyAsync(u => u.UserEmail == user.UserEmail);

                // Kiểm tra định dạng email
                if (!Regex.IsMatch(user.UserEmail, @"^[^\s@]+@[^\s@]+\.com$"))
                {
                    ModelState.AddModelError("UserEmail", "Email sai định dạng");
                    return View(user);
                }

                if (!userExists)
                {
                    // Thêm user vào cơ sở dữ liệu
                    _context.Add(user);
                    user.UserRole = "Customer";
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    // Tên user đã tồn tại
                    ModelState.AddModelError("UserEmail", "User đã tồn tại hãy đặt tên khác");
                    return View(user);
                }
            }
            return View(user);
        }
    }
}
