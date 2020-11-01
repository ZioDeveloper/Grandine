using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grandine.Models;
using System.IO;
using System.Data.SqlClient;

namespace Grandine.Controllers
{
    public class ImportazioneDatiController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: ImportazioneDati
        public ActionResult Index()
        {
            var model = new Models.HomeModel();
            var commesse = from co in db.Commesse
                           join m in db.Clienti on co.IDCliente equals m.ID
                           where m.IsActive == true
                           //where co.ID == 2
                           select co;
            model.Commesse = commesse.ToList();
            var elencoCommesse = new SelectList(model.Commesse.ToList().OrderBy(m => m.Descrizione), "ID", "Descrizione");

            ViewData["Commesse"] = elencoCommesse;

            return View();
        }

        public ActionResult UploadData(IEnumerable<HttpPostedFileBase> files,string IDCommessa)
        {
            string filename = "";
            string path = "";
            bool IsCompleted = false;

            foreach (var file in files)
            {

                filename = System.IO.Path.GetFileName(file.FileName);

                path = System.IO.Path.Combine(Server.MapPath("~/App_Data/CSV"), filename);
                file.SaveAs(path);
                //Create a DataTable.  
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] {
                    new DataColumn("Telaio", typeof(string)),
                    new DataColumn("Targa", typeof(string)),
                    new DataColumn("Modello", typeof(string)),
                    new DataColumn("Gravita",typeof(string)) });

                //Read the contents of CSV file.  
                string csvData = System.IO.File.ReadAllText(path);

                DataTable importedData = new DataTable();


                using (StreamReader sr = new StreamReader(path))
                {


                    string header = sr.ReadLine();
                    if (string.IsNullOrEmpty(header))
                    {

                        RedirectToAction("Index", "ImportazioneDati");
                    }



                    string[] headerColumns = header.Split(';');
                    foreach (string headerColumn in headerColumns)
                    {
                        importedData.Columns.Add(headerColumn);
                    }



                    while (!sr.EndOfStream)
                    {

                        string line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line)) continue;
                        string[] fields = line.Split(';');
                        DataRow importedRow = importedData.NewRow();

                        for (int i = 0; i < fields.Count(); i++)
                        {

                            importedRow[i] = fields[i];

                        }

                        importedData.Rows.Add(importedRow);
                    }


                    IsCompleted = SaveImportDataToDatabase(importedData, IDCommessa);
                }

            }
            //return View("Index");
            if (IsCompleted)
            {
                TempData["Message"] = "Dati importati correttamente";
                TempData["Result"] = "OK";
                //return Content("<script language='javascript' type='text/javascript'>alert('Thanks for Feedback!');</script>");
                return RedirectToAction("Index", "Messaggi");
            }
            else
            {
                TempData["Message"] = "Errore nell'importazione dei dati !";
                TempData["Result"] = "KO";
                return RedirectToAction("Index", "Messaggi");
            }
        }

        public bool SaveImportDataToDatabase(DataTable imported_data, string IDCommessa)
        {
            //string sqlstm = "";
            //int I = 0;
            //string cs = Utils.GetConnectionStringByName("WiseDB");
            //cs += "User ID=" + Utils.AppUser + ";Password=" + Utils.AppPassword;
            //using (SqlConnection conn = new SqlConnection(cs))
            //{
            string Telaio = "";
            string Targa = "";
            string Modello = "";
            string Gravita = "";
            int noOfRowInserted = 0;
            var sql = "";
            //    conn.Open();
            foreach (DataRow importRow in imported_data.Rows)
            {
                try
                {
                    Telaio = importRow["Telaio"].ToString();
                    Targa = importRow["Targa"].ToString();
                    Modello = importRow["Modello"].ToString();
                    Gravita = importRow["Gravita"].ToString();
                }
                catch (Exception exc)
                {
                    return false;
                    string mess = exc.Message;
                }
                try
                {
                     sql = @" INSERT INTO TelaiAnagrafica (Telaio, Targa, Modello, IDGravita ,IDCommessa) Values (@Telaio, @Targa, @Modello, @IDGravita, @IDCommessa)";
                    noOfRowInserted = db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@Telaio", Telaio),
                        new SqlParameter("@Targa", Targa),
                        new SqlParameter("@Modello", Modello),
                        new SqlParameter("@IDGravita", Gravita),
                        new SqlParameter("@IDCommessa", IDCommessa));
                }
                catch(Exception exc)
                {
                    return false;
                    string mess = exc.Message;
                }
                // recupero ID
                var maxid = (from m in db.TelaiAnagrafica
                             orderby m.ID descending
                             select m.ID).FirstOrDefault();
                // Insert status
                sql = " INSERT INTO StoricoStatus  (IDTelaio, IDStato, IDUtente) " +
                      "   VALUES (@IDTelaio, @IDStato, @IDUtente) ";

                noOfRowInserted = db.Database.ExecuteSqlCommand(sql,
                     new SqlParameter("@IDTelaio", maxid),
                     new SqlParameter("@IDStato", "GI"),
                     new SqlParameter("@IDUtente", Session["UserName"].ToString()));

                // Insert ricambi
                sql = " INSERT INTO Ricambi  (IDTelaio) " +
                      "   VALUES (@IDTelaio) ";

                noOfRowInserted = db.Database.ExecuteSqlCommand(sql,
                     new SqlParameter("@IDTelaio", maxid));

                
            }


            return true;
        }



        public void DownloadTemplate()
        {


            Response.ContentType = "App_Data/csv";
            Response.AppendHeader("Content-Disposition", "attachment; filename=TemplateTelai.csv");
            Response.TransmitFile(Server.MapPath("~/App_Data/TemplateTelai.csv"));
            Response.End();
        }
    }
}