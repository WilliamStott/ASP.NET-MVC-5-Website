using System.Web;
using System.Web.Optimization;

namespace Assignment
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /**
            * Bundles multiple scripts/css files into a single line that can be added to the HTML. 
            * This is much better than having multiple lines for scripts and css over all web pages.
            * As well as reducing the amount of inport scripts, it minifies all of the files and puts them together
            * reducing readability for anyone trying to easily read and exploit the code.
            **/
            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/script.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
