﻿using System;
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

                    // Classe
                    var myClasse = (from m in db.Utenti
                                 where m.ID == myIDUtente
                                 select m.IDClasse).FirstOrDefault();

                    var myTipo = (from m in db.ClassiUtente
                                  where m.ID.ToString() == myClasse.ToString()
                                  select m.Classe).FirstOrDefault();

                    Session["UserName"] = myIDUtente;
                    Session["Classeutente"] = myClasse;
                    ViewBag.Message = "OK !";
                    ViewBag.DescrizioneClasse = myTipo;

                    // Conta i clienti attivi
                    var clienti = from m in db.Clienti
                                 where m.IsActive == true
                                 select m;
                    model.Clienti = clienti.ToList();

                    var elencoClienti = new SelectList(model.Clienti.ToList().Where(m=> m.IsActive== true).OrderBy(m => m.RagioneSociale) , "ID", "RagioneSociale");
                    ViewData["Clienti"] = elencoClienti;

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

        [HttpGet]
        public ActionResult LoginInUse(string  aCodiceUtente)
        {
            var model = new Models.HomeModel();

            if (aCodiceUtente != null)
            {

                var myIDUtente = (from s in db.Utenti
                                  where s.ID.ToString() == aCodiceUtente
                                  
                                  select s.ID).FirstOrDefault();

                if (myIDUtente != null)
                {
                    var utente = from m in db.Utenti
                                 where m.ID == myIDUtente
                                 select m;
                    model.Utenti = utente.ToList();

                    // Classe
                    var myClasse = (from m in db.Utenti
                                    where m.ID == myIDUtente
                                    select m.IDClasse).FirstOrDefault();

                    var myTipo = (from m in db.ClassiUtente
                                  where m.ID.ToString() == myClasse.ToString()
                                  select m.Classe).FirstOrDefault();

                    // Conta i clienti attivi
                    var clienti = from m in db.Clienti
                                  where m.IsActive == true
                                  select m;
                    model.Clienti = clienti.ToList();
                    ViewBag.DescrizioneClasse = myTipo;

                    var elencoClienti = new SelectList(model.Clienti.ToList().Where(m => m.IsActive == true).OrderBy(m => m.RagioneSociale), "ID", "RagioneSociale");
                    ViewData["Clienti"] = elencoClienti;

                    return View("Logged", model);
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