using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoAnBanDienThoai.Data;
using DoAnBanDienThoai.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace DoAnBanDienThoai.Controllers
{
    public class AdminController : Controller
    {
        //[Authorize(Roles = "Administrator")]
        public IActionResult Index()    
        {
            return View();
                          
        }
    }
}

       