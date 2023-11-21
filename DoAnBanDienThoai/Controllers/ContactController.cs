using Microsoft.AspNetCore.Mvc;

namespace DoAnBanDienThoai.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
