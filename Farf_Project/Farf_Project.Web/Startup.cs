using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Farf_Project.Core;
using Farf_Project.Infrastructure;
using System.Data;
using System.IO;
using System.Text;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace Farf_Project.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup()
        {
            // app settings configuration
            var configurationBuilder = (new ConfigurationBuilder()).AddJsonFile("appSettings.json");
            this.Configuration = configurationBuilder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hdhgsdfghseifhgsldfhgksdfogsdf523452345dsfgsdfg"))
                };
            });

            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("administrator", policy => policy.Requirements.Add(new ScopePermissionRequirement("administrator")));
                options.AddPolicy("operator", policy => policy.Requirements.Add(new ScopePermissionRequirement("operator")));
            });

            services.AddSignalR();

            // Add system services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add scope authorization handler
            services.AddSingleton<IAuthorizationHandler, ScopePermissionHandler>();

            // Add application services
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IPointsService, PointsService>();
            services.AddTransient<IRoutesService, RoutesService>();
            services.AddTransient<ILogExceptionManager, LogExceptionManager>();

            // Add application data repositories
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IPointsRepository, PointsRepository>();
            services.AddTransient<IRoutesRepository, RoutesRepository>();
            services.AddTransient<ILogExceptionRepository, LogExceptionRepository>();

            // Access Token services
            services.AddTransient<ISessionTokenRepository, SessionTokenRepository>();
            services.AddTransient<TokenManagerMiddleware>();
            services.AddTransient<ITokenManager, TokenManager>();

            // Add Database Connection
            services.AddTransient<IDbConnection>(db => new Npgsql.NpgsqlConnection(this.Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Localization: Here we are building a list of supported cultures which will be used in the
            //               RequestLocalizationOptions in the app.UseRequestLocalization call below.
            var supportedCultures = new[]
              {
                    // Localization: Notice that neutral cultures (like 'es') are
                    //               listed after specific cultures. This best practice
                    //               ensures that if a particular culture request could
                    //               be satisifed by either a supported specific culture
                    //               or a supported neutral culture, the specific culture
                    //               will be preferred.
                    new CultureInfo("en-US"),
                    new CultureInfo("de-DE"),
                    new CultureInfo("pt"),
              };

            // Localization: Here we are configuring the RequstLocalization including setting the supported cultures from above
            //               in the RequestLocalizationOptions. We are also setting the default request culture to be used
            //               for current culture. These options will be used wherever we request localized strings.
            //               For more information see https://docs.asp.net/en/latest/fundamentals/localization.html
            //
            //               Request locale will be read from an Accept-Language header, a culture query string, or
            //               an ASP.NET Core culture cookie. Other options can be supported with custom RequestCultureProvider
            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),

                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,

                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(requestLocalizationOptions);

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMiddleware(typeof(TokenManagerMiddleware));

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                !Path.HasExtension(context.Request.Path.Value) &&
                !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
