using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication3.Models;
using System.Data;
using WebMatrix.WebData;
using WebMatrix.Data;

namespace MvcApplication3.Controllers
{
    public class AccVerificationController : Controller
    {
        [AllowAnonymous]
        public ActionResult LoginVerify(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginVerify(LoginModel model, string returnUrl)
        {

            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {

                var dbs = Database.Open("newConnection");
                string command = @"Insert into verifyEmail values(@0,'yesU')";
                dbs.Execute(command, model.UserName);

                return RedirectToAction("Indexs", "Home");
            }

            ModelState.AddModelError("", "Username or password does not exist");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LoginCandidateVerify(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginCandidateVerify(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                
                var dbs = Database.Open("newConnection");
                string command = @"Insert into verifyEmail values(@0,'yesC')";
                dbs.Execute(command,model.UserName);

                return RedirectToAction("CandidateLogin", "Home");
            }

            ModelState.AddModelError("", "Username or password does not exist");
            return View(model);
        }
    }
}