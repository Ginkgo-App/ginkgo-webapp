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
            if (Session["token"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            string token = Session["token"].ToString();

            DashboardViewModel model = new DashboardViewModel();

            return View(model);
        }
    }
}