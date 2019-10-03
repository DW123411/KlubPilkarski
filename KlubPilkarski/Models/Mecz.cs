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
    
    public partial class Mecz
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mecz()
        {
            this.ZawodnikMecz = new HashSet<ZawodnikMecz>();
        }
    
        public int IdM { get; set; }
        public int IdS { get; set; }
        public int IdKolejka { get; set; }
        public System.DateTime Data { get; set; }
        public int IdStadion { get; set; }
        public int IdKlubGospodarze { get; set; }
        public int IdKlubGoscie { get; set; }
        public int BramkiGospodarze { get; set; } = 0;
        public int BramkiGoscie { get; set; } = 0;
        public int PunktyGospodarze { get; set; } = 1;
        public int PunktyGoscie { get; set; } = 1;
        public int FormaGospodarze { get; set; } = 0;
        public int FormaGoscie { get; set; } = 0;
        public string Opis { get; set; }
        public int IdSedzia { get; set; }
        public int Kibice { get; set; }
    
        public virtual Klub Klub { get; set; }
        public virtual Klub Klub1 { get; set; }
        public virtual Kolejka Kolejka { get; set; }
        public virtual Sedzia Sedzia { get; set; }
        public virtual Sezon Sezon { get; set; }
        public virtual Stadion Stadion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ZawodnikMecz> ZawodnikMecz { get; set; }
    }
}
