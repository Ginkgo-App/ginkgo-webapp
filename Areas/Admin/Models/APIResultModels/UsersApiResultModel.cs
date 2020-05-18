using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Models.APIResultModels
{
    public class UsersApiResultModel :  APIResultModel
    {
        public List<UserModel> Data { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public int TotalPage { get; set; }
        public int TotalElement { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}