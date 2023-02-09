using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using İkinciElKitapSatisVol1.Models;
using İkinciElKitapSatisVol1.Security;

namespace İkinciElKitapSatisVol1.Controllers
{
    public class SiparisController : Controller
    {
        // GET: Siparis
        İkinciElKitapSatisEntities1 k = new İkinciElKitapSatisEntities1();
        [MyAuthorization(Roles = "2 , 3")]
        public ActionResult Index()
        {
            string userName = HttpContext.User.Identity.Name;
            Users u = k.Users.FirstOrDefault(x => x.UserFirstName + x.UserLastName == userName);
            List<Siparis> siparis = k.Siparis.Where(x => x.MusteriID == u.UserID).ToList();
            ViewBag.VerilenSiparis=siparis;
            List<KitapSiparisEdilir> siparisEdilirs = k.KitapSiparisEdilir.Where(x => x.SaticiID == u.UserID).ToList();
            List<SiparişDetayı> siparis1 = new List<SiparişDetayı>();
            foreach(KitapSiparisEdilir item in siparisEdilirs)
            {
                var item2 = k.SiparişDetayı.FirstOrDefault(x => x.SiparisID == item.SiparisID);
                siparis1.Add(item2);
                
            }
            ViewBag.AlinanSiparis = siparis1;
            return View();
        }


        [HttpPost]
        [MyAuthorization(Roles = "3")]
        public bool SiparisSil(int id)
        {
            Siparis siparis = k.Siparis.FirstOrDefault(x => x.SiparisID == id );
            List<KitapSiparisEdilir> kitapSiparisEdilir = k.KitapSiparisEdilir.Where(x => x.SiparisID == id).ToList();
            Fatura fatura = k.Fatura.FirstOrDefault(x => x.SiparisID == id);
            Kitap kitap;
            if (siparis != null)
            {
                foreach (var item in kitapSiparisEdilir)
                {
                    kitap = k.Kitap.FirstOrDefault(x => x.KitapID == item.KitapID);
                    kitap.Adet += item.UrunAdedi;
                    k.KitapSiparisEdilir.Remove(item);
                    k.SaveChanges();
                }

                k.Fatura.Remove(fatura);
                k.Siparis.Remove(siparis);
                k.SaveChanges();
                return true;
            }

            return false;



        }

        [MyAuthorization(Roles = "3")]
        public ActionResult VerilenSiparisDetayi(int id)
        {
            List<SiparişDetayı> siparişDetayı = k.SiparişDetayı.Where(x => x.SiparisID == id).ToList();
            ViewBag.Detay=siparişDetayı;
            return View();
        }

        [MyAuthorization(Roles = "2")]
        public ActionResult AlinanSiparisDetayi(int id)
        {
            SiparişDetayı siparişDetayı = k.SiparişDetayı.FirstOrDefault(x => x.SiparisID == id);
            ViewBag.detayy = siparişDetayı;
            return View();
        }

        [MyAuthorization(Roles = "3")]
        public ActionResult Fatura(int id)
        {
            Fatura fatura = k.Fatura.FirstOrDefault(x => x.SiparisID == id);
            return View(fatura);
        }

    }
}