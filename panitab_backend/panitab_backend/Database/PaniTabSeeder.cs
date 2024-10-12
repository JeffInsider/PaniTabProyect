using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using panitab_backend.Constants;
using panitab_backend.Database.Entities;

namespace panitab_backend.Database
{
    public class PaniTabSeeder
    {
        public static async Task LoadDataAsync
            (
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            PaniTabContext context,
            ILoggerFactory loggerFactory
            )
        {
            try
            {
                await LoadRolesAndUsersAsync(userManager, roleManager, loggerFactory);
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PaniTabSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de datos");
            }
        }

        public static async Task LoadRolesAndUsersAsync
            (
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory
            )
        {
            try
            {
                if (!await roleManager.Roles.AnyAsync())
                {
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.Admin));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.Store));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.Checker));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.Office));
                }

                if (!await userManager.Users.AnyAsync())
                {
                    var userAdmin = new UserEntity
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        UserName = "admin",
                        Email = "admin@PaniTab.es",
                    };

                    await userManager.CreateAsync(userAdmin, "Admin01*.");

                    await userManager.AddToRoleAsync(userAdmin, RolesConstant.Admin);
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PaniTabSeeder>();
                logger.LogError(e.Message);
            }
        }
    }
}
