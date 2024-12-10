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
    public class MovimientosController : Controller
    {
        private GestionViajerosEntities db = new GestionViajerosEntities();

        // GET: Movimientos
        public ActionResult Index()
        {
            var movimientos = db.Movimientos.Include(m => m.PuntosControl).Include(m => m.Usuarios).Include(m => m.Viajeros);
            return View(movimientos.ToList());
        }

        // GET: Movimientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimientos movimientos = db.Movimientos.Find(id);
            if (movimientos == null)
            {
                return HttpNotFound();
            }
            return View(movimientos);
        }

        // GET: Movimientos/Create
        public ActionResult Create()
        {
            ViewBag.IdPuntoControl = new SelectList(db.PuntosControl, "Id", "Nombre");
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "NombreUsuario");
            ViewBag.IdViajero = new SelectList(db.Viajeros, "Id", "Nombre");
            return View();
        }

        // POST: Movimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdViajero,Fecha,Destino,Origen,TipoSolicitud,IdUsuario,IdPuntoControl")] Movimientos movimientos)
        {
            if (ModelState.IsValid)
            {
                db.Movimientos.Add(movimientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPuntoControl = new SelectList(db.PuntosControl, "Id", "Nombre", movimientos.IdPuntoControl);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "NombreUsuario", movimientos.IdUsuario);
            ViewBag.IdViajero = new SelectList(db.Viajeros, "Id", "Nombre", movimientos.IdViajero);
            return View(movimientos);
        }

        // GET: Movimientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimientos movimientos = db.Movimientos.Find(id);
            if (movimientos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPuntoControl = new SelectList(db.PuntosControl, "Id", "Nombre", movimientos.IdPuntoControl);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "NombreUsuario", movimientos.IdUsuario);
            ViewBag.IdViajero = new SelectList(db.Viajeros, "Id", "Nombre", movimientos.IdViajero);
            return View(movimientos);
        }

        // POST: Movimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdViajero,Fecha,Destino,Origen,TipoSolicitud,IdUsuario,IdPuntoControl")] Movimientos movimientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPuntoControl = new SelectList(db.PuntosControl, "Id", "Nombre", movimientos.IdPuntoControl);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "NombreUsuario", movimientos.IdUsuario);
            ViewBag.IdViajero = new SelectList(db.Viajeros, "Id", "Nombre", movimientos.IdViajero);
            return View(movimientos);
        }

        // GET: Movimientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimientos movimientos = db.Movimientos.Find(id);
            if (movimientos == null)
            {
                return HttpNotFound();
            }
            return View(movimientos);
        }

        // POST: Movimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movimientos movimientos = db.Movimientos.Find(id);
            db.Movimientos.Remove(movimientos);
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
