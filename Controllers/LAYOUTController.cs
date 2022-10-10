using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CasaSW.Controllers
{
    public class LAYOUTController : Controller
    {
        // GET: LAYOUT
        public ActionResult _LayoutSAC()
        {
            return View();
        }
        public ActionResult _LayoutADMIN()
        {
            return View();
        }
    }
}