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
    public class TourAjaxController : Controller
    {
        private APIConnector connector = new APIConnector();

        public ActionResult GetTours()
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
                int tourInfoId = Convert.ToInt32(Request["tourInfoId"]);

                // call api to get data
                string token = Session["token"].ToString();
                var client = connector.Initial();
                client.Timeout = -1;
                var request = new RestRequest("admin/tour-infos/" + tourInfoId + "/tours", Method.GET);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddParameter("page", page);
                request.AddParameter("pageSize", length);
                request.AddParameter("id", tourInfoId);

                IRestResponse response = client.Execute(request);

                // handle result

                if (response.IsSuccessful)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    TourApiResultModel content = JsonConvert.DeserializeObject<TourApiResultModel>(response.Content, settings);
                    List<TourModel> tours = new List<TourModel>();
                    tours.AddRange(content.Data);
                    totalRecords = content.Pagination.TotalElement;
                    int recFilter = totalRecords;

                    // handle search
                    if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrWhiteSpace(searchValue))
                    {
                        tours = tours.Where(
                            p => p.Name.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.StartDay.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.EndDay.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.TotalDay.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.TotalNight.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.Price.ToString().ToLower().Contains(searchValue.ToLower())
                            ).ToList();

                        totalRecords = tours.Count;
                        recFilter = tours.Count;
                    }

                    // handle sort by column
                    if (sortColumnName == "Name")
                    {
                        tours = (sortDirection == "asc") ? tours.OrderBy(t => t.Name).ToList() : tours.OrderByDescending(t => t.Name).ToList();
                    }
                    else if (sortColumnName == "StartDay")
                    {
                        tours = (sortDirection == "asc") ? tours.OrderBy(t => t.StartDay).ToList() : tours.OrderByDescending(t => t.StartDay).ToList();
                    }
                    else if (sortColumnName == "EndDay")
                    {
                        tours = (sortDirection == "asc") ? tours.OrderBy(t => t.EndDay).ToList() : tours.OrderByDescending(t => t.EndDay).ToList();
                    }
                    else if (sortColumnName == "TotalDay")
                    {
                        tours = (sortDirection == "asc") ? tours.OrderBy(t => t.TotalDay).ToList() : tours.OrderByDescending(t => t.TotalDay).ToList();
                    }
                    else if (sortColumnName == "TotalNight")
                    {
                        tours = (sortDirection == "asc") ? tours.OrderBy(t => t.TotalNight).ToList() : tours.OrderByDescending(t => t.TotalNight).ToList();
                    }
                    else if (sortColumnName == "Price")
                    {
                        tours = (sortDirection == "asc") ? tours.OrderBy(t => t.Price).ToList() : tours.OrderByDescending(t => t.Price).ToList();
                    }

                    result = Json(new
                    {
                        draw = Convert.ToInt32(draw),
                        recordsTotal = totalRecords,
                        recordsFiltered = totalRecords,
                        data = tours
                    }, JsonRequestBehavior.AllowGet); ;
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