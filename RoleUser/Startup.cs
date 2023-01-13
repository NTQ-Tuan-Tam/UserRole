using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoleUser.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;


namespace RoleUser
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
            services.AddControllersWithViews();

            var connectionString = Configuration.GetConnectionString("DbContext");
            services.AddDbContext<EmployeeContext>(options => options.UseSqlServer(connectionString));
            

            services.AddAuthentication("CookieAuth").AddCookie("CookieAuth",
                options => options.LoginPath = "/Login/Login"
                );

            #region code cu
            // ??ng kEservice cookice
            //       services.AddAuthentication("DemoSecurityScheme")
            //.AddCookie("DemoSecurityScheme", options =>
            //{
            //    options.AccessDeniedPath = new PathString("/Account/Access");
            //    options.Cookie = new CookieBuilder
            //    {
            //        //Domain = "",
            //        HttpOnly = true,
            //        Name = ".aspNetCoreDemo.Security.Cookie",
            //        Path = "/",
            //        SameSite = SameSiteMode.Lax,
            //        SecurePolicy = CookieSecurePolicy.SameAsRequest
            //    };
            //    options.Events = new CookieAuthenticationEvents
            //    {
            //        OnSignedIn = context =>
            //        {
            //            Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
            //                "OnSignedIn", context.Principal.Identity.Name);
            //            return Task.CompletedTask;
            //        },
            //        OnSigningOut = context =>
            //        {
            //            Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
            //                "OnSigningOut", context.HttpContext.User.Identity.Name);
            //            return Task.CompletedTask;
            //        },
            //        OnValidatePrincipal = context =>
            //        {
            //            Console.WriteLine("{0} - {1}: {2}", DateTime.Now,
            //                "OnValidatePrincipal", context.Principal.Identity.Name);
            //            return Task.CompletedTask;
            //        }
            //    };
            //    //options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            //    options.LoginPath = new PathString("/Account/Login");
            //    options.ReturnUrlParameter = "RequestPath";
            //    options.SlidingExpiration = true;
            //});
            #endregion
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Login}/{action=Index}/{id?}");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Login", "{action=Login}/{id?}", new { controller = "Login" });
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Users", "{action=Index}/{id?}", new { controller = "Users" });
            });
        }
    }
}
