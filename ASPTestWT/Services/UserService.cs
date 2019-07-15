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

namespace ASPTestWT.Services
{
    public class UserService
    {
        public async Task<List<UserViewModel>> user()
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
                HttpResponseMessage Res = await client.GetAsync("api/test?ID=2825458e-9a3b-4218-b035-80eb05e67eec");
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
                        model.Add(vm);
                    }

                    // var user = JsonConvert.DeserializeObject<List<User>>(userJson.Substring(1, userJson.Length - 2));
                    return model;
                }
                return model;
            }
        }


    }
}