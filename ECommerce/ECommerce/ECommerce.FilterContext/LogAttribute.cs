using System;
using System.Linq;
using ECommerce.Data.Entities;
using ECommerce.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;


namespace ECommerce.FilterContext
{
    public class LogAttribute : Attribute,IActionFilter
    {
        
        public LogAttribute()
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int? userId = Helper.HttpContextHelper.Current.Session.GetInt32("UserId");
            string controller = ((ControllerActionDescriptor) context.ActionDescriptor).ControllerName;
            string action = ((ControllerActionDescriptor) context.ActionDescriptor).ActionName;
            string ipAddress = Helper.HttpContextHelper.Current.Connection.RemoteIpAddress.Address.ToString();

            var unitofWork = Helper.HttpContextHelper.Current.RequestServices.GetService<IUnitOfWork>();
            Data.Entities.Log log = new Log()
            {
                Action = action,
                Controller = controller,
                Active = true,
                CreateDate = DateTime.UtcNow,
                IpAddress = ipAddress,
                UserId = userId
            };
            unitofWork.LogRepository.Insert(log);
            unitofWork.Complete();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

    }
}
