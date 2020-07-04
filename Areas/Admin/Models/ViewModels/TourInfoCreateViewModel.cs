using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ginko_webapp.Areas.Admin.Models.ViewModels
{
    public class TourInfoCreateViewModel
    {
        private int id = 0;
        private string name = "";
        private List<HttpPostedFileBase> images = new List<HttpPostedFileBase>();
        private string deletedAt = "";
        private double rating = 0;
        private int startPlace = 0;
        private int destinatePlace = 0;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public List<HttpPostedFileBase> Images { get => images; set => images = value; }
        public string DeletedAt { get => deletedAt; set => deletedAt = value; }
        public double Rating { get => rating; set => rating = value; }
        public int StartPlace { get => startPlace; set => startPlace = value; }
        public int DestinatePlace { get => destinatePlace; set => destinatePlace = value; }
    }
}