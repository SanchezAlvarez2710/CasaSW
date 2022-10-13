using CasaSW.Permisos;
using System.Web;
using System.Web.Mvc;

namespace CasaSW
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Permisos.ValidarSesion());
        }
    }
}
