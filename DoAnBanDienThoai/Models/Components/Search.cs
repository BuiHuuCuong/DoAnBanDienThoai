using Microsoft.AspNetCore.Mvc;
using DoAnBanDienThoai.Data;

namespace DoAnBanDienThoai.Models.Components
{
    public class Search : ViewComponent
    {
        private readonly DoAnBanDienThoaiContext _context;
        public Search(DoAnBanDienThoaiContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(_context.Category.ToList());
        }
    }
}
