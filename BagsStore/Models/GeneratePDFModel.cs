using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BagsStore.Models
{
    public class GeneratePDFModel
    {
        public String PDFContent { get; set; }
        public String PDFLogo { get; set; }
        public String Number { get; set; }
        public String Price { get; set; }

        public String StoreName { get; set; }

        public String PickUp { get; set; }
        public String Bags { get; set; }

        public String DropOff { get; set; }

        public String Name { get; set; }

        public String LastName { get; set; }

        public String Phone { get; set; }

        public String Email { get; set; }
        public String PaymentMethod { get; set; }

        public String Link { get; set; }

        public String MapPicture { get; set; }

        public String EmailConf { get; set; }

        public String WHours { get; set; }

        public String Street { get; set; }

        public String Additional { get; set; }
    }
}