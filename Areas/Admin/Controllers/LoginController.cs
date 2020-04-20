using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

using ginko_webapp.Models;
using ginko_webapp.Areas.Admin.Models;
using System.Web.Security;
using System.Threading.Tasks;
using System.Net.Http;
using ginko_webapp.Helper;
using Facebook;
using System.Configuration;
using System.Net.Http.Formatting;
using RestSharp;
using Newtonsoft.Json;

// using namespace for same libraries
using UserModel = ginko_webapp.Models.UserModel;
using HttpCookie = System.Web.HttpCookie;

namespace ginko_webapp.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private APIConnector connector = new APIConnector();

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        // GET: Admin/Login
        public ActionResult Index()
        {
            if (Session["admin"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            LoginViewModel model = new LoginViewModel();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Call api
                var client = connector.Initial();
                client.Timeout = -1;
                var request = new RestRequest("users/authenticate", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody( new {
                        username = model.Email,
                        password = model.Password
                    });
                
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    UserModel userModel = new UserModel();
                    userModel = JsonConvert.DeserializeObject<UserModel>(response.Content);

                    // Store access token and user data to session
                    Session["admin"] = userModel;

                    // If check remember store refresh token
                    if (model.IsRemember)
                    {

                    }
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Index", "Login");
                }
            }
            return View();
        }

        public ActionResult LoginWithFacebook()
        {
            var facebookClient = new FacebookClient();
            var loginUrl = facebookClient.GetLoginUrl(new { 
                client_id = ConfigurationManager.AppSettings["fbAppId"],
                client_secret = ConfigurationManager.AppSettings["fbAppPrivateKey"],
                redirect_uri  = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var facebookClient = new FacebookClient();
            dynamic result = facebookClient.Post("oauth/access_token", new {
                client_id = ConfigurationManager.AppSettings["fbAppId"],
                client_secret = ConfigurationManager.AppSettings["fbAppPrivateKey"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code,
            });

            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken)) 
            {
                facebookClient.AccessToken = accessToken;
                // Get facebook user's info
                dynamic me = facebookClient.Get("me?fields=first_name,middle_name,last_name,id,email");

                UserModel admin = new UserModel();

                admin.Username = me.email;
                admin.FirstName = me.first_name;
                admin.LastName = me.last_name;

                // Call api to store user data to database 

                // Store user to session
                Session["admin"] = admin;

                return RedirectToAction("Index", "Dashboard");
            }

            return RedirectToAction("Index", "Login");
        }

        public ActionResult Logout()
        {
            // Delete admin variable in session
            Session["admin"] = null;
            return RedirectToAction("Index");
        }
    }
}