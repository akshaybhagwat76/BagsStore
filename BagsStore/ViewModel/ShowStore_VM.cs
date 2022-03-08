using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BagsStore.ViewModel
{
    public class ShowStore_VM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Type { get; set; }
        public string WorkingHours { get; set; }
        public string Price { get; set; }
        public string Picture { get; set; }
        public bool? Partner { get; set; }
        public int? LocationId { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string MapPicture { get; set; }
        public string Link { get; set; }
        public string RealName { get; set; }
        public string HourFrom { get; set; }
        public string HourTo { get; set; }
        public string Services { get; set; }
        public string DayOff { get; set; }
        public string Additional { get; set; }
        public string PictureMobile { get; set; }
    }
}