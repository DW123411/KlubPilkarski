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

        // GET: ZawodnikMecz/DetailsSummary/5
        [Authorize(Roles = "Trener")]
        public ActionResult DetailsSummary(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var wystepy = db.ZawodnikMecz.Where(zm => zm.IdZ == id);
            var zawodnik = db.Zawodnik.Find(id);
            String pozycja = "-";
            if (wystepy == null)
            {
                return HttpNotFound();
            }
            int iloscWystepow = 0;
            int br = 0;
            int lo = 0;
            int so = 0;
            int po = 0;
            int spd = 0;
            int sp = 0;
            int lp = 0;
            int pp = 0;
            int spo = 0;
            int n = 0;
            int cn = 0;
            int ls = 0;
            int ps = 0;
            foreach (ZawodnikMecz zawodnikMecz in wystepy)
            {
                if (zawodnikMecz.Zawodnik.IdZ == zawodnik.IdZ && zawodnikMecz.MinutyDo - zawodnikMecz.MinutyOd > 20)
                {
                    if (zawodnikMecz.Pozycja == "BR")
                    {
                        br++;
                    }
                    else if (zawodnikMecz.Pozycja == "LO")
                    {
                        lo++;
                    }
                    else if (zawodnikMecz.Pozycja == "ŚO")
                    {
                        so++;
                    }
                    else if (zawodnikMecz.Pozycja == "PO")
                    {
                        po++;
                    }
                    else if (zawodnikMecz.Pozycja == "ŚPD")
                    {
                        spd++;
                    }
                    else if (zawodnikMecz.Pozycja == "ŚP")
                    {
                        sp++;
                    }
                    else if (zawodnikMecz.Pozycja == "LP")
                    {
                        lp++;
                    }
                    else if (zawodnikMecz.Pozycja == "PP")
                    {
                        pp++;
                    }
                    else if (zawodnikMecz.Pozycja == "ŚPO")
                    {
                        spo++;
                    }
                    else if (zawodnikMecz.Pozycja == "CN")
                    {
                        cn++;
                    }
                    else if (zawodnikMecz.Pozycja == "N")
                    {
                        n++;
                    }
                    else if (zawodnikMecz.Pozycja == "LS")
                    {
                        ls++;
                    }
                    else if (zawodnikMecz.Pozycja == "PS")
                    {
                        ps++;
                    }
                    iloscWystepow++;
                }
            }
            if (iloscWystepow != 0)
            {
                if (br >= lo && br >= so && br >= po && br >= spd && br >= sp && br >= lp && br >= pp && br >= spo && br >= cn && br >= n && br >= ls && br >= ps)
                {
                    pozycja = "BR";
                }
                else if (lo >= br && lo >= so && lo >= po && lo >= spd && lo >= sp && lo >= lp && lo >= pp && lo >= spo && lo >= cn && lo >= n && lo >= ls && lo >= ps)
                {
                    pozycja = "LO";
                }
                else if (so >= br && so >= lo && so >= po && so >= spd && so >= sp && so >= lp && so >= pp && so >= spo && so >= cn && so >= n && so >= ls && so >= ps)
                {
                    pozycja = "ŚO";
                }
                else if (po >= br && po >= lo && po >= so && po >= spd && po >= sp && po >= lp && po >= pp && po >= spo && po >= cn && po >= n && po >= ls && po >= ps)
                {
                    pozycja = "PO";
                }
                else if (spd >= br && spd >= lo && spd >= so && spd >= po && spd >= sp && spd >= lp && spd >= pp && spd >= spo && spd >= cn && spd >= n && spd >= ls && spd >= ps)
                {
                    pozycja = "ŚPD";
                }
                else if (sp >= br && sp >= lo && sp >= so && sp >= po && sp >= spd && sp >= lp && sp >= pp && sp >= spo && sp >= cn && sp >= n && sp >= ls && sp >= ps)
                {
                    pozycja = "ŚP";
                }
                else if (lp >= br && lp >= lo && lp >= so && lp >= po && lp >= spd && lp >= sp && lp >= pp && lp >= spo && lp >= cn && lp >= n && lp >= ls && lp >= ps)
                {
                    pozycja = "LP";
                }
                else if (pp >= br && pp >= lo && pp >= so && pp >= po && pp >= spd && pp >= sp && pp >= lp && pp >= spo && pp >= cn && pp >= n && pp >= ls && pp >= ps)
                {
                    pozycja = "PP";
                }
                else if (spo >= br && spo >= lo && spo >= so && spo >= po && spo >= spd && spo >= sp && spo >= lp && spo >= pp && spo >= cn && spo >= n && spo >= ls && spo >= ps)
                {
                    pozycja = "ŚPO";
                }
                else if (cn >= br && cn >= lo && cn >= so && cn >= po && cn >= spd && cn >= sp && cn >= lp && cn >= pp && cn >= spo && cn >= n && cn >= ls && cn >= ps)
                {
                    pozycja = "CN";
                }
                else if (n >= br && n >= lo && n >= so && n >= po && n >= spd && n >= sp && n >= lp && n >= pp && n >= spo && n >= cn && n >= ls && n >= ps)
                {
                    pozycja = "N";
                }
                else if (ls >= br && ls >= lo && ls >= so && ls >= po && ls >= spd && ls >= sp && ls >= lp && ls >= pp && ls >= spo && ls >= cn && ls >= n && ls >= ps)
                {
                    pozycja = "LS";
                }
                else if (ps >= br && ps >= lo && ps >= so && ps >= po && ps >= spd && ps >= sp && ps >= lp && ps >= pp && ps >= spo && ps >= cn && ps >= n && ps >= ls)
                {
                    pozycja = "PS";
                }
            }
            ViewBag.Imie = zawodnik.Imie;
            ViewBag.Nazwisko = zawodnik.Nazwisko;
            ViewBag.Pozycja = pozycja;
            return View(wystepy);
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

        //GET: ZawodnikMecz/AllFromClub/5
        [Authorize(Roles = "Trener")]
        public ActionResult AllFromClub()
        {
            var wystepyZawodnikow = db.ZawodnikMecz.Where(zm => zm.Mecz.Klub.Nazwa == "Manchester United F.C." || zm.Mecz.Klub1.Nazwa == "Manchester United F.C.").ToList();
            var zawodnicy = db.Zawodnik.Where(z => z.Klub.Nazwa.Equals("Manchester United F.C.")).ToList();
            Dictionary<int, double> oceny = new Dictionary<int, double>();
            Dictionary<int, string> pozycja = new Dictionary<int, string>();
            double sumaOcen = 0;
            int iloscWystepow = 0;
            int br = 0;
            int lo = 0;
            int so = 0;
            int po = 0;
            int spd = 0;
            int sp = 0;
            int lp = 0;
            int pp = 0;
            int spo = 0;
            int n = 0;
            int cn = 0;
            int ls = 0;
            int ps = 0;
            foreach (Zawodnik zawodnik in zawodnicy)
            {
                foreach (ZawodnikMecz zawodnikMecz in wystepyZawodnikow)
                {
                    if (zawodnikMecz.Zawodnik.IdZ == zawodnik.IdZ && zawodnikMecz.MinutyDo - zawodnikMecz.MinutyOd > 20)
                    {
                        if (zawodnikMecz.Pozycja == "BR")
                        {
                            br++;
                        }
                        else if (zawodnikMecz.Pozycja == "LO")
                        {
                            lo++;
                        }
                        else if (zawodnikMecz.Pozycja == "ŚO")
                        {
                            so++;
                        }
                        else if (zawodnikMecz.Pozycja == "PO")
                        {
                            po++;
                        }
                        else if (zawodnikMecz.Pozycja == "ŚPD")
                        {
                            spd++;
                        }
                        else if (zawodnikMecz.Pozycja == "ŚP")
                        {
                            sp++;
                        }
                        else if (zawodnikMecz.Pozycja == "LP")
                        {
                            lp++;
                        }
                        else if (zawodnikMecz.Pozycja == "PP")
                        {
                            pp++;
                        }
                        else if (zawodnikMecz.Pozycja == "ŚPO")
                        {
                            spo++;
                        }
                        else if (zawodnikMecz.Pozycja == "CN")
                        {
                            cn++;
                        }
                        else if (zawodnikMecz.Pozycja == "N")
                        {
                            n++;
                        }
                        else if (zawodnikMecz.Pozycja == "LS")
                        {
                            ls++;
                        }
                        else if (zawodnikMecz.Pozycja == "PS")
                        {
                            ps++;
                        }
                        sumaOcen += zawodnikMecz.Forma;
                        iloscWystepow++;
                    }
                }
                if (iloscWystepow != 0)
                {
                    if (br >= lo && br >= so && br >= po && br >= spd && br >= sp && br >= lp && br >= pp && br >= spo && br >= cn && br >= n && br >= ls && br >= ps)
                    {
                        pozycja[zawodnik.IdZ] = "BR";
                    }
                    else if (lo >= br && lo >= so && lo >= po && lo >= spd && lo >= sp && lo >= lp && lo >= pp && lo >= spo && lo >= cn && lo >= n && lo >= ls && lo >= ps)
                    {
                        pozycja[zawodnik.IdZ] = "LO";
                    }
                    else if (so >= br && so >= lo && so >= po && so >= spd && so >= sp && so >= lp && so >= pp && so >= spo && so >= cn && so >= n && so >= ls && so >= ps)
                    {
                        pozycja[zawodnik.IdZ] = "ŚO";
                    }
                    else if (po >= br && po >= lo && po >= so && po >= spd && po >= sp && po >= lp && po >= pp && po >= spo && po >= cn && po >= n && po >= ls && po >= ps)
                    {
                        pozycja[zawodnik.IdZ] = "PO";
                    }
                    else if (spd >= br && spd >= lo && spd >= so && spd >= po && spd >= sp && spd >= lp && spd >= pp && spd >= spo && spd >= cn && spd >= n && spd >= ls && spd >= ps)
                    {
                        pozycja[zawodnik.IdZ] = "ŚPD";
                    }
                    else if (sp >= br && sp >= lo && sp >= so && sp >= po && sp >= spd && sp >= lp && sp >= pp && sp >= spo && sp >= cn && sp >= n && sp >= ls && sp >= ps)
                    {
                        pozycja[zawodnik.IdZ] = "ŚP";
                    }
                    else if (lp >= br && lp >= lo && lp >= so && lp >= po && lp >= spd && lp >= sp && lp >= pp && lp >= spo && lp >= cn && lp >= n && lp >= ls && lp >= ps)
                    {
                        pozycja[zawodnik.IdZ] = "LP";
                    }
                    else if (pp >= br && pp >= lo && pp >= so && pp >= po && pp >= spd && pp >= sp && pp >= lp && pp >= spo && pp >= cn && pp >= n && pp >= ls && pp >= ps)
                    {
                        pozycja[zawodnik.IdZ] = "PP";
                    }
                    else if (spo >= br && spo >= lo && spo >= so && spo >= po && spo >= spd && spo >= sp && spo >= lp && spo >= pp && spo >= cn && spo >= n && spo >= ls && spo >= ps)
                    {
                        pozycja[zawodnik.IdZ] = "ŚPO";
                    }
                    else if (cn >= br && cn >= lo && cn >= so && cn >= po && cn >= spd && cn >= sp && cn >= lp && cn >= pp && cn >= spo && cn >= n && cn >= ls && cn >= ps)
                    {
                        pozycja[zawodnik.IdZ] = "CN";
                    }
                    else if (n >= br && n >= lo && n >= so && n >= po && n >= spd && n >= sp && n >= lp && n >= pp && n >= spo && n >= cn && n >= ls && n >= ps)
                    {
                        pozycja[zawodnik.IdZ] = "N";
                    }
                    else if (ls >= br && ls >= lo && ls >= so && ls >= po && ls >= spd && ls >= sp && ls >= lp && ls >= pp && ls >= spo && ls >= cn && ls >= n && ls >= ps)
                    {
                        pozycja[zawodnik.IdZ] = "LS";
                    }
                    else if (ps >= br && ps >= lo && ps >= so && ps >= po && ps >= spd && ps >= sp && ps >= lp && ps >= pp && ps >= spo && ps >= cn && ps >= n && ps >= ls)
                    {
                        pozycja[zawodnik.IdZ] = "PS";
                    }
                    oceny[zawodnik.IdZ] = (sumaOcen / iloscWystepow) / 10;
                }
                br = 0;
                lo = 0;
                so = 0;
                po = 0;
                spd = 0;
                sp = 0;
                lp = 0;
                pp = 0;
                spo = 0;
                n = 0;
                cn = 0;
                ls = 0;
                ps = 0;
                sumaOcen = 0;
                iloscWystepow = 0;
            }
            ViewBag.Oceny = oceny;
            ViewBag.Pozycje = pozycja;
            return View(zawodnicy.ToList());
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
