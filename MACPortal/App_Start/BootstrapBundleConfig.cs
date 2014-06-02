using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace BootstrapSupport
{
    public class BootstrapBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-migrate-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/jquery.validate.js",
                "~/scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js",
                "~/Scripts/Highcharts-3.0.1/js/highcharts.js",
                "~/Scripts/jquery.lazyload.js",
                "~/Scripts/knockout-3.1.0.js",
                "~/Scripts/knockout.mapping-latest.js",
                "~/Scripts/fineup/all.fineuploader-4.4.0.min.js",
                "~/Scripts/jquery.cookie.js",
                "~/Scripts/jshashtable-3.0.js",
                "~/Scripts/lightbox-2.6.min.js",
                "~/Scripts/site.js"
                ));

            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/fonts.css",
                "~/Content/themes/default/bootstrap.css",
                "~/Content/bootstrap-responsive.css",
                "~/Content/bootstrap-mvc-validation.css",
                "~/Content/fineup/fineuploader-4.4.0.min.css",
                "~/Content/lightbox.css",
                "~/Content/body.css"
                ));
        }
    }
}