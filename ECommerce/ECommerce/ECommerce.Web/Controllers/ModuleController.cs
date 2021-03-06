﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Data.Enum;
using ECommerce.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    public class ModuleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ModuleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [FilterContext.Log]
        [FilterContext.Auth(UserTitle.Customer)]
        public IActionResult UserBar()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId != null)
            {
                var user = _unitOfWork.UserRepository.Get((int)userId);

                return View(user);
            }
            else
            {
                return Unauthorized();
            }
        }
        [FilterContext.Log]
        [FilterContext.Auth(UserTitle.Customer)]
        public IActionResult AccountMenu()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            var user = _unitOfWork.UserRepository.GetById((int)userId);
            return View(user);
        }
    }
}