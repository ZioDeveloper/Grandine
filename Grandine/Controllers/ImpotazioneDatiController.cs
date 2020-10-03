using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grandine.Controllers
{
    public class ImpotazioneDatiController : Controller
    {
        // GET: ImpotazioneDati
        public ActionResult Index()
        {
            return View();
        }

        public void DownloadTemplate()
        {

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=delete.png");
            Response.TransmitFile(Server.MapPath("~/img/delete.png"));
            Response.End();
        }
    }
}