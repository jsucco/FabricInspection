using System.Web;
using System.Web.Optimization;

namespace Inspection_mvc
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                        "~/Scripts/jquery-1.11.1.min.js", 
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/grid.locale-en.js", 
                        "~/Scripts/jquery.jqGrid.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            //bundles.Add(new StyleBundle("~/Content/jqgrid").Include(
            //          "~/Content/ui.jqgrid.css",
            //          "~/Content/jquery.wijmo-pro.all.3.20141.34.min.css", 
            //          "~/Content/jquery-wijmo.css"
            //    ));
            bundles.Add(new StyleBundle("~/Content/jqgrid").Include(
                      "~/Content/jquery-wijmo.css",
                      "~/Content/jquery.wijmo-pro.all.3.20141.34.min.css",
                      "~/Content/ui.jqgrid.css"
                ));
        }
    }
}
