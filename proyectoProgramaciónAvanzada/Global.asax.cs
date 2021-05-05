using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using proyectoProgramaciónAvanzada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace proyectoProgramaciónAvanzada
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ApplicationDbContext db = new ApplicationDbContext();
            CrearRoles(db);
            CrearSuperUusario(db);
            AsignarPermiso(db);
            db.Dispose();
        }

        private void CrearRoles(ApplicationDbContext db)
        {
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!rolemanager.RoleExists("estudiante"))
            {
                rolemanager.Create(new IdentityRole("estudiante"));
            }
            if (!rolemanager.RoleExists("profesor"))
            {
                rolemanager.Create(new IdentityRole("profesor"));
            }
            if (!rolemanager.RoleExists("admin"))
            {
                rolemanager.Create(new IdentityRole("admin"));
            }
        }

        private void CrearSuperUusario(ApplicationDbContext db)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = usermanager.FindByName("admin@ufide.ac.cr");

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "admin@ufide.ac.cr",
                    Email = "admin@ufide.ac.cr"
                };
                usermanager.Create(user, "Welcome1!");
            }
        }

        private void AsignarPermiso(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var user = userManager.FindByName("admin@ufide.ac.cr");

            if (!userManager.IsInRole(user.Id, "admin"))
            {
                userManager.AddToRole(user.Id, "admin");
            }

        }
    }
}
