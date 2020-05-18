using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Models.APIResultModels
{
    public class APIResultModel
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }
}