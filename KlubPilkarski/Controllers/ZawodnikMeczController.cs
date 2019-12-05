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
    public class ZawodnikMeczController : Controller
    {
        private BazaDanychEntities db = new BazaDanychEntities();
        RatingController ratingController = new RatingController();

        // GET: ZawodnikMecz
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Index()
        {
            var zawodnikMecz = db.ZawodnikMecz.Include(z => z.Mecz).Include(z => z.Zawodnik);
            return View(zawodnikMecz.ToList());
        }

        // GET: ZawodnikMecz/Details/5
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZawodnikMecz zawodnikMecz = db.ZawodnikMecz.Find(id);
            if (zawodnikMecz == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pozycja = zawodnikMecz.Pozycja;
            return View(zawodnikMecz);
        }

        // GET: ZawodnikMecz/Create
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Create()
        {
            var mecze =
                db.Mecz
                .Select(s => new
                {
                    IdM = s.IdM,
                    Opis = s.Sezon.RokOd + "/" + s.Sezon.RokDo + ", Kolejka " + s.Kolejka.Nr + ", " + s.Klub1.Nazwa + " - " + s.Klub.Nazwa
                })
                .ToList();
            var zawodnicy =
                db.Zawodnik
                .Select(s => new
                {
                    IdZ = s.IdZ,
                    Opis = s.Imie + " " + s.Nazwisko + ", " +s.Klub.Nazwa
                })
                .ToList();
            ViewBag.IdM = new SelectList(mecze, "IdM", "Opis");
            ViewBag.IdZ = new SelectList(zawodnicy, "IdZ", "Opis");

            ZawodnikMecz defaultZawodnikMecz = new ZawodnikMecz();
            return View(defaultZawodnikMecz);
        }

        // POST: ZawodnikMecz/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "PracownikKlubu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdZM,IdM,IdZ,Pozycja,MinutyOd,MinutyDo,Bramki,Asysty,UtworzoneSzanse,StrzalyCelne,StrzalyNiecelne,StrzalyZablokowane,SlupkiPoprzeczki,KartkiZolte,KartkiCzerwone,PodaniaUdanePolowaWlasna,PodaniaUdanePolowaPrzeciwnika,PodaniaNieudane,Dosrodkowania,DlugiePodaniaUdane,DlugiePodaniaNieudane,KluczowePodania,Kontakty,RzutyRozne,Wyrzuty,PojedynkiWygrane,PojedynkiPrzegrane,Wybicia,DryblingiUdane,Straty,Faulowany,Faule,OdbioryUdane,OdbioryNieudane,GlowkiWygrane,GlowkiPrzegrane,Przejecia,Spalone,OdzyskanePilki,ObronaWyskok,ObronaPoleKarne,ObronaWyjscie,Piastkowania,ObronaWysokiejPilki,StraconeBramki,Forma")] ZawodnikMecz zawodnikMecz)
        {
            if (ModelState.IsValid)
            {
                RatingController rc = new RatingController();
                zawodnikMecz.Forma = ratingController.CalculateRating(zawodnikMecz);
                db.ZawodnikMecz.Add(zawodnikMecz);
                db.SaveChanges();
                MeczController mc = new MeczController();
                mc.CalculateClubsRating(zawodnikMecz.IdM);
                return RedirectToAction("Index");
            }

            var mecze =
                db.Mecz
                .Select(s => new
                {
                    IdM = s.IdM,
                    Opis = s.Sezon.RokOd + "/" + s.Sezon.RokDo + ", Kolejka " + s.Kolejka.Nr + ", " + s.Klub1.Nazwa + " - " + s.Klub.Nazwa
                })
                .ToList();
            var zawodnicy =
                db.Zawodnik
                .Select(s => new
                {
                    IdZ = s.IdZ,
                    Opis = s.Imie + " " + s.Nazwisko + ", " + s.Klub.Nazwa
                })
                .ToList();
            ViewBag.IdM = new SelectList(mecze, "IdM", "Opis", zawodnikMecz.IdM);
            ViewBag.IdZ = new SelectList(zawodnicy, "IdZ", "Opis", zawodnikMecz.IdZ);
            return View(zawodnikMecz);
        }

        // GET: ZawodnikMecz/Edit/5
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZawodnikMecz zawodnikMecz = db.ZawodnikMecz.Find(id);
            if (zawodnikMecz == null)
            {
                return HttpNotFound();
            }
            var mecze =
                db.Mecz
                .Select(s => new
                {
                    IdM = s.IdM,
                    Opis = s.Sezon.RokOd + "/" + s.Sezon.RokDo + ", Kolejka " + s.Kolejka.Nr + ", " + s.Klub1.Nazwa + " - " + s.Klub.Nazwa
                })
                .ToList();
            var zawodnicy =
                db.Zawodnik
                .Select(s => new
                {
                    IdZ = s.IdZ,
                    Opis = s.Imie + " " + s.Nazwisko + ", " + s.Klub.Nazwa
                })
                .ToList();
            ViewBag.IdM = new SelectList(mecze, "IdM", "Opis", zawodnikMecz.IdM);
            ViewBag.IdZ = new SelectList(zawodnicy, "IdZ", "Opis", zawodnikMecz.IdZ);
            ViewBag.Pozycja = zawodnikMecz.Pozycja;
            ViewBag.IdZawodnik = zawodnikMecz.IdZ;
            return View(zawodnikMecz);
        }

        // POST: ZawodnikMecz/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "PracownikKlubu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdZM,IdM,IdZ,Pozycja,MinutyOd,MinutyDo,Bramki,Asysty,UtworzoneSzanse,StrzalyCelne,StrzalyNiecelne,StrzalyZablokowane,SlupkiPoprzeczki,KartkiZolte,KartkiCzerwone,PodaniaUdanePolowaWlasna,PodaniaUdanePolowaPrzeciwnika,PodaniaNieudane,Dosrodkowania,DlugiePodaniaUdane,DlugiePodaniaNieudane,KluczowePodania,Kontakty,RzutyRozne,Wyrzuty,PojedynkiWygrane,PojedynkiPrzegrane,Wybicia,DryblingiUdane,Straty,Faulowany,Faule,OdbioryUdane,OdbioryNieudane,GlowkiWygrane,GlowkiPrzegrane,Przejecia,Spalone,OdzyskanePilki,ObronaWyskok,ObronaPoleKarne,ObronaWyjscie,Piastkowania,ObronaWysokiejPilki,StraconeBramki,Forma")] ZawodnikMecz zawodnikMecz)
        {
            if (ModelState.IsValid)
            {
                RatingController rc = new RatingController();
                zawodnikMecz.Forma = ratingController.CalculateRating(zawodnikMecz);
                db.Entry(zawodnikMecz).State = EntityState.Modified;
                db.SaveChanges();
                MeczController mc = new MeczController();
                mc.CalculateClubsRating(zawodnikMecz.IdM);
                return RedirectToAction("Index");
            }
            var mecze =
                db.Mecz
                .Select(s => new
                {
                    IdM = s.IdM,
                    Opis = s.Sezon.RokOd + "/" + s.Sezon.RokDo + ", Kolejka " + s.Kolejka.Nr + ", " + s.Klub1.Nazwa + " - " + s.Klub.Nazwa
                })
                .ToList();
            var zawodnicy =
                db.Zawodnik
                .Select(s => new
                {
                    IdZ = s.IdZ,
                    Opis = s.Imie + " " + s.Nazwisko + ", " + s.Klub.Nazwa
                })
                .ToList();
            ViewBag.IdM = new SelectList(mecze, "IdM", "Opis", zawodnikMecz.IdM);
            ViewBag.IdZ = new SelectList(zawodnicy, "IdZ", "Opis", zawodnikMecz.IdZ);
            ViewBag.Pozycja = zawodnikMecz.Pozycja;
            return View(zawodnikMecz);
        }

        [Authorize(Roles = "PracownikKlubu")]
        public JsonResult FetchPlayers(int id)
        {
            var mecz = db.Mecz.Find(id);
            var zawodnicy = db.ZawodnicyMecz(mecz.IdKlubGospodarze, mecz.IdKlubGoscie);
            return Json(zawodnicy, JsonRequestBehavior.AllowGet);
        }

        // GET: ZawodnikMecz/Delete/5
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZawodnikMecz zawodnikMecz = db.ZawodnikMecz.Find(id);
            if (zawodnikMecz == null)
            {
                return HttpNotFound();
            }
            return View(zawodnikMecz);
        }

        // POST: ZawodnikMecz/Delete/5
        [Authorize(Roles = "PracownikKlubu")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZawodnikMecz zawodnikMecz = db.ZawodnikMecz.Find(id);
            db.ZawodnikMecz.Remove(zawodnikMecz);
            db.SaveChanges();
            MeczController mc = new MeczController();
            mc.CalculateClubsRating(zawodnikMecz.IdM);
            return RedirectToAction("Index");
        }

        //GET: ZawodnikMecz/CalculateRating/5
        public ActionResult CalculateRating(int id)
        {
            ZawodnikMecz zawodnikMecz = db.ZawodnikMecz.Find(id);
            RatingController rc = new RatingController();
            zawodnikMecz.Forma = ratingController.CalculateRating(zawodnikMecz);
            if (ModelState.IsValid)
            {
                db.Entry(zawodnikMecz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" + id);
            }
            return RedirectToAction("Details/"+id);
        }

        //GET: ZawodnikMecz/AllInMatch/5
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult AllInMatch(int id)
        {
            var wystepyWMeczu = db.ZawodnikMecz.Include(z => z.Zawodnik).Include(m => m.Mecz).Where(m => m.Mecz.IdM.Equals(id));
            ViewBag.Match = id;
            ViewBag.SelectedMatch = db.Mecz.Find(id);
            return View(wystepyWMeczu.ToList());
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
