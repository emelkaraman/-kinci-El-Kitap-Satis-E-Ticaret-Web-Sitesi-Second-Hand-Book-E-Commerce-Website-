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
    
    public partial class KitapSiparisEdilir
    {
        public int ID { get; set; }
        public int KitapID { get; set; }
        public int SiparisID { get; set; }
        public decimal BirimFiyat { get; set; }
        public int UrunAdedi { get; set; }
        public int SaticiID { get; set; }
    
        public virtual Kitap Kitap { get; set; }
        public virtual Siparis Siparis { get; set; }
        public virtual Users Users { get; set; }
    }
}
