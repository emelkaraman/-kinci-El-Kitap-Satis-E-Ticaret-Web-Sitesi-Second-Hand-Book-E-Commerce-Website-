using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using İkinciElKitapSatisVol1.Models;
using İkinciElKitapSatisVol1.AppClass;
using İkinciElKitapSatisVol1.Security;

namespace İkinciElKitapSatisVol1.Controllers
{
    public class KitapController : Controller
    {
        İkinciElKitapSatisEntities1 k = new İkinciElKitapSatisEntities1();
        
         
        // GET: Kitaplar
        [HttpGet]
        
        public ActionResult Index()
        {
            List<DetayliKitapBilgisi> liste = k.DetayliKitapBilgisi.ToList();
            return View(liste);
        }

        [MyAuthorization (Roles="2")]
        public ActionResult Kitaplarim()
        {
            string userName = HttpContext.User.Identity.Name;
            Users user = k.Users.FirstOrDefault(x => x.UserFirstName + x.UserLastName == userName);
            List<DetayliKitapBilgisi> kitap = k.DetayliKitapBilgisi.Where(x => x.saticiID == user.UserID).ToList();
            List<Yazar> yazar = k.Yazar.ToList();
            List<Kategori> kategoris = k.Kategori.ToList();
            List<AltKategori> altkategoris = k.AltKategori.ToList();
            ViewBag.Kategori = kategoris;
            ViewBag.AltKategori = altkategoris;
            ViewBag.Kitap = kitap;
            ViewBag.Yazar = yazar;
            return View();
        }



        [HttpPost]
        [MyAuthorization(Roles = "2")]
        public bool KitapSil(int id)
        {
            Kitap kitap = k.Kitap.FirstOrDefault(x => x.KitapID == id);
            KitapYaz kitapYaz = k.KitapYaz.FirstOrDefault(x => x.KitapID == id);
            KitapSiparisEdilir kitapSiparisEdilir = k.KitapSiparisEdilir.FirstOrDefault(x => x.KitapID == id);
            if (kitap != null)
            {
                

                if(kitapYaz!=null )
                {
                    if(kitapSiparisEdilir != null)
                    {
                        k.KitapSiparisEdilir.Remove(kitapSiparisEdilir);
                        k.KitapYaz.Remove(kitapYaz);
                        k.Kitap.Remove(kitap);
                        k.SaveChanges();
                    }

                    else
                    {
                        k.KitapYaz.Remove(kitapYaz);
                        k.Kitap.Remove(kitap);
                        k.SaveChanges();
                        return true;

                    }
                    
                }

                else
                {
                    k.Kitap.Remove(kitap);
                    k.SaveChanges();
                    return true;
                }
                
            }
            return false;

        }
        [HttpPost]
        [MyAuthorization(Roles = "2")]
        public ActionResult YazarEkle(Yazar y)
        {
            Yazar yazar = k.Yazar.FirstOrDefault(x => x.YazarAdi == y.YazarAdi);
            if(yazar==null)
            {
                k.Yazar.Add(y);
                k.SaveChanges();
                return RedirectToAction("Kitaplarim");
            }
            return RedirectToAction("Kitaplarim");

            
        }

        [MyAuthorization(Roles = "2")]
        public ActionResult KitapEkle()
        {
            string userName = HttpContext.User.Identity.Name;
            Users Satici = k.Users.FirstOrDefault(x => x.UserFirstName + x.UserLastName == userName);
            return View(Satici);
        }

        [HttpPost]
        public JsonResult KategoriAltKategori(int? KategoriID, string tip)
        {
            //geriye döndüreceğim sonucListim
            List<SelectListItem> sonuc = new List<SelectListItem>();
            //bu işlem başarılı bir şekilde gerçekleşti mi onun kontrolunnü yapıyorum
            bool basariliMi = true;
            try
            {
                switch (tip)
                {
                    case "KategoriGetir":
                        //veritabanımızdaki kategoriler tablomuzdan kategorileri sonuc değişkenimze atıyoruz
                        foreach (var kategori in k.Kategori.ToList())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = kategori.KategoriAdi,
                                Value = kategori.KategoriID.ToString()
                            });
                        }
                        break;
                    case "AltKategoriGetir":
                        //altkategorileri getireceğimizz kategoriyi selecten seçilen kategoriID sine göre 
                        foreach (var altkategori in k.AltKategori.Where(Kategori => Kategori.KategoriID == KategoriID).ToList())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = altkategori.AltKategoriAdi,
                                Value = altkategori.AltKategoriID.ToString()
                            });
                        }
                        break;

                    default:
                        break;

                }
            }
            catch (Exception)
            {
                //hata ile karşılaşırsak buraya düşüyor
                basariliMi = false;
                sonuc = new List<SelectListItem>();
                sonuc.Add(new SelectListItem
                {
                    Text = "Bir hata oluştu :(",
                    Value = "Default"
                });

            }
            //Oluşturduğum sonucları json olarak geriye gönderiyorum
            return Json(new { ok = basariliMi, text = sonuc });
        }

        [HttpPost]
        [MyAuthorization(Roles = "2")]
        public ActionResult KitapEkle(Kitap kitap)
        {
            Kitap kitap1 = k.Kitap.FirstOrDefault(x => x.KitapAdi == kitap.KitapAdi && x.SaticiID==kitap.SaticiID );
            if(kitap1!=null)
            {
                kitap1.Adet = kitap1.Adet + kitap.Adet;
                k.SaveChanges();
                return RedirectToAction("Kitaplarim");
            }
            else
            {
                k.Kitap.Add(kitap);
                k.SaveChanges();
                ViewBag.Yazar = k.Yazar.ToList();
                return View("KitapYazarİliski", kitap);

            }


        }

        [MyAuthorization(Roles = "3")]
        public ActionResult UrunDetayiGetir(int id)
        {
            DetayliKitapBilgisi kitap = k.DetayliKitapBilgisi.FirstOrDefault(x => x.KitapID == id);
            return View(kitap);
        }

        [HttpPost]
        [MyAuthorization(Roles = "2")]
        public ActionResult KategoriEkle(Kategori kategori)
        {
            Kategori kategoris = k.Kategori.FirstOrDefault(x => x.KategoriAdi == kategori.KategoriAdi);
            if(kategoris==null)
            {
                k.Kategori.Add(kategori);
                k.SaveChanges();
                return RedirectToAction("Kitaplarim");
            }

            return RedirectToAction("Kitaplarim");

        }

        [HttpPost]
        [MyAuthorization(Roles = "2")]
        public ActionResult AltKategoriEkle(AltKategori Altkategori)
        {
            AltKategori altkategoris = k.AltKategori.FirstOrDefault(x => x.AltKategoriAdi == Altkategori.AltKategoriAdi);
            if (altkategoris == null)
            {
                k.AltKategori.Add(Altkategori);
                k.SaveChanges();
                return RedirectToAction("Kitaplarim");
            }

            return RedirectToAction("Kitaplarim");

        }

        [HttpPost]
        [MyAuthorization(Roles = "2")]
        public ActionResult KitapYazarİliski(KitapYaz kitapYaz)
        {
            KitapYaz yaz = k.KitapYaz.FirstOrDefault(x => x.ID == kitapYaz.ID );
            if(yaz==null)
            {
                k.KitapYaz.Add(kitapYaz);
                k.SaveChanges();
                return RedirectToAction("Kitaplarim");
            }
            else
            {
                return RedirectToAction("Kitaplarim");
            }
            
        }

        [MyAuthorization(Roles = "3")]
        public ActionResult SepeteEkle(int id)
        {
            string username = HttpContext.User.Identity.Name;
            Users user = k.Users.FirstOrDefault(x => x.UserFirstName + x.UserLastName == username);
            if(Session["Sepet"]==null)
            {
                List<Item> Sepet = new List<Item>();
                var kitap = k.Kitap.FirstOrDefault(x=>x.KitapID==id && x.SaticiID!=user.UserID);
                if (kitap != null)
                {
                    Sepet.Add(new Item()
                    {
                        kitaplar = kitap,
                        adet = 1,
                        tutar=(decimal) kitap.Fiyat*1
                    });
                    Session["Sepet"] = Sepet;
                    return RedirectToAction("Index");
                }
                else
                   return RedirectToAction("Index");
            }
            else
            {
                List<Item> Sepet=(List<Item>)Session["Sepet"];
                var kitap = k.Kitap.FirstOrDefault(x => x.KitapID == id && x.SaticiID != user.UserID);
                bool eklendi = false;
                foreach(var item in Sepet)
                {
                    if(item.kitaplar.KitapID==id)
                    {
                        int eskiadet = item.adet;
                        Sepet.Remove(item);
                        Sepet.Add(new Item()
                        {
                            kitaplar = kitap,
                            adet = eskiadet + 1,
                            tutar=(decimal)kitap.Fiyat*(eskiadet+1)
                           
                        });
                        eklendi = true;
                        break; 
                    }
                    eklendi = false;
                    
                }

                if (eklendi==false)
                {
                    Sepet.Add(new Item()
                    {
                        kitaplar = kitap,
                        adet = 1,
                        tutar = (decimal)kitap.Fiyat * 1
                    });
                    eklendi = true;
                    Session["Sepet"] = Sepet;
                    return RedirectToAction("Index");
                }
                else
                    return RedirectToAction("Index");

            }


            }
            
            
        

        [MyAuthorization(Roles = "3")]
        public ActionResult SepettenKaldir(int id)
        {
            List<Item> Sepet = (List<Item>)Session["Sepet"];
            //var kitap = k.Kitap.Find(id);
            foreach(var item in Sepet)
            {
                if(item.kitaplar.KitapID==id)
                {
                    Sepet.Remove(item);
                    break;
                }
            }
                
            Session["Sepet"] = Sepet;
            return RedirectToAction("Index");
           
        }

        [MyAuthorization(Roles = "3")]
        public ActionResult Sepetim()
        {
            List<Item> Sepet = (List<Item>)Session["Sepet"];
            string username = HttpContext.User.Identity.Name;
            Users user = k.Users.FirstOrDefault(x => x.UserFirstName + x.UserLastName == username);
            ViewBag.Kargo = k.KargoFirmasi.ToList();
            ViewBag.User = user;
            ViewBag.Kart = k.Kart.Where(x => x.UserID == user.UserID);
            decimal tutar = 0;
            if(Sepet!=null)
            {
                foreach (var item in Sepet)
                {
                    tutar += (decimal)item.kitaplar.Fiyat * item.adet;
                }
            }
            
            ViewBag.ToplamTutar = tutar;
            return View(Sepet);
        }

        
        [HttpPost]
        [MyAuthorization(Roles = "3")]
        public ActionResult SiparisEkle(Siparis siparis)
        {
            List<Item> Sepet = (List<Item>)Session["Sepet"];
            if(Sepet!=null)
            {
                if (siparis != null)
                {
                    
                    k.Siparis.Add(siparis);
                    Fatura fatura = new Fatura();
                    fatura.FaturaAd = Convert.ToString(siparis.MusteriID + siparis.SiparisID);
                    fatura.FaturaNo = Convert.ToString(siparis.SipTarihi );
                    fatura.FaturaTarih = siparis.SipTarihi;
                    fatura.SiparisID = siparis.SiparisID;
                    k.Fatura.Add(fatura);

                    k.SaveChanges();
                    
                    foreach(var item in Sepet)
                    {
                        KitapSiparisEdilir kitapSiparis = new KitapSiparisEdilir();
                        Kitap kitap = k.Kitap.FirstOrDefault(x => x.KitapID == item.kitaplar.KitapID);
                        kitapSiparis.KitapID = item.kitaplar.KitapID;
                        kitapSiparis.SiparisID = siparis.SiparisID;
                        kitapSiparis.BirimFiyat =(decimal) item.kitaplar.Fiyat;
                        kitapSiparis.UrunAdedi = item.adet;
                        kitapSiparis.SaticiID = item.kitaplar.SaticiID;
                        k.KitapSiparisEdilir.Add(kitapSiparis);
                        kitap.Adet -= item.adet;
                        k.SaveChanges();
                        
                    }
                    Session["Sepet"] = null;

                    return RedirectToAction("Index");
                }

                else 
                    return RedirectToAction("Sepetim");
            }


            return RedirectToAction("Index");
           
            
        }
    }
}