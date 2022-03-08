using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BagsStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              "Who we are",
              "who-we-are/",
              new { controller = "whoweare", action = "Index" }
            );

            routes.MapRoute(
            "Become our partner",
            "become-our-partner/",
            new { controller = "BecomeOurPartner", action = "Index" }
          );

            routes.MapRoute(
            "How it works",
            "how-it-works/",
            new { controller = "HowItWorks", action = "Index" }
          );

            routes.MapRoute(
            "Tips for trips",
            "tips-for-trips/",
            new { controller = "TipsForTrips", action = "Index" }
          );

            routes.MapRoute(
           "Terms and conditions",
           "terms-and-conditions/",
           new { controller = "TermsAndConditions", action = "Index" }
         );

            routes.MapRoute(
           "Privacy Policy",
           "privacy-policy/",
           new { controller = "PrivacyPolicy", action = "Index" }
         );

            routes.MapRoute(
             "Luggage storage Ljubljana",
             "luggage-storage/ljubljana",
             new { controller = "LuggageStorage", action = "Ljubljana" }
           );

            routes.MapRoute(
             "Luggage storage Bled",
             "luggage-storage/bled",
             new { controller = "LuggageStorage", action = "Bled" }
           );

            routes.MapRoute(
            "Luggage storage Belgrade",
            "luggage-storage/belgrade",
            new { controller = "LuggageStorage", action = "Belgrade" }
          );

            routes.MapRoute(
            "Luggage storage Nis",
            "luggage-storage/nis",
            new { controller = "LuggageStorage", action = "Nis" }
          );

            routes.MapRoute(
          "Luggage storage Viena",
          "luggage-storage/vienna",
          new { controller = "LuggageStorage", action = "Vienna" }
        );
            routes.MapRoute(
        "Luggage storage Budapest",
        "luggage-storage/budapest",
        new { controller = "LuggageStorage", action = "Budapest" }
      );

            routes.MapRoute(
             "Luggage storage Search",
             "luggage-storage/search",
             new { controller = "LuggageStorage", action = "Search" }
           );
            routes.MapRoute(
           "Luggage storage Store",
           "luggage-storage/Store/{id}",
           defaults: new { controller = "LuggageStorage", action = "Store", id=UrlParameter.Optional }
         );
            routes.MapRoute(
          "Luggage storage In",
          "luggage-storage/In/{cityname}",
          defaults: new { controller = "LuggageStorage", action = "In", cityname = UrlParameter.Optional }
        );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
           
        }
    }
}
