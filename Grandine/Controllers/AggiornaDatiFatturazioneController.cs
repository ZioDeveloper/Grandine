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
    public class AggiornaDatiFatturazioneController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();
        // GET: AggiornaDatiFatturazione
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

        public void DownloadTemplate()
        {

            Response.ContentType = "App_Data/csv";
            Response.AppendHeader("Content-Disposition", "attachment; filename=TemplateFatture.csv");
            Response.TransmitFile(Server.MapPath("~/App_Data/TemplateFatture.csv"));
            Response.End();
        }

        public ActionResult UploadData(IEnumerable<HttpPostedFileBase> files, string IDCommessa)
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
                dt.Columns.AddRange(new DataColumn[5] {
                    new DataColumn("Telaio", typeof(string)),
                    new DataColumn("Targa", typeof(string)),
                    new DataColumn("Importo", typeof(string)),
                    new DataColumn("DataFattura",typeof(string)),
                    new DataColumn("NFattura",typeof(string)) });

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
                TempData["Result"] = "OK";
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
            string Importo = "";
            string DataFattura = "";
            string NFattura = "";
            DateTime myDataFattura = new DateTime();
            int noOfRowUpdated = 0;
            var sql = "";
            //    conn.Open();
            foreach (DataRow importRow in imported_data.Rows)
            {
                try
                {
                    Telaio = importRow["Telaio"].ToString();
                    Importo = importRow["Importo"].ToString();
                    DataFattura = importRow["DataFattura"].ToString();
                    NFattura = importRow["NFattura"].ToString();
                }
                catch (Exception exc)
                {
                    return false;
                    string mess = exc.Message;
                }
                try
                {
                    myDataFattura = DateTime.ParseExact(DataFattura, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

                    float myNumber = float.Parse(Importo);

                    sql = @"UPDATE TelaiAnagrafica " +
                           " SET DataFatturaPassiva = @DataFatturaPassiva ," +
                           "     ImportoFattPass = @ImportoFattPass ," +
                           "     NumFattTecnico = @NumFattTecnico " +
                           " WHERE Telaio = @Telaio OR Targa = @Targa";
                    noOfRowUpdated = db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@Telaio", Telaio),
                        new SqlParameter("@Targa", Telaio),
                        new SqlParameter("@DataFatturaPassiva", myDataFattura),
                        new SqlParameter("@ImportoFattPass", myNumber),
                        new SqlParameter("@NumFattTecnico", NFattura));

                }
                catch (Exception exc)
                {
                    string mess = exc.Message;
                    return false;
                    
                }
               


            }


            return true;
        }

        public ActionResult VerifyData(IEnumerable<HttpPostedFileBase> files, string IDCommessa)
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
                dt.Columns.AddRange(new DataColumn[5] {
                    new DataColumn("Telaio", typeof(string)),
                    new DataColumn("Targa", typeof(string)),
                    new DataColumn("Importo", typeof(string)),
                    new DataColumn("DataFattura",typeof(string)),
                    new DataColumn("NFattura",typeof(string)) });

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


                    IsCompleted = ContolImportData(importedData, IDCommessa);
                }

            }
            //return View("Index");
            if (IsCompleted)
            {
                TempData["Message"] = "Dati corretti , procedere con importazione";
                TempData["Result"] = "OK";
                //return Content("<script language='javascript' type='text/javascript'>alert('Thanks for Feedback!');</script>");
                return RedirectToAction("Index", "Messaggi");
            }
            else
            {
                TempData["Message"] = "Errore nei dati !";
                TempData["Result"] = "KO";
                return RedirectToAction("Index", "Messaggi");
            }
        }

        public bool ContolImportData(DataTable imported_data, string IDCommessa)
        {
 
            string Telaio = "";
            string Importo = "";
            string DataFattura = "";
            string NFattura = "";
            DateTime myDataFattura = new DateTime();
            int noOfRowUpdated = 0;
            var sql = "";
            bool result = false;
            //    conn.Open();
            foreach (DataRow importRow in imported_data.Rows)
            {
                try
                {
                    Telaio = importRow["Telaio"].ToString();
                    Importo = importRow["Importo"].ToString();
                    DataFattura = importRow["DataFattura"].ToString();
                    NFattura = importRow["NFattura"].ToString();
                }
                catch (Exception exc)
                {
                    return false;
                    string mess = exc.Message;
                }
                try
                {
                    myDataFattura = DateTime.ParseExact(DataFattura, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);

                    float myNumber = float.Parse(Importo);

                    var cnt = (from s in db.TelaiAnagrafica
                                      where s.Telaio.ToString() == Telaio ||
                                            s.Targa.ToString() == Telaio
                               select s.ID).FirstOrDefault();
                    if (cnt > 0)
                        result = true;
                    else
                    {
                        result = false;
                        return result;
                    }
                }
                catch (Exception exc)
                {
                    string mess = exc.Message;
                    return false;

                }



            }


            return result;
        }

    }
   
}