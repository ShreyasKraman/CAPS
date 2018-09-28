using MvcApplication3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.Data;

namespace MvcApplication3.Controllers
{
    public class CollegeFinderController : Controller
    {
        // GET: CollegeFinder
       [AllowAnonymous]
        public ActionResult collegeFinder()
        {

            return View();
        }

        [HttpPost]
    public ActionResult collegeFinder(CollegeFinderModel m , FormCollection form)
    {
        return RedirectToAction("preferenceCollege","ApplicationForm");
        }
        [HttpPost]
        public JsonResult collegeFinders(string[] array)
        {
            try
            {
                var region = array[0];
                var branch = array[1];
                var rank = array[2];
                var db = Database.Open("sqlConnection");
                string command = "select collegeName from collegeFinder where region='" + region + "' AND cutoff<='" + rank + "' AND Branch='" + branch + "'";
                var data = db.QueryValue(command);
                string a = null;
                //foreach (var i in data)
                //{
                //    a += i;
                //}

                return Json(region);
            }
            catch(Exception e)
            {
                return Json(e.Message.ToString());
            }
        }

        
    }
}