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
    
    public partial class Zawodnik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Zawodnik()
        {
            this.ZawodnikMecz = new HashSet<ZawodnikMecz>();
        }
    
        public int IdZ { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int IdK { get; set; }
        public string Opis { get; set; }
    
        public virtual Klub Klub { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ZawodnikMecz> ZawodnikMecz { get; set; }
    }
}