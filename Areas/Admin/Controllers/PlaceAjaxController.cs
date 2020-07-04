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
    public class PlaceAjaxController : Controller
    {
        private APIConnector connector = new APIConnector();
        public ActionResult GetPlaces()
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

                // call api to get users data
                string token = Session["token"].ToString();
                var client = connector.Initial();
                client.Timeout = -1;
                var request = new RestRequest("admin/places", Method.GET);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddParameter("page", page);
                request.AddParameter("pageSize", length);

                IRestResponse response = client.Execute(request);

                // handle result

                if (response.IsSuccessful)
                {
                    PlacesApiResultModel content = JsonConvert.DeserializeObject<PlacesApiResultModel>(response.Content);
                    List<PlaceModel> places = new List<PlaceModel>();
                    places.AddRange(content.Data);
                    totalRecords = content.Pagination.TotalElement;
                    int recFilter = totalRecords;

                    // handle search
                    if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrWhiteSpace(searchValue))
                    {
                        places = places.Where(
                            p => p.Name.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.Description.ToString().ToLower().Contains(searchValue.ToLower())
                            ).ToList();

                        totalRecords = places.Count;
                        recFilter = places.Count;
                    }

                    // handle sort by column
                    if (sortColumnName == "Name")
                    {
                        places = (sortDirection == "asc") ? places.OrderBy(u => u.Name).ToList() : places.OrderByDescending(u => u.Name).ToList();
                    }
                    else if (sortColumnName == "Description")
                    {
                        places = (sortDirection == "asc") ? places.OrderBy(u => u.Description).ToList() : places.OrderByDescending(u => u.Description).ToList();
                    }

                    result = Json(new
                    {
                        draw = Convert.ToInt32(draw),
                        recordsTotal = totalRecords,
                        recordsFiltered = totalRecords,
                        data = places
                    }, JsonRequestBehavior.AllowGet); ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public ActionResult DeletePlace(int placeId)
        {
            JsonResult result = new JsonResult();
            try
            {
                // call api to get users data
                string token = Session["token"].ToString();
                var client = connector.Initial();
                client.Timeout = -1;
                var request = new RestRequest("admin/places/" + placeId, Method.DELETE);
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