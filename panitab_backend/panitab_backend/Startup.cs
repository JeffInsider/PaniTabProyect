using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using panitab_backend.Database;
using panitab_backend.Database.Entities;
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

            var name = Configuration.GetConnectionString("DefaultConnection");

            //Add DBContext esto configura la base de datos y la conexion
            services.AddDbContext<PaniTabContext>(options =>
                options.UseMySql(name, ServerVersion.AutoDetect(name),
                    mySqlOptions => mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore)));


            //Add costum services
            services.AddTransient<IAuthService, AuthService>();

            //Add Identity
            services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<PaniTabContext>()
            .AddDefaultTokenProviders();

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
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            //Add AutoMapper
            services.AddAutoMapper(typeof(Startup));


            //Configure CORS para acceder desde cualquier origen
            //services.AddCors(options =>
            //{
            //    var allowURLS = Configuration.GetSection("AllowURLS").Get<string[]>();

            //    options.AddPolicy("CorsPolicy",
            //        builder => builder
            //            .WithOrigins(allowURLS)
            //            .AllowAnyMethod()
            //            .AllowAnyHeader()
            //            .AllowAnyOrigin());
            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //acceder a la api desde cualquier origen
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //para rutas mas rapidas
            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
