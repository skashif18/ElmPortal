using Blazored.Toast;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Plugins.DataStore.SQL;
using Plugins.DataStore.SQL.Generic;
using Plugins.DataStore.SQL.Infrastructure.Services;
using Plugins.DataStore.SQL.Service;
using Syncfusion.Blazor;
using UseCases.DataStorePluginInterfaces;
using UseCases.DataStorePluginInterfaces.Generic;
using WebApp.Data;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddBlazoredToast();

            services.AddDbContext<PortalContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AdminContextConnection"));
                
            });

            services.AddDbContext<AdminContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AdminContextConnection"));
            });

            services.AddSyncfusionBlazor();
           

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", p => p.RequireClaim("Position", "Admin"));
                options.AddPolicy("EmployeeOnly", p => p.RequireClaim("Position", "Employee"));
                options.AddPolicy("ManagerOnly", p => p.RequireClaim("Position", "Manager"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICurrentUserServiceDB, CurrentUserServiceDB>();
            services.AddTransient<IEmployeeLeaveRepository, EmployeeLeaveRepository>();
            


            //Dependency Injection for Use Cases and Repositories

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
