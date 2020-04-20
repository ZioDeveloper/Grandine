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
    public class CommesseXClientisController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: CommesseXClientis
        public ActionResult Index()
        {
            var commesseXClienti = db.CommesseXClienti.Include(c => c.Clienti).Include(c => c.Commesse);
            return View(commesseXClienti.ToList());
        }

        // GET: CommesseXClientis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommesseXClienti commesseXClienti = db.CommesseXClienti.Find(id);
            if (commesseXClienti == null)
            {
                return HttpNotFound();
            }
            return View(commesseXClienti);
        }

        // GET: CommesseXClientis/Create
        public ActionResult Create()
        {
            //ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Codice");
            //ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice");
            ViewBag.IDCliente = new SelectList(GetCliente(), "Text", "Value");
            ViewBag.IDCommessa = new SelectList(GetCommessa(), "Text", "Value");
            return View();
        }

        // POST: CommesseXClientis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDCommessa,IDCliente")] CommesseXClienti commesseXClienti)
        {
            if (ModelState.IsValid)
            {
                db.CommesseXClienti.Add(commesseXClienti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Codice", commesseXClienti.IDCliente);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", commesseXClienti.IDCommessa);
            return View(commesseXClienti);
        }

        public IEnumerable<SelectListItem> GetCommessa()
        {
            IEnumerable<SelectListItem> list = null;

            var query = (from s in db.Commesse
                         where 0 == 0

                         select new SelectListItem { Text = s.ID.ToString(), Value = s.Descrizione.ToString() }).Distinct();
            list = query.ToList();




            return list;
        }

        public IEnumerable<SelectListItem> GetCliente()
        {
            IEnumerable<SelectListItem> list = null;

            var query = (from s in db.Clienti
                         where 0 == 0

                         select new SelectListItem { Text = s.ID.ToString(), Value = s.RagioneSociale }).Distinct();
            list = query.ToList();




            return list;
        }

        // GET: CommesseXClientis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommesseXClienti commesseXClienti = db.CommesseXClienti.Find(id);
            if (commesseXClienti == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Codice", commesseXClienti.IDCliente);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", commesseXClienti.IDCommessa);
            return View(commesseXClienti);
        }

        // POST: CommesseXClientis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDCommessa,IDCliente")] CommesseXClienti commesseXClienti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commesseXClienti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Codice", commesseXClienti.IDCliente);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", commesseXClienti.IDCommessa);
            return View(commesseXClienti);
        }

        // GET: CommesseXClientis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommesseXClienti commesseXClienti = db.CommesseXClienti.Find(id);
            if (commesseXClienti == null)
            {
                return HttpNotFound();
            }
            return View(commesseXClienti);
        }

        // POST: CommesseXClientis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommesseXClienti commesseXClienti = db.CommesseXClienti.Find(id);
            db.CommesseXClienti.Remove(commesseXClienti);
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
