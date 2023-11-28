using DoAnBanDienThoai.Data;
using Microsoft.AspNetCore.Mvc;

namespace DoAnBanDienThoai.Models.Components
{
    public class BrandMenu : ViewComponent
    {
        private readonly DoAnBanDienThoaiContext _context;

        public BrandMenu(DoAnBanDienThoaiContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(_context.Brand.ToList());
        }
    }
}
