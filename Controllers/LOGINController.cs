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
                         where o.username == oUsuario.Username 
                         select new UserAdmin
                         {
                             IdUsuario = (int)p.id_persona,
                             Username = o.username,
                             Password = o.password,
                             Rol = p.rol
                         };


            if (acceso.Any())
            {
                if (acceso.ElementAt(0).Password == oUsuario.Password)
                {
                    var condicion = acceso.ElementAt(0).Rol.ToLower();
                    Session["usuario"] = oUsuario;
                    switch (condicion)
                    {
                        case "sac":
                            return RedirectToAction("Index", "HOME");
                        case "admin":
                            return RedirectToAction("Index", "HOME");
                    }
                }
                ViewData["Mensaje"] = "Contraseña no coincide";
                return View();
            }            
            ViewData["Mensaje"] = "Usuario no encontrado";
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