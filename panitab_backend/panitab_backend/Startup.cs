using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using panitab_backend.Database;
using panitab_backend.Database.Entities;
using panitab_backend.Helpers;
using panitab_backend.Services;
using panitab_backend.Services.Interfaces;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Text;

namespace panitab_backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //para usar cors
            services.AddEndpointsApiExplorer();
            //habilita la documentación de la api
            services.AddSwaggerGen();
            //para acceder a la petición http
            services.AddHttpContextAccessor();


            //var name = Configuration.GetConnectionString("DefaultConnection");

            ////Add DBContext esto configura la base de datos y la conexion
            //services.AddDbContext<PaniTabContext>(options =>
            //    options.UseMySql(name, ServerVersion.AutoDetect(name),
            //        mySqlOptions => mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore)));

            // add deContext para la conexion de la base de datos
            services.AddDbContext<PaniTabContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Add costum services
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IAuditService, AuditService>();
            services.AddTransient<IUsersService, UsersService>();

            //Add Identity
            services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<PaniTabContext>()
            .AddDefaultTokenProviders();

            //Registrar TokenValidationParameters
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = Configuration["JWT:ValidAudience"],
                ValidIssuer = Configuration["JWT:ValidIssuer"],
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
            };
            services.AddSingleton(tokenValidationParameters);

            //Add authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddAuthorization();
            //Utilizado para la validacion con identity en audit
            services.AddHttpContextAccessor();

            //Add AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddCors(opt =>
            {
                var allowURLS = Configuration.GetSection("AllowURLS").Get<string[]>();
                opt.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins(allowURLS)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //acceder a la api desde cualquier origen
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            //para rutas mas rapidas
            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication(); //usar autenticación antes de autorización

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
