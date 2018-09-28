using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication3.Controllers
{
    public class PaymentController:Controller
    {
        public ActionResult payment()
        {
            return View();
        }
        
    }
}