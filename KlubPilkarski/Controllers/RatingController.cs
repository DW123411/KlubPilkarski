using KlubPilkarski.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KlubPilkarski.Controllers
{
    public class RatingController : Controller
    {
        private PlayerRatingModel ratingModel = new PlayerRatingModel();

        public int CalculateRating(ZawodnikMecz zawodnikMecz)
        {
            double rating = 0;
            double parametrAtak = 1;
            double parametrObrona = 1;
            if (zawodnikMecz.Pozycja == "PO" || zawodnikMecz.Pozycja == "ŚO" || zawodnikMecz.Pozycja == "LO" || zawodnikMecz.Pozycja == "ŚPD")
            {
                parametrAtak = 0.5;
                parametrObrona = 1.5;
                rating = CalculateStats(zawodnikMecz, parametrAtak, parametrObrona, false);
                if(zawodnikMecz.OdbioryUdane == 0)
                {
                    rating -= 5;
                }
                if(zawodnikMecz.StraconeBramki != null)
                {
                    rating += ratingModel.ParametrStraconeBramki * (double)zawodnikMecz.StraconeBramki;
                }
                if (rating > 100)
                {
                    rating = 100;
                }
                if (rating < 0)
                {
                    rating = 0;
                }
                return (int)rating;
            }else if (zawodnikMecz.Pozycja == "PP" || zawodnikMecz.Pozycja == "ŚP" || zawodnikMecz.Pozycja == "LP")
            {
                rating = CalculateStats(zawodnikMecz, parametrAtak, parametrObrona, false);
                if (zawodnikMecz.StraconeBramki != null)
                {
                    rating += 0.25 * ratingModel.ParametrStraconeBramki * (double)zawodnikMecz.StraconeBramki;
                }
                if (rating > 100)
                {
                    rating = 100;
                }
                if (rating < 0)
                {
                    rating = 0;
                }
                return (int)rating;
            }else if (zawodnikMecz.Pozycja == "ŚPO" || zawodnikMecz.Pozycja == "CN" || zawodnikMecz.Pozycja == "PS" || zawodnikMecz.Pozycja == "N" || zawodnikMecz.Pozycja == "LS")
            {
                parametrAtak = 1.5;
                parametrObrona = 0.5;
                rating = CalculateStats(zawodnikMecz, parametrAtak, parametrObrona, false);
                if(zawodnikMecz.Pozycja == "N")
                {
                    if (zawodnikMecz.Bramki == 0) 
                    {
                        rating -= 5;
                    }
                    if (zawodnikMecz.StraconeBramki != null) 
                    {
                        rating += 0.25 * ratingModel.ParametrStraconeBramki * (double)zawodnikMecz.StraconeBramki;
                    }
                }
                else
                {
                    if (zawodnikMecz.StraconeBramki != null)
                    {
                        rating += 0.25 * ratingModel.ParametrStraconeBramki * (double)zawodnikMecz.StraconeBramki;
                    }
                }
                if (rating > 100)
                {
                    rating = 100;
                }
                if (rating < 0)
                {
                    rating = 0;
                }
                return (int)rating;
            }
            else
            {
                rating = CalculateStats(zawodnikMecz, parametrAtak, parametrObrona, true);
                if (rating > 100)
                {
                    rating = 100;
                }
                if (rating < 0)
                {
                    rating = 0;
                }
                return (int)rating;
            }
        }

        private double CalculateStats(ZawodnikMecz zawodnikMecz, double parametrAtak, double parametrObrona, Boolean czyBramkarz)
        {
            double result = 50;
            if (czyBramkarz)
            {
                if (zawodnikMecz.StraconeBramki != null) 
                {
                    result += ratingModel.ParametrStraconeBramki * (double)zawodnikMecz.StraconeBramki;
                }
                if (zawodnikMecz.ObronaWyskok != null)
                {
                    result += ratingModel.ParametrObronaWyskok * (double)zawodnikMecz.ObronaWyskok;
                }
                if (zawodnikMecz.ObronaPoleKarne != null)
                {
                    result += ratingModel.ParametrObronaPoleKarne * (double)zawodnikMecz.ObronaPoleKarne;
                }
                if (zawodnikMecz.ObronaWyjscie != null)
                {
                    result += ratingModel.ParametrObronaWyjscie * (double)zawodnikMecz.ObronaWyjscie;
                }
                if (zawodnikMecz.Piastkowania != null)
                {
                    result += ratingModel.ParametrPiastkowania * (double)zawodnikMecz.Piastkowania;
                }
                if (zawodnikMecz.ObronaWysokiejPilki != null)
                {
                    result += ratingModel.ParametrObronaWysokiejPilki * (double)zawodnikMecz.ObronaWysokiejPilki;
                }
                if (zawodnikMecz.KartkiCzerwone == 0) 
                {
                    if (zawodnikMecz.KartkiZolte != null)
                    {
                        result += ratingModel.ParametrKartkiZolte * (double)zawodnikMecz.KartkiZolte;
                    }
                }
                if (zawodnikMecz.KartkiCzerwone != null)
                {
                    result += ratingModel.ParametrKartkiCzerwone * (double)zawodnikMecz.KartkiCzerwone;
                }
                if (zawodnikMecz.PodaniaUdanePolowaWlasna != null)
                {
                    result += ratingModel.ParametrPodaniaUdanePolowaWlasna * (double)zawodnikMecz.PodaniaUdanePolowaWlasna;
                }
                if (zawodnikMecz.PodaniaUdanePolowaPrzeciwnika != null)
                {
                    result += ratingModel.ParametrPodaniaUdanePolowaPrzeciwnika * (double)zawodnikMecz.PodaniaUdanePolowaPrzeciwnika;
                }
                if (zawodnikMecz.PodaniaNieudane != null)
                {
                    result += ratingModel.ParametrPodaniaNieudane * (double)zawodnikMecz.PodaniaNieudane;
                }
                if (zawodnikMecz.Dosrodkowania != null)
                {
                    result += ratingModel.ParametrDosrodkowania * (double)zawodnikMecz.Dosrodkowania;
                }
                if (zawodnikMecz.DlugiePodaniaUdane != null)
                {
                    result += ratingModel.ParametrDlugiePodaniaUdane * (double)zawodnikMecz.DlugiePodaniaUdane;
                }
                if (zawodnikMecz.DlugiePodaniaNieudane != null)
                {
                    result += ratingModel.ParametrDlugiePodaniaNieudane * (double)zawodnikMecz.DlugiePodaniaNieudane;
                }
                if (zawodnikMecz.KluczowePodania != null)
                {
                    result += ratingModel.ParametrKluczowePodania * (double)zawodnikMecz.KluczowePodania;
                }
                if (zawodnikMecz.Kontakty != null)
                {
                    result += ratingModel.ParametrKontakty * (double)zawodnikMecz.Kontakty;
                }
                if (zawodnikMecz.Wyrzuty
                    != null)
                {
                    result += ratingModel.ParametrWyrzuty * (double)zawodnikMecz.Wyrzuty;
                }
                return result;
            }
            else
            {
                if (zawodnikMecz.Bramki != null)
                {
                    result += parametrAtak * ratingModel.ParametrBramki * (double)zawodnikMecz.Bramki;
                }
                if (zawodnikMecz.Asysty != null)
                {
                    result += parametrAtak * ratingModel.ParametrAsysty * (double)zawodnikMecz.Asysty;
                }
                if (zawodnikMecz.UtworzoneSzanse != null)
                {
                    result += parametrAtak * ratingModel.ParametrUtworzoneSzanse * (double)zawodnikMecz.UtworzoneSzanse;
                }
                if (zawodnikMecz.StrzalyCelne != null)
                {
                    result += parametrAtak * ratingModel.ParametrStrzalyCelne * (double)zawodnikMecz.StrzalyCelne;
                }
                if (zawodnikMecz.StrzalyNiecelne != null)
                {
                    result += parametrAtak * ratingModel.ParametrStrzalyNiecelne * (double)zawodnikMecz.StrzalyNiecelne;
                }
                if (zawodnikMecz.StrzalyZablokowane != null)
                {
                    result += parametrAtak * ratingModel.ParametrStrzalyZablokowane * (double)zawodnikMecz.StrzalyZablokowane;
                }
                if (zawodnikMecz.SlupkiPoprzeczki != null)
                {
                    result += parametrAtak * ratingModel.ParametrSlupkiPoprzeczki * (double)zawodnikMecz.SlupkiPoprzeczki;
                }
                if (zawodnikMecz.KartkiCzerwone == 0)
                {
                    if (zawodnikMecz.KartkiZolte != null)
                    {
                        result += ratingModel.ParametrKartkiZolte * (double)zawodnikMecz.KartkiZolte;
                    }
                }
                if (zawodnikMecz.KartkiCzerwone != null)
                {
                    result += ratingModel.ParametrKartkiCzerwone * (double)zawodnikMecz.KartkiCzerwone;
                }
                if (zawodnikMecz.PodaniaUdanePolowaWlasna != null)
                {
                    result += ratingModel.ParametrPodaniaUdanePolowaWlasna * (double)zawodnikMecz.PodaniaUdanePolowaWlasna;
                }
                if (zawodnikMecz.PodaniaUdanePolowaPrzeciwnika != null)
                {
                    result += ratingModel.ParametrPodaniaUdanePolowaPrzeciwnika * (double)zawodnikMecz.PodaniaUdanePolowaPrzeciwnika;
                }
                if (zawodnikMecz.PodaniaNieudane != null)
                {
                    result += ratingModel.ParametrPodaniaNieudane * (double)zawodnikMecz.PodaniaNieudane;
                }
                if (zawodnikMecz.Dosrodkowania != null)
                {
                    result += ratingModel.ParametrDosrodkowania * (double)zawodnikMecz.Dosrodkowania;
                }
                if (zawodnikMecz.DlugiePodaniaUdane != null)
                {
                    result += ratingModel.ParametrDlugiePodaniaUdane * (double)zawodnikMecz.DlugiePodaniaUdane;
                }
                if (zawodnikMecz.DlugiePodaniaNieudane != null)
                {
                    result += ratingModel.ParametrDlugiePodaniaNieudane * (double)zawodnikMecz.DlugiePodaniaNieudane;
                }
                if (zawodnikMecz.KluczowePodania != null)
                {
                    result += ratingModel.ParametrKluczowePodania * (double)zawodnikMecz.KluczowePodania;
                }
                if (zawodnikMecz.Kontakty != null)
                {
                    result += ratingModel.ParametrKontakty * (double)zawodnikMecz.Kontakty;
                }
                if (zawodnikMecz.Wyrzuty != null)
                {
                    result += ratingModel.ParametrWyrzuty * (double)zawodnikMecz.Wyrzuty;
                }
                if (zawodnikMecz.RzutyRozne != null)
                {
                    result += ratingModel.ParametrRzutyRozne * (double)zawodnikMecz.RzutyRozne;
                }
                if (zawodnikMecz.PojedynkiWygrane != null)
                {
                    result += parametrObrona * ratingModel.ParametrPojedynkiWygrane * (double)zawodnikMecz.PojedynkiWygrane;
                }
                if (zawodnikMecz.PojedynkiPrzegrane != null)
                {
                    result += parametrObrona * ratingModel.ParametrPojedynkiPrzegrane * (double)zawodnikMecz.PojedynkiPrzegrane;
                }
                if (zawodnikMecz.Wybicia != null)
                {
                    result += parametrObrona * ratingModel.ParametrWybicia * (double)zawodnikMecz.Wybicia;
                }
                if (zawodnikMecz.DryblingiUdane != null)
                {
                    result += parametrAtak * ratingModel.ParametrDryblingiUdane * (double)zawodnikMecz.DryblingiUdane;
                }
                if (zawodnikMecz.Straty != null)
                {
                    result += parametrObrona * ratingModel.ParametrStraty * (double)zawodnikMecz.Straty;
                }
                if (zawodnikMecz.Faulowany != null)
                {
                    result += ratingModel.ParametrFaulowany * (double)zawodnikMecz.Faulowany;
                }
                if (zawodnikMecz.Faule != null)
                {
                    result += ratingModel.ParametrFaule * (double)zawodnikMecz.Faule;
                }
                if (zawodnikMecz.OdbioryUdane != null)
                {
                    result += parametrObrona * ratingModel.ParametrOdbioryUdane * (double)zawodnikMecz.OdbioryUdane;
                }
                if (zawodnikMecz.OdbioryNieudane != null)
                {
                    result += parametrObrona * ratingModel.ParametrOdbioryNieudane * (double)zawodnikMecz.OdbioryNieudane;
                }
                if (zawodnikMecz.GlowkiWygrane != null)
                {
                    result += ratingModel.ParametrGlowkiWygrane * (double)zawodnikMecz.GlowkiWygrane;
                }
                if (zawodnikMecz.GlowkiPrzegrane != null)
                {
                    result += ratingModel.ParametrGlowkiPrzegrane * (double)zawodnikMecz.GlowkiPrzegrane;
                }
                if (zawodnikMecz.Przejecia != null)
                {
                    result += ratingModel.ParametrPrzejecia * (double)zawodnikMecz.Przejecia;
                }
                if (zawodnikMecz.Spalone != null)
                {
                    result += ratingModel.ParametrSpalone * (double)zawodnikMecz.Spalone;
                }
                if (zawodnikMecz.OdzyskanePilki != null)
                {
                    result += parametrObrona * ratingModel.ParametrOdzyskanePilki * (double)zawodnikMecz.OdzyskanePilki;
                }
                return result;
            }
        } 
    }
}