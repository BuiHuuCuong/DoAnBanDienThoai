using Microsoft.AspNetCore.Mvc;

namespace DoAnBanDienThoai.Controllers
{
    public class AccountController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }


    }
}
