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
    public class MeczController : Controller
    {
        private BazaDanychEntities db = new BazaDanychEntities();

        // GET: Mecz
        public ActionResult Index()
        {
            var mecz = db.Mecz.Include(m => m.Klub).Include(m => m.Klub1).Include(m => m.Kolejka).Include(m => m.Sedzia).Include(m => m.Sezon).Include(m => m.Stadion);
            return View(mecz.ToList());
        }

        // GET: AllInSeason
        public ActionResult AllInSeason(int id)
        {
            var meczeWSezonie = db.Mecz.Include(m => m.Klub).Include(m => m.Klub1).Include(m => m.Kolejka).Include(m => m.Sedzia).Include(m => m.Sezon).Include(m => m.Stadion).Where(m => m.Sezon.IdS.Equals(id)).OrderBy(m => m.Data);
            ViewBag.Season = id;
            ViewBag.SelectedSeason = db.Sezon.Find(id);
            return View(meczeWSezonie.ToList());
        }

        // GET: LeagueTable
        public ActionResult LeagueTable(int id)
        {
            var tabelaLigowa = db.TabelaLigowaSezon(id);
            ViewBag.Season = id;
            ViewBag.SelectedSeason = db.Sezon.Find(id);
            return View(tabelaLigowa.ToList());
        }

        // GET: Mecz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mecz mecz = db.Mecz.Find(id);
            if (mecz == null)
            {
                return HttpNotFound();
            }
            return View(mecz);
        }

        // GET: Mecz/Create
        public ActionResult Create()
        {
            var sezony =
                db.Sezon
                .Select(s => new
                {
                    IdS = s.IdS,
                    Opis = s.RokOd + "/" + s.RokDo
                })
                .ToList();
            var sedziowie =
                db.Sedzia
                .Select(s => new
                {
                    IdSedzia = s.IdSedzia,
                    Opis = s.Imie + " " + s.Nazwisko
                })
                .ToList();
            var stadiony =
                db.Stadion
                .Select(s => new
                {
                    IdStadion = s.IdStadion,
                    Opis = s.Nazwa + " - " + s.Miejscowosc
                })
                .ToList();
            ViewBag.IdKlubGoscie = new SelectList(db.Klub, "IdK", "Nazwa");
            ViewBag.IdKlubGospodarze = new SelectList(db.Klub, "IdK", "Nazwa");
            ViewBag.IdKolejka = new SelectList(db.Kolejka, "IdKolejka", "Nr");
            ViewBag.IdSedzia = new SelectList(sedziowie, "IdSedzia", "Opis");
            ViewBag.IdS = new SelectList(sezony, "IdS", "Opis");
            ViewBag.IdStadion = new SelectList(stadiony, "IdStadion", "Opis");
            return View();
        }

        // POST: Mecz/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdM,IdS,IdKolejka,Data,IdStadion,IdKlubGospodarze,IdKlubGoscie,BramkiGospodarze,BramkiGoscie,PunktyGospodarze,PunktyGoscie,FormaGospodarze,FormaGoscie,Opis,IdSedzia,Kibice")] Mecz mecz)
        {
            if (ModelState.IsValid)
            {
                db.Mecz.Add(mecz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdKlubGoscie = new SelectList(db.Klub, "IdK", "Siedziba", mecz.IdKlubGoscie);
            ViewBag.IdKlubGospodarze = new SelectList(db.Klub, "IdK", "Siedziba", mecz.IdKlubGospodarze);
            ViewBag.IdKolejka = new SelectList(db.Kolejka, "IdKolejka", "IdKolejka", mecz.IdKolejka);
            ViewBag.IdSedzia = new SelectList(db.Sedzia, "IdSedzia", "Imie", mecz.IdSedzia);
            ViewBag.IdS = new SelectList(db.Sezon, "IdS", "IdS", mecz.IdS);
            ViewBag.IdStadion = new SelectList(db.Stadion, "IdStadion", "Nazwa", mecz.IdStadion);
            return View(mecz);
        }

        // GET: Mecz/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mecz mecz = db.Mecz.Find(id);
            if (mecz == null)
            {
                return HttpNotFound();
            }
            var sezony =
                db.Sezon
                .Select(s => new
                {
                    IdS = s.IdS,
                    Opis = s.RokOd + "/" + s.RokDo
                })
                .ToList();
            var sedziowie =
                db.Sedzia
                .Select(s => new
                {
                    IdSedzia = s.IdSedzia,
                    Opis = s.Imie + " " + s.Nazwisko
                })
                .ToList();
            var stadiony =
                db.Stadion
                .Select(s => new
                {
                    IdStadion = s.IdStadion,
                    Opis = s.Nazwa + " - " + s.Miejscowosc
                })
                .ToList();
            ViewBag.IdKlubGoscie = new SelectList(db.Klub, "IdK", "Nazwa", mecz.IdKlubGoscie);
            ViewBag.IdKlubGospodarze = new SelectList(db.Klub, "IdK", "Nazwa", mecz.IdKlubGospodarze);
            ViewBag.IdKolejka = new SelectList(db.Kolejka, "IdKolejka", "Nr", mecz.IdKolejka);
            ViewBag.IdSedzia = new SelectList(sedziowie, "IdSedzia", "Opis", mecz.IdSedzia);
            ViewBag.IdS = new SelectList(sezony, "IdS", "Opis", mecz.IdS);
            ViewBag.IdStadion = new SelectList(stadiony, "IdStadion", "Opis", mecz.IdStadion);
            return View(mecz);
        }

        // POST: Mecz/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdM,IdS,IdKolejka,Data,IdStadion,IdKlubGospodarze,IdKlubGoscie,BramkiGospodarze,BramkiGoscie,PunktyGospodarze,PunktyGoscie,FormaGospodarze,FormaGoscie,Opis,IdSedzia,Kibice")] Mecz mecz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mecz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdKlubGoscie = new SelectList(db.Klub, "IdK", "Siedziba", mecz.IdKlubGoscie);
            ViewBag.IdKlubGospodarze = new SelectList(db.Klub, "IdK", "Siedziba", mecz.IdKlubGospodarze);
            ViewBag.IdKolejka = new SelectList(db.Kolejka, "IdKolejka", "IdKolejka", mecz.IdKolejka);
            ViewBag.IdSedzia = new SelectList(db.Sedzia, "IdSedzia", "Imie", mecz.IdSedzia);
            ViewBag.IdS = new SelectList(db.Sezon, "IdS", "IdS", mecz.IdS);
            ViewBag.IdStadion = new SelectList(db.Stadion, "IdStadion", "Nazwa", mecz.IdStadion);
            return View(mecz);
        }

        // GET: Mecz/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mecz mecz = db.Mecz.Find(id);
            if (mecz == null)
            {
                return HttpNotFound();
            }
            return View(mecz);
        }

        // POST: Mecz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mecz mecz = db.Mecz.Find(id);
            db.Mecz.Remove(mecz);
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
