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
    public class StadionController : Controller
    {
        private BazaDanychEntities db = new BazaDanychEntities();

        // GET: Stadion
        public ActionResult Index()
        {
            return View(db.Stadion.ToList());
        }

        // GET: Stadion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadion stadion = db.Stadion.Find(id);
            if (stadion == null)
            {
                return HttpNotFound();
            }
            return View(stadion);
        }

        // GET: Stadion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stadion/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdStadion,Nazwa,Miejscowosc,Pojemnosc")] Stadion stadion)
        {
            if (ModelState.IsValid)
            {
                db.Stadion.Add(stadion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stadion);
        }

        // GET: Stadion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadion stadion = db.Stadion.Find(id);
            if (stadion == null)
            {
                return HttpNotFound();
            }
            return View(stadion);
        }

        // POST: Stadion/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdStadion,Nazwa,Miejscowosc,Pojemnosc")] Stadion stadion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stadion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stadion);
        }

        // GET: Stadion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadion stadion = db.Stadion.Find(id);
            if (stadion == null)
            {
                return HttpNotFound();
            }
            return View(stadion);
        }

        // POST: Stadion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stadion stadion = db.Stadion.Find(id);
            db.Stadion.Remove(stadion);
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
