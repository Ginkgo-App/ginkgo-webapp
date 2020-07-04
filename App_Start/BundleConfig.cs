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

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include("~/Scripts/Libraries/Datatable/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable-bootstrap").Include("~/Scripts/Libraries/Datatable/dataTables.bootstrap4.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/Libraries/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/Libraries/Bootstrap/bootstrap.js"));

            bundles.Add(new StyleBundle("~/fontawesome/css").Include("~/Content/Libraries/FontAwesome/all.min.css"));

            bundles.Add(new StyleBundle("~/bootstrap/css").Include("~/Content/Libraries/Bootstrap/bootstrap.css"));

            bundles.Add(new StyleBundle("~/datatable/css").Include("~/Content/Libraries/Datatable/dataTables.bootstrap4.min.css"));


            // Admin custom js, css
            bundles.Add(new StyleBundle("~/admin/css").Include("~/Content/style-admin.min.css"));
            bundles.Add(new ScriptBundle("~/admin/js").Include("~/Scripts/script-admin.min.js"));

            // Date Picker
            bundles.Add(new ScriptBundle("~/datepicker/js").Include("~/Scripts/Libraries/DatePicker/bootstrap-datepicker.min.js")); 
            bundles.Add(new StyleBundle("~/datepicker/css").Include("~/Content/Libraries/DatePicker/bootstrap-datepicker3.min.css"));

            // Avt Picker
            bundles.Add(new ScriptBundle("~/avtpicker/js").Include("~/Scripts/Customs/AvatarPicker.js"));
            bundles.Add(new StyleBundle("~/avtpicker/css").Include("~/Content/Customs/AvatarPicker.css"));

            // Select2
            bundles.Add(new ScriptBundle("~/select2/js").Include("~/Scripts/Libraries/Select2/select2.min.js"));
            bundles.Add(new StyleBundle("~/select2/css").Include("~/Content/Libraries/Select2/select2.min.css", 
                "~/Content/Libraries/Select2/select2-bootstrap.min.css"));

            // Bootstrap file input
            bundles.Add(new ScriptBundle("~/file-input/js").Include("~/Scripts/Libraries/Bootstrap-FileInput/fileinput.min.js",
                "~/Scripts/Libraries/Bootstrap-FileInput/themes/fas/theme.min.js"));
            bundles.Add(new StyleBundle("~/file-input/css").Include("~/Content/Libraries/Bootstrap-FileInput/fileinput.min.css"));

            // Bootstrap Dynamic Table
            bundles.Add(new ScriptBundle("~/bstable/js").Include("~/Scripts/Libraries/crud-bstable/bstable.js"));
        }
    }
}
