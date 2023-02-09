using İkinciElKitapSatisVol1.Models;
using İkinciElKitapSatisVol1.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace İkinciElKitapSatisVol1.Controllers
{
    [MyAuthorization(Roles = "1")]
    public class MusteriController : Controller
    {
        İkinciElKitapSatisEntities1 k = new İkinciElKitapSatisEntities1();
        // GET: Users
        public ActionResult Index()
        {
            ViewBag.Consumer = k.detayliMusteriListesi.ToList();

            return View();
        }

        [HttpPost]
        public bool MusteriSil(int id)
        {
            Users_UserTurleriIliskisi u = k.Users_UserTurleriIliskisi.FirstOrDefault(x => x.UsersID == id && x.TurID == 3);
            Users u4 = k.Users.FirstOrDefault(x => x.UserID == id);
            Users_UserTurleriIliskisi u2 = k.Users_UserTurleriIliskisi.FirstOrDefault(x => x.UsersID == id && x.TurID == 2);
            Users_UserTurleriIliskisi u3 = k.Users_UserTurleriIliskisi.FirstOrDefault(x => x.UsersID == id && x.TurID == 1);
             if (u != null)
             {

                if(u2!=null || u3!=null)
                {
                    k.Users_UserTurleriIliskisi.Remove(u);
                    k.SaveChanges();
                    return true;
                }
                else if(u3!=null)
                {
                    return false;
                }

                k.Users_UserTurleriIliskisi.Remove(u);
                k.Users.Remove(u4);
                k.SaveChanges();
                return true;
             }
             else
                return false;

           
            


        }
    }
}