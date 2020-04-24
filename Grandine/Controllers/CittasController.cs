using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Grandine.Models;

namespace Grandine.Controllers
{
    public class CittasController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: Cittas
        public ActionResult Index()
        {
            return View(db.Citta.ToList());

        }

        // GET: Cittas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citta citta = db.Citta.Find(id);
            if (citta == null)
            {
                return HttpNotFound();
            }
            return View(citta);
        }

        // GET: Cittas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cittas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descr")] Citta citta)
        {
            if (ModelState.IsValid)
            {
                db.Citta.Add(citta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(citta);
        }

        // GET: Cittas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citta citta = db.Citta.Find(id);
            if (citta == null)
            {
                return HttpNotFound();
            }
            return View(citta);
        }

        // POST: Cittas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descr")] Citta citta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(citta);
        }

        // GET: Cittas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citta citta = db.Citta.Find(id);
            if (citta == null)
            {
                return HttpNotFound();
            }
            return View(citta);
        }

        // POST: Cittas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Citta citta = db.Citta.Find(id);
            db.Citta.Remove(citta);
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
