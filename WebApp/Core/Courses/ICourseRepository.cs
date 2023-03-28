using System.Linq;

namespace NbSites.Web.Core.Courses
{
    public interface ICourseRepository
    {
        IQueryable<Course> QueryAll();
    }
}