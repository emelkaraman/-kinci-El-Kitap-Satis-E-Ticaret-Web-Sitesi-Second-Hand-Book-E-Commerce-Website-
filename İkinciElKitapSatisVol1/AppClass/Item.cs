using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using İkinciElKitapSatisVol1.Models;

namespace İkinciElKitapSatisVol1.AppClass
{
    public class Item
    {
        public Kitap kitaplar { get; set; }
        public int adet { get; set; }
        public decimal tutar { get; set; }
        
    }
}