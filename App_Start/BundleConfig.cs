using System.Web;
using System.Web.Optimization;

namespace BookStore
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            //khai baso teen va copy theo thu tu cua css va js
            bundles.Add(new StyleBundle("~/freecss").Include(
                     "~/css/font-awesome.css",
                     "~/css/bootstrap.css",
                     "~/css/jquery.smartmenus.bootstrap.css",
                      "~/css/jquery.simpleLens.css",
                      "~/css/slick.css",
                      "~/css/nouislider.css",
                       "~/css/theme-color/default-theme.css",
                       "~/css/sequence-theme.modern-slide-in.css",
                       "~/css/style.css"

                     ));
            //google font  https://fonts.googleapis.com/css?family=Lato
            //google font https://fonts.googleapis.com/css?family=Raleway

            //jquerylibrary https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js
            bundles.Add(new StyleBundle("~/freejs").Include(
                     "~/js/bootstrap.js",
                     "~/js/jquery.smartmenus.js",
                     "~/js/jquery.smartmenus.bootstrap.js",
                     "~/js/sequence.js",
                     "~/js/sequence-theme.modern-slide-in.js",
                     "~/js/jquery.simpleGallery.js",
                     "~/js/jquery.simpleLens.js",
                     "~/js/slick.js",
                     "~/js/nouislider.js",
                     "~/js/custom.js"
                     ));

        }
    }
}
