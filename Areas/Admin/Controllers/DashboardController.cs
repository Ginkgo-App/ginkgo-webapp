using ginko_webapp.Areas.Admin.Models;
using ginko_webapp.Helper;
using ginko_webapp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HttpCookie = System.Web.HttpCookie;

namespace ginko_webapp.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        APIConnector connector = new APIConnector();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            UserModel admin = (UserModel)Session["admin"];

            // Call api to get list users
            var client = connector.Initial();
            client.Timeout = -1;
            var request = new RestRequest("users", Method.GET);
            request.AddHeader("Authorization", "Bearer " + admin.Token);
            IRestResponse response = client.Execute(request);

            List<UserModel> usersList = JsonConvert.DeserializeObject<List<UserModel>>(response.Content);

            DashboardViewModel model = new DashboardViewModel();
            model.ListUsers = usersList;
            model.Admin = admin;

            return View(model);
        }
    }
}