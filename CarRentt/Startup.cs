using CarRentt.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarRentt.Startup))]
namespace CarRentt
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
            createUser();
        }
        public void CreateRoles()
        {
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if (!RoleManager.RoleExists("admins"))
            {
                role = new IdentityRole();
                role.Name = "admins";
                RoleManager.Create(role);
            }
            if (!RoleManager.RoleExists("Client"))
            {
                role = new IdentityRole();
                role.Name = "Client";
                RoleManager.Create(role);
            }
            if (!RoleManager.RoleExists("agnet"))
            {
                role = new IdentityRole();
                role.Name = "agnet";
                RoleManager.Create(role);
            }
        }
        public void createUser()
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser();
            user.Email = "admin@CarRent.ma";
            user.UserName = "admin@CarRent.ma";
            var check = UserManager.Create(user, "Pro*0809");
            if (check.Succeeded)
                UserManager.AddToRole(user.Id, "admins");
        }
    }
}
