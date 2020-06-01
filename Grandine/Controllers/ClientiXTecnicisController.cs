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
    public class ClientiXTecnicisController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        

        // GET: ClientiXTecnicis
        public ActionResult Index()
        {
            
            var clientiXTecnici = db.ClientiXTecnici.Include(c => c.Clienti);
            return View(clientiXTecnici.ToList());
        }

        // GET: ClientiXTecnicis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientiXTecnici clientiXTecnici = db.ClientiXTecnici.Find(id);
            if (clientiXTecnici == null)
            {
                return HttpNotFound();
            }
            return View(clientiXTecnici);
        }

        // GET: ClientiXTecnicis/Create
        public ActionResult Create()
        {
            ViewBag.IDCliente = new SelectList(GetCliente(), "Text", "Value");
            
            ViewBag.IDTecnico = new SelectList(GetTecnico(), "Text", "Value");

            // Example !
            //ViewBag.IDComponente = new SelectList(GetComponenti(IDComponente, myCasa), "Text", "Value");

            return View();
        }

        public IEnumerable<SelectListItem> GetCliente()
        {
            IEnumerable<SelectListItem> list = null;

            var query = (from ca in db.Clienti
                            where 0 == 0
                            where ca.IsActive == true
                            select new SelectListItem { Text = ca.ID.ToString(), Value = ca.RagioneSociale.ToString() }).Distinct();
            list = query.ToList();

            


            return list;
        }

        public IEnumerable<SelectListItem> GetTecnico()
        {
            IEnumerable<SelectListItem> list = null;

            var query = (from ca in db.Tecnici
                         where 0 == 0
                         
                         select new SelectListItem { Text = ca.ID.ToString() , Value = ca.Cognome + " " + ca.Nome}).Distinct();
            list = query.ToList();




            return list;
        }

        // POST: ClientiXTecnicis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDCliente,IDTecnico")] ClientiXTecnici clientiXTecnici)
        {
            if (ModelState.IsValid)
            {
                db.ClientiXTecnici.Add(clientiXTecnici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Codice", clientiXTecnici.IDCliente);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice", clientiXTecnici.IDTecnico);
            return View(clientiXTecnici);
        }

        

        // GET: ClientiXTecnicis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientiXTecnici clientiXTecnici = db.ClientiXTecnici.Find(id);
            if (clientiXTecnici == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Codice", clientiXTecnici.IDCliente);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice", clientiXTecnici.IDTecnico);
            return View(clientiXTecnici);
        }

        // POST: ClientiXTecnicis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDCliente,IDTecnico")] ClientiXTecnici clientiXTecnici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientiXTecnici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCliente = new SelectList(db.Clienti, "ID", "Codice", clientiXTecnici.IDCliente);
            ViewBag.IDTecnico = new SelectList(db.Tecnici, "ID", "Codice", clientiXTecnici.IDTecnico);
            return View(clientiXTecnici);
        }

        // GET: ClientiXTecnicis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientiXTecnici clientiXTecnici = db.ClientiXTecnici.Find(id);
            if (clientiXTecnici == null)
            {
                return HttpNotFound();
            }
            return View(clientiXTecnici);
        }

        // POST: ClientiXTecnicis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientiXTecnici clientiXTecnici = db.ClientiXTecnici.Find(id);
            db.ClientiXTecnici.Remove(clientiXTecnici);
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
