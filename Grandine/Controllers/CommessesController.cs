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
    public class CommessesController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: Commesses
        public ActionResult Index()
        {
            return View(db.Commesse.ToList());
        }

        // GET: Commesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commesse commesse = db.Commesse.Find(id);
            if (commesse == null)
            {
                return HttpNotFound();
            }
            return View(commesse);
        }

        // GET: Commesses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Commesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Codice,Descrizione,StarDate,EndDate")] Commesse commesse)
        {
            if (ModelState.IsValid)
            {
                db.Commesse.Add(commesse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commesse);
        }

        // GET: Commesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commesse commesse = db.Commesse.Find(id);
            if (commesse == null)
            {
                return HttpNotFound();
            }
            return View(commesse);
        }

        // POST: Commesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Codice,Descrizione,StarDate,EndDate")] Commesse commesse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commesse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commesse);
        }

        // GET: Commesses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commesse commesse = db.Commesse.Find(id);
            if (commesse == null)
            {
                return HttpNotFound();
            }
            return View(commesse);
        }

        // POST: Commesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commesse commesse = db.Commesse.Find(id);
            db.Commesse.Remove(commesse);
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
