using İkinciElKitapSatisVol1.Models;
using İkinciElKitapSatisVol1.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace İkinciElKitapSatisVol1.Controllers
{
    [MyAuthorization (Roles = "1")]
    public class AdminController : Controller
    {
        
        İkinciElKitapSatisEntities1 k = new İkinciElKitapSatisEntities1();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Admin = k.detayliAdminListesi.ToList();

            return View();
        }

        public ActionResult AdminEkle()
        {
            string name = HttpContext.User.Identity.Name;
            ViewBag.Users = k.Users.Where(x=>x.UserFirstName + x.UserLastName!=name).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AdminEkle(Users_UserTurleriIliskisi u)
        {
            Users_UserTurleriIliskisi u1 = k.Users_UserTurleriIliskisi.FirstOrDefault(x => x.UsersID == u.UsersID && x.TurID == u.TurID);
            if(u1==null)
            {
                k.Users_UserTurleriIliskisi.Add(u);
                k.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                string name = HttpContext.User.Identity.Name;
                ViewBag.Users = k.Users.Where(x => x.UserFirstName + x.UserLastName != name).ToList();
                ViewBag.Mesaj = "Seçtiğiniz kullanıcının adminlik yetkisi vardır.";
                return View();
            }
            
        }
    }
}