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
    public class BisarchistasController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: Bisarchistas
        public ActionResult Index()
        {
            return View(db.Bisarchista.OrderBy(s=>s.Descr).ToList());
        }

        // GET: Bisarchistas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bisarchista bisarchista = db.Bisarchista.Find(id);
            if (bisarchista == null)
            {
                return HttpNotFound();
            }
            return View(bisarchista);
        }

        // GET: Bisarchistas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bisarchistas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Descr")] Bisarchista bisarchista)
        {
            if (ModelState.IsValid)
            {
                db.Bisarchista.Add(bisarchista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bisarchista);
        }

        // GET: Bisarchistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bisarchista bisarchista = db.Bisarchista.Find(id);
            if (bisarchista == null)
            {
                return HttpNotFound();
            }
            return View(bisarchista);
        }

        // POST: Bisarchistas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Descr")] Bisarchista bisarchista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bisarchista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bisarchista);
        }

        // GET: Bisarchistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bisarchista bisarchista = db.Bisarchista.Find(id);
            if (bisarchista == null)
            {
                return HttpNotFound();
            }
            return View(bisarchista);
        }

        // POST: Bisarchistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bisarchista bisarchista = db.Bisarchista.Find(id);
            db.Bisarchista.Remove(bisarchista);
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
