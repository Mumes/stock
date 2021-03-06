using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buisness.Update;
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using stock.Models;
using stockDataEF.Models;
using stockDataEF.Models.Repositories;
using stockDataEF.Models.Tables;
using Serilog;
namespace stock
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection)); 
            services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();//???


            services.AddControllersWithViews();
          
            services.Configure<IdentityOptions>(o =>
            {
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 5;
                o.SignIn.RequireConfirmedEmail = false;
            }
                );
            services.AddAuthentication();
            services.AddAuthorization( options =>
                {
                var policy = new AuthorizationPolicyBuilder().
                                RequireAuthenticatedUser().
                                Build();
                    options.AddPolicy("auth",policy);
                }
                );
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });

            services.AddScoped<IRepository<Product>, SQLProductRepository>();
            services.AddScoped<IRepository<Stock>, SQLStockRepository>();
            services.AddScoped<IRepository<DatedPrice>, SQLDatedPriceRepository>();
            services.AddScoped<IRepository<ExternalLogin>, SQLExternalLoginRepository>();
            services.AddScoped<UpdateStocks>();
            services.AddHangfire(x => x.UseSqlServerStorage(connection));
            services.AddHangfireServer();
            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UpdateStocks updateStocks)
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
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");                   
            });
           app.UseHangfireDashboard();      
            RecurringJob.AddOrUpdate("id_update_stocks",() => updateStocks.Update(), Cron.Minutely);

        }
    }
}
