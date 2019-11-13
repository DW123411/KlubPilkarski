using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KlubPilkarski.Models;

namespace KlubPilkarski.Controllers
{
    public class KolejkaController : Controller
    {
        private BazaDanychEntities db = new BazaDanychEntities();

        // GET: Kolejka
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Index()
        {
            return View(db.Kolejka.ToList());
        }

        // GET: Kolejka/Create
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kolejka/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "PracownikKlubu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdKolejka,Nr,DataOd,DataDo")] Kolejka kolejka)
        {
            if (ModelState.IsValid)
            {
                db.Kolejka.Add(kolejka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kolejka);
        }

        // GET: Kolejka/Edit/5
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kolejka kolejka = db.Kolejka.Find(id);
            if (kolejka == null)
            {
                return HttpNotFound();
            }
            return View(kolejka);
        }

        // POST: Kolejka/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "PracownikKlubu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdKolejka,Nr,DataOd,DataDo")] Kolejka kolejka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kolejka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kolejka);
        }

        // GET: Kolejka/Delete/5
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kolejka kolejka = db.Kolejka.Find(id);
            if (kolejka == null)
            {
                return HttpNotFound();
            }
            return View(kolejka);
        }

        // POST: Kolejka/Delete/5
        [Authorize(Roles = "PracownikKlubu")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kolejka kolejka = db.Kolejka.Find(id);
            db.Kolejka.Remove(kolejka);
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
