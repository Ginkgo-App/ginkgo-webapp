using ginko_webapp.Areas.Admin.Helpers;
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
    public class TourInfoController : Controller
    {
        private APIConnector connector = new APIConnector();
        // GET: Admin/PlaceInfo
        public ActionResult Index()
        {
            return View();
        }

        private SelectList GetPlacesPicklist()
        {
            // Call API to get Places 
            string token = Session["token"].ToString();
            var client = connector.Initial();
            client.Timeout = -1;
            var request = new RestRequest("admin/places", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("page", 0);
            request.AddParameter("pageSize", 0);
            IRestResponse response = client.Execute(request);

            List<PlaceModel> places = new List<PlaceModel>();
            if (response.IsSuccessful)
            {
                PlacesApiResultModel content = JsonConvert.DeserializeObject<PlacesApiResultModel>(response.Content);
                places = content.Data;
            }

            return new SelectList(places, "Id", "Name");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.PlacesPicklist = this.GetPlacesPicklist();

            TourInfoCreateViewModel model = new TourInfoCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePost(TourInfoCreateViewModel model)
        {
            ViewBag.PlacesPicklist = this.GetPlacesPicklist();

            if (ModelState.IsValid)
            {
                if (Session["token"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                string token = Session["token"].ToString();
                // store img to IMGUR first
                List<string> imagesUrl = new List<string>();
                if (model.Images.Count() > 0)
                {
                    imagesUrl = AdminHelper.Instance.StoreImagur(model.Images);
                }

                var client = connector.Initial();
                client.Timeout = -1;
                var request = new RestRequest("admin/tour-infos", Method.POST);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    Name = model.Name,
                    StartPlaceId = model.StartPlace,
                    DestinatePlaceId = model.DestinatePlace,
                    Images = JsonConvert.SerializeObject(imagesUrl),
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

                    return RedirectToAction("Index", "TourInfo");
                }
                else
                {
                    return RedirectToAction("Create", "TourInfo");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            ViewBag.PlacesPicklist = this.GetPlacesPicklist();

            if (id == null)
            {
                return RedirectToAction("Index", "TourInfo");
            }

            string token = Session["token"].ToString();

            var client = connector.Initial();
            client.Timeout = -1;
            var request = new RestRequest("admin/tour-infos/" + id, Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = client.Execute(request);

            TourInfoModel tourInfo = new TourInfoModel();
            TourInfoEditViewModel model;

            if (response.IsSuccessful)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                TourInfoApiResultModel content = JsonConvert.DeserializeObject<TourInfoApiResultModel>(response.Content, settings);
                tourInfo = content.Data[0];
                model = new TourInfoEditViewModel()
                {
                    Name = tourInfo.Name,
                    Images = tourInfo.Images,
                    Rating = tourInfo.Rating,
                    StartPlace = tourInfo.StartPlace.Id,
                    DestinatePlace = tourInfo.DestinatePlace.Id,
                    CreateBy = tourInfo.CreateBy.Name
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "TourInfo");
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(string id, TourInfoEditViewModel model)
        {
            ViewBag.PlacesPicklist = this.GetPlacesPicklist();
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
                var request = new RestRequest("admin/tour-infos/" + id, Method.PUT);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    Name = model.Name,
                    StartPlaceId = model.StartPlace,
                    DestinatePlaceId = model.DestinatePlace,
                    Images = JsonConvert.SerializeObject(imagesUrl),
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

                    return RedirectToAction("Index", "TourInfo");
                }
                else
                {
                    return RedirectToAction("Edit", "TourInfo");
                }
            }
            return View(model);
        }

        [HttpGet]
        [ActionName("Detail")]
        public ActionResult Detail(string id)
        {
            ViewBag.PlacesPicklist = this.GetPlacesPicklist();

            if (id == null)
            {
                return RedirectToAction("Index", "Place");
            }

            string token = Session["token"].ToString();

            var client = connector.Initial();
            client.Timeout = -1;
            var request = new RestRequest("admin/tour-infos/" + id, Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = client.Execute(request);

            TourInfoModel tourInfo = new TourInfoModel();
            TourInfoEditViewModel model;

            if (response.IsSuccessful)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                TourInfoApiResultModel content = JsonConvert.DeserializeObject<TourInfoApiResultModel>(response.Content, settings);
                tourInfo = content.Data[0];
                model = new TourInfoEditViewModel()
                {
                    Id = tourInfo.Id,
                    Name = tourInfo.Name,
                    Images = tourInfo.Images,
                    Rating = tourInfo.Rating,
                    StartPlace = tourInfo.StartPlace.Id,
                    DestinatePlace = tourInfo.DestinatePlace.Id,
                    CreateBy = tourInfo.CreateBy.Name
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Place");
            }
        }
    }
}
