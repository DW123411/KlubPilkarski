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
        [Authorize(Roles = "PracownikKlubu,Trener")]
        public ActionResult Index()
        {
            var mecz = db.Mecz.Include(m => m.Klub).Include(m => m.Klub1).Include(m => m.Kolejka).Include(m => m.Sedzia).Include(m => m.Sezon).Include(m => m.Stadion);
            return View(mecz.ToList());
        }

        // GET: AllInSeason
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult AllInSeason(int id)
        {
            var meczeWSezonie = db.Mecz.Include(m => m.Klub).Include(m => m.Klub1).Include(m => m.Kolejka).Include(m => m.Sedzia).Include(m => m.Sezon).Include(m => m.Stadion).Where(m => m.Sezon.IdS.Equals(id)).OrderBy(m => m.Data);
            ViewBag.Season = id;
            ViewBag.SelectedSeason = db.Sezon.Find(id);
            return View(meczeWSezonie.ToList());
        }

        // GET: AllInMatchweek
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult AllInMatchweek(int id)
        {
            var meczeWKolejce = db.Mecz.Include(m => m.Klub).Include(m => m.Klub1).Include(m => m.Kolejka).Include(m => m.Sedzia).Include(m => m.Sezon).Include(m => m.Stadion).Where(m => m.Kolejka.IdKolejka.Equals(id)).OrderBy(m => m.Data);
            ViewBag.Matchweek = id;
            ViewBag.SelectedMatchweek = db.Kolejka.Find(id);
            return View(meczeWKolejce.ToList());
        }

        // GET: LeagueTable
        [Authorize(Roles = "PracownikKlubu")]
        public ActionResult LeagueTable(int id)
        {
            var tabelaLigowa = db.TabelaLigowaSezon(id);
            ViewBag.Season = id;
            ViewBag.SelectedSeason = db.Sezon.Find(id);
            return View(tabelaLigowa.ToList());
        }

        //GET: LatestLeagueTable
        public ActionResult LatestLeagueTable()
        {
            var sezony = db.Sezon.OrderByDescending(r => r.RokDo);
            ViewBag.SelectedSeason = sezony.First();
            var tabelaLigowa = db.TabelaLigowaSezon(sezony.First().IdS);
            return View("LeagueTable",tabelaLigowa.ToList());
        }

        //GET: UpcomingMatches
        public ActionResult UpcomingMatches()
        {
            var mecze = db.Mecz.Include(m => m.Klub).Include(m => m.Klub1).Include(m => m.Kolejka).Include(m => m.Sedzia).Include(m => m.Sezon).Include(m => m.Stadion).Where(m => m.Data > DateTime.Now && (m.Klub.Nazwa == "Manchester United F.C." || m.Klub1.Nazwa == "Manchester United F.C"));
            return View(mecze.ToList());
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
        [Authorize(Roles = "PracownikKlubu")]
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
        [Authorize(Roles = "PracownikKlubu")]
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
            return View(mecz);
        }

        // GET: Mecz/Edit/5
        [Authorize(Roles = "PracownikKlubu")]
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
            ViewBag.IdKlub = mecz.IdKlubGoscie;
            return View(mecz);
        }

        // POST: Mecz/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "PracownikKlubu")]
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
            ViewBag.IdKlub = mecz.IdKlubGoscie;
            return View(mecz);
        }

        [Authorize(Roles = "PracownikKlubu")]
        public JsonResult FetchClubs(int id)
        {
            var kluby = db.Klub.Where(k => k.IdK != id).Select(k => new { IdK = k.IdK, Nazwa = k.Nazwa }).ToList();
            return Json(kluby, JsonRequestBehavior.AllowGet);
        }

        // GET: Mecz/Delete/5
        [Authorize(Roles = "PracownikKlubu")]
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
        [Authorize(Roles = "PracownikKlubu")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mecz mecz = db.Mecz.Find(id);
            db.Mecz.Remove(mecz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void CalculateClubsRating(int id)
        {
            Sezon ostatniSezon = null;
            var sezony = db.Sezon.ToList();
            var mecz = db.Mecz.Find(id);
            int formaGospodarze = 500;
            int formaGoscie = 500;
            if (mecz.Kolejka.Nr == 1)
            {
                if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 6)
                {
                    foreach (Sezon sezon in sezony)
                    {
                        if (sezon.RokOd == DateTime.Now.Year - 2 && sezon.RokDo == DateTime.Now.Year - 1)
                        {
                            ostatniSezon = sezon;
                        }
                    }
                }
                else
                {
                    foreach (Sezon sezon in sezony)
                    {
                        if (sezon.RokOd == DateTime.Now.Year - 1 && sezon.RokDo == DateTime.Now.Year)
                        {
                            ostatniSezon = sezon;
                        }
                    }
                }
                if (ostatniSezon != null) 
                {
                    var meczeGospodarzyWOstatnimSezonie = db.Mecz.Where(m => m.IdS == ostatniSezon.IdS && (m.IdKlubGospodarze == mecz.IdKlubGospodarze || m.IdKlubGoscie == mecz.IdKlubGospodarze)).OrderByDescending(m => m.Kolejka.Nr).ToList();
                    var ostatniMeczGospodarzy = meczeGospodarzyWOstatnimSezonie.First();
                    if (ostatniMeczGospodarzy.IdKlubGospodarze == mecz.IdKlubGospodarze)
                    {
                        formaGospodarze = ostatniMeczGospodarzy.FormaGospodarze;
                    }
                    else
                    {
                        formaGospodarze = ostatniMeczGospodarzy.FormaGoscie;
                    }
                    var meczeGoscirWOstatnimSezonie = db.Mecz.Where(m => m.IdS == ostatniSezon.IdS && (m.IdKlubGospodarze == mecz.IdKlubGoscie || m.IdKlubGoscie == mecz.IdKlubGoscie)).OrderByDescending(m => m.Kolejka.Nr).ToList();
                    var ostatniMeczGosci = meczeGospodarzyWOstatnimSezonie.First();
                    if (ostatniMeczGosci.IdKlubGospodarze == mecz.IdKlubGoscie)
                    {
                        formaGoscie = ostatniMeczGosci.FormaGospodarze;
                    }
                    else
                    {
                        formaGoscie = ostatniMeczGosci.FormaGoscie;
                    }
                }
            }
            else
            {
                var ostatniMeczGospodarzy = db.Mecz.Where(m => m.Kolejka.Nr == mecz.Kolejka.Nr - 1 && (m.IdKlubGospodarze == mecz.IdKlubGospodarze || m.IdKlubGoscie == mecz.IdKlubGospodarze)).ToList().First();
                if(ostatniMeczGospodarzy != null)
                {
                    if (ostatniMeczGospodarzy.IdKlubGospodarze == mecz.IdKlubGospodarze)
                    {
                        formaGospodarze = ostatniMeczGospodarzy.FormaGospodarze;
                    }
                    else
                    {
                        formaGospodarze = ostatniMeczGospodarzy.FormaGoscie;
                    }
                }
                var ostatniMeczGosci = db.Mecz.Where(m => m.Kolejka.Nr == mecz.Kolejka.Nr - 1 && (m.IdKlubGospodarze == mecz.IdKlubGoscie || m.IdKlubGoscie == mecz.IdKlubGoscie)).ToList().First();
                if (ostatniMeczGosci != null)
                {
                    if (ostatniMeczGosci.IdKlubGospodarze == mecz.IdKlubGoscie)
                    {
                        formaGoscie = ostatniMeczGosci.FormaGospodarze;
                    }
                    else
                    {
                        formaGoscie = ostatniMeczGosci.FormaGoscie;
                    }
                }
            }
            int roznica = Math.Abs(formaGospodarze - formaGoscie);
            double e = 1 / (Math.Pow(10, -roznica / 400) + 1);
            int k = 35;
            double deltaE = 0;
            if (mecz.PunktyGospodarze == 1 && mecz.PunktyGoscie == 1)
            {
                deltaE = (0.5 - e) * k;
                mecz.FormaGospodarze = formaGospodarze + (int)deltaE;
                mecz.FormaGoscie = formaGoscie + (int)deltaE;
            }
            else
            {
                deltaE = (1 - e) * k;
                if (mecz.PunktyGospodarze == 3 && mecz.PunktyGoscie == 0)
                {
                    mecz.FormaGospodarze = formaGospodarze + (int)deltaE;
                    mecz.FormaGoscie = formaGoscie - (int)deltaE;
                }
                else
                {
                    mecz.FormaGospodarze = formaGospodarze - (int)deltaE;
                    mecz.FormaGoscie = formaGoscie + (int)deltaE;
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(mecz).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // GET: Mecz/UpcomingMatch
        [Authorize(Roles = "Trener")]
        public ActionResult UpcomingMatch()
        {
            string klub = "Manchester United F.C.";
            string klubPrzeciwnika = "";
            var mecz = db.Mecz.Where(m => m.Data >= DateTime.Now && (m.Klub1.Nazwa == klub || m.Klub.Nazwa == klub)).OrderBy(m => m.Data).ToList().First();
            var wystepyZawodnikow = db.ZawodnikMecz.Where(z => z.Zawodnik.Klub.IdK == mecz.Klub1.IdK || z.Zawodnik.Klub.IdK == mecz.Klub.IdK).ToList();
            var zawodnicy = db.Zawodnik.Where(z => z.IdK == mecz.IdKlubGospodarze || z.IdK == mecz.IdKlubGoscie);
            if (mecz.Klub1.Nazwa == klub)
            {
                klubPrzeciwnika = mecz.Klub.Nazwa;
            }
            else
            {
                klubPrzeciwnika = mecz.Klub1.Nazwa;
            }
            Dictionary<int, double> oceny = new Dictionary<int, double>();
            Dictionary<int, string> pozycja = new Dictionary<int, string>();
            double sumaOcen = 0;
            int iloscWystepow = 0;
            double sumaOcenObroncy = 0;
            int iloscWystepowObroncy = 0;
            double sumaOcenPomocnicy = 0;
            int iloscWystepowPomocnicy = 0;
            double sumaOcenNapastnicy = 0;
            int iloscWystepowNapastnicy = 0;
            double sumaOcenObroncyPrzeciwnik = 0;
            int iloscWystepowObroncyPrzeciwnik = 0;
            double sumaOcenPomocnicyPrzeciwnik = 0;
            int iloscWystepowPomocnicyPrzeciwnik = 0;
            double sumaOcenNapastnicyPrzeciwnik = 0;
            int iloscWystepowNapastnicyPrzeciwnik = 0;
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
                        }else if (zawodnikMecz.Pozycja == "LO")
                        {
                            lo++;
                        }else if(zawodnikMecz.Pozycja == "ŚO")
                        {
                            so++;
                        }else if(zawodnikMecz.Pozycja == "PO")
                        {
                            po++;
                        }else if (zawodnikMecz.Pozycja == "ŚPD")
                        {
                            spd++;
                        }else if(zawodnikMecz.Pozycja == "ŚP")
                        {
                            sp++;
                        }else if(zawodnikMecz.Pozycja == "LP")
                        {
                            lp++;
                        }else if(zawodnikMecz.Pozycja == "PP")
                        {
                            pp++;
                        }else if(zawodnikMecz.Pozycja == "ŚPO")
                        {
                            spo++;
                        }else if(zawodnikMecz.Pozycja == "CN")
                        {
                            cn++;
                        }else if(zawodnikMecz.Pozycja == "N")
                        {
                            n++;
                        }else if(zawodnikMecz.Pozycja == "LS")
                        {
                            ls++;
                        }else if(zawodnikMecz.Pozycja == "PS")
                        {
                            ps++;
                        }
                        sumaOcen += zawodnikMecz.Forma;
                        iloscWystepow++;
                        if (((zawodnikMecz.Mecz.Klub1.Nazwa == klub && zawodnikMecz.Zawodnik.IdK == zawodnikMecz.Mecz.Klub1.IdK) || (zawodnikMecz.Mecz.Klub.Nazwa == klub && zawodnikMecz.Zawodnik.IdK == zawodnikMecz.Mecz.Klub.IdK)) && (zawodnikMecz.Pozycja == "PO" || zawodnikMecz.Pozycja == "ŚO" || zawodnikMecz.Pozycja == "LO"))
                        {
                            sumaOcenObroncy += zawodnikMecz.Forma;
                            iloscWystepowObroncy++;
                        } else if (((zawodnikMecz.Mecz.Klub1.Nazwa == klub && zawodnikMecz.Zawodnik.IdK == zawodnikMecz.Mecz.Klub1.IdK) || (zawodnikMecz.Mecz.Klub.Nazwa == klub && zawodnikMecz.Zawodnik.IdK == zawodnikMecz.Mecz.Klub.IdK)) && (zawodnikMecz.Pozycja == "ŚPD" || zawodnikMecz.Pozycja == "PP" || zawodnikMecz.Pozycja == "ŚP" || zawodnikMecz.Pozycja == "LP" || zawodnikMecz.Pozycja == "ŚPO"))
                        {
                            sumaOcenPomocnicy += zawodnikMecz.Forma;
                            iloscWystepowPomocnicy++;
                        } else if (((zawodnikMecz.Mecz.Klub1.Nazwa == klub && zawodnikMecz.Zawodnik.IdK == zawodnikMecz.Mecz.Klub1.IdK) || (zawodnikMecz.Mecz.Klub.Nazwa == klub && zawodnikMecz.Zawodnik.IdK == zawodnikMecz.Mecz.Klub.IdK)) && (zawodnikMecz.Pozycja == "CN" || zawodnikMecz.Pozycja == "PS" || zawodnikMecz.Pozycja == "N" || zawodnikMecz.Pozycja == "LS"))
                        {
                            sumaOcenNapastnicy += zawodnikMecz.Forma;
                            iloscWystepowNapastnicy++;
                        }else if (((zawodnikMecz.Mecz.Klub1.Nazwa != klub && zawodnikMecz.Zawodnik.IdK == zawodnikMecz.Mecz.Klub1.IdK) || (zawodnikMecz.Mecz.Klub.Nazwa != klub && zawodnikMecz.Zawodnik.IdK == zawodnikMecz.Mecz.Klub.IdK)) && (zawodnikMecz.Pozycja == "PO" || zawodnikMecz.Pozycja == "ŚO" || zawodnikMecz.Pozycja == "LO"))
                        {
                            sumaOcenObroncyPrzeciwnik += zawodnikMecz.Forma;
                            iloscWystepowObroncyPrzeciwnik++;
                        }
                        else if (((zawodnikMecz.Mecz.Klub1.Nazwa != klub && zawodnikMecz.Zawodnik.IdK == zawodnikMecz.Mecz.Klub1.IdK) || (zawodnikMecz.Mecz.Klub.Nazwa != klub && zawodnikMecz.Zawodnik.IdK == zawodnikMecz.Mecz.Klub.IdK)) && (zawodnikMecz.Pozycja == "ŚPD" || zawodnikMecz.Pozycja == "PP" || zawodnikMecz.Pozycja == "ŚP" || zawodnikMecz.Pozycja == "LP" || zawodnikMecz.Pozycja == "ŚPO"))
                        {
                            sumaOcenPomocnicyPrzeciwnik += zawodnikMecz.Forma;
                            iloscWystepowPomocnicyPrzeciwnik++;
                        }
                        else if (((zawodnikMecz.Mecz.Klub1.Nazwa != klub && zawodnikMecz.Zawodnik.IdK == zawodnikMecz.Mecz.Klub1.IdK) || (zawodnikMecz.Mecz.Klub.Nazwa != klub && zawodnikMecz.Zawodnik.IdK == zawodnikMecz.Mecz.Klub.IdK)) && (zawodnikMecz.Pozycja == "CN" || zawodnikMecz.Pozycja == "PS" || zawodnikMecz.Pozycja == "N" || zawodnikMecz.Pozycja == "LS"))
                        {
                            sumaOcenNapastnicyPrzeciwnik += zawodnikMecz.Forma;
                            iloscWystepowNapastnicyPrzeciwnik++;
                        }
                    }
                }
                if (iloscWystepow != 0)
                {
                    if(br >= lo && br >= so && br >= po && br >= spd && br >= sp && br >= lp && br >= pp && br >= spo && br >= cn && br >= n && br >= ls && br >= ps)
                    {
                        pozycja[zawodnik.IdZ] = "BR";
                    }else if (lo >= br && lo >= so && lo >= po && lo >= spd && lo >= sp && lo >= lp && lo >= pp && lo >= spo && lo >= cn && lo >= n && lo >= ls && lo >= ps)
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
            int iloscBr = 0;
            int iloscLo = 0;
            int iloscSo = 0;
            int iloscPo = 0;
            int iloscSpd = 0;
            int iloscSp = 0;
            int iloscLp = 0;
            int iloscPp = 0;
            int iloscSpo = 0;
            int iloscN = 0;
            int iloscCn = 0;
            int iloscLs = 0;
            int iloscPs = 0;
            int iloscBrPrzeciwnik = 0;
            int iloscLoPrzeciwnik = 0;
            int iloscSoPrzeciwnik = 0;
            int iloscPoPrzeciwnik = 0;
            int iloscSpdPrzeciwnik = 0;
            int iloscSpPrzeciwnik = 0;
            int iloscLpPrzeciwnik = 0;
            int iloscPpPrzeciwnik = 0;
            int iloscSpoPrzeciwnik = 0;
            int iloscNPrzeciwnik = 0;
            int iloscCnPrzeciwnik = 0;
            int iloscLsPrzeciwnik = 0;
            int iloscPsPrzeciwnik = 0;
            var pozycjaArray = pozycja.ToArray();
            for (int i=0;i<pozycjaArray.Length;i++)
            {
                if(db.Zawodnik.Find(pozycjaArray[i].Key).Klub.Nazwa == klub)
                {
                    if(pozycjaArray[i].Value == "BR")
                    {
                        iloscBr++;
                    }
                    else if (pozycjaArray[i].Value == "LO")
                    {
                        iloscLo++;
                    }
                    else if (pozycjaArray[i].Value == "ŚO")
                    {
                        iloscSo++;
                    }
                    else if (pozycjaArray[i].Value == "PO")
                    {
                        iloscPo++;
                    }
                    else if (pozycjaArray[i].Value == "ŚPD")
                    {
                        iloscSpd++;
                    }
                    else if (pozycjaArray[i].Value == "ŚP")
                    {
                        iloscSp++;
                    }
                    else if (pozycjaArray[i].Value == "LP")
                    {
                        iloscLp++;
                    }
                    else if (pozycjaArray[i].Value == "PP")
                    {
                        iloscPp++;
                    }
                    else if (pozycjaArray[i].Value == "ŚPO")
                    {
                        iloscSpo++;
                    }
                    else if (pozycjaArray[i].Value == "CN")
                    {
                        iloscCn++;
                    }
                    else if (pozycjaArray[i].Value == "N")
                    {
                        iloscN++;
                    }
                    else if (pozycjaArray[i].Value == "LS")
                    {
                        iloscLs++;
                    }
                    else if (pozycjaArray[i].Value == "PS")
                    {
                        iloscPs++;
                    }
                }
                else
                {
                    if (pozycjaArray[i].Value == "BR")
                    {
                        iloscBrPrzeciwnik++;
                    }
                    else if (pozycjaArray[i].Value == "LO")
                    {
                        iloscLoPrzeciwnik++;
                    }
                    else if (pozycjaArray[i].Value == "ŚO")
                    {
                        iloscSoPrzeciwnik++;
                    }
                    else if (pozycjaArray[i].Value == "PO")
                    {
                        iloscPoPrzeciwnik++;
                    }
                    else if (pozycjaArray[i].Value == "ŚPD")
                    {
                        iloscSpdPrzeciwnik++;
                    }
                    else if (pozycjaArray[i].Value == "ŚP")
                    {
                        iloscSpPrzeciwnik++;
                    }
                    else if (pozycjaArray[i].Value == "LP")
                    {
                        iloscLpPrzeciwnik++;
                    }
                    else if (pozycjaArray[i].Value == "PP")
                    {
                        iloscPpPrzeciwnik++;
                    }
                    else if (pozycjaArray[i].Value == "ŚPO")
                    {
                        iloscSpoPrzeciwnik++;
                    }
                    else if (pozycjaArray[i].Value == "CN")
                    {
                        iloscCnPrzeciwnik++;
                    }
                    else if (pozycjaArray[i].Value == "N")
                    {
                        iloscNPrzeciwnik++;
                    }
                    else if (pozycjaArray[i].Value == "LS")
                    {
                        iloscLsPrzeciwnik++;
                    }
                    else if (pozycjaArray[i].Value == "PS")
                    {
                        iloscPsPrzeciwnik++;
                    }
                }
            }
            double[] ocenyFormacji = new double[3];
            double[] ocenyFormacjiPrzeciwnik = new double[3];
            ocenyFormacji[0] = (sumaOcenObroncy / iloscWystepowObroncy) / 10;
            ocenyFormacji[1] = (sumaOcenPomocnicy / iloscWystepowPomocnicy) / 10;
            ocenyFormacji[2] = (sumaOcenNapastnicy / iloscWystepowNapastnicy) / 10;
            ocenyFormacjiPrzeciwnik[0] = (sumaOcenObroncyPrzeciwnik / iloscWystepowObroncyPrzeciwnik) / 10;
            ocenyFormacjiPrzeciwnik[1] = (sumaOcenPomocnicyPrzeciwnik / iloscWystepowPomocnicyPrzeciwnik) / 10;
            ocenyFormacjiPrzeciwnik[2] = (sumaOcenNapastnicyPrzeciwnik / iloscWystepowNapastnicyPrzeciwnik) / 10;
            List<Zawodnik> zawodnicyWyswietl = new List<Zawodnik>();
            int idMeczPierwszy = -1;
            int idMeczPierwszyPrzeciwnik = -1;
            if (DateTime.Now.Month >= 7)
            {
                idMeczPierwszy = db.Mecz.Where(m => m.Sezon.RokOd == DateTime.Now.Year && (m.Klub1.Nazwa == klub || m.Klub.Nazwa == klub)).OrderBy(m => m.Data).First().IdM;
                idMeczPierwszyPrzeciwnik = db.Mecz.Where(m => m.Sezon.RokOd == DateTime.Now.Year && (m.Klub1.Nazwa == klubPrzeciwnika || m.Klub.Nazwa == klubPrzeciwnika)).OrderBy(m => m.Data).First().IdM;
            }
            else
            {
                idMeczPierwszy = db.Mecz.Where(m => m.Sezon.RokDo == DateTime.Now.Year && (m.Klub1.Nazwa == klub || m.Klub.Nazwa == klub)).OrderBy(m => m.Data).First().IdM;
                idMeczPierwszyPrzeciwnik = db.Mecz.Where(m => m.Sezon.RokDo == DateTime.Now.Year && (m.Klub1.Nazwa == klubPrzeciwnika || m.Klub.Nazwa == klubPrzeciwnika)).OrderBy(m => m.Data).First().IdM;
            }
            var meczPierwszy = db.Mecz.Find(idMeczPierwszy);
            var meczPierwszyPrzeciwnik = db.Mecz.Find(idMeczPierwszyPrzeciwnik);
            int iloscBrSklad = 0;
            int iloscLoSklad = 0;
            int iloscSoSklad = 0;
            int iloscPoSklad = 0;
            int iloscSpdSklad = 0;
            int iloscSpSklad = 0;
            int iloscLpSklad = 0;
            int iloscPpSklad = 0;
            int iloscSpoSklad = 0;
            int iloscNSklad = 0;
            int iloscCnSklad = 0;
            int iloscLsSklad = 0;
            int iloscPsSklad = 0;
            int iloscBrPrzeciwnikSklad = 0;
            int iloscLoPrzeciwnikSklad = 0;
            int iloscSoPrzeciwnikSklad = 0;
            int iloscPoPrzeciwnikSklad = 0;
            int iloscSpdPrzeciwnikSklad = 0;
            int iloscSpPrzeciwnikSklad = 0;
            int iloscLpPrzeciwnikSklad = 0;
            int iloscPpPrzeciwnikSklad = 0;
            int iloscSpoPrzeciwnikSklad = 0;
            int iloscNPrzeciwnikSklad = 0;
            int iloscCnPrzeciwnikSklad = 0;
            int iloscLsPrzeciwnikSklad = 0;
            int iloscPsPrzeciwnikSklad = 0;
            if (meczPierwszy != null && meczPierwszyPrzeciwnik != null)
            {
                var zawodnicyMeczPierwszy = db.ZawodnikMecz.Where(zm => zm.IdM == idMeczPierwszy && zm.Zawodnik.Klub.Nazwa == klub && zm.MinutyOd == 1);
                var zawodnicyMeczPierwszyPrzeciwnik = db.ZawodnikMecz.Where(zm => zm.IdM == idMeczPierwszyPrzeciwnik && zm.Zawodnik.Klub.Nazwa == klubPrzeciwnika && zm.MinutyOd == 1);
                foreach (ZawodnikMecz zawodnikMecz in zawodnicyMeczPierwszy)
                {
                    if (pozycja[zawodnikMecz.IdZ] == "LO")
                    {
                        iloscLoSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "ŚO")
                    {
                        iloscSoSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "PO")
                    {
                        iloscPoSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "ŚPD")
                    {
                        iloscSpdSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "ŚP")
                    {
                        iloscSpSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "LP")
                    {
                        iloscLpSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "PP")
                    {
                        iloscPpSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "ŚPO")
                    {
                        iloscSpoSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "CN")
                    {
                        iloscCnSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "N")
                    {
                        iloscNSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "LS")
                    {
                        iloscLsSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "PS")
                    {
                        iloscPsSklad++;
                    }
                    zawodnicyWyswietl.Add(zawodnikMecz.Zawodnik);
                }
                foreach (ZawodnikMecz zawodnikMecz in zawodnicyMeczPierwszyPrzeciwnik)
                {
                    if (pozycja[zawodnikMecz.IdZ] == "LO")
                    {
                        iloscLoPrzeciwnikSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "ŚO")
                    {
                        iloscSoPrzeciwnikSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "PO")
                    {
                        iloscPoPrzeciwnikSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "ŚPD")
                    {
                        iloscSpdPrzeciwnikSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "ŚP")
                    {
                        iloscSpPrzeciwnikSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "LP")
                    {
                        iloscLpPrzeciwnikSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "PP")
                    {
                        iloscPpPrzeciwnikSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "ŚPO")
                    {
                        iloscSpoPrzeciwnikSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "CN")
                    {
                        iloscCnPrzeciwnikSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "N")
                    {
                        iloscNPrzeciwnikSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "LS")
                    {
                        iloscLsPrzeciwnikSklad++;
                    }
                    else if (pozycja[zawodnikMecz.IdZ] == "PS")
                    {
                        iloscPsPrzeciwnikSklad++;
                    }
                    zawodnicyWyswietl.Add(zawodnikMecz.Zawodnik);
                }
                foreach(Zawodnik zawodnik in zawodnicy)
                {
                    Zawodnik zawodnikWyswietl = zawodnicyWyswietl.Find(z => z.IdK == zawodnik.IdK && pozycja.ContainsKey(zawodnik.IdZ) && pozycja[zawodnik.IdZ] == pozycja[z.IdZ] && oceny.ContainsKey(zawodnik.IdZ) && oceny[zawodnik.IdZ] > oceny[z.IdZ]);
                    Boolean czyWyswietlic = true;
                    foreach(Zawodnik zawodnikWys in zawodnicyWyswietl)
                    {
                        if (zawodnik.Equals(zawodnikWys))
                        {
                            czyWyswietlic = false;
                        }
                    }
                    if (zawodnikWyswietl != null && czyWyswietlic)
                    {
                        zawodnicyWyswietl.Remove(zawodnikWyswietl);
                        zawodnicyWyswietl.Add(zawodnik);
                    }
                }
            }
            bool czyPusty = false;
            if (zawodnicyWyswietl.Count == 0)
            {
                czyPusty = true;
            }
            ViewBag.IloscBr = iloscBrSklad;
            ViewBag.IloscLo = iloscLoSklad;
            ViewBag.IloscSo = iloscSoSklad;
            ViewBag.IloscPo = iloscPoSklad;
            ViewBag.IloscSpd = iloscSpdSklad;
            ViewBag.IloscSp = iloscSpSklad;
            ViewBag.IloscLp = iloscLpSklad;
            ViewBag.IloscPp = iloscPpSklad;
            ViewBag.IloscSpo = iloscSpoSklad;
            ViewBag.IloscN = iloscNSklad;
            ViewBag.IloscCn = iloscCnSklad;
            ViewBag.IloscLs = iloscLsSklad;
            ViewBag.IloscPs = iloscPsSklad;
            ViewBag.IloscBrPrzeciwnik = iloscBrPrzeciwnikSklad;
            ViewBag.IloscLoPrzeciwnik = iloscLoPrzeciwnikSklad;
            ViewBag.IloscSoPrzeciwnik = iloscSoPrzeciwnikSklad;
            ViewBag.IloscPoPrzeciwnik = iloscPoPrzeciwnikSklad;
            ViewBag.IloscSpdPrzeciwnik = iloscSpdPrzeciwnikSklad;
            ViewBag.IloscSpPrzeciwnik = iloscSpPrzeciwnikSklad;
            ViewBag.IloscLpPrzeciwnik = iloscLpPrzeciwnikSklad;
            ViewBag.IloscPpPrzeciwnik = iloscPpPrzeciwnikSklad;
            ViewBag.IloscSpoPrzeciwnik = iloscSpoPrzeciwnikSklad;
            ViewBag.IloscNPrzeciwnik = iloscNPrzeciwnikSklad;
            ViewBag.IloscCnPrzeciwnik = iloscCnPrzeciwnikSklad;
            ViewBag.IloscLsPrzeciwnik = iloscLsPrzeciwnikSklad;
            ViewBag.IloscPsPrzeciwnik = iloscPsPrzeciwnikSklad;
            ViewBag.OcenyFormacji = ocenyFormacji;
            ViewBag.OcenyFormacjiPrzeciwnik = ocenyFormacjiPrzeciwnik;
            ViewBag.Oceny = oceny;
            ViewBag.Pozycja = pozycja;
            ViewBag.Klub = klub;
            ViewBag.KlubPrzeciwnika = klubPrzeciwnika;
            ViewBag.CzyPusty = czyPusty;
            return View(zawodnicyWyswietl);
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
