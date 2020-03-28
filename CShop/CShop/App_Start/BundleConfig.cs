using System.Web;
using System.Web.Optimization;

namespace CShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Content/js/bootstrap.min.js", "~/Content/js/easing.js", "~/Content/move-top.js", "~/Content/js/jquery.flexisel.js", "~/Content/js/responsiveslides.min.js", "~/Content/js/jquery-2.2.3.min.js"));
           
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/respond.js", "~/Content/js/owl.carousel.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(

                      "~/Content/css/w.css", "~/Content/css/bootstrap.min.css", "~/Content/css/b.css", "~/Content/css/fontawesome-all.min.css","~/Content/css/jquery-ui1.css", "~/Content/css/easy-responsive-tabs.css", "~/Content/css/flexslider.css", "~/Content/css/flexslider.css", "~/Content/css/JiSlider.css", "~/Content/css/shop.css", "~/Content/css/style.css"));
        }
    }
}
