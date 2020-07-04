using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace ginko_webapp.Areas.Admin.Models.ObjectModels
{
    public class TourModel
    {
        private int id;
        private UserModel createBy;
        private string name;
        private string startDay;
        private string endDay;
        private int totalDay;
        private int totalNight;
        private int maxMember;
        private int tourInfoId;
        private long price;
        private List<string> services;
        private List<TimelineModel> timelines;
        private string joinAt;
        private string acceptedAt;
        private double rating;
        private TourInfoModel tourInfo;
        private string timelinesJson;

        public int Id { get => id; set => id = value; }
        public UserModel CreateBy { get => createBy; set => createBy = value; }
        public string Name {
            get
            {
                if (this.startDay == null)
                {
                    return "";
                }
                else
                {
                    return this.name;
                }
            }
            set
            {
                this.name = value;
            }
        }
        public string StartDay {
            get
            {
                if(this.startDay == null)
                {
                    return "";
                }
                else
                {
                    DateTime convertDate = DateTime.Parse(this.startDay.ToString());
                    return convertDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
            }
            set
            {
                this.startDay = value;
            }
        }
        public string EndDay {
            get
            {
                if (this.endDay == null)
                {
                    return "";
                }
                else
                {
                    DateTime convertDate = DateTime.Parse(this.endDay.ToString());
                    return convertDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
            }
            set
            {
                this.endDay = value;
            }
        }
        public int TotalDay { get => totalDay; set => totalDay = value; }
        public int TotalNight { get => totalNight; set => totalNight = value; }
        public int MaxMember { get => maxMember; set => maxMember = value; }
        public int TourInfoId { get => tourInfoId; set => tourInfoId = value; }
        public long Price { get => price; set => price = value; }
        public List<string> Services { get => services; set => services = value; }
        public List<TimelineModel> Timelines
        {
            get
            {
                return this.timelines;
            }
            set
            {
                this.timelines = value;
                this.timelinesJson = Json.Encode(value);
            }
        }
        public string JoinAt { get => joinAt; set => joinAt = value; }
        public string AcceptedAt { get => acceptedAt; set => acceptedAt = value; }
        public double Rating { get => rating; set => rating = value; }
        public TourInfoModel TourInfo { get => tourInfo; set => tourInfo = value; }
        public string TimelinesJson { get => timelinesJson; set => timelinesJson = value; }
    }
}