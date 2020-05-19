using ginko_webapp.Areas.Admin.Models.APIResultModels;
using ginko_webapp.Areas.Admin.Models.ObjectModels;
using ginko_webapp.Helper;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ginko_webapp.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private APIConnector connector = new APIConnector();

        public ActionResult CheckAuthentication(ref string token)
        {
            if(Session["token"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            token = Session["token"].ToString();
            return null;
        }

        public ActionResult Index()
        {
            string token = "";
            CheckAuthentication(ref token);

            return View();
        }

        public ActionResult View(string id)
        {
            if(id == null)
            {
                return RedirectToAction("Index", "User");
            }

            string token = "";
            CheckAuthentication(ref token);

            var client = connector.Initial();
            client.Timeout = -1;
            var request = new RestRequest("users/" + id, Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = client.Execute(request);

            UserModel user = new UserModel();

            if (response.IsSuccessful)
            {
                UsersApiResultModel content = JsonConvert.DeserializeObject<UsersApiResultModel>(response.Content);
                user = content.Data[0];
            }

            ViewData["user"] = user;

            return View();
        }
    }
}