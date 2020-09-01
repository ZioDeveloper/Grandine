using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
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

            //var telaiAnagrafica = db.TelaiAnagrafica.Include
            //        (t => t.Bisarchista).Include
            //        (t => t.Bisarchista1).Include
            //        (t => t.Carrozzeria).Include
            //        (t => t.Carrozzeria1).Include
            //        (t => t.Commesse).Where(t=>t.IDCommessa == myIDCommessa).Include
            //        (t=>t.StoricoStatus);

            // Ultima usata prima di LINQ
            //var telaiAnagrafica = db.Telai_LastStatus_vw.Where(t => t.IDCommessa == myIDCommessa);

            var model = new Models.HomeModel();
            
            var telaiAnagrafica = from m in db.Telai_LastStatus_vw
                          where m.IDCommessa.ToString() == myIDCommessa.ToString()
                          select m;
            model.Telai_LastStatus_vw = telaiAnagrafica.ToList();
            ViewBag.ClasseUtente = Session["Classeutente"].ToString();
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
            //ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr");
            //ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr");
            //ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale");
            //ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale");
            ////ViewBag.IDCommessa = 1; //new SelectList(db.Commesse, "ID", "Codice");

            //var model = new Models.HomeModel();
            //// Lista tecnici
            //var tecnici = from m in db.Tecnici
            //               select m;
            //model.Tecnici = tecnici.ToList();
            //var elencoTecnici = new SelectList(model.Tecnici.ToList().OrderBy(m => m.ID), "ID", "Cognome");
            //ViewData["Tecnici"] = elencoTecnici;
            //return View();

            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr");
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr");
            ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale");
            ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale");
            //ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Descrizione");
            ViewBag.IDCommessa = new SelectList(db.Commesse.ToList().Where(m=>m.ID == IDCommessa), "ID", "Descrizione");
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Cognome");
            ViewBag.IDCarGlass = new SelectList(db.Carglass, "ID", "RagioneSociale");

           

            return View();
        }

        // GET: TelaiAnagraficas/Create
        public ActionResult CreateNew(int? IDCommessa)
        {
            //ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr");
            //ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr");
            //ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale");
            //ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale");
            ////ViewBag.IDCommessa = 1; //new SelectList(db.Commesse, "ID", "Codice");

            //var model = new Models.HomeModel();
            //// Lista tecnici
            //var tecnici = from m in db.Tecnici
            //               select m;
            //model.Tecnici = tecnici.ToList();
            //var elencoTecnici = new SelectList(model.Tecnici.ToList().OrderBy(m => m.ID), "ID", "Cognome");
            //ViewData["Tecnici"] = elencoTecnici;
            //return View();

            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr");
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr");
            ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale");
            ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale");
            //ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Descrizione");
            ViewBag.IDCommessa = new SelectList(db.Commesse.ToList().Where(m => m.ID == IDCommessa), "ID", "Descrizione");
            //ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Cognome");
            ViewBag.IDTecnico = new SelectList(GetTecniciXCommessa(IDCommessa), "Text", "Value");
            ViewBag.IDCarGlass = new SelectList(db.Carglass, "ID", "RagioneSociale");
            return View();
        }

        // POST: TelaiAnagraficas/Create
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

                // Insert StoricoStatus
                int myID = telaiAnagrafica.ID;
                var sql = @"INSERT INTO dbo.StoricoStatus  (IDTelaio ,IDStato ,IDUtente) Values (@IDTelaio ,@IDStato  ,@IDUtente)";
                int noOfRowInserted = db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("@IDTelaio", telaiAnagrafica.ID),
                    new SqlParameter("@IDStato", "GI"),
                      new SqlParameter("@IDUtente", Session["UserName"]));

                // INSERT Ricambi
                sql = @"INSERT INTO dbo.Ricambi  (IDTelaio ) Values (@IDTelaio )";
                noOfRowInserted = db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("@IDTelaio", telaiAnagrafica.ID));

                // END

                return RedirectToAction("Index", new { IDCommessa = telaiAnagrafica.IDCommessa });
            }

            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaAndata);
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaRitorno);
            ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria1);
            ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria2);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Descrizione", telaiAnagrafica.IDCommessa);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Cognome", telaiAnagrafica.IDTecnico);
            return View(telaiAnagrafica);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNew([Bind(Include = "ID,Telaio,Modello,InsertUser,InsertDate,IDCommessa,IDTecnico,DataIn")] TelaiAnagrafica telaiAnagrafica)
        {
            if (ModelState.IsValid)
            {

                db.TelaiAnagrafica.Add(telaiAnagrafica);
                db.SaveChanges();

                // Insert StoricoStatus
                int myID = telaiAnagrafica.ID;
                var sql = @"INSERT INTO dbo.StoricoStatus  (IDTelaio ,IDStato ,IDUtente) Values (@IDTelaio ,@IDStato  ,@IDUtente)";
                int noOfRowInserted = db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("@IDTelaio", telaiAnagrafica.ID),
                    new SqlParameter("@IDStato", "GI"),
                      new SqlParameter("@IDUtente", Session["UserName"]));

                // INSERT Ricambi
                sql = @"INSERT INTO dbo.Ricambi  (IDTelaio ) Values (@IDTelaio )";
                noOfRowInserted = db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("@IDTelaio", telaiAnagrafica.ID));

                // END

                return RedirectToAction("Index", new { IDCommessa = telaiAnagrafica.IDCommessa });
            }

            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaAndata);
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaRitorno);
            ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria1);
            ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria2);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Descrizione", telaiAnagrafica.IDCommessa);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Cognome", telaiAnagrafica.IDTecnico);
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

            float Totale = 0;
            float Fatturato = (float)(telaiAnagrafica.ImpFattAtt ?? 0);
            

            float CostoAndata = (float)(telaiAnagrafica.CostoAndata ?? 0);
            float CostoRitorno = (float)(telaiAnagrafica.CostoRitorno ?? 0);
            float ImportoFattPass = (float)(telaiAnagrafica.ImportoFattPass ?? 0);
            float ImportoCarrozzeria1 = (float)(telaiAnagrafica.ImportoCarrozzeria1 ?? 0);
            float ImportoCarrozzeria2 = (float)(telaiAnagrafica.ImportoCarrozzeria2 ?? 0);
            float ImportoFattCarGlass = (float)(telaiAnagrafica.ImportoFattCarGlass ?? 0);
            float ImportoCarrozzeria3 = (float)(telaiAnagrafica.ImportoCarrozzeria3 ?? 0);
            Totale = CostoAndata + CostoRitorno + ImportoFattPass + ImportoCarrozzeria1 + ImportoCarrozzeria2 + ImportoCarrozzeria3;
            float Differenza = Fatturato - Totale;
            ViewBag.Totale = Totale.ToString();
            ViewBag.Fatturato = Fatturato;
            ViewBag.Differenza = Differenza;

            
            var tmp =(from m in db.Telai_LastStatus_vw
                    where m.ID == id
                    select m.LastStatus).FirstOrDefault();
            ViewBag.LastStatus = tmp;


            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaAndata);
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaRitorno);
           // ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria1);
           // ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria2);
           // ViewBag.IDCarrozzeria3 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria3);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Descrizione", telaiAnagrafica.IDCommessa);

           // ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Cognome", telaiAnagrafica.IDTecnico);

            ViewBag.IDTecnico = new SelectList(GetTecniciXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value", telaiAnagrafica.IDTecnico);
            ViewBag.IDCarrozzeria1 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value", telaiAnagrafica.IDCarrozzeria1);
            ViewBag.IDCarrozzeria2 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value", telaiAnagrafica.IDCarrozzeria2);
            ViewBag.IDCarrozzeria3 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value", telaiAnagrafica.IDCarrozzeria3);

            ViewBag.IDCarGlass = new SelectList(db.Carglass, "ID", "RagioneSociale");

            ViewBag.IDGravita = new SelectList(db.Gravita, "ID", "Descr", telaiAnagrafica.IDGravita);

            ViewBag.IDStatus = new SelectList(db.Status, "ID", "Descr");

            return View(telaiAnagrafica);
        }

        // POST: TelaiAnagraficas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = " ID,Telaio,IDCommessa,Modello,InsertUser,InsertDate,NomeFile,Annotazioni,DataIn,DataOut,NFattAttiva,DataFattAtt, " +
                                                 " ImpFattAtt,IDTecnico,NumFattTecnico,DataFatturaPassiva,ImportoFattPass,IDCarrozzeria1,NumFattCarrozzeria1, " +
                                                 " DataFatturaCarrozzeria1,ImportoCarrozzeria1,IDCarrozzeria2,NumFattCarrozzeria2,DataFatturaCarrozzeria2, " +
                                                 " ImportoCarrozzeria2,IDCarrozzeria3,NumFattCarrozzeria3,DataFatturaCarrozzeria3,ImportoCarrozzeria3,IDBisarchistaAndata, " +
                                                 "  NumFattBisarchistaA,DataFattBisarchistaA,CostoAndata,IDBisarchistaRitorno,NumFattBisarchistaR, " +
                                                 "  DataFattBisarchistaR,CostoRitorno,Costi, Chiave, Fila,IDGravita,Targa,IsUrgente")] TelaiAnagrafica telaiAnagrafica,string IDStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telaiAnagrafica).State = EntityState.Modified;
                db.SaveChanges();

                // Insert StoricoStatus
                if (IDStatus != "**")
                {
                    int myID = telaiAnagrafica.ID;
                    var sql = @"INSERT INTO dbo.StoricoStatus  (IDTelaio ,IDStato ,IDUtente) Values (@IDTelaio ,@IDStato  ,@IDUtente)";
                    int noOfRowInserted = db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@IDTelaio", telaiAnagrafica.ID),
                        new SqlParameter("@IDStato", IDStatus.ToString()),
                          new SqlParameter("@IDUtente", Session["UserName"]));
                }

                return RedirectToAction("Edit", new { id = telaiAnagrafica.ID });
                return RedirectToAction("Index", new { IDCommessa = telaiAnagrafica.IDCommessa });
            }
            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaAndata);
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaRitorno);
            //ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria1);
            //ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria2);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Descrizione", telaiAnagrafica.IDCommessa);
            // ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Cognome", telaiAnagrafica.IDTecnico);
            ViewBag.IDTecnico = new SelectList(GetTecniciXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value");
            ViewBag.IDCarrozzeria1 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value");
            ViewBag.IDCarrozzeria2 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value");
            ViewBag.IDCarrozzeria3 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value");
            ViewBag.IDGravita = new SelectList(db.Gravita, "ID", "Descr", telaiAnagrafica.Gravita);

            float Totale = 0;
            float Fatturato = (float)(telaiAnagrafica.ImpFattAtt ?? 0);
            

            float CostoAndata = (float)(telaiAnagrafica.CostoAndata ?? 0);
            float CostoRitorno = (float)(telaiAnagrafica.CostoRitorno ?? 0);
            float ImportoFattPass = (float)(telaiAnagrafica.ImportoFattPass ?? 0);
            float ImportoCarrozzeria1 = (float)(telaiAnagrafica.ImportoCarrozzeria1 ?? 0);
            float ImportoCarrozzeria2 = (float)(telaiAnagrafica.ImportoCarrozzeria2 ?? 0);
            float ImportoFattCarGlass = (float)(telaiAnagrafica.ImportoFattCarGlass ?? 0);
            Totale = CostoAndata + CostoRitorno + ImportoFattPass + ImportoCarrozzeria1 + ImportoCarrozzeria2 + ImportoFattCarGlass;
            float Differenza = Fatturato - Totale;
            ViewBag.Totale = Totale.ToString();
            ViewBag.Fatturato = Fatturato;
            ViewBag.Differenza = Differenza;


            return View(telaiAnagrafica);
        }

        public ActionResult EditTecnico(int? id)
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

            float Totale = 0;
            float Fatturato = (float)(telaiAnagrafica.ImpFattAtt ?? 0);


            float CostoAndata = (float)(telaiAnagrafica.CostoAndata ?? 0);
            float CostoRitorno = (float)(telaiAnagrafica.CostoRitorno ?? 0);
            float ImportoFattPass = (float)(telaiAnagrafica.ImportoFattPass ?? 0);
            float ImportoCarrozzeria1 = (float)(telaiAnagrafica.ImportoCarrozzeria1 ?? 0);
            float ImportoCarrozzeria2 = (float)(telaiAnagrafica.ImportoCarrozzeria2 ?? 0);
            float ImportoFattCarGlass = (float)(telaiAnagrafica.ImportoFattCarGlass ?? 0);
            float ImportoCarrozzeria3 = (float)(telaiAnagrafica.ImportoCarrozzeria3 ?? 0);
            Totale = CostoAndata + CostoRitorno + ImportoFattPass + ImportoCarrozzeria1 + ImportoCarrozzeria2 + ImportoCarrozzeria3;
            float Differenza = Fatturato - Totale;
            ViewBag.Totale = Totale.ToString();
            ViewBag.Fatturato = Fatturato;
            ViewBag.Differenza = Differenza;


            var tmp = (from m in db.Telai_LastStatus_vw
                       where m.ID == id
                       select m.LastStatus).FirstOrDefault();
            ViewBag.LastStatus = tmp;


            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaAndata);
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaRitorno);
            // ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria1);
            // ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria2);
            // ViewBag.IDCarrozzeria3 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria3);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Descrizione", telaiAnagrafica.IDCommessa);

            // ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Cognome", telaiAnagrafica.IDTecnico);

            ViewBag.IDTecnico = new SelectList(GetTecniciXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value", telaiAnagrafica.IDTecnico);
            ViewBag.IDCarrozzeria1 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value", telaiAnagrafica.IDCarrozzeria1);
            ViewBag.IDCarrozzeria2 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value", telaiAnagrafica.IDCarrozzeria2);
            ViewBag.IDCarrozzeria3 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value", telaiAnagrafica.IDCarrozzeria3);

            ViewBag.IDCarGlass = new SelectList(db.Carglass, "ID", "RagioneSociale");

            ViewBag.IDGravita = new SelectList(db.Gravita, "ID", "Descr", telaiAnagrafica.IDGravita);

            ViewBag.IDStatus = new SelectList(db.Status, "ID", "Descr");

            return View(telaiAnagrafica);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTecnico([Bind(Include = " ID,Telaio,IDCommessa,Modello,InsertUser,InsertDate,NomeFile,Annotazioni,DataIn,DataOut,NFattAttiva,DataFattAtt, " +
                                                 " ImpFattAtt,IDTecnico,NumFattTecnico,DataFatturaPassiva,ImportoFattPass,IDCarrozzeria1,NumFattCarrozzeria1, " +
                                                 " DataFatturaCarrozzeria1,ImportoCarrozzeria1,IDCarrozzeria2,NumFattCarrozzeria2,DataFatturaCarrozzeria2, " +
                                                 " ImportoCarrozzeria2,IDCarrozzeria3,NumFattCarrozzeria3,DataFatturaCarrozzeria3,ImportoCarrozzeria3,IDBisarchistaAndata, " +
                                                 "  NumFattBisarchistaA,DataFattBisarchistaA,CostoAndata,IDBisarchistaRitorno,NumFattBisarchistaR, " +
                                                 "  DataFattBisarchistaR,CostoRitorno,Costi, Chiave, Fila,IDGravita,Targa,IsUrgente")] TelaiAnagrafica telaiAnagrafica, string IDStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telaiAnagrafica).State = EntityState.Modified;
                db.SaveChanges();

                // Insert StoricoStatus
                //if (IDStatus != "**" && !String.IsNullOrEmpty(IDStatus.ToString()))
                //{
                //    int myID = telaiAnagrafica.ID;
                //    var sql = @"INSERT INTO dbo.StoricoStatus  (IDTelaio ,IDStato ,IDUtente) Values (@IDTelaio ,@IDStato  ,@IDUtente)";
                //    int noOfRowInserted = db.Database.ExecuteSqlCommand(sql,
                //        new SqlParameter("@IDTelaio", telaiAnagrafica.ID),
                //        new SqlParameter("@IDStato", IDStatus.ToString()),
                //          new SqlParameter("@IDUtente", Session["UserName"]));
                //}

                return RedirectToAction("EditTecnico", new { id = telaiAnagrafica.ID });
                return RedirectToAction("Index", new { IDCommessa = telaiAnagrafica.IDCommessa });
            }
            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaAndata);
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaRitorno);
            //ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria1);
            //ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria2);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Descrizione", telaiAnagrafica.IDCommessa);
            // ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Cognome", telaiAnagrafica.IDTecnico);
            ViewBag.IDTecnico = new SelectList(GetTecniciXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value");
            ViewBag.IDCarrozzeria1 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value");
            ViewBag.IDCarrozzeria2 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value");
            ViewBag.IDCarrozzeria3 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value");
            ViewBag.IDGravita = new SelectList(db.Gravita, "ID", "Descr", telaiAnagrafica.Gravita);

            float Totale = 0;
            float Fatturato = (float)(telaiAnagrafica.ImpFattAtt ?? 0);


            float CostoAndata = (float)(telaiAnagrafica.CostoAndata ?? 0);
            float CostoRitorno = (float)(telaiAnagrafica.CostoRitorno ?? 0);
            float ImportoFattPass = (float)(telaiAnagrafica.ImportoFattPass ?? 0);
            float ImportoCarrozzeria1 = (float)(telaiAnagrafica.ImportoCarrozzeria1 ?? 0);
            float ImportoCarrozzeria2 = (float)(telaiAnagrafica.ImportoCarrozzeria2 ?? 0);
            float ImportoFattCarGlass = (float)(telaiAnagrafica.ImportoFattCarGlass ?? 0);
            Totale = CostoAndata + CostoRitorno + ImportoFattPass + ImportoCarrozzeria1 + ImportoCarrozzeria2 + ImportoFattCarGlass;
            float Differenza = Fatturato - Totale;
            ViewBag.Totale = Totale.ToString();
            ViewBag.Fatturato = Fatturato;
            ViewBag.Differenza = Differenza;


            return View(telaiAnagrafica);
        }

        public ActionResult EditController(int? id)
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

            float Totale = 0;
            float Fatturato = (float)(telaiAnagrafica.ImpFattAtt ?? 0);


            float CostoAndata = (float)(telaiAnagrafica.CostoAndata ?? 0);
            float CostoRitorno = (float)(telaiAnagrafica.CostoRitorno ?? 0);
            float ImportoFattPass = (float)(telaiAnagrafica.ImportoFattPass ?? 0);
            float ImportoCarrozzeria1 = (float)(telaiAnagrafica.ImportoCarrozzeria1 ?? 0);
            float ImportoCarrozzeria2 = (float)(telaiAnagrafica.ImportoCarrozzeria2 ?? 0);
            float ImportoFattCarGlass = (float)(telaiAnagrafica.ImportoFattCarGlass ?? 0);
            float ImportoCarrozzeria3 = (float)(telaiAnagrafica.ImportoCarrozzeria3 ?? 0);
            Totale = CostoAndata + CostoRitorno + ImportoFattPass + ImportoCarrozzeria1 + ImportoCarrozzeria2 + ImportoCarrozzeria3;
            float Differenza = Fatturato - Totale;
            ViewBag.Totale = Totale.ToString();
            ViewBag.Fatturato = Fatturato;
            ViewBag.Differenza = Differenza;


            var tmp = (from m in db.Telai_LastStatus_vw
                       where m.ID == id
                       select m.LastStatus).FirstOrDefault();
            ViewBag.LastStatus = tmp;


            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaAndata);
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaRitorno);
            // ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria1);
            // ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria2);
            // ViewBag.IDCarrozzeria3 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria3);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Descrizione", telaiAnagrafica.IDCommessa);

            // ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Cognome", telaiAnagrafica.IDTecnico);

            ViewBag.IDTecnico = new SelectList(GetTecniciXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value", telaiAnagrafica.IDTecnico);
            ViewBag.IDCarrozzeria1 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value", telaiAnagrafica.IDCarrozzeria1);
            ViewBag.IDCarrozzeria2 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value", telaiAnagrafica.IDCarrozzeria2);
            ViewBag.IDCarrozzeria3 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value", telaiAnagrafica.IDCarrozzeria3);

            ViewBag.IDCarGlass = new SelectList(db.Carglass, "ID", "RagioneSociale");

            ViewBag.IDGravita = new SelectList(db.Gravita, "ID", "Descr", telaiAnagrafica.IDGravita);

            ViewBag.IDStatus = new SelectList(db.Status, "ID", "Descr");

            return View(telaiAnagrafica);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditController([Bind(Include = " ID,Telaio,IDCommessa,Modello,InsertUser,InsertDate,NomeFile,Annotazioni,DataIn,DataOut,NFattAttiva,DataFattAtt, " +
                                                 " ImpFattAtt,IDTecnico,NumFattTecnico,DataFatturaPassiva,ImportoFattPass,IDCarrozzeria1,NumFattCarrozzeria1, " +
                                                 " DataFatturaCarrozzeria1,ImportoCarrozzeria1,IDCarrozzeria2,NumFattCarrozzeria2,DataFatturaCarrozzeria2, " +
                                                 " ImportoCarrozzeria2,IDCarrozzeria3,NumFattCarrozzeria3,DataFatturaCarrozzeria3,ImportoCarrozzeria3,IDBisarchistaAndata, " +
                                                 "  NumFattBisarchistaA,DataFattBisarchistaA,CostoAndata,IDBisarchistaRitorno,NumFattBisarchistaR, " +
                                                 "  DataFattBisarchistaR,CostoRitorno,Costi, Chiave, Fila,IDGravita,Targa,IsUrgente")] TelaiAnagrafica telaiAnagrafica, string IDStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telaiAnagrafica).State = EntityState.Modified;
                db.SaveChanges();

                // Insert StoricoStatus
                //if (IDStatus != "**" && !String.IsNullOrEmpty(IDStatus.ToString()))
                //{
                //    int myID = telaiAnagrafica.ID;
                //    var sql = @"INSERT INTO dbo.StoricoStatus  (IDTelaio ,IDStato ,IDUtente) Values (@IDTelaio ,@IDStato  ,@IDUtente)";
                //    int noOfRowInserted = db.Database.ExecuteSqlCommand(sql,
                //        new SqlParameter("@IDTelaio", telaiAnagrafica.ID),
                //        new SqlParameter("@IDStato", IDStatus.ToString()),
                //          new SqlParameter("@IDUtente", Session["UserName"]));
                //}

                return RedirectToAction("EditController", new { id = telaiAnagrafica.ID });
                return RedirectToAction("Index", new { IDCommessa = telaiAnagrafica.IDCommessa });
            }
            ViewBag.IDBisarchistaAndata = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaAndata);
            ViewBag.IDBisarchistaRitorno = new SelectList(db.Bisarchista, "ID", "Descr", telaiAnagrafica.IDBisarchistaRitorno);
            //ViewBag.IDCarrozzeria1 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria1);
            //ViewBag.IDCarrozzeria2 = new SelectList(db.Carrozzeria, "ID", "RagioneSociale", telaiAnagrafica.IDCarrozzeria2);
            ViewBag.IDCommessa = new SelectList(db.Commesse, "ID", "Descrizione", telaiAnagrafica.IDCommessa);
            // ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Cognome", telaiAnagrafica.IDTecnico);
            ViewBag.IDTecnico = new SelectList(GetTecniciXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value");
            ViewBag.IDCarrozzeria1 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value");
            ViewBag.IDCarrozzeria2 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value");
            ViewBag.IDCarrozzeria3 = new SelectList(GetCarrozzeriaXCommessa(telaiAnagrafica.IDCommessa), "Text", "Value");
            ViewBag.IDGravita = new SelectList(db.Gravita, "ID", "Descr", telaiAnagrafica.Gravita);

            float Totale = 0;
            float Fatturato = (float)(telaiAnagrafica.ImpFattAtt ?? 0);


            float CostoAndata = (float)(telaiAnagrafica.CostoAndata ?? 0);
            float CostoRitorno = (float)(telaiAnagrafica.CostoRitorno ?? 0);
            float ImportoFattPass = (float)(telaiAnagrafica.ImportoFattPass ?? 0);
            float ImportoCarrozzeria1 = (float)(telaiAnagrafica.ImportoCarrozzeria1 ?? 0);
            float ImportoCarrozzeria2 = (float)(telaiAnagrafica.ImportoCarrozzeria2 ?? 0);
            float ImportoFattCarGlass = (float)(telaiAnagrafica.ImportoFattCarGlass ?? 0);
            Totale = CostoAndata + CostoRitorno + ImportoFattPass + ImportoCarrozzeria1 + ImportoCarrozzeria2 + ImportoFattCarGlass;
            float Differenza = Fatturato - Totale;
            ViewBag.Totale = Totale.ToString();
            ViewBag.Fatturato = Fatturato;
            ViewBag.Differenza = Differenza;


            return View(telaiAnagrafica);
        }

        public IEnumerable<SelectListItem> GetTecniciXCommessa(int? aIDCommessa)
        {
            IEnumerable<SelectListItem> list = null;

            
                var query = (from t in db.Tecnici
                             join c in db.CommesseXTecnici on t.ID equals c.IDTecnico
                             where c.IDCommessa == aIDCommessa
                             select new SelectListItem { Text = t.ID.ToString(), Value = t.Cognome });
                list = query.ToList();

            return list;
        }

        public IEnumerable<SelectListItem> GetCarrozzeriaXCommessa(int? aIDCommessa)
        {
            IEnumerable<SelectListItem> list = null;


            var query = (from carr in db.Carrozzeria
                         join comm in db.CommesseXCarrozzerie on carr.ID equals comm.IDCarrozzeria
                         where comm.IDCommessa == aIDCommessa
                         select new SelectListItem { Text = carr.ID.ToString(), Value = carr.RagioneSociale });
            list = query.ToList();

            return list;
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
            CancellaDocumenti(id);
            CancellaRicambi(id);

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

        public void CancellaDocumenti(int IDtelaio)
        {
            var sql = @"DELETE FROM dbo.FotoXTelaio  WHERE IDTelaio = @IDTelaio ";
            int noOfRowDeleted = db.Database.ExecuteSqlCommand(sql,
                new SqlParameter("@IDTelaio", IDtelaio));
        }
        public void CancellaRicambi(int IDtelaio)
        {
            var sql = @"DELETE FROM dbo.Ricambi  WHERE IDTelaio = @IDTelaio ";
            int noOfRowDeleted = db.Database.ExecuteSqlCommand(sql,
                new SqlParameter("@IDTelaio", IDtelaio));
        }


        public ActionResult ScattaFoto(int? IDTelaio,int? IDTipoDocumento)
        {

            
            var model = new Models.HomeModel();

            // Lista tipidocumento
            var tipidoc = from m in db.TipiDocumento
                          select m;
            model.TipiDocumento = tipidoc.ToList();
            var elencotipidocumento = new SelectList(model.TipiDocumento.ToList().OrderBy(m => m.ID), "ID", "TipoDocumento");
            ViewData["TipiDocumento"] = elencotipidocumento;

            var myFoto = (from f in db.FotoXTelaio_vw
                         where f.IDTelaio == IDTelaio
                         select f);
            model.FotoXTelaio_vw = myFoto.ToList();


            if (IDTipoDocumento != null)
                ViewBag.IDTipoDocumento = IDTipoDocumento;
            else
                ViewBag.IDTipoDocumento = 7;

            ViewBag.IDTelaio = IDTelaio;
            return View("ScattaFoto", myFoto);
           
        }
        
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files , int? IDTelaio ,int? IDTipoDocumento)
        {
            string filename = "";
            string path = "";
            //int myIDTelaio = 0;

            //string mySearch = TempData["mySearch"] as string;
            //string myLotto = TempData["myLotto"] as string;
            //myIDTelaio = (int)TempData["myIDTelaio"];

           

            foreach (var file in files)
            {
                if (file != null)
                {
                    filename = System.IO.Path.GetFileName(file.FileName);
                    
                    path = System.IO.Path.Combine(Server.MapPath("~/DocumentiXTelai"), filename);
                    if (file != null)
                    {
                        file.SaveAs(path);
                    }

                    var sql = @"Insert Into FotoXTelaio (IDTelaio, IDTipoDocumento , NomeFile) Values (@IDTelaio, @IDTipoDocumento, @NomeFile)";
                    int noOfRowInserted = db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@IDTelaio", IDTelaio),
                        new SqlParameter("@IDTipoDocumento", IDTipoDocumento),
                        new SqlParameter("@NomeFile", filename));
                }
            }
            
            var model = new Models.HomeModel();
            
            // Lista tipidocumento
            var tipidoc = from m in db.TipiDocumento
                          select m;
            model.TipiDocumento = tipidoc.ToList();
            var elencotipidocumento = new SelectList(model.TipiDocumento.ToList().OrderBy(m => m.ID), "ID", "TipoDocumento");
            ViewData["TipiDocumento"] = elencotipidocumento;

            var myFoto = (from f in db.FotoXTelaio_vw
                          where f.IDTelaio == IDTelaio
                          select f);
            model.FotoXTelaio_vw = myFoto.ToList();
            UpdateModel(myFoto);

            ViewBag.IDTelaio = IDTelaio;
            ViewBag.IDTipoDocumento = IDTipoDocumento;
            return View("ScattaFoto", myFoto);

            //return RedirectToAction("ScattaFoto", new { IDTelaio, IDTipoDocumento });


        }

        public ActionResult Reload(int? IDTelaio, int? IDTipoDocumento)
        {
            var model = new Models.HomeModel();

            // Lista tipidocumento
            var tipidoc = from m in db.TipiDocumento
                          select m;
            model.TipiDocumento = tipidoc.ToList();
            var elencotipidocumento = new SelectList(model.TipiDocumento.ToList().OrderBy(m => m.ID), "ID", "TipoDocumento");
            ViewData["TipiDocumento"] = elencotipidocumento;

            var myFoto = (from f in db.FotoXTelaio_vw
                          where f.IDTelaio == IDTelaio
                          select f);
            model.FotoXTelaio_vw = myFoto.ToList();


            if (IDTipoDocumento != null)
                ViewBag.IDTipoDocumento = IDTipoDocumento;
            else
                ViewBag.IDTipoDocumento = 1;

            ViewBag.IDTelaio = IDTelaio;
            return View("ScattaFoto", myFoto);

        }

        public ActionResult CancellaDocumento(int? IDDocumento, int? IDTelaio, string nomefile, int?IDTipoDocumento)
        {
            var sql = @"DELETE FROM FotoXTelaio WHERE ID = @IDDocumento";
            int myRecordCounter = db.Database.ExecuteSqlCommand(sql, new SqlParameter("@IDDocumento", IDDocumento));

            string fullPath = Request.MapPath("~/DocumentiXTelai/" + nomefile);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            var model = new Models.HomeModel();

            // Lista tipidocumento
            var tipidoc = from m in db.TipiDocumento
                          select m;
            model.TipiDocumento = tipidoc.ToList();
            var elencotipidocumento = new SelectList(model.TipiDocumento.ToList().OrderBy(m => m.ID), "ID", "TipoDocumento");
            ViewData["TipiDocumento"] = elencotipidocumento;

            var myFoto = (from f in db.FotoXTelaio_vw
                          where f.IDTelaio == IDTelaio
                          select f);
            model.FotoXTelaio_vw = myFoto.ToList();

            ViewBag.IDTelaio = IDTelaio;
            ViewBag.IDTipoDocumento = IDTipoDocumento;
            return View("ScattaFoto", myFoto);
        }

        public ActionResult DragAndDrop(int? IDTelaio, int? IDTipoDocumento)
        {

            if(IDTipoDocumento == 1)
            {
                
                return RedirectToAction("ScattaFoto", new { IDTelaio, IDTipoDocumento });
                
            }

            var model = new Models.HomeModel();

            // Lista tipidocumento
            var tipidoc = from m in db.TipiDocumento
                          select m;
            model.TipiDocumento = tipidoc.ToList();
            var elencotipidocumento = new SelectList(model.TipiDocumento.ToList().OrderBy(m => m.ID), "ID", "TipoDocumento");
            ViewData["TipiDocumento"] = elencotipidocumento;

            var myFoto = (from f in db.FotoXTelaio_vw
                          where f.IDTelaio == IDTelaio
                          select f);
            model.FotoXTelaio_vw = myFoto.ToList();


            if (IDTipoDocumento != null)
                ViewBag.IDTipoDocumento = IDTipoDocumento;
            else
                ViewBag.IDTipoDocumento = 1;

            ViewBag.IDTelaio = IDTelaio;
            return View("DragAndDrop", myFoto);

        }
        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files, int? IDTelaio, int? IDTipoDocumento)
        {
            string filename = "";
            string path = "";
            foreach (var file in files)
            {
                if (file != null)
                {
                    filename = System.IO.Path.GetFileName(file.FileName);

                    path = System.IO.Path.Combine(Server.MapPath("~/DocumentiXTelai"), filename);
                    if (file != null)
                    {
                        file.SaveAs(path);
                    }

                    var sql = @"Insert Into FotoXTelaio (IDTelaio, IDTipoDocumento , NomeFile) Values (@IDTelaio, @IDTipoDocumento, @NomeFile)";
                    int noOfRowInserted = db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@IDTelaio", IDTelaio),
                        new SqlParameter("@IDTipoDocumento", IDTipoDocumento),
                        new SqlParameter("@NomeFile", filename));
                }
            }

            var model = new Models.HomeModel();

            // Lista tipidocumento
            var tipidoc = from m in db.TipiDocumento
                          select m;
            model.TipiDocumento = tipidoc.ToList();
            var elencotipidocumento = new SelectList(model.TipiDocumento.ToList().OrderBy(m => m.ID), "ID", "TipoDocumento");
            ViewData["TipiDocumento"] = elencotipidocumento;

            var myFoto = (from f in db.FotoXTelaio_vw
                          where f.IDTelaio == IDTelaio
                          select f);
            model.FotoXTelaio_vw = myFoto.ToList();
            UpdateModel(myFoto);

            ViewBag.IDTelaio = IDTelaio;
            ViewBag.IDTipoDocumento = IDTipoDocumento;
            return View("ScattaFoto", myFoto);
        }

        public ActionResult InsertStatus(int? IDTelaio)
        {


            ViewBag.IDStato = new SelectList(db.Status, "ID", "Descr");
            //ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice");
            //ViewBag.IDUtente = new SelectList(db.Utenti, "ID", "Nome");
            ViewBag.IDTelaio = new SelectList(db.TelaiAnagrafica, "ID", "Telaio");

            var model = new Models.HomeModel();
            // Lista tecnici
            var tecnici = from m in db.Tecnici
                          select m;
            model.Tecnici = tecnici.ToList();
            var elencoTecnici = new SelectList(model.Tecnici.ToList().OrderBy(m => m.ID), "ID", "Cognome");
            ViewData["Tecnici"] = elencoTecnici;

            var modelstatus = new Models.HomeModel();


            var stati = from m in db.StoricoStatus
                       where m.IDTelaio == IDTelaio
                       select m;

            modelstatus.StoricoStatus = stati.ToList();
            //modelstatus.StoricoStatus = dati.ToList();

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertStatus([Bind(Include = "ID,IDTelaio,IDStato,IDTecnico")] StoricoStatus storicoStatus)
        {
            if (ModelState.IsValid)
            {
                storicoStatus.IDUtente = "C001";
                db.StoricoStatus.Add(storicoStatus);
                db.SaveChanges();
                return RedirectToAction("Index", new { IDCommessa = Session["IDCommessa"] });
            }

            ViewBag.IDStato = new SelectList(db.Status, "ID", "Descr", storicoStatus.IDStato);
           // ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice", storicoStatus.IDTecnico);
            ViewBag.IDUtente = new SelectList(db.Utenti, "ID", "Nome", storicoStatus.IDUtente);
            ViewBag.IDTelaio = new SelectList(db.TelaiAnagrafica, "ID", "Telaio", storicoStatus.IDTelaio);
            return View(storicoStatus);
            

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
