using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
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
using ginko_webapp.Areas.Admin.Models.ViewModels;
using ginko_webapp.Areas.Admin.Models.ObjectModels;
using ginko_webapp.Areas.Admin.Models.APIResultModels;

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
                        Password = model.Password,
                        Email = model.Email  
                    });
                
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    AuthenticationApiResultModel result = JsonConvert.DeserializeObject<AuthenticationApiResultModel>(response.Content);
                    
                    if (result.ErrorCode != 0)
                    {
                        ViewBag.error = result.Message;
                        return RedirectToAction("Index", "Login");
                    }

                    // Store access token and user to session
                    Session["token"] = result.Data[0].Token;

                    // Call api to get authentication user info
                    request = new RestRequest("users/me", Method.GET);
                    request.AddHeader("Authorization", "Bearer " + result.Data[0].Token);
                    response = client.Execute(request);
                    if(response.IsSuccessful)
                    {
                        UsersApiResultModel admin = JsonConvert.DeserializeObject<UsersApiResultModel>(response.Content);
                        Session["admin"] = result.Data[0];
                    }

                    return RedirectToAction("Index", "TourInfo");
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

                admin.Email = me.email;
                admin.Name = me.first_name + me.last_name;

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
            Session["token"] = null;
            Session["admin"] = null;
            return RedirectToAction("Index");
        }
    }
}