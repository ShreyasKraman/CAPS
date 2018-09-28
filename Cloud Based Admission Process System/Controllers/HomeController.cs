using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;




namespace MvcApplication3.Controllers
{
    public class HomeController : Controller
    {
        
        
        public ActionResult Index()
        {
                return View();
           
        }

        public ActionResult CandidateLogin()
        {
            ViewBag.Message="Welcome";
                return View();

        }
        public ActionResult Indexs()
        {
            ViewBag.Message = "Welcome";
            return View();
        }

        public ActionResult preVerify()
        {
            return View();
        }

        public ActionResult candVerify()
        {
            return View();
        }
        public ActionResult Verification()
        {
            return View();
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

   

      
    }
}
