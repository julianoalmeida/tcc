using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            #region Bundle CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(
             "~/Content/css/bootstrap.min.css",
             "~/Content/css/bootstrap-responsive.min.css",
             "~/Content/css/backtotop.css",
             "~/Content/css/datepicker.css",
             "~/Content/css/datatables/jquery.dataTables.css",
             "~/Content/css/datatables/jquery.dataTables_themeroller.css",
             "~/Content/css/main.css"));

            bundles.Add(new StyleBundle("~/Content/css/font").Include(
             "~/Content/css/font/css/googleFonts.css",
             "~/Content/css/font/css/font-awesome.min.css"));
            #endregion

            bundles.Add(new ScriptBundle("~/Content/Scripts").Include(
            "~/Content/Scripts/jquery/jquery-2.1.0.js",
            "~/Content/Scripts/jquery-ui/jquery-ui-1,10,4.js",
            "~/Content/Scripts/jquery/BackToTop.jquery.js",

            "~/Content/Scripts/bootstrap/bootstrap.min.js",

            "~/Content/Scripts/outros/placeholder.js",
            "~/Content/Scripts/outros/bootbox.min.js",
            "~/Content/Scripts/outros/maskedInput.js",
            "~/Content/Scripts/outros/jquery.meio.mask.js",
            "~/Content/Scripts/outros/jquery.meio.mask.config.js",
            "~/Content/Scripts/outros/jscast-maxlength.js",

            "~/Content/Scripts/bootstrap/bootstrap-datepicker.js",
            "~/Content/Scripts/datatables/jquery.dataTables.js",
            "~/Content/Scripts/main.js",
            "~/Content/Scripts/sistema/modaisGeral.js"
            ));
        }
    }
}