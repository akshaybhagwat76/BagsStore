using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BagsStore.Models
{
    public class StripeSettings
    {
        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
    }
}