using System.Web;
using System.Web.Optimization;

namespace BagsStore
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/css/css").Include(
                     "~/css/bootstrap.min.css",
                     "~/css/jquery.datetimepicker.min.css",
                     "~/css/font-awesome.css",
                     "~/css/owl.carousel.min.css",
                     "~/css/owl.theme.default.min.css",
                     "~/css/style.css"));

          //  var cdnPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js";

            bundles.Add(new ScriptBundle("~/js/js").Include(
                     "~/js/jquery.min.js",
                     "~/js/jquery.datetimepicker.js",
                     "~/js/bootstrap.min.js",
                     "~/js/owl.carousel.min.js",
                     "~/js/script.js"
                     ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
