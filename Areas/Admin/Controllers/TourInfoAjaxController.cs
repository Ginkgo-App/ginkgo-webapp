using ginko_webapp.Areas.Admin.Models.APIResultModels;
using ginko_webapp.Areas.Admin.Models.ObjectModels;
using ginko_webapp.Helper;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ginko_webapp.Areas.Admin.Controllers
{
    public class TourInfoAjaxController : Controller
    {
        private APIConnector connector = new APIConnector();

        public ActionResult GetTourInfos()
        {
            JsonResult result = new JsonResult();
            try
            {
                // server side params
                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string draw = Request.Form.GetValues("draw")[0];
                string searchValue = Request.Form.GetValues("search[value]")[0];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];
                int totalRecords = 0;
                int page = start / length + 1;

                // call api to get data
                string token = Session["token"].ToString();
                var client = connector.Initial();
                client.Timeout = -1;
                var request = new RestRequest("admin/tour-infos", Method.GET);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddParameter("page", page);
                request.AddParameter("pageSize", length);

                IRestResponse response = client.Execute(request);

                // handle result

                if (response.IsSuccessful)
                {
                    var settings = new JsonSerializerSettings { 
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    TourInfoApiResultModel content = JsonConvert.DeserializeObject<TourInfoApiResultModel>(response.Content, settings);
                    List<TourInfoModel> tourInfos = new List<TourInfoModel>();
                    tourInfos.AddRange(content.Data);
                    totalRecords = content.Pagination.TotalElement;
                    int recFilter = totalRecords;

                    // handle search
                    if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrWhiteSpace(searchValue))
                    {
                        tourInfos = tourInfos.Where(
                            p => p.Name.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.Rating.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.StartPlace.Name.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.DestinatePlace.Name.ToString().ToLower().Contains(searchValue.ToLower())
                            ).ToList();

                        totalRecords = tourInfos.Count;
                        recFilter = tourInfos.Count;
                    }

                    // handle sort by column
                    if (sortColumnName == "Name")
                    {
                        tourInfos = (sortDirection == "asc") ? tourInfos.OrderBy(t => t.Name).ToList() : tourInfos.OrderByDescending(t => t.Name).ToList();
                    }
                    else if (sortColumnName == "Rating")
                    {
                        tourInfos = (sortDirection == "asc") ? tourInfos.OrderBy(t => t.Rating).ToList() : tourInfos.OrderByDescending(t => t.Rating).ToList();
                    }
                    else if (sortColumnName == "StartPlace")
                    {
                        tourInfos = (sortDirection == "asc") ? tourInfos.OrderBy(t => t.StartPlace.Name).ToList() : tourInfos.OrderByDescending(t => t.StartPlace.Name).ToList();
                    }
                    else if (sortColumnName == "DestinatePlace")
                    {
                        tourInfos = (sortDirection == "asc") ? tourInfos.OrderBy(t => t.DestinatePlace.Name).ToList() : tourInfos.OrderByDescending(t => t.DestinatePlace.Name).ToList();
                    }

                    result = Json(new
                    {
                        draw = Convert.ToInt32(draw),
                        recordsTotal = totalRecords,
                        recordsFiltered = totalRecords,
                        data = tourInfos
                    }, JsonRequestBehavior.AllowGet); ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public ActionResult DeleteTourInfo(int tourinfoid)
        {
            JsonResult result = new JsonResult();
            try
            {
                // call api to get users data
                string token = Session["token"].ToString();
                var client = connector.Initial();
                client.Timeout = -1;
                var request = new RestRequest("admin/tour-infos/" + tourinfoid, Method.DELETE);
                request.AddHeader("Authorization", "Bearer " + token);

                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    return Json(new { Status = "success" });
                }
                else
                {
                    return Json(new { Status = "false" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
    }
}