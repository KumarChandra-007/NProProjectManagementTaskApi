using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.DBContext;
using ProjectManagement.Interfaces;
using ProjectManagement.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace ProjectManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure services here
            services.AddControllersWithViews();
            services.AddDbContext<DBConnection>();
            // Add other services
            
            services.AddScoped<ITaskManagementServices, TaskManagementServices>();
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configure error handling for non-development environments
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("MyAllowSpecificOrigins");
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

}
