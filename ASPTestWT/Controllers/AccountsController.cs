using ASPTestWT.Services;
using ASPTestWT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPTestWT.Controllers
{
    public class AccountsController : Controller
    {
        LoginService ls;

        public AccountsController()
        {
            ls = new LoginService();
        }
        public ActionResult Login(string message)
        {
            LoginViewModel loginvm = new LoginViewModel();
            ViewBag.Message = !string.IsNullOrEmpty(message) ? message : string.Empty;  
            return View(loginvm);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel vmodel)
        {
            LoginViewModel vm = new LoginViewModel();
            string message = string.Empty;
            if (!ModelState.IsValid)
            {
                return View("~/Views/Accounts/Login.cshtml", vmodel);
            }
            else
            {
                CheckCredentials(vmodel, ref message);
                if(message.Contains("Invalid"))
                {
                    return RedirectToAction("Login", new { message = message });
                }
                else
                {
                    return RedirectToAction("Index","Home");
                }
            }
            
        }

        public string CheckCredentials(LoginViewModel vmodel, ref string message)
        {
            LoginViewModel vm = new LoginViewModel();
            message = vmodel.username.ToLower() == "wonder" && vmodel.password == "Tree" ? "Valid User" : "Invalid User";
            //if (vmodel.username == "Faheel" && vmodel.password == "abc")
            //{
            //    message = "Valid User";
            //}
            //else
            //{
            //    message = "Invalid User";
            //}
            return message;
        }

        public ActionResult About()
        {
            return View();
        }
    }
}