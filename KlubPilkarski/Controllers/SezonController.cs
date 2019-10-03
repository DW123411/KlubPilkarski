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
    public class SezonController : Controller
    {
        private BazaDanychEntities db = new BazaDanychEntities();

        // GET: Sezon
        public ActionResult Index()
        {
            return View(db.Sezon.ToList());
        }

        // GET: Sezon/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sezon/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdS,RokOd,RokDo")] Sezon sezon)
        {
            if (ModelState.IsValid)
            {
                db.Sezon.Add(sezon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sezon);
        }

        // GET: Sezon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sezon sezon = db.Sezon.Find(id);
            if (sezon == null)
            {
                return HttpNotFound();
            }
            return View(sezon);
        }

        // POST: Sezon/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdS,RokOd,RokDo")] Sezon sezon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sezon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sezon);
        }

        // GET: Sezon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sezon sezon = db.Sezon.Find(id);
            if (sezon == null)
            {
                return HttpNotFound();
            }
            return View(sezon);
        }

        // POST: Sezon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sezon sezon = db.Sezon.Find(id);
            db.Sezon.Remove(sezon);
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
