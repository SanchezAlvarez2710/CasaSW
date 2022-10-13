using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CasaSW.Models.ViewModel;
using CasaSW.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Ajax.Utilities;
using System.Runtime.CompilerServices;
using System.Data.Entity.Core;

namespace CasaSW.Controllers
{
    public class LOGINController : Controller
    {
        private CASASWEntities db = new CASASWEntities();

        // GET: ACCESOs
        public ActionResult Login()
        {
            return View();
        }

        //public ActionResult Registrar()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Registrar(UserAdmin oUsuario)
        //{
        //    bool registrado;
        //    string mensaje;

        //    if(oUsuario.Password == oUsuario.ConfirmarPassword)
        //    {
        //        oUsuario.Password = ConvertirSha256(oUsuario.Password);
        //    }
        //    else
        //    {
        //        ViewData["Mensaje"] = "Las contraseñas no coinciden";
        //        return View();
        //    }

        //    using (SqlConnection cn = new SqlConnection(cadena))
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
        //        cmd.Parameters.AddWithValue("Username", oUsuario.Username);
        //        cmd.Parameters.AddWithValue("Password", oUsuario.Password);
        //        cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cn.Open();

        //        cmd.ExecuteNonQuery();

        //        registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
        //        mensaje = cmd.Parameters["Mensaje"].Value.ToString();
        //    }

        //    ViewData["Mensaje"] = mensaje;

        //    if (registrado)
        //    {
        //        return RedirectToAction("Login", "LOGIN");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        [HttpPost]
        public ActionResult Login(UserAdmin oUsuario)
        {
            oUsuario.Password = ConvertirSha256(oUsuario.Password);
         
            IEnumerable<CasaSW.Models.ViewModel.UserAdmin> acceso = from o in db.PERSONA
                         join p in db.ADMIN on o.id_persona equals p.id_persona
                         where o.username == oUsuario.Username && o.password == oUsuario.Password 
                         select new UserAdmin
                         {
                             IdUsuario = (int)p.id_persona,
                             Username = o.username,
                             Password = o.password,
                             Rol = p.rol
                         };
<<<<<<< HEAD
            //try
            //{
            //    acceso.Any();
            //}
            //catch(EntityException e)
            //{
            //    Console.WriteLine(e);
            //}

            if (acceso.Any())
            {
               
                var condicion = acceso.ElementAt(0).Rol.ToLower();
                oUsuario.Rol = condicion;
                Session["usuario"] = oUsuario;
                return RedirectToAction("Index", "HOME");
                
=======

            if (acceso.Any())
            {
                if (acceso.ElementAt(0).Password == oUsuario.Password)
                {
                    var condicion = acceso.ElementAt(0).Rol.ToLower();
                    oUsuario.Rol = condicion;
                    Session["usuario"] = oUsuario;
                    return RedirectToAction("Index", "HOME");
                }
                ViewData["Mensaje"] = "Contraseña incorrecta";
                return View();
>>>>>>> 774fa1bd1398e6b159266155379f2130a307feb3
            }            
            ViewData["Mensaje"] = "Usuario o contraseña incorrectos";
            return View();            
        }

        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

    }
}