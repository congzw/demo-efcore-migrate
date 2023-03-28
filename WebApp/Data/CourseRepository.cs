using NbSites.Web.Core.Courses;
using System.Collections.Generic;
using System.Linq;

namespace NbSites.Web.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DemoDbContext dbContext;

        public CourseRepository(DemoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Course> QueryAll()
        {
            return new List<Course>().AsQueryable();
            //return dbContext.Courses;
        }
    }
}