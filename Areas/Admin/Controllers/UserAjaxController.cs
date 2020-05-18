using ginko_webapp.Areas.Admin.Models.APIResultModels;
using ginko_webapp.Areas.Admin.Models;
using ginko_webapp.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ginko_webapp.Areas.Admin.Controllers
{
    public class UserAjaxController : Controller
    {
        private APIConnector connector = new APIConnector();

        public ActionResult GetUsers()
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
                var request = new RestRequest("admin/users", Method.GET);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddParameter("page", page);
                request.AddParameter("pageSize", length);

                IRestResponse response = client.Execute(request);

                // handle result

                if (response.IsSuccessful)
                {
                    UsersApiResultModel content = JsonConvert.DeserializeObject<UsersApiResultModel>(response.Content);
                    List<UserModel> users = new List<UserModel>();
                    users.AddRange(content.Data);
                    totalRecords = content.Pagination.TotalElement;
                    int recFilter = totalRecords;

                    // handle search
                    if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrWhiteSpace(searchValue))
                    {
                        users = users.Where(
                            p => p.Name.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.Email.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.PhoneNumber.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.Birthday.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.Gender.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.Address.ToString().ToLower().Contains(searchValue.ToLower()) ||
                            p.Role.ToString().ToLower().Contains(searchValue.ToLower())
                            ).ToList();

                        totalRecords = users.Count;
                        recFilter = users.Count;
                    }

                    // handle sort by column
                    if(sortColumnName == "Name")
                    {
                        users = (sortDirection == "asc") ? users.OrderBy(u => u.Name).ToList() : users.OrderByDescending(u => u.Name).ToList();
                    }
                    else if(sortColumnName == "Email")
                    {
                        users = (sortDirection == "asc") ? users.OrderBy(u => u.Email).ToList() : users.OrderByDescending(u => u.Email).ToList();
                    }
                    else if(sortColumnName == "Phone")
                    {
                        users = (sortDirection == "asc") ? users.OrderBy(u => u.PhoneNumber).ToList() : users.OrderByDescending(u => u.PhoneNumber).ToList();
                    }
                    else if(sortColumnName == "Birthday")
                    {
                        users = (sortDirection == "asc") ? users.OrderBy(u => u.Birthday).ToList() : users.OrderByDescending(u => u.Birthday).ToList();
                    }
                    else if(sortColumnName == "Gender")
                    {
                        users = (sortDirection == "asc") ? users.OrderBy(u => u.Gender).ToList() : users.OrderByDescending(u => u.Gender).ToList();
                    }
                    else if(sortColumnName == "Address")
                    {
                        users = (sortDirection == "asc") ? users.OrderBy(u => u.Address).ToList() : users.OrderByDescending(u => u.Address).ToList();
                    }
                    else if(sortColumnName == "Role")
                    {
                        users = (sortDirection == "asc") ? users.OrderBy(u => u.Role).ToList() : users.OrderByDescending(u => u.Role).ToList();
                    }

                    result = Json(new {
                        draw = Convert.ToInt32(draw),
                        recordsTotal = totalRecords,
                        recordsFiltered = totalRecords,
                        data = users
                    }, JsonRequestBehavior.AllowGet);;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
    }

    public class DataTableData
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<UserModel> data { get; set; }
    }
};