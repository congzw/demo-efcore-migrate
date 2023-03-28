using System.Collections.Generic;
using System.Linq;

namespace NbSites.Web.Core.Courses
{
    public class CourseService
    {
        private readonly ICourseRepository repository;

        public CourseService(ICourseRepository repository)
        {
            this.repository = repository;
        }

        public List<Course> GetCourse(GetCourse args)
        {
            var query = repository.QueryAll();
            if (!string.IsNullOrWhiteSpace(args.Search))
            {
                query = query.Where(x => x.Name.Contains(args.Search));
            }
            return query.ToList();
        }
    }

    public class GetCourse
    {
        public string Search { get; set; }
    }
}