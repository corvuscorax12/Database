using Database.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Database.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Database
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ArchivalContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("host='127.0.0.1' port=5432 dbname=archival user=postgres sslmode=prefer connect_timeout=10")));
            builder.Services.AddIdentity<UserModel, IdentityRole>()
         .AddEntityFrameworkStores<UsersdbContext>();

            // .AddDefaultTokenProviders();
            builder.Services.AddRazorPages(options=>
            {
                options.Conventions.AllowAnonymousToAreaFolder("Areas", "/Identity/Account/*");
                options.Conventions.AuthorizePage("/Identity/Account/Login");
            });
            builder.Services.AddDbContext<UsersdbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("host='127.0.0.1' port=5432 dbname=archival user=postgres sslmode=prefer connect_timeout=10")));
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;
            });

           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
            });
       //     app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.MapRazorPages();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}