using System.Web;
using System.Web.Optimization;

namespace proyectoProgramaciónAvanzada
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                      "~/Scripts/materialize.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/materialize.css"));

            bundles.Add(new StyleBundle("~/Content/bodyFlex").Include(
                      "~/Content/body_flex.css"));

            bundles.Add(new StyleBundle("~/Content/iconos").Include(
                      "~/Content/fontawesome-free-5.15.1-web/css/all.min.css"));
        }
    }
}
