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
    public class CarrozzeriasController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: Carrozzerias
        public ActionResult Index()
        {
            return View(db.Carrozzeria.ToList());
        }

        // GET: Carrozzerias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrozzeria carrozzeria = db.Carrozzeria.Find(id);
            if (carrozzeria == null)
            {
                return HttpNotFound();
            }
            return View(carrozzeria);
        }

        // GET: Carrozzerias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carrozzerias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RagioneSociale,Mail,Tel")] Carrozzeria carrozzeria)
        {
            if (ModelState.IsValid)
            {
                db.Carrozzeria.Add(carrozzeria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carrozzeria);
        }

        // GET: Carrozzerias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrozzeria carrozzeria = db.Carrozzeria.Find(id);
            if (carrozzeria == null)
            {
                return HttpNotFound();
            }
            return View(carrozzeria);
        }

        // POST: Carrozzerias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RagioneSociale,Mail,Tel")] Carrozzeria carrozzeria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carrozzeria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carrozzeria);
        }

        // GET: Carrozzerias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrozzeria carrozzeria = db.Carrozzeria.Find(id);
            if (carrozzeria == null)
            {
                return HttpNotFound();
            }
            return View(carrozzeria);
        }

        // POST: Carrozzerias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carrozzeria carrozzeria = db.Carrozzeria.Find(id);
            db.Carrozzeria.Remove(carrozzeria);
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
