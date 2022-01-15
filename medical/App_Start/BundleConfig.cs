using System.Web;
using System.Web.Optimization;

namespace medical
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
        //    bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
        //                "~/Scripts/jquery-{version}.js"));

        //    bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
        //                "~/Scripts/jquery.validate*"));

        //    // Use the development version of Modernizr to develop with and learn from. Then, when you're
        //    // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
        //    bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
        //                "~/Scripts/modernizr-*"));

        //    bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
        //              "~/Content/sb-admin-2.js", "~/Content/sb-admin-2.min.js"));

        //    bundles.Add(new StyleBundle("~/Content/css").Include(
        //              "~/Content/sb-admin-2.css",
        //              "~/Content/sb-admin-2.min.css"));




            bundles.Add(new ScriptBundle("~/bundles/special/jquery").Include(
                        "~/Scripts/special/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/special/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/special/modernizr").Include(
                        "~/Scripts/special/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/special/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/special/css").Include(
                      "~/Content/special/bootstrap.css",
                      "~/Content/special/site.css"));
        }
    }
}
