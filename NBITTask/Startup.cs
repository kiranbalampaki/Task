using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using NBITTask.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(NBITTask.Startup))]
namespace NBITTask
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            CreateRolesAndUsers();
        }

        public void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
            //Creating new Super Admin in the beginning
            if (!roleManager.RoleExists("SuperAdmin"))
            {
                var role = new IdentityRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);
                var user = new ApplicationUser
                {
                    UserName = "kiranmgr1996@gmail.com",
                    Email = "kiranmgr1996@gmail.com",
                    PhoneNumber = "9813382575",
                    EmailConfirmed = true,
                    Role = role,
                };
                var password = "SuperAdmin@123";
                var usr = userManager.Create(user, password);

                if (usr.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "SuperAdmin");
                }
            }
        }
    }
}
