using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Trails4Health.Data;
using Trails4Health.Models;
using Trails4Health.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace Trails4Health
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
            // Add framework services.
            // 2.0 (b.d.AUTENTICAÇÃO) 
            /* SERVIÇO PARA AUTENTICAÇÃO: configurar ASPNET CORE identity services */
            services.AddDbContext<LoginsApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionStringLogins")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<LoginsApplicationDbContext>()
                .AddDefaultTokenProviders();

            // 2.1 (b.d.AUTENTICAÇÃO)
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                // Adiciono outras configurações se necessarias (ver ppt 148)
                // Lockout settings
                options.Lockout.MaxFailedAccessAttempts = 10;
                // Add other lockout settings if needed ...
                // Add other user settings if needed ...
                //options.User.RequireUniqueEmail = true;
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            // 4. (b.d.AUTENTICAÇÃO) 
            // .como tenho 2 base dados não posso fazer migrações como antes...
            // .busco por: developer command prompt for VS 2017:
            //    .cd endereço do project Trails4Health .Nota: não é a dir do sln!
            //    .dotnet restore, dotnet build, ... ver(ppt 150)
            //    .dotnet ef migrations add Initial 
            // .criar utilizadores em /Data/UsersSeedData
            //  *** se quiser mudar repositorio...
            //- assim não preciso de mudar mais nada que nao seja FakeProductRepository
            // services.AddTransient<ITrails4HealthRepository, FakeProductRepository>(); // mudado!!

                  

            /* configurar a app para usar a ConnectionStringTrails4Health e ligar á B.D.*/
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer
             (
                 // vou por nome da string connection do appsettings.jason
                 Configuration.GetConnectionString("ConnectionStringTrails4Health")
             )
          );
            //  *** se quiser mudar repositorio...
            //- assim não preciso de mudar mais nada que nao seja FakeProductRepository
            // services.AddTransient<ITrails4HealthRepository, FakeProductRepository>(); // mudado!!

            /* quando são criados os componentes que usam ITrails4HealthRepository (no momento apenas Trilhos(controler)) 
               recebem um objecto EFTrails4HealthRepository, este objecto providencia aos componentes acesso á B.D. */
            services.AddTransient<ITrails4HealthRepository, EFTrails4HealthRepository>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            // 3. (b.d.AUTENTICAÇÃO) 
            //app.UseIdentity();

            // ERRO: decimal Distancia 00.00 ou 00,00
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization
            const string enUSCulture = "en-GB"; // alterei do original "en-US", por causa do formato da data. Pretende-se que a data esteja no formato dd/mm/yyyy

            var supportedCultures = new[] {
                new CultureInfo(enUSCulture)
                //new CultureInfo("en-GB"),
                //new CultureInfo("en"),
                //new CultureInfo("es-ES"),
                //new CultureInfo("es-MX"),
                //new CultureInfo("es"),
                //new CultureInfo("fr-FR"),
                //new CultureInfo("fr"),
            };
            // END_ERRO

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUSCulture),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Nota: SeedData só pode ser chamada depois das Migrações e updates !!
            //SeedData.EnsurePopulated(app.ApplicationServices);
        }
    }
}
