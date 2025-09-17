using System.Web;
using System.Web.Optimization;

namespace RPS.Web
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
                      "~/Content/Styles/site.css"));

            bundles.Add(new StyleBundle("~/Content/kendocss").Include(
                      "~/Content/kendo/2024.4.1112/bootstrap-4.css"));

            bundles.Add(new StyleBundle("~/Content/dashboardcss").Include(
                "~/Content/styles/dashboard.css"));

            bundles.Add(new StyleBundle("~/Content/activeissuescss").Include(
                "~/Content/Styles/active-issues.css"));

            bundles.Add(new StyleBundle("~/Content/backlogcss").Include(
                "~/Content/styles/backlog.css"));

            bundles.Add(new StyleBundle("~/Content/detailcss").Include(
                "~/Content/styles/detail.css"));

            bundles.Add(new StyleBundle("~/Content/chitchatcss").Include(
                "~/Content/styles/chitchat.css"));
        }
    }
}
