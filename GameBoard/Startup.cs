﻿using System;
using GameBoard.Configuration;
using GameBoard.LogicLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameBoard
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
            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                    options.ConsentCookie = new CookieBuilder
                    {
                        Name = "GameBoard.Consent",
                        Expiration = TimeSpan.FromDays(365),
                        IsEssential = true
                    };
                });

            services.AddAntiforgery(
                options => { options.Cookie = new CookieBuilder
                {
                    Name = "GameBoard.Antiforgery",
                    SameSite = SameSiteMode.None,
                    HttpOnly = true,
                    IsEssential = true
                };});

            LogicLayer.Configuration.ConfigureDbContext(
                services,
                Configuration.GetConnectionString("DefaultConnection"));

            LogicLayer.Configuration.ConfigureServices(services);

            services.AddDefaultIdentity<IdentityUser>(
                    options =>
                    {
                        options.Password.RequiredLength = 8;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.User.RequireUniqueEmail = true;
                    })
                .AddDbContextStores();

            services.Configure<HostConfiguration>(Configuration.GetSection(nameof(HostConfiguration)));

            services.ConfigureApplicationCookie(
                options => { options.Cookie.Name = "GameBoard.Identity"; });
                
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(
                routes =>
                {
                    routes.MapRoute(
                        "default",
                        "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }
}