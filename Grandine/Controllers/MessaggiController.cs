using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grandine.Controllers
{
    public class MessaggiController : Controller
    {
        // GET: Messaggi
        public ActionResult Index()
        {
            return View();
        }
    }
}