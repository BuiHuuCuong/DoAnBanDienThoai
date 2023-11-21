using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoAnBanDienThoai.Models;

namespace DoAnBanDienThoai.Data
{
    public class DoAnBanDienThoaiContext : DbContext
    {
        public DoAnBanDienThoaiContext (DbContextOptions<DoAnBanDienThoaiContext> options)
            : base(options)
        {
        }

        public DbSet<DoAnBanDienThoai.Models.User> User { get; set; } = default!;
    }
}
