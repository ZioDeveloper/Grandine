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
    public class RicambiController : Controller
    {
        private GRANDINEEntities db = new GRANDINEEntities();

        // GET: Ricambi
        public ActionResult Index()
        {
            var ricambi = db.Ricambi.Include(r => r.TelaiAnagrafica);
            return View(ricambi.ToList());
        }

        // GET: Ricambi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ricambi ricambi = db.Ricambi.Find(id);
            if (ricambi == null)
            {
                return HttpNotFound();
            }
            return View(ricambi);
        }

        // GET: Ricambi/Create
        public ActionResult Create()
        {
            ViewBag.IDTelaio = new SelectList(db.TelaiAnagrafica, "ID", "Telaio");
            return View();
        }

        // POST: Ricambi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDTelaio,COFANO_ANT,LOGO_ANT,FANALE_ANT_SX,FRECCIA_PARAURT_ANT_SX,CATADIOTTRO_ANT_SX,FRECCIA_PARAFANGO_ANT_SX,CALANDRA_SOTTOPARABREZZA,PARABREZZA,GUARNIZIONE_PARABREZZA_SX,CORPO_SPECCHIO_SX,VETRINO_SPECCHIO_RETROVISORE_SX,CALOTTA_SX,FRECCIA_SPECCHIO_SX,CORNICE_PORTA_ANT_SX,MONTANTE_ANT_PORTA_ANT_SX,MONTANTE_POST_PORTA_ANT_SX,CORNICE_PORTA_POST_SX,MONTANTE_ANT_PORTA_POST_SX,MONTANTE_POST_PORTA_POST_SX,CORNICE_PARAFANGO_SX,RASAVETRO_ANT_SX,RASAVETRO_POST_SX,CORRIMANO_SX,BASE_ANTENNA,TERZO_STOP,CORNICE_COFANO_POST,FANALE_POST_PARAFANGO_SX,COFANO_POST,LOGO_POST,LUNOTTO,CAPPELLIERA,SPORTELLO_CARBURANTE,CORRIMANO_DX,RASAVETRO_POST_DX,RASAVETRO_ANT_DX,CORNICE_PARAFANGO_DX,CORNICE_PORTA_POST_DX,MONTANTE_ANT_PORTA_POST_DX,MONTANTE_POST_PORTA_POST_DX,CORNICE_PORTA_ANT_DX,MONTANTE_ANT_PORTA_ANT_DX,MONTANTE_POST_PORTA_ANT_DX,CORPO_SPECCHIO_DX,VETRINO_SPECCHIO_RETROVISORE_DX,CALOTTA_DX,FRECCIA_SPECCHIO_DX,GUARNIZIONE_PARABREZZA_DX,FRECCIA_PARAFANGO_ANT_DX,CATADIOTTRO_ANT_DX,FANALE_ANT_DX,FRECCIA_PARAURTI_ANT_DX")] Ricambi ricambi)
        {
            if (ModelState.IsValid)
            {
                db.Ricambi.Add(ricambi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDTelaio = new SelectList(db.TelaiAnagrafica, "ID", "Telaio", ricambi.IDTelaio);
            return View(ricambi);
        }

        // GET: Ricambi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ricambi ricambi = db.Ricambi.Find(id);
            if (ricambi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTelaio = new SelectList(db.TelaiAnagrafica, "ID", "Telaio", ricambi.IDTelaio);
            return View(ricambi);
        }

        // POST: Ricambi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDTelaio,COFANO_ANT,LOGO_ANT,FANALE_ANT_SX,FRECCIA_PARAURT_ANT_SX,CATADIOTTRO_ANT_SX,FRECCIA_PARAFANGO_ANT_SX,CALANDRA_SOTTOPARABREZZA,PARABREZZA,GUARNIZIONE_PARABREZZA_SX,CORPO_SPECCHIO_SX,VETRINO_SPECCHIO_RETROVISORE_SX,CALOTTA_SX,FRECCIA_SPECCHIO_SX,CORNICE_PORTA_ANT_SX,MONTANTE_ANT_PORTA_ANT_SX,MONTANTE_POST_PORTA_ANT_SX,CORNICE_PORTA_POST_SX,MONTANTE_ANT_PORTA_POST_SX,MONTANTE_POST_PORTA_POST_SX,CORNICE_PARAFANGO_SX,RASAVETRO_ANT_SX,RASAVETRO_POST_SX,CORRIMANO_SX,BASE_ANTENNA,TERZO_STOP,CORNICE_COFANO_POST,FANALE_POST_PARAFANGO_SX,COFANO_POST,LOGO_POST,LUNOTTO,CAPPELLIERA,SPORTELLO_CARBURANTE,CORRIMANO_DX,RASAVETRO_POST_DX,RASAVETRO_ANT_DX,CORNICE_PARAFANGO_DX,CORNICE_PORTA_POST_DX,MONTANTE_ANT_PORTA_POST_DX,MONTANTE_POST_PORTA_POST_DX,CORNICE_PORTA_ANT_DX,MONTANTE_ANT_PORTA_ANT_DX,MONTANTE_POST_PORTA_ANT_DX,CORPO_SPECCHIO_DX,VETRINO_SPECCHIO_RETROVISORE_DX,CALOTTA_DX,FRECCIA_SPECCHIO_DX,GUARNIZIONE_PARABREZZA_DX,FRECCIA_PARAFANGO_ANT_DX,CATADIOTTRO_ANT_DX,FANALE_ANT_DX,FRECCIA_PARAURTI_ANT_DX")] Ricambi ricambi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ricambi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTelaio = new SelectList(db.TelaiAnagrafica, "ID", "Telaio", ricambi.IDTelaio);
            return View(ricambi);
        }

        // GET: Ricambi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ricambi ricambi = db.Ricambi.Find(id);
            if (ricambi == null)
            {
                return HttpNotFound();
            }
            return View(ricambi);
        }

        // POST: Ricambi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ricambi ricambi = db.Ricambi.Find(id);
            db.Ricambi.Remove(ricambi);
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
