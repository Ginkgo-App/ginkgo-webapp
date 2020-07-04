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
    public class PostAjaxController : Controller
    {
        private APIConnector connector = new APIConnector();
        public ActionResult GetPosts()
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
                var request = new RestRequest("admin/posts", Method.GET);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddParameter("page", page);
                request.AddParameter("pageSize", length);

                IRestResponse response = client.Execute(request);

                // handle result

                if (response.IsSuccessful)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    PostsApiResultModel content = JsonConvert.DeserializeObject<PostsApiResultModel>(response.Content, settings);
                    List<PostModel> posts = new List<PostModel>();
                    posts.AddRange(content.Data);
                    totalRecords = content.Pagination.TotalElement;
                    int recFilter = totalRecords;

                    // handle search
                    if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrWhiteSpace(searchValue))
                    {
                        posts = posts.Where(
                            p => p.Id.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.Content.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.Author.Name.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.Rating.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.TotalLike.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.TotalComment.ToString().ToLower().Contains(searchValue.ToLower())
                            ).ToList();

                        totalRecords = posts.Count;
                        recFilter = posts.Count;
                    }

                    // handle sort by column
                    if (sortColumnName == "Id")
                    {
                        posts = (sortDirection == "asc") ? posts.OrderBy(u => u.Id).ToList() : posts.OrderByDescending(u => u.Id).ToList();
                    }
                    else if (sortColumnName == "Content")
                    {
                        posts = (sortDirection == "asc") ? posts.OrderBy(u => u.Content).ToList() : posts.OrderByDescending(u => u.Content).ToList();
                    }
                    else if (sortColumnName == "Author")
                    {
                        posts = (sortDirection == "asc") ? posts.OrderBy(u => u.Author.Name).ToList() : posts.OrderByDescending(u => u.Author.Name).ToList();
                    }
                    else if (sortColumnName == "Rating")
                    {
                        posts = (sortDirection == "asc") ? posts.OrderBy(u => u.Rating).ToList() : posts.OrderByDescending(u => u.Rating).ToList();
                    }
                    else if (sortColumnName == "TotalLike")
                    {
                        posts = (sortDirection == "asc") ? posts.OrderBy(u => u.TotalLike).ToList() : posts.OrderByDescending(u => u.TotalLike).ToList();
                    }
                    else if (sortColumnName == "TotalComment")
                    {
                        posts = (sortDirection == "asc") ? posts.OrderBy(u => u.TotalComment).ToList() : posts.OrderByDescending(u => u.TotalComment).ToList();
                    }

                    result = Json(new
                    {
                        draw = Convert.ToInt32(draw),
                        recordsTotal = totalRecords,
                        recordsFiltered = totalRecords,
                        data = posts
                    }, JsonRequestBehavior.AllowGet); ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public ActionResult DeletePost(int postId)
        {
            JsonResult result = new JsonResult();
            try
            {
                // call api to get users data
                string token = Session["token"].ToString();
                var client = connector.Initial();
                client.Timeout = -1;
                var request = new RestRequest("admin/posts/" + postId, Method.DELETE);
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