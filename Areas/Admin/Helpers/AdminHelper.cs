using ginko_webapp.Helper;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Helpers
{
    public sealed class AdminHelper
    {
        private AdminHelper()
        {

        }
        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly AdminHelper instance = new AdminHelper();
        }
        public static AdminHelper Instance { get { return Nested.instance; } }


        public List<String> StoreImagur(List<HttpPostedFileBase> images)
        {
            string clientID = "Client-ID eee6fe9fcde03e2";
            byte[] imageData;
            string fileContent;
            List<String> listFileLinks = new List<String>();

            RestClient client = new RestClient("https://api.imgur.com/3/image");
            client.Timeout = -1;

            foreach (HttpPostedFileBase image in images)
            {
                if(image == null)
                {
                    continue;
                }

                imageData = new byte[image.ContentLength];
                image.InputStream.Read(imageData, 0, imageData.Length);
                image.InputStream.Close();
                fileContent = Convert.ToBase64String(imageData);

                
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", clientID);
                request.AddFile("image", imageData, image.FileName);
                request.AddParameter("album", "dPkd7RpwCPaPMB9");


                IRestResponse response = client.Execute(request);

                if(response.IsSuccessful)
                {
                    dynamic result = JsonConvert.DeserializeObject(response.Content);
                    listFileLinks.Add(((string)result.data.link));
                } 
            }
            return listFileLinks;
        }
    }
}