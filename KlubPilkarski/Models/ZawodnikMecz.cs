//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KlubPilkarski.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ZawodnikMecz
    {
        public int IdZM { get; set; }
        public int IdM { get; set; }
        public int IdZ { get; set; }
        public string Pozycja { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci mniejszej ni� 1")]
        public int MinutyOd { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci mniejszej ni� 1")]
        public int MinutyDo { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> Bramki { get; set; } = 0;
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> Asysty { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> UtworzoneSzanse { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> StrzalyCelne { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> StrzalyNiecelne { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> StrzalyZablokowane { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> SlupkiPoprzeczki { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> KartkiZolte { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> KartkiCzerwone { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> PodaniaUdanePolowaWlasna { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> PodaniaUdanePolowaPrzeciwnika { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> Dosrodkowania { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> DlugiePodaniaUdane { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> DlugiePodaniaNieudane { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> KluczowePodania { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> Kontakty { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> RzutyRozne { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> Wyrzuty { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> PojedynkiWygrane { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> PojedynkiPrzegrane { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> Wybicia { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> DryblingiUdane { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> Straty { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> Faulowany { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> Faule { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> OdbioryUdane { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> OdbioryNieudane { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> GlowkiWygrane { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> GlowkiPrzegrane { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> Przejecia { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> Spalone { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> OdzyskanePilki { get; set; }
        public int Forma { get; set; } = 0;
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> PodaniaNieudane { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> ObronaWyskok { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> ObronaPoleKarne { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> ObronaWyjscie { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> Piastkowania { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> ObronaWysokiejPilki { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Pole nie mo�e mie� warto�ci ujemnej")]
        public Nullable<int> StraconeBramki { get; set; }
    
        public double FormaView
        {
            get
            {
                return (double)Forma / 10;
            }
        }
        public virtual Mecz Mecz { get; set; }
        public virtual Zawodnik Zawodnik { get; set; }
    }
}
