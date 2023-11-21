using Microsoft.AspNetCore.Mvc;

namespace DoAnBanDienThoai.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
