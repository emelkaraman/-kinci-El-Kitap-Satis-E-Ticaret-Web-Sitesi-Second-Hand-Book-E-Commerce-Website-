using İkinciElKitapSatisVol1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace İkinciElKitapSatisVol1.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: Anasayfa
        İkinciElKitapSatisEntities1 k = new İkinciElKitapSatisEntities1();
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult İliskilendir(Users_UserTurleriIliskisi u)
        {
            k.Users_UserTurleriIliskisi.Add(u);
            k.SaveChanges();
            return RedirectToAction("Login");
            
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Users u)
        {
            Users user = k.Users.FirstOrDefault(x => x.UserMail == u.UserMail);
            if(user==null)
            {
                k.Users.Add(u);
                k.SaveChanges();
                ViewBag.UserTurleri = k.UserTurleri.Where(x=>x.TurID!=1).ToList();
                return View("İliskilendir",u);
            }
            else
            {
                ViewBag.Mesaj = "Bu mail kullanılmaktadır.";
                return View();
            }
                
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult IlIlce(int? ilKodu, string tip)
        {
            //geriye döndüreceğim sonucListim
            List<SelectListItem> sonuc = new List<SelectListItem>();
            //bu işlem başarılı bir şekilde gerçekleşti mi onun kontrolunnü yapıyorum
            bool basariliMi = true;
            try
            {
                switch (tip)
                {
                    case "ilGetir":
                        //veritabanımızdaki iller tablomuzdan illerimizi sonuc değişkenimze atıyoruz
                        foreach (var il in k.il.ToList())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = il.ilAdi,
                                Value = il.ilKodu.ToString()
                            });
                        }
                        break;
                    case "ilceGetir":
                        //ilcelerimizi getireceğiz ilimizi selecten seçilen ilID sine göre 
                        foreach (var ilce in k.ilce.Where(il=>il.ilKodu == ilKodu).ToList())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = ilce.ilceAdi,
                                Value = ilce.ilceKodu.ToString()
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

        

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Users U)
        {
            Users user = k.Users.FirstOrDefault(x => x.UserMail == U.UserMail && x.UserPassword == U.UserPassword);
            
            if(user!=null)
            {
                FormsAuthentication.SetAuthCookie(user.UserFirstName + user.UserLastName, false);
                return RedirectToAction("Index", "Anasayfa");
            }

            else
            {
                ViewBag.Mesaj = "Kullanıcı mail ya da şifre hatalı";
                return View();
            }
            
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Hata()
        {
            return View();
        }
    }
}