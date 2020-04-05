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
    public class ClientisController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: Clientis
        public ActionResult Index()
        {
            return View(db.Clienti.ToList());
        }

        // GET: Clientis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clienti clienti = db.Clienti.Find(id);
            if (clienti == null)
            {
                return HttpNotFound();
            }
            return View(clienti);
        }

        // GET: Clientis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Codice,Descr")] Clienti clienti)
        {
            if (ModelState.IsValid)
            {
                db.Clienti.Add(clienti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clienti);
        }

        // GET: Clientis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clienti clienti = db.Clienti.Find(id);
            if (clienti == null)
            {
                return HttpNotFound();
            }
            return View(clienti);
        }

        // POST: Clientis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Codice,Descr")] Clienti clienti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clienti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clienti);
        }

        // GET: Clientis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clienti clienti = db.Clienti.Find(id);
            if (clienti == null)
            {
                return HttpNotFound();
            }
            return View(clienti);
        }

        // POST: Clientis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clienti clienti = db.Clienti.Find(id);
            db.Clienti.Remove(clienti);
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
