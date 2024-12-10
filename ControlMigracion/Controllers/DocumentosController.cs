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
    public class DocumentosController : Controller
    {
        private GestionViajerosEntities db = new GestionViajerosEntities();

        // GET: Documentos
        public ActionResult Index()
        {
            var documentos = db.Documentos.Include(d => d.Viajeros);
            return View(documentos.ToList());
        }

        // GET: Documentos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documentos documentos = db.Documentos.Find(id);
            if (documentos == null)
            {
                return HttpNotFound();
            }
            return View(documentos);
        }

        // GET: Documentos/Create
        public ActionResult Create()
        {
            ViewBag.IdViajero = new SelectList(db.Viajeros, "Id", "Nombre");
            return View();
        }

        // POST: Documentos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TipoDocumento,NumeroDocumento,FechaExpiracion,IdViajero")] Documentos documentos)
        {
            if (ModelState.IsValid)
            {
                db.Documentos.Add(documentos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdViajero = new SelectList(db.Viajeros, "Id", "Nombre", documentos.IdViajero);
            return View(documentos);
        }

        // GET: Documentos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documentos documentos = db.Documentos.Find(id);
            if (documentos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdViajero = new SelectList(db.Viajeros, "Id", "Nombre", documentos.IdViajero);
            return View(documentos);
        }

        // POST: Documentos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TipoDocumento,NumeroDocumento,FechaExpiracion,IdViajero")] Documentos documentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdViajero = new SelectList(db.Viajeros, "Id", "Nombre", documentos.IdViajero);
            return View(documentos);
        }

        // GET: Documentos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documentos documentos = db.Documentos.Find(id);
            if (documentos == null)
            {
                return HttpNotFound();
            }
            return View(documentos);
        }

        // POST: Documentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Documentos documentos = db.Documentos.Find(id);
            db.Documentos.Remove(documentos);
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
