using ECommerce.Data.Entities;
using ECommerce.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.Design;

namespace ECommerce.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LoginAction([FromBody]Data.DTOs.User_LoginAction_Request user_LoginAction_Request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("olm bak git");
            }

            var user = _unitOfWork.UserRepository.GetByEmailAndPassword(user_LoginAction_Request.Email, user_LoginAction_Request.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetInt32("Admin",Convert.ToInt32(user.Admin));

                if (user_LoginAction_Request.RememberMe)
                {
                    //beni hatırla
                    Guid guid = Guid.NewGuid();
                    user.AutoLoginKey = guid;
                    _unitOfWork.UserRepository.Update(user);
                    _unitOfWork.Complete();

                    HttpContext.Response.Cookies.Append("rememberme", guid.ToString(),
                        new CookieOptions() {
                            Expires = DateTime.UtcNow.AddYears(1)
                        });
                }
            }

            return new JsonResult(user);
        }

        public IActionResult LogoutAction()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Admin");
            HttpContext.Response.Cookies.Delete("rememberme");

            return RedirectToAction("Index", "Home");
        }
        public IActionResult RegisterAction([FromBody]Data.DTOs.User_RegisterAction_Request dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("data sıkıntılı");
            }
            if (_unitOfWork.UserRepository.GetByEmail(dto.Email) != null)
            {
                return BadRequest("Bu email adresi zaten bir kullanıcı adına kayıtlı.");
            }

            User user = new User();
            
            user.Active = true;
            user.CreateDate = DateTime.UtcNow;
            user.Name = dto.Name;
            user.Surname = dto.Surname;
            user.Email = dto.Email;
            user.Password = Helper.CryptoHelper.Sha1(dto.Password);
            user.TitleId = (int)Data.Enums.UserTitle.Customer;



            _unitOfWork.UserRepository.Insert(user);
            _unitOfWork.Complete();
            //email veritabanında olmalı
            //model validation yapılmalı

            string validationLink = "https://localhost:8080/email-verify/" + user.Id + "/" +
                                    Helper.CryptoHelper.Sha1(user.Id.ToString());

            _unitOfWork.OutgoingEmailRepository.Insert(new OutgoingEmail()
            {
                Active = true,
                CreateDate = DateTime.UtcNow,
                Subject = "Hoşgeldiniz, başlamak için bir adım kaldı!",
                Body = "Onay linki içerir: <a href ='" + validationLink + "'>onayla</a>",
                To = user.Email
            });

            _unitOfWork.Complete();

            return new JsonResult("OK");
        }

        [Route("/email-verify/{id:int}/{authKey}")]
        public IActionResult VerifyEmail(int id, string authKey)
        {
            var authKeyChipper = Helper.CryptoHelper.Sha1(id.ToString());

            if (authKey==authKeyChipper)
            {
                var user = _unitOfWork.UserRepository.GetById(id);
                if (user !=null)
                {
                    user.EmailVerified = true;
                    _unitOfWork.Complete();
                }
            }
            else
            {
                //başarısız
            }

            return View();
        }
    }
}