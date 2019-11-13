using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KlubPilkarski.Models
{
    public class PlayerRatingModel
    {
        [Required]
        public double ParametrBramki { get; set; }
        [Required]
        public double ParametrAsysty { get; set; }
        [Required]
        public double ParametrUtworzoneSzanse { get; set; }
        [Required]
        public double ParametrStrzalyCelne { get; set; }
        [Required]
        public double ParametrStrzalyNiecelne { get; set; }
        [Required]
        public double ParametrStrzalyZablokowane { get; set; }
        [Required]
        public double ParametrSlupkiPoprzeczki { get; set; }
        public double ParametrKartkiZolte { get; set; }
        [Required]
        public double ParametrKartkiCzerwone { get; set; }
        [Required]
        public double ParametrPodaniaUdanePolowaWlasna { get; set; }
        [Required]
        public double ParametrPodaniaUdanePolowaPrzeciwnika { get; set; }
        [Required]
        public double ParametrDosrodkowania { get; set; }
        [Required]
        public double ParametrDlugiePodaniaUdane { get; set; }
        [Required]
        public double ParametrDlugiePodaniaNieudane { get; set; }
        [Required]
        public double ParametrKluczowePodania { get; set; }
        [Required]
        public double ParametrKontakty { get; set; }
        [Required]
        public double ParametrRzutyRozne { get; set; }
        [Required]
        public double ParametrWyrzuty { get; set; }
        [Required]
        public double ParametrPojedynkiWygrane { get; set; }
        [Required]
        public double ParametrPojedynkiPrzegrane { get; set; }
        [Required]
        public double ParametrWybicia { get; set; }
        [Required]
        public double ParametrDryblingiUdane { get; set; }
        [Required]
        public double ParametrStraty { get; set; }
        [Required]
        public double ParametrFaulowany { get; set; }
        [Required]
        public double ParametrFaule { get; set; }
        [Required]
        public double ParametrOdbioryUdane { get; set; }
        [Required]
        public double ParametrOdbioryNieudane { get; set; }
        [Required]
        public double ParametrGlowkiWygrane { get; set; }
        [Required]
        public double ParametrGlowkiPrzegrane { get; set; }
        [Required]
        public double ParametrPrzejecia { get; set; }
        [Required]
        public double ParametrSpalone { get; set; }
        [Required]
        public double ParametrOdzyskanePilki { get; set; }
        [Required]
        public double ParametrPodaniaNieudane { get; set; }
        [Required]
        public double ParametrObronaWyskok { get; set; }
        [Required]
        public double ParametrObronaPoleKarne { get; set; }
        [Required]
        public double ParametrObronaWyjscie { get; set; }
        [Required]
        public double ParametrPiastkowania { get; set; }
        [Required]
        public double ParametrObronaWysokiejPilki { get; set; }
        [Required]
        public double ParametrStraconeBramki { get; set; }

        public PlayerRatingModel()
        {
            ParametrBramki = 10;
            ParametrAsysty = 5;
            ParametrUtworzoneSzanse = 3;
            ParametrStrzalyCelne = 2;
            ParametrStrzalyNiecelne = -2;
            ParametrStrzalyZablokowane = -1;
            ParametrSlupkiPoprzeczki = 1;
            ParametrKartkiZolte = -5;
            ParametrKartkiCzerwone = -30;
            ParametrPodaniaUdanePolowaWlasna = 0.2;
            ParametrPodaniaUdanePolowaPrzeciwnika = 0.4;
            ParametrDosrodkowania = 0.1;
            ParametrDlugiePodaniaUdane = 0.2;
            ParametrDlugiePodaniaNieudane = -0.1;
            ParametrKluczowePodania = 2;
            ParametrKontakty = 0.05;
            ParametrRzutyRozne = 0;
            ParametrWyrzuty = 0;
            ParametrPojedynkiWygrane = 1;
            ParametrPojedynkiPrzegrane = -1;
            ParametrWybicia = 0.2;
            ParametrDryblingiUdane = 1;
            ParametrStraty = -1;
            ParametrFaulowany = 1;
            ParametrFaule = -1;
            ParametrOdbioryUdane = 1;
            ParametrOdbioryNieudane = -1;
            ParametrGlowkiWygrane = 1;
            ParametrGlowkiPrzegrane = -1;
            ParametrPrzejecia = 2;
            ParametrSpalone = -0.5;
            ParametrOdzyskanePilki = 2;
            ParametrPodaniaNieudane = -0.3;
            ParametrObronaWyskok = 1;
            ParametrObronaPoleKarne = 3;
            ParametrObronaWyjscie = 2;
            ParametrPiastkowania = 2;
            ParametrObronaWysokiejPilki = 3;
            ParametrStraconeBramki = -5;
        }
    }
}