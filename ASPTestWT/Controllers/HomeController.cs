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
using PagedList;
using PagedList.Mvc;

namespace ASPTestWT.Controllers
{
    public class HomeController : Controller
    {
        

        public async Task<ActionResult> Index(List<UserViewModel> m,int? page)
        {
            List<UserViewModel> model = new List<UserViewModel>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri("https://wonderasptest.azurewebsites.net/");

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                //HttpResponseMessage Res = await client.GetAsync("api/test/GenerateID");
                //HttpResponseMessage Res = await client.GetAsync("api/test?ID=85e63896-ad61-4009-916e-fb3ef69b9958&userid=1");
                HttpResponseMessage Res = await client.GetAsync("api/test?ID=b6608f46-5466-499e-b998-17c24e364456");
                //Checking the response is successful oot which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var userJson = Res.Content.ReadAsStringAsync().Result;
                    //var j = Json(generatedId, JsonRequestBehavior.AllowGet);
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    //EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    var u = jss.Deserialize<List<User>>(userJson.Substring(1, userJson.Length - 2));
                    //var u = jss.Deserialize<User>(userJson);
                    foreach (var user in u)
                    {
                        UserViewModel vm = new UserViewModel();
                        vm.ID = user.ID;
                        vm.FirstName = user.FirstName;
                        vm.LastName = user.LastName;
                        vm.Email = user.Email;
                        vm.Initials = vm.FirstName[0].ToString() + vm.LastName[0].ToString();
                        model.Add(vm);
                    }

                    // var user = JsonConvert.DeserializeObject<List<User>>(userJson.Substring(1, userJson.Length - 2));
                    return View(model.ToList().ToPagedList(page??1,3));
                }
                //returning the employee list to view  
                //return View(vm);
            }
            return View();
        }

        public ActionResult Users()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult search(string a)
        {
            return Json("chamara", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://wonderasptest.azurewebsites.net/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("api/Test/b6608f46-5466-499e-b998-17c24e364456?UserID=" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult AddUser()
        {
            //UserViewModel uservm = new UserViewModel();
            //ViewBag.Message = !string.IsNullOrEmpty(message) ? message : string.Empty;
            //return View(uservm);
            return View();
        }

        //[HttpPost]
        //public ActionResult AddUser(UserViewModel vmodel)
        //{
        //    UserViewModel vm = new UserViewModel();
        //    string message = string.Empty;
        //    if (!ModelState.IsValid)
        //    {
        //        return View("~/Views/User/AddUser.cshtml", vmodel);
        //    }
        //    return RedirectToAction("AddUser", new { message = message });
        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your About page.";

            return View();
        }
    }
}