
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CasaSW.Models.ViewModel;
using CasaSW.Models;
using CasaSW.Permisos;
using System.Collections.Generic;
using System.Web.UI;
using System.Text;
using System.Security.Cryptography;
using System.Data.Entity.Validation;
using System;

namespace CasaSW.Controllers
{
    public class CUSTOMERSController : Controller
    {
        private CASASWEntities db = new CASASWEntities();

        // GET: USERS
        public ActionResult Index()
        {
            var CustomerPersona = from p in db.PERSONA
                                  join u in db.USER on p.id_persona equals u.id_persona
                                  select new CustomerPersona {
                                      Id_ = p.id_persona,
                                      Username_ = p.username,
                                      Password_ = p.password,
                                      Name_ = p.name,
                                      Email_ = p.email,
                                      Denied_ = u.denied,
                                      PhoneN_ = u.phoneN,
                                      signUpDate_ = u.signUpDate,
                                      AdminFB_ = u.adminFB
                                  };
            
            return View(CustomerPersona);
        }

        public ActionResult Orders(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pERSONA = db.PERSONA.Find(id);
            var OrderProduct = from o in db.ORDER
                               join p in db.PRODUCT on o.id_product equals p.id_product
                               where o.id_persona == id
                               select new OrderProduct
                               {
                                   id_persona_ = (int)p.id_persona,
                                   username_persona = pERSONA.username,                                   
                                   orderName_ = o.orderName,
                                   orderDate = o.orderDate,
                                   state_ = o.state,
                                   subtotal_ = o.subtotal,
                                   total_ = o.total,
                                   id_product_ = p.id_product,
                                   product_name = p.name,
                                   product_type = p.type,
                                   product_version = p.version,
                                   product_description = p.description
                               };
            if (OrderProduct == null)
            {
                return HttpNotFound();
            }
            return View(OrderProduct);
        }

        public ActionResult Edit(int id)
        {
            CustomerPersona model = new CustomerPersona();

            var oPersona= db.PERSONA.Find(id);
            var oCustomer = db.USER.Find(id);

            model.Id_ = oPersona.id_persona;
            model.Username_ = oPersona.username;
            model.Password_ = oPersona.password;
            model.Email_ = oPersona.email;
            model.Name_ = oPersona.name;
            model.Denied_ = oCustomer.denied;
            model.PhoneN_ = oCustomer.phoneN;
            model.signUpDate_ = oCustomer.signUpDate;
            model.AdminFB_ = oCustomer.adminFB;

            return View(model);
        }

        // GET: CustomerPersona/Details/      
        [HttpGet]
        public PartialViewResult Details(int? id)
        {
            var CustomerPersona = from p in db.PERSONA
                                  join u in db.USER on p.id_persona equals u.id_persona
                                  where p.id_persona == id
                                  select new CustomerPersona
                                  {
                                      Id_ = p.id_persona,
                                      Username_ = p.username,
                                      Password_ = p.password,
                                      Name_ = p.name,
                                      Email_ = p.email,
                                      Denied_ = u.denied,
                                      PhoneN_ = u.phoneN,
                                      signUpDate_ = u.signUpDate,
                                      AdminFB_ = u.adminFB
                                  };
            return PartialView(CustomerPersona);
        }
        
        
        // GET: PERSONAs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PERSONAs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_persona,username,password,name,email")] PERSONA pERSONA)
        {
            if (ModelState.IsValid)
            {                
                db.PERSONA.Add(pERSONA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pERSONA);
        }
        [HttpPost]
        public ActionResult Edit(CustomerPersona oUsuario)
        {
            if (oUsuario.Password_ != null && oUsuario.Password_.Trim() != "")
            {
                
                    oUsuario.Password_ = ConvertirSha256(oUsuario.Password_);
              
            }

           
            var oPersona = db.PERSONA.Find(oUsuario.Id_);
            var oCustomer = db.USER.Find(oUsuario.Id_);

            oPersona.username = oUsuario.Username_;
            oPersona.email = oUsuario.Email_;
            oPersona.name = oUsuario.Name_;
            oCustomer.denied = oUsuario.Denied_;
            oCustomer.phoneN = oUsuario.PhoneN_;
            oCustomer.adminFB = oUsuario.AdminFB_;

            if (oUsuario.Password_ != null && oUsuario.Password_.Trim() != "")
            {
                oPersona.password = ConvertirSha256(oUsuario.Password_);
            }

            db.Entry(oPersona).State = System.Data.Entity.EntityState.Modified;

            db.Entry(oCustomer).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }

            return Redirect(Url.Content("~/CUSTOMERS/Index"));

        }

        public ActionResult Delete(int id)
        {

            var oPersona = db.PERSONA.Find(id);
            var oCustomer = db.USER.Find(id);
            var oProduct = db.PRODUCT.Find(id);
            var oOrder = db.ORDER.Find(id);
            var oRequest = db.SW_REQUEST.Find(id);
            if (oCustomer != null)
                db.USER.Remove(oCustomer);
            if (oRequest != null)
                db.SW_REQUEST.Remove(oRequest);
            if (oOrder != null)
                db.ORDER.Remove(oOrder);
            if (oProduct != null)
                db.PRODUCT.Remove(oProduct);
            
            db.SaveChanges();
          
            return Redirect(Url.Content("~/CUSTOMERS/Index"));

        }

        //FINISHED ODER
        public ActionResult Finish(int? id)
        {
            ORDER oSTATE = db.ORDER.Find(id);
            oSTATE.state = "FINISHED";
            if (ModelState.IsValid)
            {
                db.Entry(oSTATE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oSTATE);

        }
        
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        //GET PERSONA/CUSTOMERS
        public ActionResult IndexCustomers()
        {
            var UserDirection = db.USER.Join(db.PERSONA, usr => usr.id_persona, 
                per => per.id_persona, (usr, per) => new { usr, per }).ToList()/*FirstOrDefault(x => x.dir.id_persona == 1)*/;
            
            return View(UserDirection);
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
