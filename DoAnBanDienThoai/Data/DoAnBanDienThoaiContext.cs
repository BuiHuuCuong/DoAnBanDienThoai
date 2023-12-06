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
        public DoAnBanDienThoaiContext()
        {
        }

        public DoAnBanDienThoaiContext (DbContextOptions<DoAnBanDienThoaiContext> options)
            : base(options)
        {
        }

        public DbSet<DoAnBanDienThoai.Models.User> User { get; set; } = default!;

        public DbSet<DoAnBanDienThoai.Models.Brand>? Brand { get; set; }

        public DbSet<DoAnBanDienThoai.Models.Category>? Category { get; set; }

        public DbSet<DoAnBanDienThoai.Models.Product>? Product { get; set; }

        public DbSet<DoAnBanDienThoai.Models.Contact>? Contact { get; set; }

        
    }
}