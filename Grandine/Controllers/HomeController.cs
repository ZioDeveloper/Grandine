using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grandine.Models;

namespace Grandine.Controllers
{
    public class HomeController : Controller
    {

        private GRANDINEEntities db = new GRANDINEEntities();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string aCodiceUtente, string aPassword)
        {
            var model = new Models.HomeModel();

            if (aCodiceUtente != null)
            {

                var myIDUtente = (from s in db.Utenti
                                  where s.ID.ToString() == aCodiceUtente
                                  where s.Password.ToString() == aPassword
                                  select s.ID).FirstOrDefault();

                if (myIDUtente != null)
                {
                    var utente = from m in db.Utenti
                                 where m.ID == myIDUtente
                                 select m;
                    model.Utenti = utente.ToList();
                    Session["UserName"] = myIDUtente;
                    Session["UserName"] = myIDUtente;
                    ViewBag.Message = "OK !";
                    return View("Logged",model);
                }
                else
                {
                    Session["UserName"] = "";
                    ViewBag.Message = "Login non riuscita";
                    ViewBag.Action = "Login";
                    ViewBag.Parameter = null;
                    return View("Error");
                }
            }
            else
            {
                ViewBag.Message = "";
                return View("Menu");
            }


        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Gestione Concessionari";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Gestione Concessionari";

            return View();
        }
    }
}