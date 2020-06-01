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
    public class TelaiCompletiController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: TelaiCompleti
        public ActionResult Index()
        {
            var telaiAnagrafica = db.TelaiAnagrafica.Include(t => t.Bisarchista).Include(t => t.Bisarchista1).Include(t => t.Carrozzeria).Include(t => t.Carrozzeria1).Include(t => t.Commesse).Include(t => t.Tecnici);
            return View(telaiAnagrafica.ToList());
        }

        // GET: TelaiCompleti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TelaiAnagrafica telaiAnagrafica = db.TelaiAnagrafica.Find(id);
            if (telaiAnagrafica == null)
            {
                return HttpNotFound();
            }
            return View(telaiAnagrafica);
        }

        // GET: TelaiCompleti/Create
        public ActionResult Create()
        {
            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr");
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr");
            ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale");
            ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale");
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Descrizione");
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Cognome");
            return View();
        }

        // POST: TelaiCompleti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Telaio,Modello,InsertUser,InsertDate,NomeFile,IDCommessa,Annotazioni,DataIn,DataOut,NFattAttiva,DataFattAtt,ImpFattAtt,IDTecnico,DataFatturaPassiva,ImportoFattPass,IDCarrozzeria1,NumFattCarrozzeria1,DataFatturaCarrozzeria1,ImportoCarrozzeria1,IDCarrozzeria2,NumFattCarrozzeria2,DataFatturaCarrozzeria2,ImportoCarrozzeria2,IDCarglass,NumFattCarGlass,ImportoFattCarGlass,DataFatturaCarglass,IDBisarchistaAndata,NumFattBisarchistaA,DataFattBisarchistaA,CostoAndata,IDBisarchistaRitorno,NumFattBisarchistaR,DataFattBisarchistaR,CostoRitorno,Costi")] TelaiAnagrafica telaiAnagrafica)
        {
            if (ModelState.IsValid)
            {
                db.TelaiAnagrafica.Add(telaiAnagrafica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaAndata);
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaRitorno);
            ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria1);
            ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria2);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Descrizione", telaiAnagrafica.IDCommessa);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Cognome", telaiAnagrafica.IDTecnico);
            return View(telaiAnagrafica);
        }

        // GET: TelaiCompleti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TelaiAnagrafica telaiAnagrafica = db.TelaiAnagrafica.Find(id);
            if (telaiAnagrafica == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaAndata);
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaRitorno);
            ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria1);
            ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria2);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", telaiAnagrafica.IDCommessa);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice", telaiAnagrafica.IDTecnico);
            return View(telaiAnagrafica);
        }

        // POST: TelaiCompleti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Telaio,Modello,InsertUser,InsertDate,NomeFile,IDCommessa,Annotazioni,DataIn,DataOut,NFattAttiva,DataFattAtt,ImpFattAtt,IDTecnico,DataFatturaPassiva,ImportoFattPass,IDCarrozzeria1,NumFattCarrozzeria1,DataFatturaCarrozzeria1,ImportoCarrozzeria1,IDCarrozzeria2,NumFattCarrozzeria2,DataFatturaCarrozzeria2,ImportoCarrozzeria2,IDCarglass,NumFattCarGlass,ImportoFattCarGlass,DataFatturaCarglass,IDBisarchistaAndata,NumFattBisarchistaA,DataFattBisarchistaA,CostoAndata,IDBisarchistaRitorno,NumFattBisarchistaR,DataFattBisarchistaR,CostoRitorno,Costi")] TelaiAnagrafica telaiAnagrafica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telaiAnagrafica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaAndata);
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaRitorno);
            ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria1);
            ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria2);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", telaiAnagrafica.IDCommessa);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice", telaiAnagrafica.IDTecnico);
            return View(telaiAnagrafica);
        }

        // GET: TelaiCompleti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TelaiAnagrafica telaiAnagrafica = db.TelaiAnagrafica.Find(id);
            if (telaiAnagrafica == null)
            {
                return HttpNotFound();
            }
            return View(telaiAnagrafica);
        }

        // POST: TelaiCompleti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TelaiAnagrafica telaiAnagrafica = db.TelaiAnagrafica.Find(id);
            db.TelaiAnagrafica.Remove(telaiAnagrafica);
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
