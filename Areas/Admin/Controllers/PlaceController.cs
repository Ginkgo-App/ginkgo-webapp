using ginko_webapp.Areas.Admin.Helpers;
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
    public class PlaceController : Controller
    {
        private APIConnector connector = new APIConnector();
        // GET: Admin/Place
        public ActionResult Index() 
        { 
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            PlaceModel model = new PlaceModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePost(PlaceModel model)
        {
            if (ModelState.IsValid)
            {
                if (Session["token"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                string token = Session["token"].ToString();
                // store img to IMGUR first
                List<string> imagesUrl = new List<string>();
                if (model.FileImages.Count() > 0)
                {
                    imagesUrl = AdminHelper.Instance.StoreImagur(model.FileImages);
                }

                var client = connector.Initial();
                client.Timeout = -1;
                var request = new RestRequest("admin/places", Method.POST);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    Name = model.Name,
                    Images = JsonConvert.SerializeObject(imagesUrl),
                    Description = model.Description,
                    TypeId = 1
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

                    return RedirectToAction("Index", "Place");
                }
                else
                {
                    return RedirectToAction("Create", "Place");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Place");
            }

            string token = Session["token"].ToString();

            var client = connector.Initial();
            client.Timeout = -1;
            var request = new RestRequest("admin/places/" + id, Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = client.Execute(request);

            PlaceModel place = new PlaceModel();
            PlaceModel model = new PlaceModel();

            if (response.IsSuccessful)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                PlacesApiResultModel content = JsonConvert.DeserializeObject<PlacesApiResultModel>(response.Content, settings);
                model = content.Data[0];
           
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Place");
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(string id, PlaceModel model)
        {
            if (ModelState.IsValid)
            {
                if (Session["token"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                string token = Session["token"].ToString();
                // store img to IMGUR first
                List<string> imagesUrl = new List<string>();
                if (model.FileImages.Count() > 0)
                {
                    imagesUrl = AdminHelper.Instance.StoreImagur(model.FileImages);
                }

                var client = connector.Initial();
                client.Timeout = -1;
                var request = new RestRequest("admin/places/" + id, Method.PUT);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    Name = model.Name,
                    Description = model.Description,
                    Images = JsonConvert.SerializeObject(imagesUrl)
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

                    return RedirectToAction("Index", "Place");
                }
                else
                {
                    return RedirectToAction("Edit", "Place");
                }
            }
            return View(model);
        }
    }
}