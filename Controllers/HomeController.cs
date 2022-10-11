using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CasaSW.Permisos;

namespace CasaSW.Controllers
{
    [ValidarSesion]
    public class HOMEController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }        
    }
}