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
    public class CommesseXBisarchistisController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: CommesseXBisarchistis
        public ActionResult Index()
        {
            var commesseXBisarchisti = db.CommesseXBisarchisti.Include(c => c.Bisarchista).Include(c => c.Commesse);
            return View(commesseXBisarchisti.ToList());
        }

        // GET: CommesseXBisarchistis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommesseXBisarchisti commesseXBisarchisti = db.CommesseXBisarchisti.Find(id);
            if (commesseXBisarchisti == null)
            {
                return HttpNotFound();
            }
            return View(commesseXBisarchisti);
        }

        // GET: CommesseXBisarchistis/Create
        public ActionResult Create()
        {
            ViewBag.IDBisarchista = new SelectList(db.Bisarchista, "ID", "Descr");
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice");
            return View();
        }

        // POST: CommesseXBisarchistis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDCommessa,IDBisarchista")] CommesseXBisarchisti commesseXBisarchisti)
        {
            if (ModelState.IsValid)
            {
                db.CommesseXBisarchisti.Add(commesseXBisarchisti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBisarchista = new SelectList(db.Bisarchista, "ID", "Descr", commesseXBisarchisti.IDBisarchista);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", commesseXBisarchisti.IDCommessa);
            return View(commesseXBisarchisti);
        }

        public IEnumerable<SelectListItem> GetCommessa()
        {
            IEnumerable<SelectListItem> list = null;

            var query = (from ca in db.Commesse
                         where 0 == 0

                         select new SelectListItem { Text = ca.ID.ToString(), Value = ca.Descrizione.ToString() }).Distinct();
            list = query.ToList();




            return list;
        }

        // GET: CommesseXBisarchistis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommesseXBisarchisti commesseXBisarchisti = db.CommesseXBisarchisti.Find(id);
            if (commesseXBisarchisti == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBisarchista = new SelectList(db.Bisarchista, "ID", "Descr", commesseXBisarchisti.IDBisarchista);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", commesseXBisarchisti.IDCommessa);
            return View(commesseXBisarchisti);
        }

        // POST: CommesseXBisarchistis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDCommessa,IDBisarchista")] CommesseXBisarchisti commesseXBisarchisti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commesseXBisarchisti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBisarchista = new SelectList(db.Bisarchista, "ID", "Descr", commesseXBisarchisti.IDBisarchista);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", commesseXBisarchisti.IDCommessa);
            return View(commesseXBisarchisti);
        }

        // GET: CommesseXBisarchistis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommesseXBisarchisti commesseXBisarchisti = db.CommesseXBisarchisti.Find(id);
            if (commesseXBisarchisti == null)
            {
                return HttpNotFound();
            }
            return View(commesseXBisarchisti);
        }

        // POST: CommesseXBisarchistis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommesseXBisarchisti commesseXBisarchisti = db.CommesseXBisarchisti.Find(id);
            db.CommesseXBisarchisti.Remove(commesseXBisarchisti);
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
