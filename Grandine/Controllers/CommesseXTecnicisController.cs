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
    public class CommesseXTecnicisController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: CommesseXTecnicis
        public ActionResult Index()
        {
            var commesseXTecnici = db.CommesseXTecnici.Include(c => c.Commesse).Include(c => c.Tecnici);
            return View(commesseXTecnici.ToList());
        }

        // GET: CommesseXTecnicis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommesseXTecnici commesseXTecnici = db.CommesseXTecnici.Find(id);
            if (commesseXTecnici == null)
            {
                return HttpNotFound();
            }
            return View(commesseXTecnici);
        }

        // GET: CommesseXTecnicis/Create
        public ActionResult Create()
        {
            //ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice");
            //ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice");
            ViewBag.IDCommessa = new SelectList(GetCommessa(), "Text", "Value");
            ViewBag.IDTecnico = new SelectList(GetTecnico(), "Text", "Value");
            return View();
        }

        // POST: CommesseXTecnicis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDCommessa,IDTecnico")] CommesseXTecnici commesseXTecnici)
        {
            if (ModelState.IsValid)
            {
                db.CommesseXTecnici.Add(commesseXTecnici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", commesseXTecnici.IDCommessa);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice", commesseXTecnici.IDTecnico);

            

            return View(commesseXTecnici);
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

        public IEnumerable<SelectListItem> GetTecnico()
        {
            IEnumerable<SelectListItem> list = null;

            var query = (from ca in db.Tecnici
                         where 0 == 0

                         select new SelectListItem { Text = ca.ID.ToString(), Value = ca.Cognome + " " + ca.Nome }).Distinct();
            list = query.ToList();




            return list;
        }

        // GET: CommesseXTecnicis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommesseXTecnici commesseXTecnici = db.CommesseXTecnici.Find(id);
            if (commesseXTecnici == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", commesseXTecnici.IDCommessa);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice", commesseXTecnici.IDTecnico);
            return View(commesseXTecnici);
        }

        // POST: CommesseXTecnicis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDCommessa,IDTecnico")] CommesseXTecnici commesseXTecnici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commesseXTecnici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", commesseXTecnici.IDCommessa);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice", commesseXTecnici.IDTecnico);
            return View(commesseXTecnici);
        }

        // GET: CommesseXTecnicis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommesseXTecnici commesseXTecnici = db.CommesseXTecnici.Find(id);
            if (commesseXTecnici == null)
            {
                return HttpNotFound();
            }
            return View(commesseXTecnici);
        }

        // POST: CommesseXTecnicis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommesseXTecnici commesseXTecnici = db.CommesseXTecnici.Find(id);
            db.CommesseXTecnici.Remove(commesseXTecnici);
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
