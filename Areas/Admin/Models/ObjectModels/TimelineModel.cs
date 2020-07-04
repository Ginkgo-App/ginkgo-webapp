using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Models.ObjectModels
{
    public class TimelineModel
    {
        private int id;
        private string day;
        private string description;
        private int tourId;
        private string deletedAt;
        private List<TimeLineDetailModel> timelineDetails;

        public int Id { get => id; set => id = value; }
        public string Day {
            get
            {
                if (this.day == null)
                {
                    return "";
                }
                else
                {
                    DateTime convertDate = DateTime.Parse(this.day.ToString());
                    return convertDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
            }
            set
            {
                this.day = value;
            }
        }
        public string Description { get => description; set => description = value; }
        public int TourId { get => tourId; set => tourId = value; }
        public string DeletedAt { get => deletedAt; set => deletedAt = value; }
        public List<TimeLineDetailModel> TimelineDetails { get => timelineDetails; set => timelineDetails = value; }
    }

    public class TimeLineDetailModel
    {
        private int placeId;
        private int timelineId;
        private string time;
        private string detail;
        private string deletedAt;

        public int PlaceId { get => placeId; set => placeId = value; }
        public int TimelineId { get => timelineId; set => timelineId = value; }
        public string Time { get => time; set => time = value; }
        public string Detail { get => detail; set => detail = value; }
        public string DeletedAt { get => deletedAt; set => deletedAt = value; }
    }
}