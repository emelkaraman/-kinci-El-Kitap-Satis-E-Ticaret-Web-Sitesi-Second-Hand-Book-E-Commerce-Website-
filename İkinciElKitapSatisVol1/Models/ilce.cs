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
    
    public partial class ilce
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ilce()
        {
            this.Users = new HashSet<Users>();
        }
    
        public int ilceKodu { get; set; }
        public string ilceAdi { get; set; }
        public int ilKodu { get; set; }
    
        public virtual il il { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
