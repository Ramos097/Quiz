using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ControlMigracion.Controllers
{
    public class PuntosControlsController : Controller
    {
        private GestionViajerosEntities db = new GestionViajerosEntities();

        // GET: PuntosControls
        public ActionResult Index()
        {
            return View(db.PuntosControl.ToList());
        }

        // GET: PuntosControls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PuntosControl puntosControl = db.PuntosControl.Find(id);
            if (puntosControl == null)
            {
                return HttpNotFound();
            }
            return View(puntosControl);
        }

        // GET: PuntosControls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PuntosControls/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Ubicacion,CodigoControl")] PuntosControl puntosControl)
        {
            if (ModelState.IsValid)
            {
                db.PuntosControl.Add(puntosControl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(puntosControl);
        }

        // GET: PuntosControls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PuntosControl puntosControl = db.PuntosControl.Find(id);
            if (puntosControl == null)
            {
                return HttpNotFound();
            }
            return View(puntosControl);
        }

        // POST: PuntosControls/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Ubicacion,CodigoControl")] PuntosControl puntosControl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(puntosControl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(puntosControl);
        }

        // GET: PuntosControls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PuntosControl puntosControl = db.PuntosControl.Find(id);
            if (puntosControl == null)
            {
                return HttpNotFound();
            }
            return View(puntosControl);
        }

        // POST: PuntosControls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PuntosControl puntosControl = db.PuntosControl.Find(id);
            db.PuntosControl.Remove(puntosControl);
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
