using Boekingssysteem.Areas.Identity.Data;
using Boekingssysteem.Data;
using Boekingssysteem.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Boekingssysteem
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
            services.AddDbContext<BoekingssysteemContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BoekingssysteemDB")));

            //Voor identity
            services.AddDefaultIdentity<CustomUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<BoekingssysteemContext>().AddErrorDescriber<CustomIdentityErrorDescriber>();
            services.AddRazorPages();
            services.AddAuthentication()
            .AddCookie(options =>
            {
               options.LoginPath = "/login";
               options.LogoutPath = "/logout";
            });

            services.Configure<IdentityOptions>(options =>
            {
                //Wachtwoord instellingen
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
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

            //Voor identity
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            CreateRoles(serviceProvider).Wait();            
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            BoekingssysteemContext context = serviceProvider.GetRequiredService<BoekingssysteemContext>();

            IdentityResult result;

            bool roleCheck = await roleManager.RoleExistsAsync("user");
            if (!roleCheck) 
            {
                result = await roleManager.CreateAsync(new IdentityRole("user"));
            }

            roleCheck = await roleManager.RoleExistsAsync("admin");
            if (!roleCheck)
            {
                result = await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            context.SaveChanges();
        }
    }
}