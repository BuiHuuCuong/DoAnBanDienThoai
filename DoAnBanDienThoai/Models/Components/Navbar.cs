using DoAnBanDienThoai.Data;
using Microsoft.AspNetCore.Mvc;

namespace DoAnBanDienThoai.Models.Components
{
    public class Navbar : ViewComponent
    {
        private readonly DoAnBanDienThoaiContext _context;

        public Navbar(DoAnBanDienThoaiContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
