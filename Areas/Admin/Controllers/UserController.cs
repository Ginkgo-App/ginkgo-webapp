using ginko_webapp.Helper;
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

        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult View(string id)
        //{

        //}
    }
}