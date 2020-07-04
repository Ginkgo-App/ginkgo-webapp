using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Models.APIResultModels
{
    public class AuthenticationApiResultModel : APIResultModel
    {
        public List<Data> Data { get; set; }
    }

    public class Data
    {
        private string id;
        private string token;

        public string Id { get => id; set => id = (value != null) ? value : ""; }
        public string Token { get => token; set => token = (value != null) ? value : ""; }
    }
}