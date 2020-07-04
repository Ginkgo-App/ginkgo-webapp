using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Models.ViewModels
{
    public class TourInfoEditViewModel
    {
        private int id = 0;
        private string name = "";
        private List<string> images = new List<string>();
        private List<HttpPostedFileBase> fileImages = new List<HttpPostedFileBase>();
        private string deletedAt = "";
        private double rating = 0;
        private int startPlace = 0;
        private int destinatePlace = 0;
        private string createBy;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public List<string> Images { get => images; set => images = value; }
        public string DeletedAt { get => deletedAt; set => deletedAt = value; }
        public double Rating { get => rating; set => rating = value; }
        public int StartPlace { get => startPlace; set => startPlace = value; }
        public int DestinatePlace { get => destinatePlace; set => destinatePlace = value; }
        public string CreateBy { get => createBy; set => createBy = value; }
        public List<HttpPostedFileBase> FileImages { get => fileImages; set => fileImages = value; }
    }
}