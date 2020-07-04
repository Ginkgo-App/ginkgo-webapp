using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Models.ObjectModels
{
    public class PlaceModel
    {
        private int id;
        private string name;
        private string description;
        private List<string> images;
        private List<HttpPostedFileBase> fileImages;
        private int typeId;

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;

            }
        }
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;

            }
        }
        public List<string> Images
        {
            get
            {
                return this.images;
            }
            set
            {
                this.images = value;

            }
        }
        public int TypeId { get => typeId; set => typeId = value; }
        public List<HttpPostedFileBase> FileImages { get => fileImages; set => fileImages = value; }
    }
}