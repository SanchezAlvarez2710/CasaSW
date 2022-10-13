using CasaSW.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasaSW.Controllers;

namespace CasaSW.Permisos
{
    public class ValidarSesion : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var oUser = (UserAdmin)HttpContext.Current.Session["usuario"];


            if (oUser == null)
            {
                if(filterContext.Controller is LOGINController == false)
                {
                    filterContext.Result = new RedirectResult("~/LOGIN/Login");
                }
            }
            else
            {
                if (filterContext.Controller is LOGINController == true)
                {
                    filterContext.Result = new RedirectResult("~/HOME/Index");
                }
            }


            base.OnActionExecuting(filterContext);
        }


    }
}