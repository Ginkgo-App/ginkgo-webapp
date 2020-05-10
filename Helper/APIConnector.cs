using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ginko_webapp.Helper
{
    public class APIConnector
    {
        public RestClient Initial()
        {   
            return new RestClient("https://micro-api-core.herokuapp.com/api/v1/");
        }
    }
}