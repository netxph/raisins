using System.Web.Optimization;

namespace Raisins.Client.Web
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/content/css").Include("~/Content/reset.css", "~/Content/site.css"));
            

            bundles.Add(new ScriptBundle("~/scripts/knockout").Include("~/Scripts/knockout-2.2.0.js"));
            bundles.Add(new ScriptBundle("~/scripts/jquery").Include("~/Scripts/jquery-*"));
            //bundles.Add(new ScriptBundle("~/scripts/raisins").Include("~/Scripts/kkcountdown.js", "~/Scripts/jwplayer/jwplayer.js", "~/Scripts/raisins.js"));
            bundles.Add(new ScriptBundle("~/scripts/raisins").Include(
                "~/Scripts/kkcountdown.js",
                "~/Scripts/raisins.js",
                "~/Scripts/paymentController.js"
            ));

            bundles.Add(new StyleBundle("~/content/jun").Include(
                "~/Content/jun/jquery-ui.min.css",
                "~/Content/jun/jquery-ui.structure.min.css",
                "~/Content/jun/jquery-ui.theme.min.css",
                "~/Content/jun/custom.css"
            ));
            bundles.Add(new ScriptBundle("~/scripts/jun").Include(
                "~/Scripts/jun/scripts/jquery-ui.min.js"
            ));
        }

    }
}