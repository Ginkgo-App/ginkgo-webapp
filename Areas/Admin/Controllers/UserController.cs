using ginko_webapp.Areas.Admin.Models.APIResultModels;
using ginko_webapp.Areas.Admin.Models.ObjectModels;
using ginko_webapp.Areas.Admin.Models.ViewModels;
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

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if(id == null)
            {
                return RedirectToAction("Index", "User");
            }

            string token = Session["token"].ToString();

            var client = connector.Initial();
            client.Timeout = -1;
            var request = new RestRequest("admin/users/" + id, Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = client.Execute(request);

            UserModel user = new UserModel();
            UserEditViewModel model;

            if (response.IsSuccessful)
            {
                UsersApiResultModel content = JsonConvert.DeserializeObject<UsersApiResultModel>(response.Content);
                user = content.Data[0];
                model = new UserEditViewModel() { 
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.Name,
                    Avatar = user.Avatar,
                    Bio = user.Bio,
                    Slogan = user.Slogan,
                    Job = user.Job,
                    Birthday = user.Birthday,
                    Gender = user.Gender,
                    Address = user.Address,
                    Role = user.Role
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(string id, UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Session["token"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                string token = Session["token"].ToString();

                var client = connector.Initial();
                client.Timeout = -1;
                var request = new RestRequest("admin/users/" + id, Method.PUT);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    Avatar = model.Avatar,
                    Slogan = model.Slogan,
                    Bio = model.Bio,
                    Job = model.Job,
                    Gender = model.Gender,
                    Birthday = model.Birthday,
                    Role = model.Role,
                });

                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    APIResultModel result = JsonConvert.DeserializeObject<APIResultModel>(response.Content);
                    if (result.ErrorCode != 0)
                    {
                        ViewBag.error = result.Message;
                        return View();
                    }

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Edit", "User");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            UserCreateViewModel model = new UserCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePost(UserCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (Session["token"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                string token = Session["token"].ToString();

                var client = connector.Initial();
                client.Timeout = -1;
                var request = new RestRequest("admin/users", Method.POST);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    Avatar = model.Avatar,
                    Slogan = model.Slogan,
                    Bio = model.Bio,
                    Job = model.Job,
                    Gender = model.Gender,
                    Birthday = model.Birthday,
                    Role = model.Role,
                });

                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    APIResultModel result = JsonConvert.DeserializeObject<APIResultModel>(response.Content);
                    if (result.ErrorCode != 0)
                    {
                        ViewBag.error = result.Message;
                        return View();
                    }

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Create", "User");
                }
            }
            return View();
        }
    }
}