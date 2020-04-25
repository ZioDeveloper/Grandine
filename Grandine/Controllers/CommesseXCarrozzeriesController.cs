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
    public class CommesseXCarrozzeriesController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: CommesseXCarrozzeries
        public ActionResult Index()
        {
            var commesseXCarrozzerie = db.CommesseXCarrozzerie.Include(c => c.Carrozzeria).Include(c => c.Commesse);
            return View(commesseXCarrozzerie.ToList());
        }

        // GET: CommesseXCarrozzeries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommesseXCarrozzerie commesseXCarrozzerie = db.CommesseXCarrozzerie.Find(id);
            if (commesseXCarrozzerie == null)
            {
                return HttpNotFound();
            }
            return View(commesseXCarrozzerie);
        }

        // GET: CommesseXCarrozzeries/Create
        public ActionResult Create()
        {
            ViewBag.IDCarrozzeria = new SelectList(db.Carrozzeria, "ID", "RagioneSociale");
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice");
            return View();
        }

        // POST: CommesseXCarrozzeries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDCommessa,IDCarrozzeria")] CommesseXCarrozzerie commesseXCarrozzerie)
        {
            if (ModelState.IsValid)
            {
                db.CommesseXCarrozzerie.Add(commesseXCarrozzerie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCarrozzeria = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", commesseXCarrozzerie.IDCarrozzeria);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", commesseXCarrozzerie.IDCommessa);
            return View(commesseXCarrozzerie);
        }

        // GET: CommesseXCarrozzeries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommesseXCarrozzerie commesseXCarrozzerie = db.CommesseXCarrozzerie.Find(id);
            if (commesseXCarrozzerie == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCarrozzeria = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", commesseXCarrozzerie.IDCarrozzeria);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", commesseXCarrozzerie.IDCommessa);
            return View(commesseXCarrozzerie);
        }

        // POST: CommesseXCarrozzeries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDCommessa,IDCarrozzeria")] CommesseXCarrozzerie commesseXCarrozzerie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commesseXCarrozzerie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCarrozzeria = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", commesseXCarrozzerie.IDCarrozzeria);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", commesseXCarrozzerie.IDCommessa);
            return View(commesseXCarrozzerie);
        }

        // GET: CommesseXCarrozzeries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommesseXCarrozzerie commesseXCarrozzerie = db.CommesseXCarrozzerie.Find(id);
            if (commesseXCarrozzerie == null)
            {
                return HttpNotFound();
            }
            return View(commesseXCarrozzerie);
        }

        // POST: CommesseXCarrozzeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommesseXCarrozzerie commesseXCarrozzerie = db.CommesseXCarrozzerie.Find(id);
            db.CommesseXCarrozzerie.Remove(commesseXCarrozzerie);
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
