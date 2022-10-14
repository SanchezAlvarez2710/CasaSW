using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CasaSW.Controllers
{
    public class LOGOUTController : Controller
    {
        // GET: LOGOUT
        public ActionResult Logout()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "LOGIN");
        }
    }
}