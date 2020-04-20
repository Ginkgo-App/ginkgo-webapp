using System.Web;
using System.Web.Optimization;

namespace ginko_webapp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/Libraries/JQuery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/Libraries/JQuery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryeasing").Include("~/Scripts/Libraries/JQuery/jquery.easing.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapbundle").Include("~/Scripts/Libraries/Bootstrap/bootstrap.bundle.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/Libraries/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/Libraries/Bootstrap/bootstrap.js"));

            bundles.Add(new StyleBundle("~/fontawesome/css").Include("~/Content/Libraries/FontAwesome/all.min.css"));

            bundles.Add(new StyleBundle("~/bootstrap/css").Include("~/Content/Libraries/Bootstrap/bootstrap.css"));

            // Admin custom js, css
            bundles.Add(new StyleBundle("~/admin/css").Include("~/Content/style-admin.min.css"));
            bundles.Add(new ScriptBundle("~/admin/js").Include("~/Scripts/script-admin.min.js"));
        }
    }
}
