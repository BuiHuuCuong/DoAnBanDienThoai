using Microsoft.AspNetCore.Mvc;

namespace DoAnBanDienThoai.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
