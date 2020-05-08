﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Grandine.Models;

namespace Grandine.Controllers
{
    public class TelaiAnagraficasController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        int myIDCommessa = 0;

        public ActionResult CercaTelaio(string aTelaio)
        {
            return View();
        }

        // GET: TelaiAnagraficas
        public ActionResult Index(int? IDCommessa)
        {


            if (Session["IDCommessa"] == null)
            {
                Session["IDCommessa"] = IDCommessa;
                Int32.TryParse(Session["IDCommessa"].ToString(), out myIDCommessa);
            }
            else
            {
                if((Session["IDCommessa"].ToString() == IDCommessa.ToString()))
                    Int32.TryParse(Session["IDCommessa"].ToString(), out myIDCommessa);
                else
                {
                    Session["IDCommessa"] = IDCommessa;
                    Int32.TryParse(Session["IDCommessa"].ToString(), out myIDCommessa);
                }

            }
            
            var telaiAnagrafica = db.TelaiAnagrafica.Include
                    (t => t.Bisarchista).Include
                    (t => t.Bisarchista1).Include
                    (t => t.Carrozzeria).Include
                    (t => t.Carrozzeria1).Include
                    (t => t.Commesse).Where(t=>t.IDCommessa == myIDCommessa);
            return View(telaiAnagrafica.ToList());
        }

        // GET: TelaiAnagraficas/Details/5
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

        // GET: TelaiAnagraficas/Create
        public ActionResult Create(int? IDCommessa)
        {
           

            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr");
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr");
            ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale");
            ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale");
            //ViewBag.IDCommessa = 1; //new SelectList(db.Commesse, "ID", "Codice");

            var model = new Models.HomeModel();
            // Lista tecnici
            var tecnici = from m in db.Tecnici
                           select m;
            model.Tecnici = tecnici.ToList();
            var elencoTecnici = new SelectList(model.Tecnici.ToList().OrderBy(m => m.ID), "ID", "Cognome");
            ViewData["Tecnici"] = elencoTecnici;
            return View();
        }

        // POST: TelaiAnagraficas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Telaio,Modello,InsertUser,InsertDate,NomeFile,IDCommessa,Annotazioni,DataIn,DataOut,NFattAttiva,DataFattAtt,ImpFattAtt,IDCarrozzeria1,IDCarrozzeria2,IDBisarchistaAndata,IDBisarchistaRitorno")] TelaiAnagrafica telaiAnagrafica, int? IDTecnico)
        {
            if (ModelState.IsValid)
            {
                
                db.TelaiAnagrafica.Add(telaiAnagrafica);
                db.SaveChanges();

                // Insert StoricoStatus
                int myID = telaiAnagrafica.ID;
                var sql = @"INSERT INTO dbo.StoricoStatus  (IDTelaio ,IDStato ,IDTecnico ,IDUtente) Values (@IDTelaio ,@IDStato ,@IDTecnico ,@IDUtente)";
                int noOfRowInserted = db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("@IDTelaio", telaiAnagrafica.ID),
                    new SqlParameter("@IDStato", "GI"),
                    new SqlParameter("@IDTecnico", IDTecnico),
                    new SqlParameter("@IDUtente", Session["UserName"]));

                // END

                return RedirectToAction("Index", new { IDCommessa = telaiAnagrafica.IDCommessa });
            }

            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaAndata);
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaRitorno);
            ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria1);
            ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria2);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", telaiAnagrafica.IDCommessa);
            return View(telaiAnagrafica);
        }

        // GET: TelaiAnagraficas/Edit/5
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
            return View(telaiAnagrafica);
        }

        // POST: TelaiAnagraficas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Telaio,Modello,InsertUser,InsertDate,NomeFile,IDCommessa,Annotazioni,DataIn,DataOut,NFattAttiva,DataFattAtt,ImpFattAtt,IDCarrozzeria1,IDCarrozzeria2,IDBisarchistaAndata,IDBisarchistaRitorno")] TelaiAnagrafica telaiAnagrafica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telaiAnagrafica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { IDCommessa = telaiAnagrafica.IDCommessa });
            }
            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaAndata);
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaRitorno);
            ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria1);
            ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria2);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Codice", telaiAnagrafica.IDCommessa);
            return View(telaiAnagrafica);
        }

        // GET: TelaiAnagraficas/Delete/5
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

        // POST: TelaiAnagraficas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CancellaStorico(id);
            
            TelaiAnagrafica telaiAnagrafica = db.TelaiAnagrafica.Find(id);
            int myIDCommessa = (int)telaiAnagrafica.IDCommessa;
            db.TelaiAnagrafica.Remove(telaiAnagrafica);
               
                db.SaveChanges();
                return RedirectToAction("Index", new { IDCommessa = myIDCommessa });

        }

        public void CancellaStorico(int IDtelaio)
        {
            var sql = @"DELETE FROM dbo.StoricoStatus  WHERE IDTelaio = @IDTelaio ";
            int noOfRowDeleted = db.Database.ExecuteSqlCommand(sql,
                new SqlParameter("@IDTelaio", IDtelaio));
        }

        public ActionResult ScattaFoto()
        {
            var model = new Models.HomeModel();
            //var rilevamentiDett = db.RilevamentiDett.Include(r => r.Rilevamenti);
            //var rilevamentiDett = from m in db.VIG_DettDannoXRilevamento
            //                      where m.Telaio == myTelaio
            //                      where m.IsVisualizzabile == true
            //                      select m;
            //model.VIG_DettDannoXRilevamento = rilevamentiDett.ToList();

            //TelaioModello telaioModello = new TelaioModello
            //{
            //    Telaio = myTelaio,
            //    Modello = myModello,

            //    IDRilevamento = myRilevamento.ToString(),

            //};
            //ViewBag.Message = telaioModello;

            //ViewBag.ItemsCounter = rilevamentiDett.Count();
            //ViewBag.Telaio = myTelaio;
            //ViewBag.Modello = myModello;
            //ViewBag.Rilevamento = myRilevamento;
            //ViewBag.IDRilevamentoDett = myIDRilevamentoDett;
            //ViewBag.myStatus = myStatus;
            //ViewBag.IDComponente = IDComponente;
            //ViewBag.IDTipoDanno = IDTipoDanno;
            //ViewBag.IsPSA = IsPSA;

            //var myFoto = from f in db.FotoXTelaio
            //             where f.Telaio == myTelaio
            //             select f;
            //model.FotoXTelaio = myFoto.ToList();
            //Session["IDRilevamento"] = myRilevamento;
            return View("ScattaFoto");
            //return View("ScattaFoto", myFoto);
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
