using ASPTestWT.Services;
using ASPTestWT.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ASPTestWT.Entities;
using System.Collections;
using Newtonsoft.Json;

namespace ASPTestWT.Controllers
{
    public class UserController : Controller
    {
        UserService us;
        public UserController()
        {
            us = new UserService();
        }
        
         //GET: User
        public ActionResult GetUser()
        {
            Task<List<UserViewModel>> model = us.user();
            return View(model);
        }

        public ActionResult AddUser()
        {
            return View();
        }
    }
}
