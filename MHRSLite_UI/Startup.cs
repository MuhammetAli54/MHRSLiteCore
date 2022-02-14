using MHRSLite_BLL.Contracts;
using MHRSLite_BLL.EmailService;
using MHRSLite_BLL.Implementations;
using MHRSLite_DAL;
using MHRSLite_EL.IdentityModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHRSLite_UI
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
            //AspNet Core'un Connection String baðlantýsý yapabilmesi için servislerine dbcontext eklenmesi gerekiyor.
            services.AddDbContext<MyContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));
            });

            //*********************************
            //IUnitOfWork gördüðün zaman bize IUnitOfWork nesnesi üret!
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //EmailSender gördüðün zaman bize EmailSender nesnesi üret!
            services.AddTransient<IEmailSender, EmailSender>();
            //********************************
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSession(options => 
            {
                options.IdleTimeout = TimeSpan.FromSeconds(60);
            });

            //**********************************
            services.AddIdentity<AppUser, AppRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<MyContext>();
            //**********************************
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.UseRouting(); //rooting mekanizmasý için
            app.UseSession();
            app.UseAuthentication(); //login logout kullanabilmek için
            app.UseAuthorization(); //authorization attiribute kullanabilmek için

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
