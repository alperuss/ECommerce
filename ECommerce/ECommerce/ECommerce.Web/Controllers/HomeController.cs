using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Data.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        [FilterContext.Log]
        
        [FilterContext.Auth(UserTitle.Guest)]
        public IActionResult Index()
        {
            return View();
        }
    }
}