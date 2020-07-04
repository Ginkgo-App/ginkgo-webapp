using ginko_webapp.Areas.Admin.Models.APIResultModels;
using ginko_webapp.Areas.Admin.Models.ObjectModels;
using ginko_webapp.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ginko_webapp.Areas.Admin.Controllers
{
    public class TourController : Controller
    {
        private APIConnector connector = new APIConnector();

        [HttpGet]
        public ActionResult Create()
        {
            TourModel model = new TourModel();
            return View(model);
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePost(TourModel model)
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
                var request = new RestRequest("admin/tour-infos/" + model.TourInfoId + "/tours", Method.POST);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("id", model.TourInfoId);
                request.AddJsonBody(new
                {
                    Name = model.Name,
                    StartDay = model.StartDay,
                    EndDay = model.EndDay,
                    TotalDay = model.TotalDay,
                    TotalNight = model.TotalNight,
                    MaxMember = model.MaxMember,
                    TourInfoId = model.TourInfoId,
                    Price = model.Price,
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
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string tourInfoId, string tourId)
        {
            if (tourId == null || tourInfoId == null)
            {
                return RedirectToAction("Index", "TourInfo");
            }

            string token = Session["token"].ToString();

            var client = connector.Initial();
            client.Timeout = -1;
            var request = new RestRequest("admin/tour-infos/" + tourInfoId + "/tours/" + tourId, Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("id", tourInfoId);
            request.AddParameter("tourId", tourId);
            
            IRestResponse response = client.Execute(request);

            TourModel model;

            if (response.IsSuccessful)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                TourApiResultModel content = JsonConvert.DeserializeObject<TourApiResultModel>(response.Content, settings);
                model = content.Data[0];
                
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "TourInfo");
            }
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(string tourInfoId, string tourId, TourModel model)
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
                var request = new RestRequest("admin/tour-infos/" + tourInfoId + "/tours/" + tourId, Method.PUT);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("id", tourInfoId);
                request.AddParameter("tourId", tourId);
                request.AddJsonBody(new
                {
                    Id = tourId,
                    Name = model.Name,
                    Rating = model.Rating,
                    StartDay = model.StartDay,
                    EndDay = model.EndDay,
                    TotalDay = model.TotalDay,
                    TotalNight = model.TotalNight,
                    MaxMember = model.MaxMember,
                    Price = model.Price,
                    Timeline = JsonConvert.DeserializeObject<List<TimelineModel>>(Request["JsonTimelines"]),
                    Service = JsonConvert.DeserializeObject<List<String>>(Request["JsonServices"]),
                    TourInfo = JsonConvert.DeserializeObject<TourInfoModel>(Request["JsonTourInfo"]),
                    CreateBy = JsonConvert.DeserializeObject<UserModel>(Request["JsonCreateBy"]),
                    JoinAt = Request["JsonJoinAt"],
                    AcceptedAt = Request["JsonAcceptedAt"]

                });

                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    APIResultModel result = JsonConvert.DeserializeObject<APIResultModel>(response.Content);
                    if (result.ErrorCode != 0)
                    {
                        ViewBag.error = result.Message;
                        return RedirectToAction("Edit", "Tour", new { tourInfoId = tourInfoId, tourId = tourId });
                        
                    }
                    return RedirectToAction("Detail", "TourInfo", new { id = tourInfoId });
                }
                else
                {
                    return RedirectToAction("Edit", "Tour", new { tourInfoId = tourInfoId, tourId = tourId });
                }
            }
            return View(model);
        } 
    }
}