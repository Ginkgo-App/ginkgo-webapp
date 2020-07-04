using ginko_webapp.Areas.Admin.Models.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Models.APIResultModels
{
    public class PostsApiResultModel : APIResultModel
    {
        public List<PostModel> Data { get; set; }

        public Pagination Pagination { get; set; }
    }
}