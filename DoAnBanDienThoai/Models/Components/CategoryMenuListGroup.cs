using Microsoft.AspNetCore.Mvc;
using DoAnBanDienThoai.Data;

namespace DoAnBanDienThoai.Models
{
    public class CategoryMenuListGroup : ViewComponent
    {
        private readonly DoAnBanDienThoaiContext _context;
        public CategoryMenuListGroup(DoAnBanDienThoaiContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(_context.Category.ToList());
        }
    }
}
