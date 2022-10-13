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
using System.Data.Entity.Validation;

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
                            where p.rol == "SAC" && p.state == 1
                            select new UserAdmin
                            {
                                IdUsuario = (int)p.id_persona,
                                Username = o.username,
                                Password = o.password,
                                name = o.name,
                                email = o.email,
                                Rol = p.rol,
                                avatar = p.avatar,
                                state = p.state
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
            var oAdmin = db.ADMIN.Find(id);

            model.Username = oUser.username;
            model.email = oUser.email;
            model.name = oUser.name;
            model.IdUsuario = id;
            model.Password = oUser.password;
            model.avatar = oAdmin.avatar;

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
                                                                    where o.username == oUsuario.Username && p.state == 1
                                                                    select new UserAdmin
                                                                    {
                                                                        IdUsuario = (int)p.id_persona,
                                                                        Username = o.username,
                                                                        Password = o.password,
                                                                        Rol = p.rol
                                                                    };
            if (!acceso.Any())
            {
                PERSONA pERSONA = new PERSONA();
                ADMIN aDMIN = new ADMIN();

                pERSONA.id_persona = oUsuario.IdUsuario;
                pERSONA.username = oUsuario.Username;
                pERSONA.password = oUsuario.Password;
                pERSONA.email = oUsuario.email;
                pERSONA.name = oUsuario.name;
                aDMIN.id_persona = oUsuario.IdUsuario;
                aDMIN.state = 1;
                aDMIN.rol = "SAC";
                aDMIN.avatar = oUsuario.avatar;

                db.PERSONA.Add(pERSONA);
                db.ADMIN.Add(aDMIN);

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e);
                }

                ViewData["Mensaje"] = "Usuario Creado";

                return Redirect(Url.Content("~/MANAGEMENT/Index"));
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
            var oAdmin = db.ADMIN.Find(oUsuario.IdUsuario);

            oAdmin.avatar = oUsuario.avatar;
            oUser.username = oUsuario.Username;
            oUser.email = oUsuario.email;
            oUser.name = oUsuario.name;

            if(oUsuario.Password != null && oUsuario.Password.Trim() != "")
            {
                oUser.password = ConvertirSha256(oUsuario.Password);
            }

            db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;

            db.Entry(oAdmin).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch(DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            
            return Redirect(Url.Content("~/MANAGEMENT/Index"));
                      
        }

        public ActionResult Delete(int id)
        {
            
            var oUser = db.ADMIN.Find(id);
            
            oUser.state = 3;  //1=activo, 2=inactivo, 3=eliminado          

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