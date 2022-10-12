using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasaSW.Models;
using CasaSW.Models.ViewModel;
using CasaSW.Permisos;

namespace CasaSW.Controllers
{
    [ValidarSesion]
    public class SW_REQUESTController : Controller
    {
        private CASASWEntities db = new CASASWEntities();

        // GET: SW_REQUEST
        public ActionResult Index()
        {
            //CustomerPersona persona = new CustomerPersona();
            //var Customer_Persona = from p in db.PERSONA
            //                      join u in db.USER on p.id_persona equals u.id_persona
            //                      join o in db.ORDER on p.id_persona equals o.id_persona
            //                      join pr in db.PRODUCT on p.id_persona equals pr.id_persona
            //                      join rq in db.SW_REQUEST on p.id_persona equals rq.id_persona
            //                      select new RequestPOCP
            //                      {                                      
            //                          id_persona = p.id_persona,
            //                          specifiations = rq.specifiations,
            //                          customerPersona = new CustomerPersona(p.id_persona, p.username, 
            //                          p.password, p.name, p.email, u.denied, u.phoneN, u.signUpDate, u.adminFB),
            //                          orderProduct = new OrderProduct(p.id_persona, p.username, o.id_order,
            //                          o.orderName, o.state, o.subtotal, o.total,pr.id_product, pr.name, pr.type,
            //                          pr.version, pr.description)
            //                      };
            //return View(Customer_Persona);
            var sW_REQUEST = db.SW_REQUEST.Include(s => s.PERSONA);
            return View(sW_REQUEST.ToList());
        }

        // GET: SW_REQUEST/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SW_REQUEST sW_REQUEST = db.SW_REQUEST.Find(id);
            if (sW_REQUEST == null)
            {
                return HttpNotFound();
            }
            return View(sW_REQUEST);
        }

        // GET: SW_REQUEST/Create
        public ActionResult Create()
        {
            ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "username");
            return View();
        }

        // POST: SW_REQUEST/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RequestPOCP requestPOCP)
        {
            //[Bind(Include = "id_persona,specifiations")] SW_REQUEST sW_REQUEST
            if (ModelState.IsValid)
            {
             //INSTANCE DB CLASSES
            PERSONA pERSONA = new PERSONA(requestPOCP.id_persona, requestPOCP.username_persona, requestPOCP.Password_, requestPOCP.Name_, requestPOCP.Email_);
            USER uSER = new USER(0,requestPOCP.id_persona, 0, requestPOCP.PhoneN_, requestPOCP.signUpDate_, requestPOCP.AdminFB_);
            SW_REQUEST sW_REQUEST = new SW_REQUEST(0,requestPOCP.id_persona, requestPOCP.product_description);
            PRODUCT pRODUCT = new PRODUCT(requestPOCP.id_product_, requestPOCP.id_persona, requestPOCP.product_name, requestPOCP.product_type, "", requestPOCP.product_description);
            ORDER oRDER = new ORDER(requestPOCP.id_order_, requestPOCP.id_product_, requestPOCP.id_persona, requestPOCP.product_name + " Order", "IN PROCESS", 0.00, 0.00);
            //ADD ENTITIES TO DB
            //db.("INSERT INTO USER VALUES ({0},{1},{2},{3},{4})",requestPOCP.id_persona,requestPOCP.Denied_,requestPOCP.PhoneN_, requestPOCP.signUpDate_,requestPOCP.AdminFB_);
            //db.SW_REQUEST.SqlQuery("INSERT INTO USER VALUES ({0},{1})", requestPOCP.id_persona, requestPOCP.product_description);

            db.PERSONA.Add(pERSONA);
            db.USER.Add(uSER);
            db.PRODUCT.Add(pRODUCT);
            db.ORDER.Add(oRDER);
            db.SW_REQUEST.Add(sW_REQUEST);
            //SAVE CHANGES TO DB
            db.SaveChanges();
        }            

            return RedirectToAction("Index", "HOME");
            //ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "username", sW_REQUEST.id_persona);
            //if (ModelState.IsValid)
            //{
            //    db.SW_REQUEST.Add(sW_REQUEST);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "username", sW_REQUEST.id_persona);
            //return View(sW_REQUEST);
        }

        // GET: SW_REQUEST/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SW_REQUEST sW_REQUEST = db.SW_REQUEST.Find(id);
            if (sW_REQUEST == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "username", sW_REQUEST.id_persona);
            return View(sW_REQUEST);
        }

        // POST: SW_REQUEST/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_persona,specifiations")] SW_REQUEST sW_REQUEST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sW_REQUEST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "username", sW_REQUEST.id_persona);
            return View(sW_REQUEST);
        }

        // GET: SW_REQUEST/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SW_REQUEST sW_REQUEST = db.SW_REQUEST.Find(id);
            if (sW_REQUEST == null)
            {
                return HttpNotFound();
            }
            return View(sW_REQUEST);
        }

        // POST: SW_REQUEST/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SW_REQUEST sW_REQUEST = db.SW_REQUEST.Find(id);
            db.SW_REQUEST.Remove(sW_REQUEST);
            db.SaveChanges();
            return RedirectToAction("Index");
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
