using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Models.ObjectModels
{
    public class TourInfoModel
    {
        private int id;
        private string name;
        private List<string> images;
        private string deletedAt;
        private double rating;
        private PlaceModel startPlace;
        private PlaceModel destinatePlace;
        private UserModel createBy;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public List<string> Images { get => images; set => images = value; }
        public string DeletedAt { get => deletedAt; set => deletedAt = value; }
        public double Rating { get => rating; set => rating = value; }
        public PlaceModel StartPlace { get => startPlace; set => startPlace = value; }
        public PlaceModel DestinatePlace { get => destinatePlace; set => destinatePlace = value; }
        public UserModel CreateBy { get => createBy; set => createBy = value; }
    }
}