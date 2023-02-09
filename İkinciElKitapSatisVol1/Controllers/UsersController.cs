using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using İkinciElKitapSatisVol1.Models;

namespace İkinciElKitapSatisVol1.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        İkinciElKitapSatisEntities1 k = new İkinciElKitapSatisEntities1();


        public ActionResult Index()
        {
            string name = HttpContext.User.Identity.Name;
            Users user = k.Users.FirstOrDefault(x => x.UserFirstName + x.UserLastName == name);
            ViewBag.il = k.il.FirstOrDefault(x => x.ilKodu == user.ilKodu).ilAdi;
            ViewBag.ilce = k.ilce.FirstOrDefault(x => x.ilceKodu == user.ilceKodu).ilceAdi;
            ViewBag.Kart = k.Kart.Where(x => x.UserID == user.UserID).ToList();
            return View(user);
        }

        public ActionResult İliskiKur(int id)
        {
            string name = HttpContext.User.Identity.Name;
            Users user = k.Users.FirstOrDefault(x => x.UserFirstName + x.UserLastName == name);
            Users_UserTurleriIliskisi users_UserTurleriIliskisi = new Users_UserTurleriIliskisi();
            users_UserTurleriIliskisi.TurID = id;
            users_UserTurleriIliskisi.UsersID = user.UserID;
            k.Users_UserTurleriIliskisi.Add(users_UserTurleriIliskisi);
            k.SaveChanges();
            return RedirectToAction("Index");
           
        }

        [HttpPost]
        public bool KartSil(int id)
        {
            Kart Kart = k.Kart.FirstOrDefault(x => x.KartID == id);
            if (Kart != null)
            {
                    k.Kart.Remove(Kart);
                    k.SaveChanges();
                    return true;
                
            }
            return false;

        }

        public ActionResult KartEkle()
        {

            string name = HttpContext.User.Identity.Name;
            Users user = k.Users.FirstOrDefault(x => x.UserFirstName + x.UserLastName == name);

            return View(user);
        }

        [HttpPost]
        public ActionResult KartEkle(Kart kart)
        {
            Kart kart1 = k.Kart.FirstOrDefault(x => x.KartNumarası == kart.KartNumarası);
            if(kart1==null)
            {
                k.Kart.Add(kart);
                k.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Bu kart numarası kullanılmaktadır. Lütfen kart numarasını kontrol edip tekrar deneyiniz.";
                return RedirectToAction("KartEkle");
            }

        }
    }
}