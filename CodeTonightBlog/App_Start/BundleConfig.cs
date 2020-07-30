using System.Web;
using System.Web.Optimization;

namespace CodeTonightBlog
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;
            bundles.IgnoreList.Clear();
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Content/plugins/jquery.min.js",

                       "~/Content/js/custom.js",
                      "~/Content/js/template.js",
                      "~/Content/plugins/SmoothScroll.js",
                      "~/Content/plugins/jquery.parallax-1.1.3.js",
                      "~/Content/plugins/owl-carousel/owl.carousel.js",
                       "~/Content/plugins/modernizr.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/Layout").Include(
                "~/Content/plugins/jquery.min.js",
                "~/Content/bootstrap/js/bootstrap.min.js",
                "~/Content/plugins/modernizr.js",
                "~/Content/plugins/rs-plugin/js/jquery.themepunch.tools.min.js",
                "~/Content/plugins/rs-plugin/js/jquery.themepunch.revolution.min.js",
                "~/Content/plugins/isotope/isotope.pkgd.min.js",
                "~/Content/plugins/owl-carousel/owl.carousel.js",
                "~/Content/plugins/magnific-popup/jquery.magnific-popup.min.js",
                "~/Content/plugins/jquery.appear.js",
                "~/Content/plugins/jquery.countTo.js",
                "~/Content/plugins/jquery.parallax-1.1.3.js",
                "~/Content/plugins/jquery.browser.js",
                "~/Content/plugins/SmoothScroll.js",
                "~/Content/js/custom.js",
                "~/Content/js/template.js",
                "~/Content/js/summernote.js"
            ));

           bundles.Add(new ScriptBundle("~/bundles/Tags").Include(
                      "~/Content/js/Jquery.min.js"
                  ));


            bundles.Add(new StyleBundle("~/Content/cssMain").Include(
                      "~/Content/css/style.css",
                      "~/Content/css/custom.css",
                      "~/Content\bootstrap.min.css",
                      "~/Content/plugins/owl-carousel/owl.carousel.css",
                      "~/Content/css/animations.css",
                      "~/Content/plugins/magnific-popup/magnific-popup.css",
                     "~/Content/fonts/font-awesome/css/font-awesome.css",
                      "~/Content/fonts/fontello/css/fontello.css",
                      "~/Content/Tags/bootstrap-tokenfield.css"
                     ));
        }
    }
}

