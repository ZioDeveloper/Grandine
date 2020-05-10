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
    public class TecnicisController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: Tecnicis
        public ActionResult Index()
        {
            return View(db.Tecnici.OrderBy(s=>s.Cognome).ToList());
        }

        // GET: Tecnicis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnici tecnici = db.Tecnici.Find(id);
            if (tecnici == null)
            {
                return HttpNotFound();
            }
            return View(tecnici);
        }

        // GET: Tecnicis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tecnicis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Codice,Cognome,Nome,Cell,Mail")] Tecnici tecnici)
        {
            if (ModelState.IsValid)
            {
                db.Tecnici.Add(tecnici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tecnici);
        }

        // GET: Tecnicis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnici tecnici = db.Tecnici.Find(id);
            if (tecnici == null)
            {
                return HttpNotFound();
            }
            return View(tecnici);
        }

        // POST: Tecnicis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Codice,Cognome,Nome,Cell,Mail")] Tecnici tecnici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tecnici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tecnici);
        }

        // GET: Tecnicis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnici tecnici = db.Tecnici.Find(id);
            if (tecnici == null)
            {
                return HttpNotFound();
            }
            return View(tecnici);
        }

        // POST: Tecnicis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tecnici tecnici = db.Tecnici.Find(id);
            db.Tecnici.Remove(tecnici);
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
