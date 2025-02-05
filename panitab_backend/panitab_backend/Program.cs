using dotenv.net;
using Microsoft.AspNetCore.Identity;
using panitab_backend;
using panitab_backend.Database;
using panitab_backend.Database.Entities;

// crea un contructor para configurar la aplicacion
var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

//registra todos los servicios de la clase startup
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        //iniciar el contexto de la base de datos
        var context = services.GetRequiredService<PaniTabContext>();
        var userManager = services.GetRequiredService<UserManager<UserEntity>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //para inicializar la base de datos con datos iniciales
        await PaniTabSeeder.LoadDataAsync(context, loggerFactory, userManager, roleManager);
    }
    catch (Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "Error al ejecutar el Seed de datos");
    }
}

//corre la aplicacion
app.Run();