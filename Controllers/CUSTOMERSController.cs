
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CasaSW.Models.ViewModel;
using CasaSW.Models;
using CasaSW.Permisos;
using System.Collections.Generic;

namespace CasaSW.Controllers
{
    [ValidarSesion]
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

        // GET: PERSONAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERSONA pERSONA = db.PERSONA.Find(id);
            if (pERSONA == null)
            {
                return HttpNotFound();
            }
            return View(pERSONA);
        }

        // POST: PERSONAs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_persona,username,password,name,email")] PERSONA pERSONA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERSONA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pERSONA);
        }

        // POST: PERSONAs/Delete/5
        //[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            PERSONA pERSONA = db.PERSONA.Find(id);
            USER uSER = db.USER.Find();
            db.PERSONA.Remove(pERSONA);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //GET PERSONA/CUSTOMERS
        public ActionResult IndexCustomers()
        {
            var UserDirection = db.USER.Join(db.PERSONA, usr => usr.id_persona, 
                per => per.id_persona, (usr, per) => new { usr, per }).ToList()/*FirstOrDefault(x => x.dir.id_persona == 1)*/;
            
            return View(UserDirection);
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
