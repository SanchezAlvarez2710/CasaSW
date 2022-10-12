using CasaSW.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using CasaSW.Models.ViewModel;
using CasaSW.Permisos;
using System.Security.Cryptography;
using System.Text;

namespace CasaSW.Controllers
{
    public class MANAGEMENTController : Controller
    {
        private CASASWEntities db = new CASASWEntities();
        // GET: MANEGEMENT
        public ActionResult Index()
        {
            var UserAdmin = from o in db.PERSONA
                            join p in db.ADMIN on o.id_persona equals p.id_persona
                            where p.rol == "SAC"
                            select new UserAdmin
                            {
                                IdUsuario = (int)p.id_persona,
                                Username = o.username,
                                Password = o.password,
                                name = o.name,
                                email = o.email,
                                Rol = p.rol
                            };                 
            return View(UserAdmin);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            UserAdmin model = new UserAdmin();

            var oUser = db.PERSONA.Find(id);
            model.Username = oUser.username;
            model.email = oUser.email;
            model.name = oUser.name;
            model.IdUsuario = id;
            model.Password = oUser.password;

            return View(model);
        }

        [HttpPost]
       
        public ActionResult Create(UserAdmin oUsuario)
        {
          
            if (oUsuario.Password == oUsuario.ConfirmarPassword)
            {
                oUsuario.Password = ConvertirSha256(oUsuario.Password);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }


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
            if (!acceso.Any())
            {
                PERSONA pERSONA = new PERSONA(oUsuario.IdUsuario, oUsuario.Username, oUsuario.Password, oUsuario.name, oUsuario.email);
                ADMIN aDMIN = new ADMIN(oUsuario.IdUsuario, "SAC");
                db.PERSONA.Add(pERSONA);
                db.ADMIN.Add(aDMIN);
                db.SaveChanges();
                ViewData["Mensaje"] = "Usuario Creado";

                return Redirect(Url.Content("~/MANAGEMENT/"));
            }
            ViewData["Mensaje"] = "Usuario ya existe";
            return View();
        }

        public ActionResult Edit(UserAdmin oUsuario)
        {
            if (oUsuario.Password != null && oUsuario.Password.Trim() != "" && oUsuario.ConfirmarPassword != null && oUsuario.ConfirmarPassword.Trim() != "")
            {
                if (oUsuario.Password == oUsuario.ConfirmarPassword)
                {
                    oUsuario.Password = ConvertirSha256(oUsuario.Password);
                }
                else
                {
                    ViewData["Mensaje"] = "Las contraseñas no coinciden";
                    return View();
                }
            }
  
            var oUser = db.PERSONA.Find(oUsuario.IdUsuario);
            oUser.username = oUsuario.Username;
            oUser.email = oUsuario.email;
            oUser.name = oUsuario.name;
            oUser.id_persona = oUsuario.IdUsuario;

            if(oUsuario.Password != null && oUsuario.Password.Trim() != "")
            {
                oUser.password = ConvertirSha256(oUsuario.Password);
            }

            db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Redirect(Url.Content("~/MANAGEMENT/"));
                      
        }

        public ActionResult Delete(int id)
        {
            

            var oUser = db.ADMIN.Find(id);
            
           // oUser.estado = 3;  1=activo, 2=inactivo, 3=eliminado          

            db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Content("1");

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