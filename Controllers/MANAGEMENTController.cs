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
<<<<<<< HEAD
using System.Data.Entity.Validation;

namespace CasaSW.Controllers
{
   // [ValidarSesion]
=======

namespace CasaSW.Controllers
{
>>>>>>> 774fa1bd1398e6b159266155379f2130a307feb3
    public class MANAGEMENTController : Controller
    {
        private CASASWEntities db = new CASASWEntities();
        // GET: MANEGEMENT
        public ActionResult Index()
        {
            var UserAdmin = from o in db.PERSONA
                            join p in db.ADMIN on o.id_persona equals p.id_persona
<<<<<<< HEAD
                            where p.rol == "SAC" && p.state == 1
=======
                            where p.rol == "SAC"
>>>>>>> 774fa1bd1398e6b159266155379f2130a307feb3
                            select new UserAdmin
                            {
                                IdUsuario = (int)p.id_persona,
                                Username = o.username,
                                Password = o.password,
                                name = o.name,
                                email = o.email,
<<<<<<< HEAD
                                Rol = p.rol,
                                avatar = p.avatar,
                                state = p.state
=======
                                Rol = p.rol
>>>>>>> 774fa1bd1398e6b159266155379f2130a307feb3
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
<<<<<<< HEAD
            var oAdmin = db.ADMIN.Find(id);
=======
>>>>>>> 774fa1bd1398e6b159266155379f2130a307feb3
            model.Username = oUser.username;
            model.email = oUser.email;
            model.name = oUser.name;
            model.IdUsuario = id;
            model.Password = oUser.password;
<<<<<<< HEAD
            model.avatar = oAdmin.avatar;
=======
>>>>>>> 774fa1bd1398e6b159266155379f2130a307feb3

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
<<<<<<< HEAD
                                                                    where o.username == oUsuario.Username && p.state == 1
=======
                                                                    where o.username == oUsuario.Username
>>>>>>> 774fa1bd1398e6b159266155379f2130a307feb3
                                                                    select new UserAdmin
                                                                    {
                                                                        IdUsuario = (int)p.id_persona,
                                                                        Username = o.username,
                                                                        Password = o.password,
                                                                        Rol = p.rol
                                                                    };
            if (!acceso.Any())
            {
<<<<<<< HEAD
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
=======
                PERSONA pERSONA = new PERSONA(oUsuario.IdUsuario, oUsuario.Username, oUsuario.Password, oUsuario.name, oUsuario.email);
                ADMIN aDMIN = new ADMIN(oUsuario.IdUsuario, "SAC");
                db.PERSONA.Add(pERSONA);
                db.ADMIN.Add(aDMIN);
                db.SaveChanges();
>>>>>>> 774fa1bd1398e6b159266155379f2130a307feb3
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
<<<<<<< HEAD
            var oAdmin = db.ADMIN.Find(oUsuario.IdUsuario);

            oAdmin.avatar = oUsuario.avatar;
            oUser.username = oUsuario.Username;
            oUser.email = oUsuario.email;
            oUser.name = oUsuario.name;
=======
            oUser.username = oUsuario.Username;
            oUser.email = oUsuario.email;
            oUser.name = oUsuario.name;
            oUser.id_persona = oUsuario.IdUsuario;
>>>>>>> 774fa1bd1398e6b159266155379f2130a307feb3

            if(oUsuario.Password != null && oUsuario.Password.Trim() != "")
            {
                oUser.password = ConvertirSha256(oUsuario.Password);
            }

            db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
<<<<<<< HEAD
            db.Entry(oAdmin).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch(DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            
=======
            db.SaveChanges();

>>>>>>> 774fa1bd1398e6b159266155379f2130a307feb3
            return Redirect(Url.Content("~/MANAGEMENT/"));
                      
        }

        public ActionResult Delete(int id)
        {
            
<<<<<<< HEAD
            var oUser = db.ADMIN.Find(id);
            
            oUser.state = 3;  //1=activo, 2=inactivo, 3=eliminado          
=======

            var oUser = db.ADMIN.Find(id);
            
           // oUser.estado = 3;  1=activo, 2=inactivo, 3=eliminado          
>>>>>>> 774fa1bd1398e6b159266155379f2130a307feb3

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