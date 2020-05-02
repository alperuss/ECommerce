using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using ECommerce.Data.Enum;
using ECommerce.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult LoginAction([FromBody]Data.DTO.User_LoginAction_Request user_LoginAction_Request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("olm bak git");
            }

            var user = _unitOfWork.UserRepository.GetByEmailAndPassword(user_LoginAction_Request.Email, user_LoginAction_Request.Password);
            
            if (user == null)
            {
                return BadRequest("E-posta veya şifre hatalı.");
            }
            else if (!user.EmailVerified)
            {
                return BadRequest("E-posta onayı sağlanmamış.");
            }
            else if (user.Deleted)
            {
                return BadRequest("Hesabınız kapatılmış.");
            }
            else if (!user.Active)
            {
                return BadRequest("Hesabınız dondurulmuş.");
            }
            else
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetInt32("Admin", Convert.ToInt32(user.Admin));

                if (user_LoginAction_Request.RememberMe)
                {
                    //beni hatırla
                    Guid guid = Guid.NewGuid();
                    user.AutoLoginKey = guid;
                    _unitOfWork.UserRepository.Update(user);
                    _unitOfWork.Complete();

                    HttpContext.Response.Cookies.Append("rememberme", guid.ToString(),
                        new CookieOptions()
                        {
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

        public IActionResult RegisterAction([FromBody]Data.DTO.User_RegisterAction_Request dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("data sıkıntılı");
            }

            var user = _unitOfWork.UserRepository.Query().SingleOrDefault(a => a.Email == dto.Email);

            if (user != null)
            {
                return BadRequest("Bu email adresi zaten bir kullanıcı adına kayıtlı.");
            }

            user = new User()
            {
            Active = true,
            CreateDate = DateTime.UtcNow,
            Name = dto.Name,
            Surname = dto.Surname,
            Email = dto.Email,
            Password = Helper.CryptoHelper.Sha1(dto.Password),
            TitleId = (int)Data.Enum.UserTitle.Customer
             };

            _unitOfWork.UserRepository.Insert(user);

            _unitOfWork.Complete();

            string validationLink = Data.Singletons.AppSettingsDto.AppSetting.WebSite + "/email-verify/" +
                                    user.Id + "/" + Helper.CryptoHelper.Sha1(user.Id.ToString());

            _unitOfWork.OutgoingEmailRepository.Insert(new OutgoingEmail()
            {
                Active = true,
                CreateDate = DateTime.UtcNow,
                Subject = "Hoşgeldiniz, başlamak için bir adım kaldı!",
                Body = "Onay linki içerir: <a href='" + validationLink + "'>onayla</a>",
                To = user.Email
            });

            _unitOfWork.Complete();

            return new JsonResult("??");
        }

        [Route("/email-verify/{id:int}/{authKey}")]
        public IActionResult VerifyEmail(int id, string authKey)
        {
            Data.DTO.Message_Response messageResponse = new Message_Response();
            var authKeyChipper = Helper.CryptoHelper.Sha1(id.ToString());

            if (authKey == authKeyChipper)
            {
                var user = _unitOfWork.UserRepository.GetById(id);
                if (user != null)
                {
                    user.EmailVerified = true;
                    _unitOfWork.Complete();
                    messageResponse.MessageType = MessageType.Success;
                    messageResponse.Message = "E-posta doğrulama işlemi başarılı. Şimdi giriş yapabilirsiniz.";
                }
                else
                {
                    messageResponse.MessageType = MessageType.Danger;
                    messageResponse.Message = "Doğrulamak istediğiniz hesap sistemde kayıtlı değil.";
                }
            }
            else
            {
                messageResponse.MessageType = MessageType.Danger;
                messageResponse.Message = "Doğrulama kodu hatalı. Sistem yöneticisi ile irtibata geçebilirsiniz.";
            }

            return View(messageResponse);
        }
    }
}