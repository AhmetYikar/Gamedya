using System.Web;
using System.Web.Optimization;

namespace Gamedya
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
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/Template/bootstrap").Include(
                     "~/Template/assets/js/jquery.min.js",
                     "~/Template/assets/js/owl.carousel.js",
                     "~/Template/assets/js/bootstrap.min.js",
                     "~/Template/assets/js/script.js",
                     "~/Template/assets/js/offcanvas.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Template/css").Include(
                      "~/Template/assets/css/bootstrap.min.css",
                      "~/Template/assets/fonts/font-awesome/css/font-awesome.min.css",
                      "~/Template/assets/css/owl.carousel.css",
                      "~/Template/assets/css/owl.theme.default.min.css",
                      "~/Template/assets/css/offcanvas.min.css",
                      "~/Template/assets/css/style.css"));

           
        }
    }
}
