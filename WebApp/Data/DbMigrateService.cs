using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using NbSites.Web.Core.Courses;
using NbSites.Web.Core.Migrates;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace NbSites.Web.Data
{
    public class DbMigrateService : IDbMigrateService
    {
        private readonly DemoDbContext dbContext;

        public DbMigrateService(DemoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task EnsureDbExist()
        {
            await dbContext.Database.EnsureCreatedAsync();
        }
        public List<MigrationItem> GetMigrationItems()
        {
            var migrationItems = dbContext.Database.GetMigrationItems();
            return migrationItems;
        }

        public async Task ApplyMigration(string to)
        {
            var services = dbContext.Database.GetInfrastructure();
            var migrationItems = dbContext.Database.GetMigrationItems();
            var assm = services.GetService<IMigrationsAssembly>();

            var migrator = services.GetService<IMigrator>();
            if (string.IsNullOrWhiteSpace(to))
            {
                await migrator.MigrateAsync();
            }
            else
            {
                await migrator.MigrateAsync(to);
            }
        }

        public async Task Seed()
        {
            await Task.CompletedTask;
            //var courseCount = await dbContext.Courses.CountAsync();
            //if (courseCount == 0)
            //{
            //    var demoCourses = CreateDemoCourses();
            //    await dbContext.Courses.AddRangeAsync(demoCourses.ToArray());
            //    await dbContext.SaveChangesAsync();
            //}
        }

        private static List<Course> CreateDemoCourses(int seedCount = 3)
        {
            var courses = new List<Course>();
            for (int i = 1; i <= seedCount; i++)
            {
                courses.Add(new Course() { Id = $"demo-{i:000}", Title = $"ÑÝÊ¾¿Î³Ì{i:000}" });
            }
            return courses;
        }
    }
}