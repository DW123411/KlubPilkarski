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
    public class ZawodnikController : Controller
    {
        private BazaDanychEntities db = new BazaDanychEntities();

        // GET: Zawodnik
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Index()
        {
            var zawodnik = db.Zawodnik.Include(z => z.Klub);
            return View(zawodnik.ToList());
        }

        // GET: Zawodnik/Details/5
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zawodnik zawodnik = db.Zawodnik.Find(id);
            if (zawodnik == null)
            {
                return HttpNotFound();
            }
            return View(zawodnik);
        }

        // GET: Zawodnik/Create
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Create()
        {
            ViewBag.IdK = new SelectList(db.Klub, "IdK", "Nazwa");
            return View();
        }

        // POST: Zawodnik/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "PracownikKlubu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdZ,Imie,Nazwisko,IdK,Opis")] Zawodnik zawodnik)
        {
            if (ModelState.IsValid)
            {
                db.Zawodnik.Add(zawodnik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdK = new SelectList(db.Klub, "IdK", "Nazwa", zawodnik.IdK);
            return View(zawodnik);
        }

        // GET: Zawodnik/Edit/5
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zawodnik zawodnik = db.Zawodnik.Find(id);
            if (zawodnik == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdK = new SelectList(db.Klub, "IdK", "Nazwa", zawodnik.IdK);
            return View(zawodnik);
        }

        // POST: Zawodnik/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "PracownikKlubu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdZ,Imie,Nazwisko,IdK,Opis")] Zawodnik zawodnik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zawodnik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdK = new SelectList(db.Klub, "IdK", "Nazwa", zawodnik.IdK);
            return View(zawodnik);
        }

        // GET: Zawodnik/Delete/5
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zawodnik zawodnik = db.Zawodnik.Find(id);
            if (zawodnik == null)
            {
                return HttpNotFound();
            }
            return View(zawodnik);
        }

        // POST: Zawodnik/Delete/5
        [Authorize(Roles = "PracownikKlubu")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zawodnik zawodnik = db.Zawodnik.Find(id);
            db.Zawodnik.Remove(zawodnik);
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
