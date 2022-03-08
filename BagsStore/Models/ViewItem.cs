using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BagsStore.Models
{
    public class ViewItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public String Title { get; set; }
        public String SecondTitle { get; set; }
        public String Cat { get; set; }
        public String CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? Active { get; set; }
        public String Image { get; set; }
        public String SortDesc { get; set; }
    }
}