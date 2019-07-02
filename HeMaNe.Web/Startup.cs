using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeMaNe.Web.Database;
using HeMaNe.Web.Helpers;
using HeMaNe.Web.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HeMaNe.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //
            // Config-Datei
            //
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //
            // JWT
            //
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //
            // Database
            //
            services.AddDbContext<HemaneContext>();
            services.AddHttpContextAccessor();

            // Migriere, automatisch und ohne Skrupel
            using (var context = services.BuildServiceProvider().GetService<HemaneContext>())
            {
                context.Database.Migrate();
            }

            //
            // Microservices
            // Reflektionsmagie, nicht schön und inperformant (beim Start), aber der Zweck heiligt die Mittel :)
            //

            // Alle Typen in dem Assembly suchen
            var types = this.GetType().Assembly.GetTypes();

            // Alle Typen in HeMaNe.Web.Service.Concrete inkl. Implementiere Schnittstelle auf HeMaNe.Web.Service suchen
            var microServices = types.Where(t =>
                t.Namespace == "HeMaNe.Web.Service.Concrete" && t.IsClass &&
                t.GetInterfaces().Any(i => i.Namespace == "HeMaNe.Web.Service"))
                .ToDictionary(t => t, t => t.GetInterfaces().Single(i => i.Namespace == "HeMaNe.Web.Service"));

            foreach (var microService in microServices)
            {
                Console.WriteLine($"Registriere Microservice {microService.Value}");
                services.AddScoped(microService.Value, microService.Key);
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //
            // Jeder Host darf uns ansprechen
            //
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
