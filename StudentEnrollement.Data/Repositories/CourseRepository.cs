using Microsoft.EntityFrameworkCore;
using StudentEnrollement.Data.Contracts;
using StudentEnrollement.Data.DatabaseContext;

namespace StudentEnrollement.Data.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(StudentEnrollementDbContext db) : base(db)
        {
        }

        public async Task<Course> GetStudentList(int courseId)
        {
            var course = await _db.Courses
                .Include(q => q.Enrollements).ThenInclude(q => q.Stutent)
                .FirstOrDefaultAsync(q => q.Id == courseId);

            return course;
        }
    }
}
