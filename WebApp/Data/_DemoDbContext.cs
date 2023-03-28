using Microsoft.EntityFrameworkCore;
using NbSites.Web.Core.Courses;

namespace NbSites.Web.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }

        //public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Course>(entity =>
            //{
            //    entity.ToTable("demo_course");
            //    entity.HasKey(x => x.Id);
            //    entity.Property(x => x.Title).HasMaxLength(100);
            //});
        }
    }
}