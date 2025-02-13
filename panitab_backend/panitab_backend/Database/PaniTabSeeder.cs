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
            PaniTabContext context,
            ILoggerFactory loggerFactory,
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager
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
                    //aqui se crean los roles
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.STORE));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.CHECKER));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.OFFICE));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.REPORTS));
                }

                if (!await userManager.Users.AnyAsync())
                {
                    //Aqui se crean los usuarios
                    var userAdmin = new UserEntity
                    {
                        FirstName = "Administrador",
                        LastName = "PaniTab",
                        Email = "admin@gmail.com",
                        UserName = "admin@gmail.com",
                    };

                    //usuarios para probar los roles
                    var userStore = new UserEntity
                    {
                        FirstName = "Almacen",
                        LastName = "PaniTab",
                        Email = "store@gmail.com",
                        UserName = "store@gmail.com",
                    };

                    var userChecker = new UserEntity
                    {
                        FirstName = "Revisor",
                        LastName = "PaniTab",
                        Email = "checker@gmail.com",
                        UserName = "checker@gmail.com",
                    };

                    var userOffice = new UserEntity
                    {
                        FirstName = "Oficina",
                        LastName = "PaniTab",
                        Email = "office@gmail.com",
                        UserName = "office@gmail.com",
                    };

                    await userManager.CreateAsync(userAdmin, "Temporal01*");
                    await userManager.CreateAsync(userStore, "Temporal01*");
                    await userManager.CreateAsync(userChecker, "Temporal01*");
                    await userManager.CreateAsync(userOffice, "Temporal01*");

                    await userManager.AddToRoleAsync(userAdmin, RolesConstant.ADMIN);
                    await userManager.AddToRoleAsync(userStore, RolesConstant.STORE);
                    await userManager.AddToRoleAsync(userChecker, RolesConstant.CHECKER);
                    await userManager.AddToRoleAsync(userOffice, RolesConstant.OFFICE);
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
