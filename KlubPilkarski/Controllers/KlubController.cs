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
    public class KlubController : Controller
    {
        private BazaDanychEntities db = new BazaDanychEntities();

        // GET: Klub
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Index()
        {
            var klub = db.Klub.Include(k => k.Trener);
            return View(klub.ToList());
        }

        // GET: Klub/Details/5
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klub klub = db.Klub.Find(id);
            if (klub == null)
            {
                return HttpNotFound();
            }
            return View(klub);
        }

        // GET: Klub/Create
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Create()
        {
            var trenerzy =
                db.Trener
                .Select(s => new
                {
                    IdT = s.IdT,
                    Opis = s.Imie + " " + s.Nazwisko
                })
                .ToList();
            ViewBag.IdT = new SelectList(trenerzy, "IdT", "Opis");
            return View();
        }

        // POST: Klub/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "PracownikKlubu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdK,Siedziba,Nazwa,Opis,IdT")] Klub klub)
        {
            if (ModelState.IsValid)
            {
                db.Klub.Add(klub);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var trenerzy =
                db.Trener
                .Select(s => new
                {
                    IdT = s.IdT,
                    Opis = s.Imie + " " + s.Nazwisko
                })
                .ToList();
            ViewBag.IdT = new SelectList(trenerzy, "IdT", "Opis");
            return View(klub);
        }

        // GET: Klub/Edit/5
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klub klub = db.Klub.Find(id);
            if (klub == null)
            {
                return HttpNotFound();
            }
            var trenerzy =
                db.Trener
                .Select(s => new
                {
                    IdT = s.IdT,
                    Opis = s.Imie + " " + s.Nazwisko
                })
                .ToList();
            ViewBag.IdT = new SelectList(trenerzy, "IdT", "Opis", klub.IdT);
            return View(klub);
        }

        // POST: Klub/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "PracownikKlubu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdK,Siedziba,Nazwa,Opis,IdT")] Klub klub)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klub).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var trenerzy =
                db.Trener
                .Select(s => new
                {
                    IdT = s.IdT,
                    Opis = s.Imie + " " + s.Nazwisko
                })
                .ToList();
            ViewBag.IdT = new SelectList(trenerzy, "IdT", "Opis");
            return View(klub);
        }

        // GET: Klub/Delete/5
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klub klub = db.Klub.Find(id);
            if (klub == null)
            {
                return HttpNotFound();
            }
            return View(klub);
        }

        // POST: Klub/Delete/5
        [Authorize(Roles = "PracownikKlubu")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klub klub = db.Klub.Find(id);
            db.Klub.Remove(klub);
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
