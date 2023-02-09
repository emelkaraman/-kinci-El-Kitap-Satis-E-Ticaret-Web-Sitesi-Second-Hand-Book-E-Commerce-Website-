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
    public class SaticiController : Controller
    {
        İkinciElKitapSatisEntities1 k = new İkinciElKitapSatisEntities1();
        // GET: Users
        public ActionResult Index()
        {
            ViewBag.Producer = k.detayliSaticiListesi.ToList();
          

            return View();
        }


        [HttpPost]
        public bool SaticiSil(int id)
        {
            Users_UserTurleriIliskisi u = k.Users_UserTurleriIliskisi.FirstOrDefault(x => x.UsersID == id && x.TurID == 2);
            Kitap kitap = k.Kitap.FirstOrDefault(x => x.SaticiID == id);
            if(u!=null)
            {

                k.Users_UserTurleriIliskisi.Remove(u);
                k.SaveChanges();
                if(kitap!=null)
                {
                    k.Kitap.Remove(kitap);
                    k.SaveChanges();
                }
                return true;  
            }

            return false;
            
            
            
        }
    }
}