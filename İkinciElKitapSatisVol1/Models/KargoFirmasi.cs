//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ─░kinciElKitapSatisVol1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KargoFirmasi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KargoFirmasi()
        {
            this.KargoFirmYetkili = new HashSet<KargoFirmYetkili>();
            this.Siparis = new HashSet<Siparis>();
        }
    
        public int KargoFirmasiID { get; set; }
        public string KargoFirmasiAdi { get; set; }
        public string KargoFirmasiTel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KargoFirmYetkili> KargoFirmYetkili { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparis> Siparis { get; set; }
    }
}
