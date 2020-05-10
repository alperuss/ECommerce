using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Fail()
        {
            return BadRequest("yetki yok");
        }
    }
}