using System.Web;
using System.Web.Optimization;

namespace GameAdmin
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

            bundles.Add(new StyleBundle("~/Assest/css").Include(
                  "~/Assest/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/Assest/bower_components/font-awesome/css/font-awesome.min.css",
                      "~/Assest/bower_components/Ionicons/css/ionicons.min.css",
                      "~/Assest/dist/css/AdminLTE.min.css",
                      "~/Assest/dist/css/skins/_all-skins.min.css",
                      "~/Assest/bower_components/morris.js/morris.css",
                      "~/Assest/bower_components/jvectormap/jquery-jvectormap.css",
                      "~/Assest/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css",
                      "~/Assest/bower_components/bootstrap-daterangepicker/daterangepicker.css",
                      "~/Assest/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"));

            bundles.Add(new ScriptBundle("~/Assest/bootstrap").Include(
                    "~/Assest/bower_components/jquery/dist/jquery.min.js",
                    "~/Assest/bower_components/jquery-ui/jquery-ui.min.js",
                    "~/Assest/bower_components/bootstrap/dist/js/bootstrap.min.js",
                    "~/Assest/bower_components/raphael/raphael.min.js",
                    "~/Assest/bower_components/morris.js/morris.min.js",
                    "~/Assest/bower_components/jquery-sparkline/dist/jquery.sparkline.min.js",
                    "~/Assest/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                    "~/Assest/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                    "~/Assest/bower_components/jquery-knob/dist/jquery.knob.min.js",
                    "~/Assest/bower_components/moment/min/moment.min.js",
                    "~/Assest/bower_components/bootstrap-daterangepicker/daterangepicker.js",
                    "~/Assest/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
                    "~/Assest/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                    "~/Assest/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                    "~/Assest/bower_components/fastclick/lib/fastclick.js",
                    "~/Assest/dist/js/adminlte.min.js",
                    "~/Assest/dist/js/pages/dashboard.js",
                    "~/Assest/dist/js/demo.js"));
        }
    }
}
