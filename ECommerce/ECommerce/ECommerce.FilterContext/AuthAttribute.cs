using System;
using System.ComponentModel.Design;
using System.Linq;
using ECommerce.Data.Entities;
using ECommerce.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;


namespace ECommerce.FilterContext
{
    public class AuthAttribute : Attribute,IActionFilter
    {
        private readonly Data.Enum.UserTitle _userTitle;
        public AuthAttribute(Data.Enum.UserTitle userTitle)
        {
            _userTitle = userTitle;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int? titleId = Helper.HttpContextHelper.Current.Session.GetInt32("TitleId");
            /*
             *guest=0
             *customer =1
             * admin=2
             * super admin=3
             */

            if (titleId >=(int)_userTitle)
            {
                //kullanıcının yetkisi var
            }
            else
            {
                //yetki yok
                context.Result = new RedirectToRouteResult(new RouteValueDictionary{{"controller","Auth"},{"action","Fail"}});
            }
           
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

    }
}
