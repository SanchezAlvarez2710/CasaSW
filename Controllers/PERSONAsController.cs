using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasaSW.Models;
using CasaSW.Permisos;
namespace CasaSW.Controllers
{
    //[ValidarSesion]
    public class PERSONAsController : Controller
    {
        private CASASWEntities db = new CASASWEntities();

        // GET: PERSONAs
        public ActionResult Index()
        {
            //return View(db.PERSONA.ToList());
                var UserPersona = from p in db.PERSONA
                                  join u in db.USER on p.id_persona equals u.id_persona
                                  select new UserPersona {
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
            
            return View(UserPersona);
        }

        // GET: PERSONAs/Details/5
        public ActionResult Details(int? id)
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

        // GET: PERSONAs/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: PERSONAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PERSONA pERSONA = db.PERSONA.Find(id);
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
