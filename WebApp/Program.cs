using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NbSites.Web.Core.Courses;
using NbSites.Web.Data;
using System.IO;
using System;
using NbSites.Web.Core.Migrates;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Reflection;
using NbSites.Web.Common;

namespace NbSites.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            //course setup
            services.AddTransient<CourseService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            var demoDbPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "demo.db");
            services.AddDbContext<DemoDbContext>(options =>
            {
                options.UseSqlite($"Data Source={demoDbPath}", x =>
                {
                    x.MigrationsAssembly("NbSites.Web.Migrate");
                    //x.MigrationsHistoryTable("__MyMigrationsHistory", "mySchema");
                });
                //options.ReplaceService<IHistoryRepository, MyHistoryRepository>();
            });

            //migration setup
            services.AddTransient<IDbMigrateService, DbMigrateService>();
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var isDev = app.Environment.IsDevelopment();
                if (isDev)
                {
                    var dbMigrate = scope.ServiceProvider.GetService<IDbMigrateService>();
                    await dbMigrate.ApplyMigration(null);
                    await dbMigrate.Seed();
                }
            }

            app.MapGet("/", (HttpContext httpContext, CourseService courseService) =>
            {
                var courses = courseService.GetCourse(new GetCourse() { Search = "" });
                return courses;
            });

            app.MapGet("/api/migrate/list", (HttpContext httpContext, IDbMigrateService dbMigrateService) =>
            {
                return dbMigrateService.GetMigrationItems();
            });

            app.MapGet("/api/migrate/apply", async (HttpContext httpContext, IDbMigrateService dbMigrateService) =>
            {
                await dbMigrateService.ApplyMigration(null);
                return dbMigrateService.GetMigrationItems();
            });

            app.Run();
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assmName = new AssemblyName(args.Name);
            return AssemblyAutoLoad.Instance.AssemblyResolve(assmName);
        }
    }
}