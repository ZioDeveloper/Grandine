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
    public class StoricoStatusController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: StoricoStatus
        public ActionResult Index()
        {
            var storicoStatus = db.StoricoStatus.Include(s => s.Status).Include(s => s.Tecnici).Include(s => s.Utenti).Include(s => s.TelaiAnagrafica);
            return View(storicoStatus.ToList());
        }

        // GET: StoricoStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoricoStatus storicoStatus = db.StoricoStatus.Find(id);
            if (storicoStatus == null)
            {
                return HttpNotFound();
            }
            return View(storicoStatus);
        }

        // GET: StoricoStatus/Create
        public ActionResult Create()
        {
            ViewBag.IDStato = new SelectList(db.Status, "ID", "Descr");
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice");
            ViewBag.IDUtente = new SelectList(db.Utenti, "ID", "Nome");
            ViewBag.IDTelaio = new SelectList(db.TelaiAnagrafica, "ID", "Telaio");
            return View();
        }

        // POST: StoricoStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDTelaio,IDStato,IDTecnico,IDUtente,InsertDate,InsertUser")] StoricoStatus storicoStatus)
        {
            if (ModelState.IsValid)
            {
                db.StoricoStatus.Add(storicoStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDStato = new SelectList(db.Status, "ID", "Descr", storicoStatus.IDStato);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice", storicoStatus.IDTecnico);
            ViewBag.IDUtente = new SelectList(db.Utenti, "ID", "Nome", storicoStatus.IDUtente);
            ViewBag.IDTelaio = new SelectList(db.TelaiAnagrafica, "ID", "Telaio", storicoStatus.IDTelaio);
            return View(storicoStatus);
        }

        // GET: StoricoStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoricoStatus storicoStatus = db.StoricoStatus.Find(id);
            if (storicoStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDStato = new SelectList(db.Status, "ID", "Descr", storicoStatus.IDStato);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice", storicoStatus.IDTecnico);
            ViewBag.IDUtente = new SelectList(db.Utenti, "ID", "Nome", storicoStatus.IDUtente);
            ViewBag.IDTelaio = new SelectList(db.TelaiAnagrafica, "ID", "Telaio", storicoStatus.IDTelaio);
            return View(storicoStatus);
        }

        // POST: StoricoStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDTelaio,IDStato,IDTecnico,IDUtente,InsertDate,InsertUser")] StoricoStatus storicoStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storicoStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDStato = new SelectList(db.Status, "ID", "Descr", storicoStatus.IDStato);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice", storicoStatus.IDTecnico);
            ViewBag.IDUtente = new SelectList(db.Utenti, "ID", "Nome", storicoStatus.IDUtente);
            ViewBag.IDTelaio = new SelectList(db.TelaiAnagrafica, "ID", "Telaio", storicoStatus.IDTelaio);
            return View(storicoStatus);
        }

        // GET: StoricoStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoricoStatus storicoStatus = db.StoricoStatus.Find(id);
            if (storicoStatus == null)
            {
                return HttpNotFound();
            }
            return View(storicoStatus);
        }

        // POST: StoricoStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoricoStatus storicoStatus = db.StoricoStatus.Find(id);
            db.StoricoStatus.Remove(storicoStatus);
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
