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
    public class SedziaController : Controller
    {
        private BazaDanychEntities db = new BazaDanychEntities();

        // GET: Sedzia
        public ActionResult Index()
        {
            return View(db.Sedzia.ToList());
        }

        // GET: Sedzia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sedzia/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSedzia,Imie,Nazwisko")] Sedzia sedzia)
        {
            if (ModelState.IsValid)
            {
                db.Sedzia.Add(sedzia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sedzia);
        }

        // GET: Sedzia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sedzia sedzia = db.Sedzia.Find(id);
            if (sedzia == null)
            {
                return HttpNotFound();
            }
            return View(sedzia);
        }

        // POST: Sedzia/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSedzia,Imie,Nazwisko")] Sedzia sedzia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sedzia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sedzia);
        }

        // GET: Sedzia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sedzia sedzia = db.Sedzia.Find(id);
            if (sedzia == null)
            {
                return HttpNotFound();
            }
            return View(sedzia);
        }

        // POST: Sedzia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sedzia sedzia = db.Sedzia.Find(id);
            db.Sedzia.Remove(sedzia);
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
