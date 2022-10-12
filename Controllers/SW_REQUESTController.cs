using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasaSW.Models;
using CasaSW.Permisos;

namespace CasaSW.Controllers
{
    //[ValidarSesion]
    public class SW_REQUESTController : Controller
    {
        private CASASWEntities db = new CASASWEntities();

        // GET: SW_REQUEST
        public ActionResult Index()
        {
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
        public ActionResult Create([Bind(Include = "id_persona,specifiations")] SW_REQUEST sW_REQUEST)
        {
            if (ModelState.IsValid)
            {
                db.SW_REQUEST.Add(sW_REQUEST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_persona = new SelectList(db.PERSONA, "id_persona", "username", sW_REQUEST.id_persona);
            return View(sW_REQUEST);
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
